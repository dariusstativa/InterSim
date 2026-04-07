namespace InterSim.Domain.Entities;

public sealed class Answer
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid InterviewSessionId { get; set; }
    public InterviewSession InterviewSession { get; set; } = null!;

    public string QuestionText { get; set; } = string.Empty;
    public string AnswerText { get; set; } = string.Empty;

   
    public int Score { get; set; }
    public int MaxScore { get; set; } = 100;

    
    public string BreakdownJson { get; set; } = "[]";
    public string DeficitsJson { get; set; } = "[]";

    
    public Guid FeedbackBundleId { get; set; }
    public string EvaluatorVersion { get; set; } = string.Empty;
    public string FeedbackEngineVersion { get; set; } = string.Empty;

    
    public string FeedbackItemsJson { get; set; } = "[]";

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}