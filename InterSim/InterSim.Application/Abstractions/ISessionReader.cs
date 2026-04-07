using InterSim.Application.Interviews.Sessions.GetSession;

namespace InterSim.Application.Abstractions;

public interface ISessionReader
{
    Task<GetSessionResponse?> GetSessionAsync(Guid sessionId, CancellationToken ct);
}