using MediatR;

namespace InterSim.Application.Interviews.Sessions.CreateSession;

public sealed record CreateSessionCommand : IRequest<Guid>;