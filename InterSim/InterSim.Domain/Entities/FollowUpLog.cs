namespace InterSim.Domain.Entities;

public sealed class FollowUpLog
{
    public Guid Id { get; set; }

    public string QuestionText { get; set; } = string.Empty;
    public string AnswerText { get; set; } = string.Empty;

    public int Score { get; set; }

    public string Deficits { get; set; } = string.Empty;

    public string TriggerType { get; set; } = string.Empty; 
    public string ContextMode { get; set; } = string.Empty; 

    public string? FollowUpQuestion { get; set; }

    public int FollowUpCount { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}