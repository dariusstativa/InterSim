using InterSim.Application.Interviews.Sessions.CreateSession;
using MediatR;

namespace InterSim.Api.Endpoints.Interviews.Sessions;

public static class CreateSessionEndpoint
{
    public static IEndpointRouteBuilder MapCreateSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/interviews/sessions",
                async (IMediator mediator, CancellationToken ct) =>
                {
                    var id = await mediator.Send(new CreateSessionCommand(), ct);
                    return Results.Ok(new { sessionId = id });
                })
            .WithTags("Interviews")
            .Produces(StatusCodes.Status200OK);

        return app;
    }
}