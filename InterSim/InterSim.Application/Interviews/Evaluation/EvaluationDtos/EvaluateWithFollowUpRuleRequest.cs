namespace InterSim.Application.Interviews.Evaluation;

public sealed record EvaluateWithFollowUpRuleRequest(
    string QuestionText,
    string AnswerText,
    int FollowUpCount,
    int MaxFollowUps,
    FollowUpContextMode Mode,
    List<string>? PreviousFollowUps);
