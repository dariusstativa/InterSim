namespace InterSim.Application.Interviews.Evaluation;

public sealed record BehavioralTrainingRow(
    Guid SampleId,
    BehavioralFeatureVector Features,
    float TargetTotalScore
);