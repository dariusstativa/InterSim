using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Application.Abstractions;

public interface IAppDbContext
{
    DbSet<AnswerEvaluationSample> AnswerEvaluationSamples { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}