using InterSim.Application.Interviews.Sessions.GetSession;
using MediatR;

namespace InterSim.Api.Endpoints.Interviews.Sessions;

public static class GetSessionEndpoint
{
    public static IEndpointRouteBuilder MapGetSessionEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/interviews/sessions/{sessionId:guid}",
                async (Guid sessionId, IMediator mediator, CancellationToken ct) =>
                {
                    var res = await mediator.Send(new GetSessionQuery(sessionId), ct);
                    return res is null ? Results.NotFound() : Results.Ok(res);
                })
            .WithTags("Interviews")
            .Produces<GetSessionResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}