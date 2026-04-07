using InterSim.Application.Interviews.Sessions.CompleteSession;
using MediatR;

namespace InterSim.Api.Endpoints.Interviews.Sessions;

public static class CompleteSessionEndpoint
{
    public static IEndpointRouteBuilder MapCompleteSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/interviews/sessions/{sessionId:guid}/complete",
                async (Guid sessionId, IMediator mediator, CancellationToken ct) =>
                {
                    await mediator.Send(new CompleteSessionCommand(sessionId), ct);
                    return Results.NoContent();
                })
            .WithTags("Interviews")
            .Produces(StatusCodes.Status204NoContent);

        return app;
    }
}