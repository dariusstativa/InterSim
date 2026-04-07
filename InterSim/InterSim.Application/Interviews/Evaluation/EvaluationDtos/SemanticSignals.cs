namespace InterSim.Application.Interviews.Evaluation;

public sealed record SemanticSignals(
    double SimQA,
    double MaxChunkSim,
    double AvgTopChunkSim
);