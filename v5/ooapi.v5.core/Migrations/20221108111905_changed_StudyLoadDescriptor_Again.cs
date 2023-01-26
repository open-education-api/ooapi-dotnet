using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changed_StudyLoadDescriptor_Again : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "Programs",
                newName: "StudyLoadUnit");

            migrationBuilder.RenameColumn(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "EducationSpecification",
                newName: "StudyLoadUnit");

            migrationBuilder.RenameColumn(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "Courses",
                newName: "StudyLoadUnit");

            migrationBuilder.AddColumn<decimal>(
                name: "StudyLoadValue",
                schema: "ooapiv5",
                table: "Programs",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StudyLoadValue",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StudyLoadValue",
                schema: "ooapiv5",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyLoadValue",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StudyLoadValue",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "StudyLoadValue",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "StudyLoadUnit",
                schema: "ooapiv5",
                table: "Programs",
                newName: "StudyLoadDescriptor");

            migrationBuilder.RenameColumn(
                name: "StudyLoadUnit",
                schema: "ooapiv5",
                table: "EducationSpecification",
                newName: "StudyLoadDescriptor");

            migrationBuilder.RenameColumn(
                name: "StudyLoadUnit",
                schema: "ooapiv5",
                table: "Courses",
                newName: "StudyLoadDescriptor");
        }
    }
}
