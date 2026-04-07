using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Application.Abstractions;

public interface ILlmFollowUpTrigger
{
    Task<LlmFollowUpDecision> DecideAsync(
        string questionText,
        string answerText,
        int score,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps,
        CancellationToken ct = default);
};