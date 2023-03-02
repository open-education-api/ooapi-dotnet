using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedProgramAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Programs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
