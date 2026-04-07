using InterSim.Application.Interviews.Evaluation;

namespace InterSim.Application.Abstractions;

public interface IFollowUpTrigger
{
    FollowUpDecision Decide(
        int score,
        List<string> deficits,
        int followUpCount,
        int maxFollowUps);
}