using InterSim.Application.Abstractions;
using MediatR;

namespace InterSim.Application.Interviews.Answers.GetAnswersBySession;

public sealed class GetAnswersBySessionHandler
    : IRequestHandler<GetAnswersBySessionQuery, IReadOnlyList<AnswerDetailsDto>>
{
    private readonly IAnswerRepository _answers;

    public GetAnswersBySessionHandler(IAnswerRepository answers) => _answers = answers;

    public async Task<IReadOnlyList<AnswerDetailsDto>> Handle(GetAnswersBySessionQuery request, CancellationToken ct)
    {
        var answers = await _answers.GetBySessionIdAsync(request.SessionId, ct);

        return answers
            .Select(a => new AnswerDetailsDto(
                Id: a.Id,
                QuestionText: a.QuestionText,
                AnswerText: a.AnswerText,
                Score: a.Score,
                MaxScore: a.MaxScore,
                BreakdownJson: a.BreakdownJson,
                DeficitsJson: a.DeficitsJson,
                FeedbackBundleId: a.FeedbackBundleId,
                EvaluatorVersion: a.EvaluatorVersion,
                FeedbackEngineVersion: a.FeedbackEngineVersion,
                FeedbackItemsJson: a.FeedbackItemsJson,
                CreatedAt: a.CreatedAt
            ))
            .ToList();
    }
    
}