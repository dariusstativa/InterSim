using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Sessions.GetSession;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.Persistence;

public sealed class SessionReader : ISessionReader
{
    private readonly AppDbContext _db;

    public SessionReader(AppDbContext db) => _db = db;

    public async Task<GetSessionResponse?> GetSessionAsync(Guid sessionId, CancellationToken ct)
    {
        var session = await _db.InterviewSessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == sessionId, ct);

        if (session is null) return null;

        var questions = await _db.SessionQuestions
            .AsNoTracking()
            .Where(q => q.InterviewSessionId == sessionId)
            .OrderBy(q => q.CreatedAt)
            .Select(q => new QuestionDto(q.Id, q.GeneratedText, q.CreatedAt))
            .ToListAsync(ct);

        var answers = await _db.Answers
            .AsNoTracking()
            .Where(a => a.InterviewSessionId == sessionId)
            .OrderBy(a => a.CreatedAt)
            .Select(a => new AnswerDto(a.Id, a.QuestionText, a.AnswerText, a.CreatedAt))
            .ToListAsync(ct);

        return new GetSessionResponse(
            session.Id,
            session.Status,
            session.CreatedAt,
            questions,
            answers
        );
    }
}