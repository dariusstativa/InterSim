using InterSim.Application.Interviews.Evaluation;
using Xunit;

namespace InterSim.Tests.Evaluation;

public sealed class HeuristicFeedbackEngineV1Tests
{
    [Fact]
    public void Generate_BehavioralStar_ShouldReturnEvaluationAndFeedback_WithVersions()
    {
        var engine = new HeuristicFeedbackEngineV1();

        var question = "Tell me about a time you handled a difficult deadline.";
        var answer =
            "At work, my goal was to deliver a feature before release. " +
            "I planned the tasks, coordinated with teammates, and implemented the solution. " +
            "As a result, we shipped on time and reduced defects by 15%. " +
            "I learned to break down work early and communicate risks.";

        var res = engine.Generate(question, answer, QuestionType.BehavioralStar);

        Assert.NotNull(res.Evaluation);
        Assert.NotNull(res.Feedback);

        Assert.Equal(QuestionType.BehavioralStar, res.Feedback.QuestionType);
        Assert.Equal(BehavioralEvaluatorV1.EvaluatorVersion, res.Feedback.EvaluatorVersion);
        Assert.Equal(HeuristicFeedbackEngineV1.EngineVersion, res.Feedback.FeedbackEngineVersion);

        Assert.InRange(res.Evaluation.Score, 0, 100);
        Assert.Equal(100, res.Evaluation.MaxScore);
    }

    [Fact]
    public void Generate_WhenDeficitsExist_ShouldReturnSomeFeedbackItems()
    {
        var engine = new HeuristicFeedbackEngineV1();

        var question = "Tell me about a time you improved performance.";
        var answer =
            "At work, I had to improve performance. " +
            "I analyzed the issue. " +
            "In the end, it was better."; 

        var res = engine.Generate(question, answer, QuestionType.BehavioralStar);
        Assert.NotEmpty(res.Evaluation.Deficits);
        
        Assert.True(res.Feedback.Items.Count >= 1);
    }
}