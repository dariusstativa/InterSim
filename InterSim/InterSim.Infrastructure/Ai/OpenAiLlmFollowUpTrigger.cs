using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Infrastructure.AI;

public sealed class OpenAiLlmFollowUpTrigger : ILlmFollowUpTrigger
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiLlmFollowUpTrigger(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                  ?? throw new InvalidOperationException("OPENAI_API_KEY is not set.");

        _httpClient.BaseAddress ??= new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<LlmFollowUpDecision> DecideAsync(
        string questionText,
        string answerText,
        int score,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps,
        CancellationToken ct = default)
    {
        if (followUpCount >= maxFollowUps)
            return new LlmFollowUpDecision(false, "MaxFollowUpsReached");

        var prompt = $@"
You are deciding whether an interviewer should ask a follow-up question.

Interview question:
{Escape(questionText)}

Candidate answer:
{Escape(answerText)}

Current evaluator score: {score}
Detected deficits: {string.Join(", ", deficits)}
Follow-ups already asked: {followUpCount}
Maximum allowed follow-ups: {maxFollowUps}

Return ONLY JSON in this format:
{{
  ""shouldAsk"": true,
  ""reason"": ""short reason""
}}

Ask a follow-up only if the answer is incomplete, vague, missing action/result/reflection, or needs clarification.
";

        var payload = new
        {
            model = "gpt-4o-mini",
            temperature = 0.2,
            response_format = new { type = "json_object" },
            messages = new object[]
            {
                new
                {
                    role = "system",
                    content = "You decide if a follow-up question is needed. Return JSON only."
                },
                new
                {
                    role = "user",
                    content = prompt
                }
            }
        };

        var json = JsonSerializer.Serialize(payload);

        using var request = new HttpRequestMessage(HttpMethod.Post, "chat/completions")
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(request, ct);
        var raw = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"OpenAI call failed: {response.StatusCode} - {raw}");

        using var doc = JsonDocument.Parse(raw);

        var content = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        if (string.IsNullOrWhiteSpace(content))
            throw new InvalidOperationException("OpenAI returned empty content.");

        var parsed = JsonSerializer.Deserialize<LlmFollowUpDecision>(
            content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (parsed == null)
            throw new InvalidOperationException("Failed to parse LLM follow-up decision JSON.");

        return parsed;
    }

    private static string Escape(string value)
    {
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}