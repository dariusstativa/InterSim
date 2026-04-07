namespace InterSim.Domain.Entities;

public sealed class SessionQuestion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid InterviewSessionId { get; set; }
    public InterviewSession InterviewSession { get; set; } = null!;

    public Guid QuestionBankItemId { get; set; }
    public QuestionBankItem QuestionBankItem { get; set; } = null!;

    public string GeneratedText { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }
}