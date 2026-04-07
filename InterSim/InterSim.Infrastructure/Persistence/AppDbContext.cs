using InterSim.Application.Abstractions;
using InterSim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<InterviewSession> InterviewSessions => Set<InterviewSession>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<QuestionBankItem> QuestionBankItems => Set<QuestionBankItem>();
    public DbSet<SessionQuestion> SessionQuestions => Set<SessionQuestion>();
    public DbSet<AnswerEvaluationSample> AnswerEvaluationSamples => Set<AnswerEvaluationSample>();
    public DbSet<FollowUpLog> FollowUpLogs => Set<FollowUpLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InterviewSession>(e =>
        {
            e.ToTable("interview_sessions");
            e.HasKey(x => x.Id);

            e.Property(x => x.Status).HasMaxLength(32).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Answer>(e =>
        {
            e.ToTable("answers");
            e.HasKey(x => x.Id);

            e.Property(x => x.QuestionText).HasMaxLength(2000).IsRequired();
            e.Property(x => x.AnswerText).HasMaxLength(4000).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");

            e.HasOne(x => x.InterviewSession)
                .WithMany(s => s.Answers)
                .HasForeignKey(x => x.InterviewSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(x => x.InterviewSessionId);
        });

        modelBuilder.Entity<QuestionBankItem>(e =>
        {
            e.ToTable("question_bank_items");
            e.HasKey(x => x.Id);

            e.Property(x => x.Topic).HasMaxLength(50).IsRequired();
            e.Property(x => x.Difficulty).HasMaxLength(50).IsRequired();
            e.Property(x => x.Category).HasMaxLength(50).IsRequired();
            e.Property(x => x.Text).HasMaxLength(2000).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");

            e.HasIndex(x => new { x.Topic, x.Difficulty, x.Category });
        });

        modelBuilder.Entity<SessionQuestion>(e =>
        {
            e.ToTable("session_questions");
            e.HasKey(x => x.Id);

            e.Property(x => x.GeneratedText).HasMaxLength(2000).IsRequired();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");

            e.HasOne(x => x.InterviewSession)
                .WithMany(s => s.Questions)
                .HasForeignKey(x => x.InterviewSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.QuestionBankItem)
                .WithMany()
                .HasForeignKey(x => x.QuestionBankItemId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => new { x.InterviewSessionId, x.QuestionBankItemId })
                .IsUnique();
        });
        modelBuilder.Entity<FollowUpLog>(e =>
        {
            e.ToTable("interview_followup_logs");
            e.HasKey(x => x.Id);

            e.Property(x => x.QuestionText).HasMaxLength(2000);
            e.Property(x => x.AnswerText).HasMaxLength(4000);
            e.Property(x => x.Deficits).HasMaxLength(1000);
            e.Property(x => x.TriggerType).HasMaxLength(50);
            e.Property(x => x.ContextMode).HasMaxLength(50);
            e.Property(x => x.FollowUpQuestion).HasMaxLength(2000);

            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");
        });
        modelBuilder.Entity<AnswerEvaluationSample>(e =>
        {
            e.ToTable("answer_evaluation_samples");
            e.HasKey(x => x.Id);

            e.Property(x => x.QuestionText).HasMaxLength(2000).IsRequired();
            e.Property(x => x.AnswerText).HasMaxLength(4000).IsRequired();

            e.Property(x => x.Category).HasMaxLength(50).IsRequired();
            e.Property(x => x.Topic).HasMaxLength(50).IsRequired();
            e.Property(x => x.Difficulty).HasMaxLength(50).IsRequired();

            e.Property(x => x.CreatedAt).HasDefaultValueSql("NOW()");

            e.HasIndex(x => new { x.Category, x.Topic, x.Difficulty });
        });

        var seedCreatedAt = new DateTimeOffset(2026, 02, 22, 0, 0, 0, TimeSpan.Zero);

        var q1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var q2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var q3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var q4 = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var q5 = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var q6 = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var q7 = Guid.Parse("77777777-7777-7777-7777-777777777777");
        var q8 = Guid.Parse("88888888-8888-8888-8888-888888888888");
        var q9 = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var q10 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var q11 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        var q12 = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
        var q13 = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        var q14 = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        var q15 = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        var q16 = Guid.Parse("10101010-1010-1010-1010-101010101010");
        var q17 = Guid.Parse("12121212-1212-1212-1212-121212121212");
        var q18 = Guid.Parse("13131313-1313-1313-1313-131313131313");
        var q19 = Guid.Parse("14141414-1414-1414-1414-141414141414");
        var q20 = Guid.Parse("15151515-1515-1515-1515-151515151515");
        var q21 = Guid.Parse("16161616-1616-1616-1616-161616161616");
        var q22 = Guid.Parse("17171717-1717-1717-1717-171717171717");
        var q23 = Guid.Parse("18181818-1818-1818-1818-181818181818");

        modelBuilder.Entity<QuestionBankItem>().HasData(
            new QuestionBankItem
            {
                Id = q1,
                Topic = "dotnet",
                Difficulty = "junior",
                Category = "technical",
                IsTemplate = false,
                Text = "Explain dependency injection in .NET. Why is it useful?",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q2,
                Topic = "dotnet",
                Difficulty = "junior",
                Category = "technical",
                IsTemplate = false,
                Text = "What is the difference between Task and Thread in C#?",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q3,
                Topic = "sql",
                Difficulty = "junior",
                Category = "technical",
                IsTemplate = false,
                Text = "Explain ACID. Give a practical example where it matters.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },

            new QuestionBankItem
            {
                Id = q4,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you had a conflict in a team and how you handled it.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q5,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a situation where you showed initiative to solve a problem.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q6,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you failed and what you learned from it.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q7,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time when you had to learn something quickly.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q8,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you helped a teammate succeed.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q9,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time you improved a process or workflow.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q10,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you had to meet a tight deadline.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q11,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a situation where you received critical feedback. How did you respond?",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q12,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you had to prioritize multiple tasks.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q13,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a difficult problem you solved.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q14,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you disagreed with a team decision.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q15,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time you worked under pressure.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q16,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you made a mistake in a project.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q17,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time you explained something complex to someone else.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q18,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you took responsibility for a difficult task.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q19,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time you had to adapt to a change during a project.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q20,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you collaborated with someone difficult.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q21,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time you solved an unexpected issue during development.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q22,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Tell me about a time you supported a teammate who was struggling.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            },
            new QuestionBankItem
            {
                Id = q23,
                Topic = "behavioral",
                Difficulty = "junior",
                Category = "behavioral",
                IsTemplate = false,
                Text = "Describe a time you had to make a decision with incomplete information.",
                IsActive = true,
                CreatedAt = seedCreatedAt
            }
        );
    }
}