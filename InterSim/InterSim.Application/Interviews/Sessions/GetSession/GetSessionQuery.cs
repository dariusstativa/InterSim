using MediatR;

namespace InterSim.Application.Interviews.Sessions.GetSession;

public sealed record GetSessionQuery(Guid SessionId) : IRequest<GetSessionResponse?>;