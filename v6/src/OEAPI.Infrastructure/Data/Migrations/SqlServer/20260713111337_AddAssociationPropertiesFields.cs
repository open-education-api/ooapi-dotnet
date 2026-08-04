#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer;

/// <inheritdoc />
public partial class AddAssociationPropertiesFields : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "TestComponentOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "TestComponentOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "TestComponentOfferingAssociations",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "TestComponentOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "ProgrammeOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "ProgrammeOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "ProgrammeOfferingAssociations",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "ProgrammeOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "LearningComponentOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "LearningComponentOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "LearningComponentOfferingAssociations",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "LearningComponentOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ActualEndDateTime",
            "CourseOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ExpectedEndDateTime",
            "CourseOfferingAssociations",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Role",
            "CourseOfferingAssociations",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "StartDateTime",
            "CourseOfferingAssociations",
            "nvarchar(max)",
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
