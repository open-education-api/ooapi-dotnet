#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer;

/// <inheritdoc />
public partial class CourseAndPersonFieldTyping : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "CountryOfBirth",
            "Persons");

        migrationBuilder.DropColumn(
            "IceRelation",
            "Persons");

        migrationBuilder.DropColumn(
            "Nationality",
            "Persons");

        migrationBuilder.AddColumn<string>(
            "CountryOfBirthJson",
            "Persons",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "NationalityJson",
            "Persons",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AddressesJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AdmissionRequirementsJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AssessmentJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "EnrolmentJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Level",
            "Courses",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Link",
            "Courses",
            "nvarchar(2048)",
            maxLength: 2048,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "QualificationRequirementsJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResourcesJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "SupplementaryInformationJson",
            "Courses",
            "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "CountryOfBirthJson",
            "Persons");

        migrationBuilder.DropColumn(
            "NationalityJson",
            "Persons");

        migrationBuilder.DropColumn(
            "AddressesJson",
            "Courses");

        migrationBuilder.DropColumn(
            "AdmissionRequirementsJson",
            "Courses");

        migrationBuilder.DropColumn(
            "AssessmentJson",
            "Courses");

        migrationBuilder.DropColumn(
            "EnrolmentJson",
            "Courses");

        migrationBuilder.DropColumn(
            "Level",
            "Courses");

        migrationBuilder.DropColumn(
            "Link",
            "Courses");

        migrationBuilder.DropColumn(
            "QualificationRequirementsJson",
            "Courses");

        migrationBuilder.DropColumn(
            "ResourcesJson",
            "Courses");

        migrationBuilder.DropColumn(
            "SupplementaryInformationJson",
            "Courses");

        migrationBuilder.AddColumn<string>(
            "CountryOfBirth",
            "Persons",
            "nvarchar(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "IceRelation",
            "Persons",
            "nvarchar(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Nationality",
            "Persons",
            "nvarchar(256)",
            maxLength: 256,
            nullable: true);
    }
}
