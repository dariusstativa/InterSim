namespace InterSim.Domain.Entities;

public sealed class AnswerEvaluationSample
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string QuestionText { get; set; } = string.Empty;
    public string AnswerText { get; set; } = string.Empty;

    public string Category { get; set; } = "behavioral";
    public string Topic { get; set; } = "general";
    public string Difficulty { get; set; } = "junior";

    public int HumanRelevance { get; set; }
    public int HumanStructure { get; set; }
    public int HumanSpecificity { get; set; }
    public int HumanImpactReflection { get; set; }

    public int HumanTotalScore { get; set; }
    public int WordCount { get; set; }
    

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}