namespace InterSim.Application.Interviews.Evaluation;

public sealed record AnswerEvaluationResult(
    int Score,
    int MaxScore,
    IReadOnlyList<AnswerEvaluationBreakdownItem> Breakdown,
    IReadOnlyList<DeficitTag> Deficits
);