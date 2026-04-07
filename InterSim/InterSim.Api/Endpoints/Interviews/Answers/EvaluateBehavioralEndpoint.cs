using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Api.Endpoints.Interviews;

public static class EvaluateBehavioralEndpoint
{
    public sealed record EvaluateBehavioralRequest(string QuestionText, string AnswerText);

    public static IEndpointRouteBuilder MapEvaluateBehavioral(this IEndpointRouteBuilder app)
    {
        app.MapPost("/debug/evaluate/behavioral",
                (EvaluateBehavioralRequest req) =>
                {
                    var eval = BehavioralEvaluatorV1.Evaluate(req.QuestionText, req.AnswerText);

                    var feedbackItems = BaselineFeedbackSelectorV1.Select(
                        eval.Score,
                        eval.Deficits,
                        QuestionType.BehavioralStar);

                    return Results.Ok(new
                    {
                        evaluation = eval,
                        feedback = new
                        {
                            evaluatorVersion = BehavioralEvaluatorV1.EvaluatorVersion,
                            feedbackEngineVersion = "baseline-feedback-v1",
                            items = feedbackItems
                        }
                    });
                })
            .WithName("DebugEvaluateBehavioral");

        return app;
    }
}