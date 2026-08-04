#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql;

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
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "NationalityJson",
            "Persons",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AddressesJson",
            "Courses",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AdmissionRequirementsJson",
            "Courses",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "AssessmentJson",
            "Courses",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "EnrolmentJson",
            "Courses",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Level",
            "Courses",
            "character varying(64)",
            maxLength: 64,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Link",
            "Courses",
            "character varying(2048)",
            maxLength: 2048,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "QualificationRequirementsJson",
            "Courses",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "ResourcesJson",
            "Courses",
            "text",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "SupplementaryInformationJson",
            "Courses",
            "text",
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
            "character varying(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "IceRelation",
            "Persons",
            "character varying(256)",
            maxLength: 256,
            nullable: true);

        migrationBuilder.AddColumn<string>(
            "Nationality",
            "Persons",
            "character varying(256)",
            maxLength: 256,
            nullable: true);
    }
}
