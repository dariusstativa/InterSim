using MediatR;

namespace InterSim.Application.Interviews.Answers.GetAnswersBySession;

public sealed record GetAnswersBySessionQuery(Guid SessionId)
    : IRequest<IReadOnlyList<AnswerDetailsDto>>;