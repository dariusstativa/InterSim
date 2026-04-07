namespace InterSim.Application.Interviews.Evaluation;


public sealed record LlmFollowUpDecision(
    bool ShouldAsk,
    string? Reason
);