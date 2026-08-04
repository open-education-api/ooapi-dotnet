#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer;

/// <inheritdoc />
public partial class LearningComponentTestComponentFieldParity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "Abbreviation",
            "TestComponents",
            "nvarchar(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AddressesJson",
            "TestComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AssessmentJson",
            "TestComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            "Attempts",
            "TestComponents",
            "int",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "EnrolmentJson",
            "TestComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExtraDuration",
            "TestComponents",
            "nvarchar(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "PassFrom",
            "TestComponents",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResourcesJson",
            "TestComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResultValueType",
            "TestComponents",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "State",
            "TestComponents",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Abbreviation",
            "LearningComponents",
            "nvarchar(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AddressesJson",
            "LearningComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AssessmentJson",
            "LearningComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "EnrolmentJson",
            "LearningComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResourcesJson",
            "LearningComponents",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.CreateTable(
            "LearningComponentLearningOutcomes",
            table => new
            {
                LearningComponentsId = table.Column<Guid>("uniqueidentifier", nullable: false),
                LearningOutcomesId = table.Column<Guid>("uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentLearningOutcomes",
                    x => new { x.LearningComponentsId, x.LearningOutcomesId });
                table.ForeignKey(
                    "FK_LearningComponentLearningOutcomes_LearningComponents_LearningComponentsId",
                    x => x.LearningComponentsId,
                    "LearningComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_LearningComponentLearningOutcomes_LearningOutcomes_LearningOutcomesId",
                    x => x.LearningOutcomesId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentLearningOutcomes",
            table => new
            {
                LearningOutcomesId = table.Column<Guid>("uniqueidentifier", nullable: false),
                TestComponentsId = table.Column<Guid>("uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentLearningOutcomes",
                    x => new { x.LearningOutcomesId, x.TestComponentsId });
                table.ForeignKey(
                    "FK_TestComponentLearningOutcomes_LearningOutcomes_LearningOutcomesId",
                    x => x.LearningOutcomesId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_TestComponentLearningOutcomes_TestComponents_TestComponentsId",
                    x => x.TestComponentsId,
                    "TestComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_LearningComponentLearningOutcomes_LearningOutcomesId",
            "LearningComponentLearningOutcomes",
            "LearningOutcomesId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentLearningOutcomes_TestComponentsId",
            "TestComponentLearningOutcomes",
            "TestComponentsId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "LearningComponentLearningOutcomes");

        migrationBuilder.DropTable(
            "TestComponentLearningOutcomes");

        migrationBuilder.DropColumn(
            "Abbreviation",
            "TestComponents");

        migrationBuilder.DropColumn(
            "AddressesJson",
            "TestComponents");

        migrationBuilder.DropColumn(
            "AssessmentJson",
            "TestComponents");

        migrationBuilder.DropColumn(
            "Attempts",
            "TestComponents");

        migrationBuilder.DropColumn(
            "EnrolmentJson",
            "TestComponents");

        migrationBuilder.DropColumn(
            "ExtraDuration",
            "TestComponents");

        migrationBuilder.DropColumn(
            "PassFrom",
            "TestComponents");

        migrationBuilder.DropColumn(
            "ResourcesJson",
            "TestComponents");

        migrationBuilder.DropColumn(
            "ResultValueType",
            "TestComponents");

        migrationBuilder.DropColumn(
            "State",
            "TestComponents");

        migrationBuilder.DropColumn(
            "Abbreviation",
            "LearningComponents");

        migrationBuilder.DropColumn(
            "AddressesJson",
            "LearningComponents");

        migrationBuilder.DropColumn(
            "AssessmentJson",
            "LearningComponents");

        migrationBuilder.DropColumn(
            "EnrolmentJson",
            "LearningComponents");

        migrationBuilder.DropColumn(
            "ResourcesJson",
            "LearningComponents");
    }
}
