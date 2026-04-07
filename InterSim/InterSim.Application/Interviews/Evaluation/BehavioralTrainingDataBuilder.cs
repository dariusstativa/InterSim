using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Application.Interviews.Evaluation;
public sealed class BehavioralTrainingDataBuilder
{
    private readonly IAppDbContext _db;
    private readonly BehavioralEvaluatorV2 _evaluator;

    public BehavioralTrainingDataBuilder(
        IAppDbContext db,
        BehavioralEvaluatorV2 evaluator)
    {
        _db = db;
        _evaluator = evaluator;
    }

    public async Task<List<BehavioralTrainingRow>> BuildAsync(CancellationToken ct = default)
    {
        var samples = await _db.AnswerEvaluationSamples
            .AsNoTracking()
            .Where(x => x.Category == "behavioral")
            .Where(x=>x.HumanTotalScore>0)
            .ToListAsync(ct);

        var rows = new List<BehavioralTrainingRow>();

        foreach (var sample in samples)
        {
            var features = await _evaluator.ExtractFeaturesAsync(
                sample.QuestionText,
                sample.AnswerText,
                ct);

            rows.Add(new BehavioralTrainingRow(
                sample.Id,
                features,
                sample.HumanTotalScore
            ));
        }

        return rows;
    }
}