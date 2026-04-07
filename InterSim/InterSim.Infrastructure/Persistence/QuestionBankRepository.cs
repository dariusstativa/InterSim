using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.Persistence;

public sealed class QuestionBankRepository : IQuestionBankRepository
{
    private readonly AppDbContext _db;

    public QuestionBankRepository(AppDbContext db) => _db = db;

    public Task<List<QuestionBankItem>> GetCandidatesAsync(
        string topic,
        string difficulty,
        IReadOnlyCollection<Guid> excludeIds,
        CancellationToken ct)
    {
        var q = _db.QuestionBankItems
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Where(x => x.Topic == topic)
            .Where(x => x.Difficulty == difficulty);

        if (excludeIds.Count > 0)
            q = q.Where(x => !excludeIds.Contains(x.Id));

        return q.ToListAsync(ct);
    }
}