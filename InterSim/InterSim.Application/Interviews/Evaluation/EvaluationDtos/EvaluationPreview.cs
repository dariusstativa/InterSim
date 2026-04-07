namespace InterSim.Application.Interviews.Evaluation;

public sealed record EvaluationPreview(
    int TotalScore,
    int RelevanceScore,
    int StructureScore,
    int SpecificityScore,
    int ImpactReflectionScore,
    SemanticSignals Semantic,
    StarSignals Star,
    IReadOnlyList<string> Deficits
);