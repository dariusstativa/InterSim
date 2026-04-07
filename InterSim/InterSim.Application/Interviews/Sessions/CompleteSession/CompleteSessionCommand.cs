using MediatR;

namespace InterSim.Application.Interviews.Sessions.CompleteSession;

public sealed record CompleteSessionCommand(Guid SessionId) : IRequest;