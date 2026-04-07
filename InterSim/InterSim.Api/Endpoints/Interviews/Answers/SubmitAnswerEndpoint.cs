using InterSim.Api.Endpoints.Interviews.Answers;
using InterSim.Application.Interviews.Answers.SubmitAnswer;
using MediatR;

namespace InterSim.Api.Endpoints.Interviews.Answers;

public static class SubmitAnswerEndpoint
{
    public static IEndpointRouteBuilder MapSubmitAnswerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/interviews/sessions/{sessionId:guid}/answers",
                async (Guid sessionId, SubmitAnswerRequest body, IMediator mediator, CancellationToken ct) =>
                {
                    var id = await mediator.Send(
                        new SubmitAnswerCommand(sessionId, body.QuestionText, body.AnswerText),
                        ct);

                    return Results.Ok(new { answerId = id });
                })
            .WithTags("Interviews")
            .Produces(StatusCodes.Status200OK);

        return app;
    }
}