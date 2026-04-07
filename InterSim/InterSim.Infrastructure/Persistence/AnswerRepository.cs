using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.Persistence;

public sealed class AnswerRepository : IAnswerRepository
{
    private readonly AppDbContext _db;

    public AnswerRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Answer answer, CancellationToken ct) =>
        await _db.Answers.AddAsync(answer, ct);

    public async Task<IReadOnlyList<Answer>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct) =>
        await _db.Answers
            .AsNoTracking()
            .Where(a => a.InterviewSessionId == sessionId)
            .OrderBy(a => a.CreatedAt)
            .ToListAsync(ct);

    public Task SaveChangesAsync(CancellationToken ct) =>
        _db.SaveChangesAsync(ct);
}