namespace InterSim.Application.Interviews.Evaluation;

public sealed record FollowUpDecision(
    bool ShouldAsk,
    string? Reason
);