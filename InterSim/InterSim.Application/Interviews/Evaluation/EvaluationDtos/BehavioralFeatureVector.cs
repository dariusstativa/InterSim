namespace InterSim.Application.Interviews.Evaluation;

public sealed record BehavioralFeatureVector(
    double SimQA,
    double MaxChunkSim,
    double AvgTopChunkSim,
    double Situation,
    double Task,
    double Action,
    double Result,
    double Reflection,
    int WordCount,
    int ActionVerbHits,
    int HasFirstPerson,
    int HasMetrics
);