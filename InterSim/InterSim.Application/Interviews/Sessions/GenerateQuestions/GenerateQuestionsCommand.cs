using MediatR;

namespace InterSim.Application.Interviews.Sessions.GenerateQuestions;

public sealed record GenerateQuestionsCommand(
    Guid SessionId,
    string Topic,
    string Difficulty,
    int Count
) : IRequest<IReadOnlyList<string>>;