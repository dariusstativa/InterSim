using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using InterSim.Domain.Entities;
using InterSim.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.AI;

public sealed class BehavioralDatasetGenerationService
{
    private readonly AppDbContext _db;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public BehavioralDatasetGenerationService(AppDbContext db, HttpClient httpClient)
    {
        _db = db;
        _httpClient = httpClient;

        _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                  ?? throw new InvalidOperationException("OPENAI_API_KEY is not set.");

        _httpClient.BaseAddress ??= new Uri("https://api.openai.com/v1/");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<int> GenerateBehavioralSamplesAsync(CancellationToken ct = default)
    {
        var questions = await _db.QuestionBankItems
            .Where(q => q.Category == "behavioral" && q.IsActive && !q.IsTemplate)
            .Select(q => q.Text)
            .ToListAsync(ct);

        var created = 0;

        foreach (var question in questions)
        {
            var generated = await GenerateAnswersForQuestionAsync(question, ct);

            foreach (var item in generated.Answers)
            {
                var answerText = item.Text?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(answerText))
                    continue;

                var exists = await _db.AnswerEvaluationSamples.AnyAsync(
                    x => x.QuestionText == question && x.AnswerText == answerText,
                    ct);

                if (exists)
                    continue;

                var sample = new AnswerEvaluationSample
                {
                    Id = Guid.NewGuid(),
                    Category = "behavioral",
                    Topic = "general",
                    Difficulty = "junior",
                    QuestionText = question,
                    AnswerText = answerText,
                    WordCount = CountWords(answerText),
                    HumanRelevance = 0,
                    HumanStructure = 0,
                    HumanSpecificity = 0,
                    HumanImpactReflection = 0,
                    HumanTotalScore = 0,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                _db.AnswerEvaluationSamples.Add(sample);
                created++;
            }
        }

        await _db.SaveChangesAsync(ct);
        return created;
    }

    private async Task<GeneratedAnswersResponse> GenerateAnswersForQuestionAsync(
        string question,
        CancellationToken ct)
    {
        var prompt = BuildPrompt(question);

        var payload = new
        {
            model = "gpt-4o-mini",
            temperature = 0.9,
            response_format = new { type = "json_object" },
            messages = new object[]
            {
                new
                {
                    role = "system",
                    content = "You generate synthetic behavioral interview answers for a training dataset. Return valid JSON only."
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

        var parsed = JsonSerializer.Deserialize<GeneratedAnswersResponse>(
            content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        if (parsed == null || parsed.Answers == null || parsed.Answers.Count == 0)
            throw new InvalidOperationException("Failed to parse generated answers JSON.");

        return parsed;
    }

    private static string BuildPrompt(string question)
    {
        return $@"
You are generating synthetic training data for an interview evaluation system.

QUESTION:
{question}

Generate 8 different answers to this behavioral interview question.

Each answer must represent a different quality level or style.

Answer types:

1. STRONG_STAR
A strong behavioral answer with clear situation, task, action, and result.
Include a concrete software engineering example, a measurable result, and a short reflection.
80-120 words.

2. GOOD_EXAMPLE
A good answer with a real example but not perfectly structured.
No metrics required.
60-90 words.

3. MEDIUM
A somewhat acceptable answer with partial details but weaker structure.
40-70 words.

4. GENERIC
A general statement about teamwork, responsibility, or attitude without a specific example.
30-50 words.

5. VERY_SHORT
A minimal answer with very little detail.
10-20 words.

6. OFF_TOPIC
An answer that does not address the question.

7. VERBOSE
An overly long answer with unnecessary details and rambling explanation.
120-160 words.

8. REFLECTIVE
A behavioral answer with a clear lesson learned at the end.
60-100 words.

Rules:
- Answers must sound like they come from a person that is applying for a software engineer position.
- Use realistic software engineering situations such as projects, debugging, APIs, databases, teamwork, deadlines, and deployments.
- Avoid repeating the same scenario across answers.
- Do not mention the words STAR, Situation, Task, Action, Result.
- Return valid JSON only.

Use this exact JSON schema:

{{
  ""question"": ""{EscapeForJson(question)}"",
  ""answers"": [
    {{ ""type"": ""STRONG_STAR"", ""text"": ""..."" }},
    {{ ""type"": ""GOOD_EXAMPLE"", ""text"": ""..."" }},
    {{ ""type"": ""MEDIUM"", ""text"": ""..."" }},
    {{ ""type"": ""GENERIC"", ""text"": ""..."" }},
    {{ ""type"": ""VERY_SHORT"", ""text"": ""..."" }},
    {{ ""type"": ""OFF_TOPIC"", ""text"": ""..."" }},
    {{ ""type"": ""VERBOSE"", ""text"": ""..."" }},
    {{ ""type"": ""REFLECTIVE"", ""text"": ""..."" }}
  ]
}}";
    }

    private static int CountWords(string text)
    {
        return text.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Length;
    }

    private static string EscapeForJson(string value)
    {
        return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }

    private sealed class GeneratedAnswersResponse
    {
        public string Question { get; set; } = string.Empty;
        public List<GeneratedAnswerItem> Answers { get; set; } = new();
    }

    private sealed class GeneratedAnswerItem
    {
        public string Type { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}