using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterSim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnswerEvaluationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BreakdownJson",
                table: "answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeficitsJson",
                table: "answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EvaluatorVersion",
                table: "answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackBundleId",
                table: "answers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FeedbackEngineVersion",
                table: "answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeedbackItemsJson",
                table: "answers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxScore",
                table: "answers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "answers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakdownJson",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "DeficitsJson",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "EvaluatorVersion",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "FeedbackBundleId",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "FeedbackEngineVersion",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "FeedbackItemsJson",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "MaxScore",
                table: "answers");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "answers");
        }
    }
}
