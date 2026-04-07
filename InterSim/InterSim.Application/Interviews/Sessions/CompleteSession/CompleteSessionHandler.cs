using InterSim.Application.Abstractions;
using MediatR;

namespace InterSim.Application.Interviews.Sessions.CompleteSession;

public sealed class CompleteSessionHandler : IRequestHandler<CompleteSessionCommand>
{
    private readonly IInterviewSessionRepository _sessions;

    public CompleteSessionHandler(IInterviewSessionRepository sessions) => _sessions = sessions;

    public async Task Handle(CompleteSessionCommand request, CancellationToken ct)
    {
        if (request.SessionId == Guid.Empty)
            throw new InvalidOperationException("SessionId is required.");

        var session = await _sessions.GetByIdAsync(request.SessionId, ct);
        if (session is null)
            throw new InvalidOperationException("Session not found.");

        if (session.Status == "completed")
            return; 

        session.Status = "completed";
        await _sessions.SaveChangesAsync(ct);
    }
}