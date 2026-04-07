using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InterSim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnswerEvaluationSamples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "answer_evaluation_samples",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionText = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    AnswerText = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Topic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Difficulty = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HumanRelevance = table.Column<int>(type: "integer", nullable: false),
                    HumanStructure = table.Column<int>(type: "integer", nullable: false),
                    HumanSpecificity = table.Column<int>(type: "integer", nullable: false),
                    HumanImpactReflection = table.Column<int>(type: "integer", nullable: false),
                    HumanTotalScore = table.Column<int>(type: "integer", nullable: false),
                    WordCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answer_evaluation_samples", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Category", "IsTemplate", "Text", "Topic" },
                values: new object[] { "behavioral", false, "Describe a situation where you showed initiative to solve a problem.", "behavioral" });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "Category", "IsTemplate", "Text", "Topic" },
                values: new object[] { "behavioral", false, "Tell me about a time you failed and what you learned from it.", "behavioral" });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "Category", "IsTemplate", "Text", "Topic" },
                values: new object[] { "behavioral", false, "Describe a time when you had to learn something quickly.", "behavioral" });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "IsTemplate", "Text" },
                values: new object[] { false, "Tell me about a time you helped a teammate succeed." });

            migrationBuilder.InsertData(
                table: "question_bank_items",
                columns: new[] { "Id", "Category", "CreatedAt", "Difficulty", "IsActive", "IsTemplate", "Text", "Topic" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you made a mistake in a project.", "behavioral" },
                    { new Guid("12121212-1212-1212-1212-121212121212"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a time you explained something complex to someone else.", "behavioral" },
                    { new Guid("13131313-1313-1313-1313-131313131313"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you took responsibility for a difficult task.", "behavioral" },
                    { new Guid("14141414-1414-1414-1414-141414141414"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a time you had to adapt to a change during a project.", "behavioral" },
                    { new Guid("15151515-1515-1515-1515-151515151515"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you collaborated with someone difficult.", "behavioral" },
                    { new Guid("16161616-1616-1616-1616-161616161616"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a time you solved an unexpected issue during development.", "behavioral" },
                    { new Guid("17171717-1717-1717-1717-171717171717"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you supported a teammate who was struggling.", "behavioral" },
                    { new Guid("18181818-1818-1818-1818-181818181818"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a time you had to make a decision with incomplete information.", "behavioral" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a time you improved a process or workflow.", "behavioral" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you had to meet a tight deadline.", "behavioral" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a situation where you received critical feedback. How did you respond?", "behavioral" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you had to prioritize multiple tasks.", "behavioral" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a difficult problem you solved.", "behavioral" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Tell me about a time you disagreed with a team decision.", "behavioral" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "behavioral", new DateTimeOffset(new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "junior", true, false, "Describe a time you worked under pressure.", "behavioral" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_answer_evaluation_samples_Category_Topic_Difficulty",
                table: "answer_evaluation_samples",
                columns: new[] { "Category", "Topic", "Difficulty" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answer_evaluation_samples");

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("12121212-1212-1212-1212-121212121212"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("13131313-1313-1313-1313-131313131313"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("14141414-1414-1414-1414-141414141414"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("15151515-1515-1515-1515-151515151515"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("16161616-1616-1616-1616-161616161616"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("17171717-1717-1717-1717-171717171717"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("18181818-1818-1818-1818-181818181818"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Category", "IsTemplate", "Text", "Topic" },
                values: new object[] { "technical", true, "Explain {concept} and give a small example in C#.", "dotnet" });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "Category", "IsTemplate", "Text", "Topic" },
                values: new object[] { "technical", true, "When would you use {feature} and what problem does it solve?", "dotnet" });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "Category", "IsTemplate", "Text", "Topic" },
                values: new object[] { "technical", true, "How would you optimize a slow query on a {table_type} table?", "sql" });

            migrationBuilder.UpdateData(
                table: "question_bank_items",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "IsTemplate", "Text" },
                values: new object[] { true, "Describe a situation where you showed {soft_skill}. What was the result?" });
        }
    }
}
