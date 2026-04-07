using InterSim.Application.Abstractions;

namespace InterSim.Application.Interviews.Evaluation;

public sealed class RuleBasedFollowUpTrigger : IFollowUpTrigger
{
    public FollowUpDecision Decide(
        int score,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps)
    {
        if (followUpCount >= maxFollowUps)
            return new FollowUpDecision(false, "MaxFollowUpsReached");

        if (score < 75)
            return new FollowUpDecision(true, "LowScore");

        if (deficits.Contains("MissingAction"))
            return new FollowUpDecision(true, "MissingAction");

        if (deficits.Contains("MissingResult"))
            return new FollowUpDecision(true, "MissingResult");

        if (deficits.Contains("NoReflection"))
            return new FollowUpDecision(true, "NoReflection");

        return new FollowUpDecision(false, null);
    }
}