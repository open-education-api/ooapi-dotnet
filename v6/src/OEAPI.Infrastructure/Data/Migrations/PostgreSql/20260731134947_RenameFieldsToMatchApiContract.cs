using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class RenameFieldsToMatchApiContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDate",
                table: "TestComponents",
                newName: "LastPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDate",
                table: "TestComponents",
                newName: "LastPossibleOfferingEndDateTime");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "TestComponents",
                newName: "FirstPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDate",
                table: "Programmes",
                newName: "LastPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDate",
                table: "Programmes",
                newName: "LastPossibleOfferingEndDateTime");

            migrationBuilder.RenameColumn(
                name: "FirstStartDate",
                table: "Programmes",
                newName: "FirstStartDateTime");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "Programmes",
                newName: "FirstPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "WebsiteUrl",
                table: "Organisations",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "LogoUrl",
                table: "Organisations",
                newName: "Logo");

            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                table: "Organisations",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDate",
                table: "LearningComponents",
                newName: "LastPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDate",
                table: "LearningComponents",
                newName: "LastPossibleOfferingEndDateTime");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "LearningComponents",
                newName: "FirstPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDate",
                table: "Courses",
                newName: "LastPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDate",
                table: "Courses",
                newName: "LastPossibleOfferingEndDateTime");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDate",
                table: "Courses",
                newName: "FirstPossibleOfferingStartDateTime");

            migrationBuilder.RenameColumn(
                name: "HouseNumber",
                table: "Addresses",
                newName: "StreetNumber");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Addresses",
                newName: "PostCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDateTime",
                table: "TestComponents",
                newName: "LastPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDateTime",
                table: "TestComponents",
                newName: "LastPossibleOfferingEndDate");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDateTime",
                table: "TestComponents",
                newName: "FirstPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDateTime",
                table: "Programmes",
                newName: "LastPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDateTime",
                table: "Programmes",
                newName: "LastPossibleOfferingEndDate");

            migrationBuilder.RenameColumn(
                name: "FirstStartDateTime",
                table: "Programmes",
                newName: "FirstStartDate");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDateTime",
                table: "Programmes",
                newName: "FirstPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Organisations",
                newName: "Abbreviation");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Organisations",
                newName: "WebsiteUrl");

            migrationBuilder.RenameColumn(
                name: "Logo",
                table: "Organisations",
                newName: "LogoUrl");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDateTime",
                table: "LearningComponents",
                newName: "LastPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDateTime",
                table: "LearningComponents",
                newName: "LastPossibleOfferingEndDate");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDateTime",
                table: "LearningComponents",
                newName: "FirstPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingStartDateTime",
                table: "Courses",
                newName: "LastPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "LastPossibleOfferingEndDateTime",
                table: "Courses",
                newName: "LastPossibleOfferingEndDate");

            migrationBuilder.RenameColumn(
                name: "FirstPossibleOfferingStartDateTime",
                table: "Courses",
                newName: "FirstPossibleOfferingStartDate");

            migrationBuilder.RenameColumn(
                name: "PostCode",
                table: "Addresses",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "StreetNumber",
                table: "Addresses",
                newName: "HouseNumber");
        }
    }
}
