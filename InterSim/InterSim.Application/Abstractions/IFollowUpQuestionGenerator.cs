using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Application.Abstractions;

public interface IFollowUpQuestionGenerator
{
    Task<string?> GenerateFollowUpAsync(
        string questionText,
        string answerText,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps,
        FollowUpContextMode mode,
        List<string>? previousFollowUps = null,
        CancellationToken ct = default);
}