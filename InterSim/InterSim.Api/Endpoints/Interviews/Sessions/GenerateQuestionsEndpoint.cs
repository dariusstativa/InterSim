using InterSim.Application.Interviews.Sessions.GenerateQuestions;
using MediatR;

namespace InterSim.Api.Endpoints.Interviews.Sessions;

public static class GenerateQuestionsEndpoint
{
    public static IEndpointRouteBuilder MapGenerateQuestionsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/interviews/sessions/{sessionId:guid}/generate-questions",
                async (Guid sessionId, GenerateQuestionsRequest body, IMediator mediator, CancellationToken ct) =>
                {
                    var res = await mediator.Send(
                        new GenerateQuestionsCommand(sessionId, body.Topic, body.Difficulty, body.Count),
                        ct);

                    return Results.Ok(res);
                })
            .WithTags("Interviews")
            .Produces<IReadOnlyList<string>>(StatusCodes.Status200OK);

        return app;
    }
}