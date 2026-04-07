namespace InterSim.Application.Interviews.Evaluation;

public sealed record FeedbackBundle(
    Guid BundleId,
    QuestionType QuestionType,
    string EvaluatorVersion,
    string FeedbackEngineVersion,
    IReadOnlyList<FeedbackItem> Items
);