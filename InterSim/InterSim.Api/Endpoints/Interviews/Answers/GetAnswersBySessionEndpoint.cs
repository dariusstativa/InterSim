using InterSim.Application.Interviews.Answers.GetAnswersBySession;
using MediatR;

namespace InterSim.Api.Endpoints.Interviews;

public static class GetAnswersBySessionEndpoint
{
    public static IEndpointRouteBuilder MapGetAnswersBySession(this IEndpointRouteBuilder app)
    {
        app.MapGet("/interview-sessions/{sessionId:guid}/answers",
                async (Guid sessionId, IMediator mediator, CancellationToken ct) =>
                {
                    var res = await mediator.Send(new GetAnswersBySessionQuery(sessionId), ct);
                    return Results.Ok(res);
                })
            .WithName("GetAnswersBySession");

        return app;
    }
}