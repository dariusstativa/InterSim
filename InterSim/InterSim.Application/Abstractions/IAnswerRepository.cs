using InterSim.Domain.Entities;

namespace InterSim.Application.Abstractions;

public interface IAnswerRepository
{
    Task AddAsync(Answer answer, CancellationToken ct);
    Task<IReadOnlyList<Answer>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
}