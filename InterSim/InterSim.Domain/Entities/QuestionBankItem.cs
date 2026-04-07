namespace InterSim.Domain.Entities;

public sealed class QuestionBankItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Topic { get; set; } = "general";
    public string Difficulty { get; set; } = "junior";
    public string Category { get; set; } = "technical";

    public bool IsTemplate { get; set; }
    public string Text { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }
}