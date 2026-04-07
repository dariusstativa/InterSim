using InterSim.Application.Abstractions;
using MediatR;

namespace InterSim.Application.Interviews.Sessions.GenerateQuestions;

public sealed class GenerateQuestionsHandler
    : IRequestHandler<GenerateQuestionsCommand, IReadOnlyList<string>>
{
    private readonly IInterviewSessionRepository _sessions;
    private readonly ISessionQuestionRepository _sessionQuestions;
    private readonly IQuestionBankRepository _questionBank;

    public GenerateQuestionsHandler(
        IInterviewSessionRepository sessions,
        ISessionQuestionRepository sessionQuestions,
        IQuestionBankRepository questionBank)
    {
        _sessions = sessions;
        _sessionQuestions = sessionQuestions;
        _questionBank = questionBank;
    }

    public async Task<IReadOnlyList<string>> Handle(GenerateQuestionsCommand request, CancellationToken ct)
    {
        GenerateQuestionsValidator.Validate(request);

        var session = await _sessions.GetByIdAsync(request.SessionId, ct);
        if (session is null)
            throw new InvalidOperationException("Session not found.");

        if (session.Status == "completed")
            throw new InvalidOperationException("Session is completed. Questions are locked.");

        var usedIds = await _sessionQuestions.GetUsedQuestionIdsAsync(request.SessionId, ct);

        var candidates = await _questionBank.GetCandidatesAsync(
            request.Topic,
            request.Difficulty,
            usedIds,
            ct);

        if (candidates.Count == 0)
            throw new InvalidOperationException("No questions available for this topic/difficulty (or all were already used).");

        
        var rnd = new Random(request.SessionId.GetHashCode());

        
        for (int i = candidates.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            (candidates[i], candidates[j]) = (candidates[j], candidates[i]);
        }

        var take = Math.Min(request.Count, candidates.Count);
        var selected = candidates.Take(take).ToList();

        var fillers = new Dictionary<string, string[]>
        {
            ["concept"] = new[] { "dependency injection", "async/await", "LINQ", "EF Core tracking", "middleware" },
            ["feature"] = new[] { "records", "IEnumerable vs IQueryable", "CancellationToken", "background services", "DTO mapping" },
            ["table_type"] = new[] { "large", "high-traffic", "partitioned", "wide", "normalized" },
            ["soft_skill"] = new[] { "ownership", "communication", "adaptability", "problem solving", "teamwork" }
        };

        string FillTemplate(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            foreach (var kv in fillers)
            {
                var key = "{" + kv.Key + "}";
                if (text.Contains(key))
                {
                    var arr = kv.Value;
                    var pick = arr[rnd.Next(arr.Length)];
                    text = text.Replace(key, pick);
                }
            }

            return text;
        }

        var created = new List<string>(take);

        foreach (var item in selected)
        {
            var generated = item.IsTemplate ? FillTemplate(item.Text) : item.Text;

            await _sessionQuestions.AddAsync(
                request.SessionId,
                item.Id,
                generated,
                ct);

            created.Add(generated);
        }

        await _sessionQuestions.SaveChangesAsync(ct);

        return created;
    }
}