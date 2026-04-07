using System;

namespace InterSim.Application.Interviews.Evaluation;

public static class FeedbackBundleFactoryV1
{
    public const string FeedbackEngineVersion = "baseline-feedback-v1";

    public static AnswerFeedbackResult CreateBehavioral(
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
            FeedbackEngineVersion: FeedbackEngineVersion,
            Items: feedbackItems
        );

        return new AnswerFeedbackResult(
            Evaluation: evaluation,
            Feedback: bundle
        );
    }
}