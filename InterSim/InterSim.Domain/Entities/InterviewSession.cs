namespace InterSim.Domain.Entities;

public sealed class InterviewSession
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; set; }
    public string Status { get; set; } = "active";

    public List<SessionQuestion> Questions { get; set; } = new();
    public List<Answer> Answers { get; set; } = new();
}