using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.Persistence;

public sealed class InterviewSessionRepository : IInterviewSessionRepository
{
    private readonly AppDbContext _db;
    public InterviewSessionRepository(AppDbContext db) => _db = db;

    public Task<InterviewSession?> GetByIdAsync(Guid sessionId, CancellationToken ct) =>
        _db.InterviewSessions.FirstOrDefaultAsync(x => x.Id == sessionId, ct);

    public async Task AddAsync(InterviewSession session, CancellationToken ct) =>
        await _db.InterviewSessions.AddAsync(session, ct);

    public Task SaveChangesAsync(CancellationToken ct) =>
        _db.SaveChangesAsync(ct);
}