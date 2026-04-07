using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InterSim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interview_sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "question_bank_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Topic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Difficulty = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsTemplate = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_bank_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InterviewSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    AnswerText = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_answers_interview_sessions_InterviewSessionId",
                        column: x => x.InterviewSessionId,
                        principalTable: "interview_sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session_questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InterviewSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionBankItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    GeneratedText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_session_questions_interview_sessions_InterviewSessionId",
                        column: x => x.InterviewSessionId,
                        principalTable: "interview_sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_session_questions_question_bank_items_QuestionBankItemId",
                        column: x => x.QuestionBankItemId,
                        principalTable: "question_bank_items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "question_bank_items",
                columns: new[] { "Id", "Category", "CreatedAt", "Difficulty", "IsActive", "IsTemplate", "Text", "Topic" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "technical", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Explain dependency injection in .NET. Why is it useful?", "dotnet" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "technical", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "What is the difference between Task and Thread in C#?", "dotnet" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "technical", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Explain ACID. Give a practical example where it matters.", "sql" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you had a conflict in a team and how you handled it.", "behavioral" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "technical", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, true, "Explain {concept} and give a small example in C#.", "dotnet" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "technical", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, true, "When would you use {feature} and what problem does it solve?", "dotnet" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "technical", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, true, "How would you optimize a slow query on a {table_type} table?", "sql" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, true, "Describe a situation where you showed {soft_skill}. What was the result?", "behavioral" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_InterviewSessionId",
                table: "answers",
                column: "InterviewSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_question_bank_items_Topic_Difficulty_Category",
                table: "question_bank_items",
                columns: new[] { "Topic", "Difficulty", "Category" });

            migrationBuilder.CreateIndex(
                name: "IX_session_questions_InterviewSessionId_QuestionBankItemId",
                table: "session_questions",
                columns: new[] { "InterviewSessionId", "QuestionBankItemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_session_questions_QuestionBankItemId",
                table: "session_questions",
                column: "QuestionBankItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "session_questions");

            migrationBuilder.DropTable(
                name: "interview_sessions");

            migrationBuilder.DropTable(
                name: "question_bank_items");
        }
    }
}
