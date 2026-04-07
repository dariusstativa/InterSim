using InterSim.Application.Interviews.Evaluation;
using Xunit;

namespace InterSim.Tests.Evaluation;

public sealed class BehavioralEvaluatorV1Tests
{
    [Fact]
    public void Evaluate_GoodStarAnswer_ShouldDetectResult_AndHaveHighScore()
    {
        var question = "Tell me about a time you had a conflict with a teammate. What did you do?";
        var answer =
            "In a team project at university, there was a conflict about the API design because we had different priorities. " +
            "My responsibility was to unblock the team and keep the deadline. " +
            "I scheduled a short meeting, asked each person to explain their constraints, and proposed a compromise. " +
            "I implemented the change, updated the documentation, and created a small test suite to prevent regressions. " +
            "As a result, we merged within 2 days, reduced review comments by 30%, and delivered on time. " +
            "I learned to make trade-offs explicit early and to document decisions so the same debate doesn’t repeat.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.True(res.Score >= 75, $"Expected score >= 75, got {res.Score}");
        Assert.DoesNotContain(DeficitTag.MissingResult, res.Deficits);
        Assert.DoesNotContain(DeficitTag.OffTopic, res.Deficits);
        Assert.DoesNotContain(DeficitTag.TooShort, res.Deficits);
    }

    [Fact]
    public void Evaluate_ResultWithComma_ShouldStillDetectResult()
    {
        var question = "Tell me about a time you improved a process.";
        var answer =
            "In a team project, my goal was to speed up feedback loops. " +
            "I documented the workflow and proposed small changes. " +
            "As a result, we reduced cycle time by 20%, and the team shipped faster. " +
            "I learned to validate assumptions early.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.DoesNotContain(DeficitTag.MissingResult, res.Deficits);
        Assert.True(res.Score > 0);
    }

    [Fact]
    public void Evaluate_TooShortAnswer_ShouldFlagTooShort_AndLowStructure()
    {
        var question = "Tell me about a time you solved a problem under pressure.";
        var answer = "I fixed it quickly and it worked.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Contains(DeficitTag.TooShort, res.Deficits);
        Assert.Contains(DeficitTag.LowStructure, res.Deficits);
        Assert.True(res.Score <= 40, $"Expected low score, got {res.Score}");
    }

    [Fact]
    public void Evaluate_TooLongAnswer_ShouldFlagTooLong()
    {
        var question = "Tell me about a time you led an initiative.";

        
        var chunk =
            "In a team project at work, my responsibility was to coordinate people and deliver value. " +
            "I planned tasks, communicated priorities, documented decisions, and aligned stakeholders. " +
            "As a result, we improved delivery speed and reduced risk. I learned to be proactive and transparent. ";

        var answer = string.Concat(Enumerable.Repeat(chunk, 12)); // ~12 * 30+ words => well over 350

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Contains(DeficitTag.TooLong, res.Deficits);
    }

    [Fact]
    public void Evaluate_OffTopicAnswer_ShouldFlagOffTopic_AndScoreLow()
    {
        var question = "Tell me about a time you disagreed with your manager.";
        var answer =
            "I like working with databases and Docker. I enjoy writing tests and learning new tools. " +
            "I also prefer clean architecture and good documentation.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Contains(DeficitTag.OffTopic, res.Deficits);
        Assert.True(res.Score <= 35, $"Expected low score for off-topic, got {res.Score}");
    }

    [Fact]
    public void Evaluate_MissingReflection_ShouldFlagNoReflection()
    {
        var question = "Tell me about a time you handled a difficult deadline.";
        var answer =
            "At work, my goal was to deliver a feature before release. " +
            "I planned the tasks, coordinated with teammates, and implemented the solution. " +
            "As a result, we shipped on time and reduced defects by 15%.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Contains(DeficitTag.NoReflection, res.Deficits);
    }

    [Fact]
    public void Evaluate_MissingMetrics_WhenHasResult_ShouldFlagMissingMetrics()
    {
        var question = "Tell me about a time you improved performance.";
        var answer =
            "In a project at work, my goal was to improve performance. " +
            "I analyzed bottlenecks and optimized queries. " +
            "As a result, the system was much faster. " +
            "I learned to measure before optimizing.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Contains(DeficitTag.MissingMetrics, res.Deficits);
    }

    [Fact]
    public void Evaluate_WeOnlyOwnership_ShouldFlagNoOwnership()
    {
        var question = "Tell me about a time you contributed to a team success.";
        var answer =
            "In a team project at work, the objective was to deliver a new API. " +
            "We planned the work, we implemented the endpoints, and we tested the changes. " +
            "As a result, we delivered on time and improved reliability by 10%. " +
            " learned to communicate clearly.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

      
        Assert.Contains(DeficitTag.NoOwnership, res.Deficits);
    }

    [Fact]
    public void Evaluate_MultipleMissingStarParts_ShouldFlagLowStructure()
    {
        var question = "Tell me about a time you handled conflict.";
        var answer =
            "I implemented a solution. It improved things. " + // weak
            "Next time I would communicate earlier.";          // reflection only

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Contains(DeficitTag.LowStructure, res.Deficits);
    }

    [Fact]
    public void Evaluate_Score_ShouldAlwaysBeWithin0To100()
    {
        var question = "Tell me about a time you failed and what you learned.";
        var answer =
            "I had to fix a production issue. I did a hotfix. As a result, it improved by 9999%. I learned a lot.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.InRange(res.Score, 0, 100);
        Assert.Equal(100, res.MaxScore);
    }

    [Fact]
    public void Evaluate_Breakdown_ShouldAlwaysHaveFourCriteria()
    {
        var question = "Tell me about a time you improved a process.";
        var answer =
            "In a project at work, my goal was to improve the process. " +
            "I documented steps and proposed changes. " +
            "As a result, we improved quality by 10%. " +
            "I learned to keep changes small.";

        var res = BehavioralEvaluatorV1.Evaluate(question, answer);

        Assert.Equal(4, res.Breakdown.Count);
        Assert.Contains(res.Breakdown, b => b.Criterion == "Relevance" && b.MaxPoints == 25);
        Assert.Contains(res.Breakdown, b => b.Criterion == "Structure" && b.MaxPoints == 25);
        Assert.Contains(res.Breakdown, b => b.Criterion == "Specificity" && b.MaxPoints == 25);
        Assert.Contains(res.Breakdown, b => b.Criterion == "Impact & Learning" && b.MaxPoints == 25);
    }
}