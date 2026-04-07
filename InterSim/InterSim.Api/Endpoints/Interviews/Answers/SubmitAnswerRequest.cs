namespace InterSim.Api.Endpoints.Interviews.Answers;

public sealed record SubmitAnswerRequest(
    string QuestionText,
    string AnswerText
);