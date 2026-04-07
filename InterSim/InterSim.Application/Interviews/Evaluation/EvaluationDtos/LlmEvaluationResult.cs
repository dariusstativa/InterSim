namespace InterSim.Application.Interviews.Evaluation;

public sealed record LlmEvaluationResult(
    int Relevance,
    int Structure,
    int Specificity,
    int ImpactReflection,
    int Total,
    string Feedback,
    string? FollowUpQuestion
);