namespace InterSim.Application.Interviews.Sessions.GetSession;

public sealed record GetSessionResponse(
    Guid SessionId,
    string Status,
    DateTimeOffset CreatedAt,
    IReadOnlyList<QuestionDto> Questions,
    IReadOnlyList<AnswerDto> Answers
);