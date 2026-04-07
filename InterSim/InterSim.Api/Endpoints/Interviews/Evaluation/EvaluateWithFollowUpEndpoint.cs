using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Evaluation;
using InterSim.Domain.Entities;
using InterSim.Infrastructure.Persistence;

namespace InterSim.Api.Endpoints.Interviews.Evaluation;

public static class EvaluateWithFollowUpEndpoint
{
    public static void MapEvaluateWithFollowUp(this IEndpointRouteBuilder app)
    {
        app.MapPost("/interviews/evaluate-with-followup",
            async (
                EvaluateWithFollowUpRequest req,
                BehavioralEvaluatorV2 evaluator,
                IFollowUpTrigger trigger,
                IFollowUpQuestionGenerator generator,
                AppDbContext db,
                CancellationToken ct) =>
            {
                var preview = await evaluator.EvaluatePreviewAsync(
                    req.QuestionText,
                    req.AnswerText,
                    ct);

                var decision = trigger.Decide(
                    preview.TotalScore,
                    preview.Deficits.ToList(),
                    req.FollowUpCount,
                    req.MaxFollowUps);

                string? followUp = null;

                if (decision.ShouldAsk)
                {
                    followUp = await generator.GenerateFollowUpAsync(
                        req.QuestionText,
                        req.AnswerText,
                        preview.Deficits.ToList(),
                        req.FollowUpCount,
                        req.MaxFollowUps,
                        req.Mode,
                        req.PreviousFollowUps,
                        ct);
                }

                var log = new FollowUpLog
                {
                    Id = Guid.NewGuid(),
                    QuestionText = req.QuestionText,
                    AnswerText = req.AnswerText,
                    Score = preview.TotalScore,
                    Deficits = string.Join(",", preview.Deficits),
                    TriggerType = "Rule",
                    ContextMode = req.Mode.ToString(),
                    FollowUpQuestion = followUp,
                    FollowUpCount = req.FollowUpCount,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                db.FollowUpLogs.Add(log);
                await db.SaveChangesAsync(ct);

                return Results.Ok(new
                {
                    score = preview.TotalScore,
                    relevance = preview.RelevanceScore,
                    structure = preview.StructureScore,
                    specificity = preview.SpecificityScore,
                    impactReflection = preview.ImpactReflectionScore,
                    deficits = preview.Deficits,
                    shouldAskFollowUp = decision.ShouldAsk,
                    followUpReason = decision.Reason,
                    followUpQuestion = followUp
                });
            });
    }
}

  