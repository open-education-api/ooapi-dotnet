using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationAttributes",
                schema: "ooapiv5",
                table: "EducationSpecificationAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAttributes",
                schema: "ooapiv5",
                table: "CourseAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingAttributes",
                schema: "ooapiv5",
                table: "BuildingAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionAttributes",
                schema: "ooapiv5",
                table: "AcademicSessionAttributes");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationAttributes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "CourseAttributes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "BuildingAttributes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AcademicSessionAttributes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationAttributes",
                schema: "ooapiv5",
                table: "EducationSpecificationAttributes",
                columns: new[] { "EducationSpecificationId", "PropertyName", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAttributes",
                schema: "ooapiv5",
                table: "CourseAttributes",
                columns: new[] { "CourseId", "PropertyName", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingAttributes",
                schema: "ooapiv5",
                table: "BuildingAttributes",
                columns: new[] { "BuildingId", "PropertyName", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionAttributes",
                schema: "ooapiv5",
                table: "AcademicSessionAttributes",
                columns: new[] { "AcademicSessionId", "PropertyName", "Language" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationAttributes",
                schema: "ooapiv5",
                table: "EducationSpecificationAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseAttributes",
                schema: "ooapiv5",
                table: "CourseAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingAttributes",
                schema: "ooapiv5",
                table: "BuildingAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionAttributes",
                schema: "ooapiv5",
                table: "AcademicSessionAttributes");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationAttributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "CourseAttributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "BuildingAttributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AcademicSessionAttributes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationAttributes",
                schema: "ooapiv5",
                table: "EducationSpecificationAttributes",
                columns: new[] { "EducationSpecificationId", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseAttributes",
                schema: "ooapiv5",
                table: "CourseAttributes",
                columns: new[] { "CourseId", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingAttributes",
                schema: "ooapiv5",
                table: "BuildingAttributes",
                columns: new[] { "BuildingId", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionAttributes",
                schema: "ooapiv5",
                table: "AcademicSessionAttributes",
                columns: new[] { "AcademicSessionId", "Language" });
        }
    }
}
