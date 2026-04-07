using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Infrastructure.AI;

public sealed class OpenAiFollowUpQuestionGenerator : IFollowUpQuestionGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAiFollowUpQuestionGenerator(HttpClient httpClient)
    {
        _httpClient = httpClient;

        _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                  ?? throw new InvalidOperationException("OPENAI_API_KEY is not set.");

        _httpClient.BaseAddress ??= new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string?> GenerateFollowUpAsync(
        string questionText,
        string answerText,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps,
        FollowUpContextMode mode,
        List<string>? previousFollowUps = null,
        CancellationToken ct = default)
    {
        if (followUpCount >= maxFollowUps)
            return null;

        var prompt = BuildPrompt(
            questionText,
            answerText,
            deficits,
            followUpCount,
            maxFollowUps,
            mode,
            previousFollowUps);

        var payload = new
        {
            model = "gpt-4o-mini",
            temperature = 0.7,
            response_format = new { type = "json_object" },
            messages = new object[]
            {
                new
                {
                    role = "system",
                    content = "You generate follow-up interview questions. Return JSON only."
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
            return null;

        var parsed = JsonSerializer.Deserialize<FollowUpResponse>(
            content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return parsed?.FollowUpQuestion;
    }

    private static string BuildPrompt(
        string question,
        string answer,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps,
        FollowUpContextMode mode,
        List<string>? previousFollowUps)
    {
        var baseInstruction = @"
You are an interviewer for an IT company asking a follow-up question to a candidate.

Goal:
Ask ONE follow-up question that helps complete the missing parts of the answer.
Do NOT ask multiple questions.
Do NOT repeat previous questions.
The question must be specific to the candidate's answer.

Return JSON:
{
  ""followUpQuestion"": ""...""
}";

        if (mode == FollowUpContextMode.Limited)
        {
            return $@"
{baseInstruction}

INTERVIEW QUESTION:
{Escape(question)}

CANDIDATE ANSWER:
{Escape(answer)}
";
        }

        if (mode == FollowUpContextMode.Medium)
        {
            return $@"
{baseInstruction}

INTERVIEW QUESTION:
{Escape(question)}

CANDIDATE ANSWER:
{Escape(answer)}

Ask a follow-up question if the answer is incomplete, lacks results, lacks actions, or lacks reflection.
";
        }

       
        var deficitsText = string.Join(", ", deficits);
        var prev = previousFollowUps == null ? "" : string.Join("\n", previousFollowUps);

        return $@"
{baseInstruction}

INTERVIEW QUESTION:
{Escape(question)}

CANDIDATE ANSWER:
{Escape(answer)}

DETECTED MISSING PARTS:
{deficitsText}

FOLLOW UPS SO FAR:
{followUpCount} / {maxFollowUps}

PREVIOUS FOLLOW-UP QUESTIONS:
{prev}

Ask a question that targets the missing parts and does not repeat previous questions.
";
    }

    private static string Escape(string value)
    {
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }

    private sealed class FollowUpResponse
    {
        public string? FollowUpQuestion { get; set; }
    }
}