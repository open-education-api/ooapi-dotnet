using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class AddResultCostStudyLoadAndMissingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attendance",
                table: "TestComponentOfferingAssociations",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredPersonalNeedsJson",
                table: "TestComponentOfferingAssociations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormalDocument",
                table: "Programmes",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LevelOfQualification",
                table: "Programmes",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attendance",
                table: "LearningComponentOfferingAssociations",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "RequiredPersonalNeedsJson",
                table: "TestComponentOfferingAssociations");

            migrationBuilder.DropColumn(
                name: "FormalDocument",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "LevelOfQualification",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "Attendance",
                table: "LearningComponentOfferingAssociations");
        }
    }
}
