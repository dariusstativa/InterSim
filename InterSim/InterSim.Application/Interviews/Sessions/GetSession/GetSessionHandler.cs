using InterSim.Application.Abstractions;
using MediatR;

namespace InterSim.Application.Interviews.Sessions.GetSession;

public sealed class GetSessionHandler : IRequestHandler<GetSessionQuery, GetSessionResponse?>
{
    private readonly ISessionReader _reader;

    public GetSessionHandler(ISessionReader reader) => _reader = reader;

    public Task<GetSessionResponse?> Handle(GetSessionQuery request, CancellationToken ct) =>
        _reader.GetSessionAsync(request.SessionId, ct);
}