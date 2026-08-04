using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class AddTier1bResponseFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPossibleOfferingStartDate",
                table: "TestComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingEndDate",
                table: "TestComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingStartDate",
                table: "TestComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResultExpected",
                table: "TestComponents",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentsJson",
                table: "TestComponentOfferings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultWeight",
                table: "TestComponentOfferings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttemptsJson",
                table: "TestComponentOfferingAssociations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentsJson",
                table: "TestComponentOfferingAssociations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraDuration",
                table: "TestComponentOfferingAssociations",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InitialAttemptOnAssociation",
                table: "TestComponentOfferingAssociations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IrregularitiesJson",
                table: "TestComponentOfferingAssociations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumNumberOfAttemptsOnAssociation",
                table: "TestComponentOfferingAssociations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressesJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdmissionRequirementsJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssessmentJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnrolmentJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPossibleOfferingStartDate",
                table: "Programmes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingEndDate",
                table: "Programmes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingStartDate",
                table: "Programmes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Programmes",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Programmes",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationRequirementsJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourcesJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SupplementaryInformationJson",
                table: "Programmes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPossibleOfferingStartDate",
                table: "LearningComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingEndDate",
                table: "LearningComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingStartDate",
                table: "LearningComponents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResultWeight",
                table: "LearningComponentOfferings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstPossibleOfferingStartDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingEndDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPossibleOfferingStartDate",
                table: "Courses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyLoadJson",
                table: "CourseOfferingAssociations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProgrammeLearningOutcomes",
                columns: table => new
                {
                    LearningOutcomesId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgrammesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeLearningOutcomes", x => new { x.LearningOutcomesId, x.ProgrammesId });
                    table.ForeignKey(
                        name: "FK_ProgrammeLearningOutcomes_LearningOutcomes_LearningOutcomes~",
                        column: x => x.LearningOutcomesId,
                        principalTable: "LearningOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammeLearningOutcomes_Programmes_ProgrammesId",
                        column: x => x.ProgrammesId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeLearningOutcomes_ProgrammesId",
                table: "ProgrammeLearningOutcomes",
                column: "ProgrammesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgrammeLearningOutcomes");

            migrationBuilder.DropColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "TestComponents");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingEndDate",
                table: "TestComponents");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingStartDate",
                table: "TestComponents");

            migrationBuilder.DropColumn(
                name: "ResultExpected",
                table: "TestComponents");

            migrationBuilder.DropColumn(
                name: "DocumentsJson",
                table: "TestComponentOfferings");

            migrationBuilder.DropColumn(
                name: "ResultWeight",
                table: "TestComponentOfferings");

            migrationBuilder.DropColumn(
                name: "AttemptsJson",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "DocumentsJson",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "ExtraDuration",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "InitialAttemptOnAssociation",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "IrregularitiesJson",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "MaximumNumberOfAttemptsOnAssociation",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "AddressesJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "AdmissionRequirementsJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "AssessmentJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "EnrolmentJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingEndDate",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingStartDate",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "QualificationRequirementsJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "ResourcesJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "SupplementaryInformationJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "LearningComponents");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingEndDate",
                table: "LearningComponents");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingStartDate",
                table: "LearningComponents");

            migrationBuilder.DropColumn(
                name: "ResultWeight",
                table: "LearningComponentOfferings");

            migrationBuilder.DropColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingEndDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LastPossibleOfferingStartDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudyLoadJson",
                table: "CourseOfferingAssociations");
        }
    }
}
