using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Application.Abstractions;
public interface ILlmJudge
{
    Task<LlmEvaluationResult> EvaluateBehavioralAsync(
        string questionText,
        string answerText,
        int maxFollowUps = 1,
        CancellationToken ct = default);
}