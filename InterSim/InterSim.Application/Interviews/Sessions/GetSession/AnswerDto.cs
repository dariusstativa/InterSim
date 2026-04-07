namespace InterSim.Application.Interviews.Sessions.GetSession;

public sealed record AnswerDto(
    Guid AnswerId,
    string QuestionText,
    string AnswerText,
    DateTimeOffset CreatedAt
);