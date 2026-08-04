#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql;

/// <inheritdoc />
public partial class AddAssociationPropertiesFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "TestComponentOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "TestComponentOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "TestComponentOfferingAssociations",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "TestComponentOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "ProgrammeOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "ProgrammeOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "ProgrammeOfferingAssociations",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "ProgrammeOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "LearningComponentOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "LearningComponentOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "LearningComponentOfferingAssociations",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "LearningComponentOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "CourseOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "CourseOfferingAssociations",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "CourseOfferingAssociations",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "CourseOfferingAssociations",
            "text",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "ActualEndDateTime",
            "TestComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "ExpectedEndDateTime",
            "TestComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "Role",
            "TestComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "StartDateTime",
            "TestComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "ActualEndDateTime",
            "ProgrammeOfferingAssociations");

        migrationBuilder.DropColumn(
            "ExpectedEndDateTime",
            "ProgrammeOfferingAssociations");

        migrationBuilder.DropColumn(
            "Role",
            "ProgrammeOfferingAssociations");

        migrationBuilder.DropColumn(
            "StartDateTime",
            "ProgrammeOfferingAssociations");

        migrationBuilder.DropColumn(
            "ActualEndDateTime",
            "LearningComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "ExpectedEndDateTime",
            "LearningComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "Role",
            "LearningComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "StartDateTime",
            "LearningComponentOfferingAssociations");

        migrationBuilder.DropColumn(
            "ActualEndDateTime",
            "CourseOfferingAssociations");

        migrationBuilder.DropColumn(
            "ExpectedEndDateTime",
            "CourseOfferingAssociations");

        migrationBuilder.DropColumn(
            "Role",
            "CourseOfferingAssociations");

        migrationBuilder.DropColumn(
            "StartDateTime",
            "CourseOfferingAssociations");
    }
}
