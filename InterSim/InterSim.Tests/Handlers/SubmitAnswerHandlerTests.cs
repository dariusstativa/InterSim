using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Answers.SubmitAnswer;
using InterSim.Application.Interviews.Evaluation;
using InterSim.Domain.Entities;
using Xunit;

namespace InterSim.Tests.Handlers;

public sealed class SubmitAnswerHandlerTests
{
    [Fact]
    public async Task Handle_WhenSessionNotFound_ShouldThrow()
    {
        var sessions = new FakeInterviewSessionRepository(null);
        var answers = new FakeAnswerRepository();
        var engine = new FakeFeedbackEngine();

        var handler = new SubmitAnswerHandler(sessions, answers, engine);

        var cmd = new SubmitAnswerCommand(
            SessionId: Guid.Parse("11111111-1111-1111-1111-111111111111"),
            QuestionText: "Q",
            AnswerText: "A"
        );

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenSessionCompleted_ShouldThrow()
    {
        var session = new InterviewSession
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Status = "completed"
        };

        var sessions = new FakeInterviewSessionRepository(session);
        var answers = new FakeAnswerRepository();
        var engine = new FakeFeedbackEngine();

        var handler = new SubmitAnswerHandler(sessions, answers, engine);

        var cmd = new SubmitAnswerCommand(
            SessionId: session.Id,
            QuestionText: "Q",
            AnswerText: "A"
        );

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(cmd, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenValid_ShouldPersistAnswerWithEvaluationAndFeedback()
    {
        var session = new InterviewSession
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Status = "active"
        };

        var sessions = new FakeInterviewSessionRepository(session);
        var answers = new FakeAnswerRepository();
        var engine = new FakeFeedbackEngine();

        var handler = new SubmitAnswerHandler(sessions, answers, engine);

        var cmd = new SubmitAnswerCommand(
            SessionId: session.Id,
            QuestionText: "Tell me about a time you handled a deadline.",
            AnswerText: "At work, my goal was to deliver. I implemented changes. As a result, we improved by 10%. I learned to plan."
        );

        var id = await handler.Handle(cmd, CancellationToken.None);

        Assert.Equal(1, answers.Stored.Count);
        var saved = answers.Stored[0];

        Assert.Equal(id, saved.Id);
        Assert.Equal(session.Id, saved.InterviewSessionId);
        Assert.False(string.IsNullOrWhiteSpace(saved.QuestionText));
        Assert.False(string.IsNullOrWhiteSpace(saved.AnswerText));

       
        Assert.InRange(saved.Score, 0, 100);
        Assert.Equal(100, saved.MaxScore);

        Assert.NotEqual(Guid.Empty, saved.FeedbackBundleId);
        Assert.Equal(BehavioralEvaluatorV1.EvaluatorVersion, saved.EvaluatorVersion);
        Assert.Equal(HeuristicFeedbackEngineV1.EngineVersion, saved.FeedbackEngineVersion);

        Assert.False(string.IsNullOrWhiteSpace(saved.BreakdownJson));
        Assert.False(string.IsNullOrWhiteSpace(saved.DeficitsJson));
        Assert.False(string.IsNullOrWhiteSpace(saved.FeedbackItemsJson));
    }

   

    private sealed class FakeInterviewSessionRepository : IInterviewSessionRepository
    {
        private InterviewSession? _session;

        public FakeInterviewSessionRepository(InterviewSession? session) => _session = session;

        public Task<InterviewSession?> GetByIdAsync(Guid sessionId, CancellationToken ct)
            => Task.FromResult(_session is not null && _session.Id == sessionId ? _session : null);

        public Task AddAsync(InterviewSession session, CancellationToken ct)
        {
            _session = session;
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;
    }

    private sealed class FakeAnswerRepository : IAnswerRepository
    {
        public List<Answer> Stored { get; } = new();

        public Task AddAsync(Answer answer, CancellationToken ct)
        {
            Stored.Add(answer);
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Answer>> GetBySessionIdAsync(Guid sessionId, CancellationToken ct)
        {
            IReadOnlyList<Answer> res = Stored.FindAll(a => a.InterviewSessionId == sessionId);
            return Task.FromResult(res);
        }

        public Task SaveChangesAsync(CancellationToken ct) => Task.CompletedTask;
    }

    private sealed class FakeFeedbackEngine : IFeedbackEngine
    {
        public AnswerFeedbackResult Generate(string questionText, string answerText, QuestionType questionType)
        {
           
            var evaluation = BehavioralEvaluatorV1.Evaluate(questionText, answerText);

            var items = BaselineFeedbackSelectorV1.Select(
                evaluation.Score,
                evaluation.Deficits,
                QuestionType.BehavioralStar);

            var bundle = new FeedbackBundle(
                BundleId: Guid.Parse("99999999-9999-9999-9999-999999999999"),
                QuestionType: QuestionType.BehavioralStar,
                EvaluatorVersion: BehavioralEvaluatorV1.EvaluatorVersion,
                FeedbackEngineVersion: HeuristicFeedbackEngineV1.EngineVersion,
                Items: items
            );

            return new AnswerFeedbackResult(evaluation, bundle);
        }
    }
}