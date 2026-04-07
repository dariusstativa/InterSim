namespace InterSim.Application.Interviews.Sessions.GetSession;

public sealed record QuestionDto(
    Guid SessionQuestionId,
    string Text,
    DateTimeOffset CreatedAt
);