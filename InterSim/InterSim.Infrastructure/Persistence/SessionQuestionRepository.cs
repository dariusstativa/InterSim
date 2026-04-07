using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.Persistence;

public sealed class SessionQuestionRepository : ISessionQuestionRepository
{
    private readonly AppDbContext _db;

    public SessionQuestionRepository(AppDbContext db) => _db = db;

    public Task<List<Guid>> GetUsedQuestionIdsAsync(Guid sessionId, CancellationToken ct) =>
        _db.SessionQuestions
            .AsNoTracking()
            .Where(x => x.InterviewSessionId == sessionId)
            .Select(x => x.QuestionBankItemId)
            .ToListAsync(ct);

    public async Task AddAsync(Guid sessionId, Guid questionBankItemId, string generatedText, CancellationToken ct)
    {
        await _db.SessionQuestions.AddAsync(new SessionQuestion
        {
            InterviewSessionId = sessionId,
            QuestionBankItemId = questionBankItemId,
            GeneratedText = generatedText
        }, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct) => _db.SaveChangesAsync(ct);
}