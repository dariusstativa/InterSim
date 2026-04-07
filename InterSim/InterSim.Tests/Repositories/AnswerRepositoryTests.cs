using InterSim.Domain.Entities;
using InterSim.Infrastructure.Persistence;
using InterSim.Tests.TestInfra;
using Xunit;

namespace InterSim.Tests.Repositories;

public sealed class AnswerRepositoryTests
{
    [Fact]
    public async Task GetBySessionIdAsync_ShouldFilterBySession_AndOrderByCreatedAt()
    {
        await using var db = DbContextFactory.CreateInMemory("answers_repo_test_fixed_1");
        var repo = new AnswerRepository(db);

        var sessionA = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var sessionB = Guid.Parse("22222222-2222-2222-2222-222222222222");

       

        var a1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1");
        var a2Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2");
        var b1Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1");

        var now = DateTimeOffset.Parse("2026-03-01T12:00:00Z");

        var a1 = new Answer
        {
            Id = a1Id,
            InterviewSessionId = sessionA,
            QuestionText = "Q1",
            AnswerText = "A1",
            CreatedAt = now.AddMinutes(-10)
        };

        var a2 = new Answer
        {
            Id = a2Id,
            InterviewSessionId = sessionA,
            QuestionText = "Q2",
            AnswerText = "A2",
            CreatedAt = now.AddMinutes(-5)
        };

        var b1 = new Answer
        {
            Id = b1Id,
            InterviewSessionId = sessionB,
            QuestionText = "QB",
            AnswerText = "AB",
            CreatedAt = now.AddMinutes(-7)
        };

        await repo.AddAsync(a2, CancellationToken.None);
        await repo.AddAsync(b1, CancellationToken.None);
        await repo.AddAsync(a1, CancellationToken.None);
        await repo.SaveChangesAsync(CancellationToken.None);

        var results = await repo.GetBySessionIdAsync(sessionA, CancellationToken.None);

        Assert.Equal(2, results.Count);
        Assert.Equal(a1Id, results[0].Id);
        Assert.Equal(a2Id, results[1].Id);
        Assert.All(results, x => Assert.Equal(sessionA, x.InterviewSessionId));
    }
}