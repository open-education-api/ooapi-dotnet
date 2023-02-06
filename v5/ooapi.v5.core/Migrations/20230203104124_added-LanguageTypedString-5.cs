using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings",
                columns: new[] { "AcademicSessionId", "Language" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings",
                columns: new[] { "AcademicSessionId", "PropertyName", "Language" });
        }
    }
}
