using InterSim.Domain.Entities;

namespace InterSim.Application.Abstractions;

public interface IQuestionBankRepository
{
    Task<List<QuestionBankItem>> GetCandidatesAsync(
        string topic,
        string difficulty,
        IReadOnlyCollection<Guid> excludeIds,
        CancellationToken ct);
}