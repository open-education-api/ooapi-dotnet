using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionLanguageTypedStrings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings");

            migrationBuilder.RenameTable(
                name: "AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                newName: "AcademicSessionNames",
                newSchema: "ooapiv5");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionNames",
                schema: "ooapiv5",
                table: "AcademicSessionNames",
                columns: new[] { "AcademicSessionId", "Language" });

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionNames_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionNames",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionNames_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionNames",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.RenameTable(
                name: "AcademicSessionNames",
                schema: "ooapiv5",
                newName: "AcademicSessionLanguageTypedStrings",
                newSchema: "ooapiv5");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings",
                columns: new[] { "AcademicSessionId", "Language" });

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionLanguageTypedStrings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionLanguageTypedStrings",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
