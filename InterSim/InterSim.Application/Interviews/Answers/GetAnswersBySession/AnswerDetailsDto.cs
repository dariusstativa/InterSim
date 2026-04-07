namespace InterSim.Application.Interviews.Answers.GetAnswersBySession;

public sealed record AnswerDetailsDto(
    Guid Id,
    string QuestionText,
    string AnswerText,
    int Score,
    int MaxScore,
    string BreakdownJson,
    string DeficitsJson,
    Guid FeedbackBundleId,
    string EvaluatorVersion,
    string FeedbackEngineVersion,
    string FeedbackItemsJson,
    DateTimeOffset CreatedAt
);