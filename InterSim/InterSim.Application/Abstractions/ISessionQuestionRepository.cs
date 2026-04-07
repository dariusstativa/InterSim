namespace InterSim.Application.Abstractions;

public interface ISessionQuestionRepository
{
    Task<List<Guid>> GetUsedQuestionIdsAsync(Guid sessionId, CancellationToken ct);

    Task AddAsync(Guid sessionId, Guid questionBankItemId, string generatedText, CancellationToken ct);

    Task SaveChangesAsync(CancellationToken ct);
}