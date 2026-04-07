using InterSim.Domain.Entities;

namespace InterSim.Application.Abstractions;

public interface IInterviewSessionRepository
{
    Task<InterviewSession?> GetByIdAsync(Guid sessionId, CancellationToken ct);
    Task AddAsync(InterviewSession session, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}