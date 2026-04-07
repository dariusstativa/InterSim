using InterSim.Application.Abstractions;
using InterSim.Application.Interviews.Evaluation;
using InterSim.Domain.Entities;
using MediatR;

namespace InterSim.Application.Interviews.Answers.SubmitAnswer;

public sealed class SubmitAnswerHandler : IRequestHandler<SubmitAnswerCommand, Guid>
{
    private readonly IInterviewSessionRepository _sessions;
    private readonly IAnswerRepository _answers;
    private readonly IFeedbackEngine _feedbackEngine;

    public SubmitAnswerHandler(
        IInterviewSessionRepository sessions,
        IAnswerRepository answers,
        IFeedbackEngine feedbackEngine)
    {
        _sessions = sessions;
        _answers = answers;
        _feedbackEngine = feedbackEngine;
    }

    public async Task<Guid> Handle(SubmitAnswerCommand request, CancellationToken ct)
    {
        SubmitAnswerValidator.Validate(request);

        var session = await _sessions.GetByIdAsync(request.SessionId, ct);
        if (session is null)
            throw new InvalidOperationException("Session not found.");

        if (session.Status == "completed")
            throw new InvalidOperationException("Session is completed. Answers are locked.");

        var result = _feedbackEngine.Generate(
            request.QuestionText,
            request.AnswerText,
            QuestionType.BehavioralStar);

        var answer = new Answer
        {
            InterviewSessionId = request.SessionId,
            QuestionText = request.QuestionText,
            AnswerText = request.AnswerText,

            Score = result.Evaluation.Score,
            MaxScore = result.Evaluation.MaxScore,
            BreakdownJson = System.Text.Json.JsonSerializer.Serialize(result.Evaluation.Breakdown),
            DeficitsJson = System.Text.Json.JsonSerializer.Serialize(result.Evaluation.Deficits),

            FeedbackBundleId = result.Feedback.BundleId,
            EvaluatorVersion = result.Feedback.EvaluatorVersion,
            FeedbackEngineVersion = result.Feedback.FeedbackEngineVersion,
            FeedbackItemsJson = System.Text.Json.JsonSerializer.Serialize(result.Feedback.Items),

            CreatedAt = DateTimeOffset.UtcNow
        };

        await _answers.AddAsync(answer, ct);
        await _answers.SaveChangesAsync(ct);

        return answer.Id;
    }
}