using System;

namespace InterSim.Application.Interviews.Evaluation;

public sealed class HeuristicFeedbackEngineV1 : IFeedbackEngine
{
    public const string EngineVersion = "baseline-feedback-v1";

    public AnswerFeedbackResult Generate(
        string questionText,
        string answerText,
        QuestionType questionType)
    {
        questionText ??= string.Empty;
        answerText ??= string.Empty;

        return questionType switch
        {
            QuestionType.BehavioralStar => GenerateBehavioral(questionText, answerText),
            _ => throw new NotSupportedException(
                $"Question type '{questionType}' is not supported by {nameof(HeuristicFeedbackEngineV1)}.")
        };
    }

    private static AnswerFeedbackResult GenerateBehavioral(
        string questionText,
        string answerText)
    {
        var evaluation = BehavioralEvaluatorV1.Evaluate(questionText, answerText);

        var feedbackItems = BaselineFeedbackSelectorV1.Select(
            evaluation.Score,
            evaluation.Deficits,
            QuestionType.BehavioralStar);

        var bundle = new FeedbackBundle(
            BundleId: Guid.NewGuid(),
            QuestionType: QuestionType.BehavioralStar,
            EvaluatorVersion: BehavioralEvaluatorV1.EvaluatorVersion,
            FeedbackEngineVersion: EngineVersion,
            Items: feedbackItems
        );

        return new AnswerFeedbackResult(
            Evaluation: evaluation,
            Feedback: bundle
        );
    }
}