namespace InterSim.Application.Interviews.Evaluation;

public sealed record AnswerFeedbackResult(
    AnswerEvaluationResult Evaluation,
    FeedbackBundle Feedback
);