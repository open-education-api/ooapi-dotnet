#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql;

/// <inheritdoc />
public partial class LearningComponentTestComponentFieldParity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "Abbreviation",
            "TestComponents",
            "character varying(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AddressesJson",
            "TestComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AssessmentJson",
            "TestComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<int>(
            "Attempts",
            "TestComponents",
            "integer",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "EnrolmentJson",
            "TestComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExtraDuration",
            "TestComponents",
            "character varying(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "PassFrom",
            "TestComponents",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResourcesJson",
            "TestComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResultValueType",
            "TestComponents",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "State",
            "TestComponents",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Abbreviation",
            "LearningComponents",
            "character varying(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AddressesJson",
            "LearningComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AssessmentJson",
            "LearningComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "EnrolmentJson",
            "LearningComponents",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResourcesJson",
            "LearningComponents",
            "text",
            nullable: true);

        migrationBuilder.CreateTable(
            "LearningComponentLearningOutcomes",
            table => new
            {
                LearningComponentsId = table.Column<Guid>("uuid", nullable: false),
                LearningOutcomesId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentLearningOutcomes",
                    x => new { x.LearningComponentsId, x.LearningOutcomesId });
                table.ForeignKey(
                    "FK_LearningComponentLearningOutcomes_LearningComponents_Learni~",
                    x => x.LearningComponentsId,
                    "LearningComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_LearningComponentLearningOutcomes_LearningOutcomes_Learning~",
                    x => x.LearningOutcomesId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentLearningOutcomes",
            table => new
            {
                LearningOutcomesId = table.Column<Guid>("uuid", nullable: false),
                TestComponentsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentLearningOutcomes",
                    x => new { x.LearningOutcomesId, x.TestComponentsId });
                table.ForeignKey(
                    "FK_TestComponentLearningOutcomes_LearningOutcomes_LearningOutc~",
                    x => x.LearningOutcomesId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_TestComponentLearningOutcomes_TestComponents_TestComponents~",
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
