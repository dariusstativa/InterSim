using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFollowUpLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "interview_followup_logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    AnswerText = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    Deficits = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    TriggerType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ContextMode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FollowUpQuestion = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    FollowUpCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_interview_followup_logs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interview_followup_logs");
        }
    }
}
