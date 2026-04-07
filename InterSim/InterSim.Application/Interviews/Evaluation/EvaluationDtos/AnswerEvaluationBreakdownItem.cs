namespace InterSim.Application.Interviews.Evaluation;

public sealed record AnswerEvaluationBreakdownItem(
    string Criterion,
    int Points,
    int MaxPoints,
    string Notes
);