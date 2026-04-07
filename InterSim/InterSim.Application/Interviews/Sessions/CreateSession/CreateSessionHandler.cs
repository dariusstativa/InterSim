using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using MediatR;

namespace InterSim.Application.Interviews.Sessions.CreateSession;

public sealed class CreateSessionHandler : IRequestHandler<CreateSessionCommand, Guid>
{
    private readonly IInterviewSessionRepository _repo;

    public CreateSessionHandler(IInterviewSessionRepository repo) => _repo = repo;

    public async Task<Guid> Handle(CreateSessionCommand request, CancellationToken ct)
    {
        var session = new InterviewSession { Status = "active" };
        await _repo.AddAsync(session, ct);
        await _repo.SaveChangesAsync(ct);
        return session.Id;
    }
}