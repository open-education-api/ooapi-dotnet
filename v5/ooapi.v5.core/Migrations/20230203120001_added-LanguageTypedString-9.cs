using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionNames_AcademicSessions_AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_AcademicSessionNames_AcademicSessionNamesAcademicSessionId_AcademicSessionNamesLanguage",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_AcademicSessionNamesAcademicSessionId_AcademicSessionNamesLanguage",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessionNames_AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.DropColumn(
                name: "AcademicSessionNamesAcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "AcademicSessionNamesLanguage",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

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

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionNamesAcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AcademicSessionNamesLanguage",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_AcademicSessionNamesAcademicSessionId_AcademicSessionNamesLanguage",
                schema: "ooapiv5",
                table: "AcademicSessions",
                columns: new[] { "AcademicSessionNamesAcademicSessionId", "AcademicSessionNamesLanguage" },
                unique: true,
                filter: "[AcademicSessionNamesAcademicSessionId] IS NOT NULL AND [AcademicSessionNamesLanguage] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessionNames_AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames",
                column: "AcademicSessionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionNames_AcademicSessions_AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames",
                column: "AcademicSessionId1",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessions_AcademicSessionNames_AcademicSessionNamesAcademicSessionId_AcademicSessionNamesLanguage",
                schema: "ooapiv5",
                table: "AcademicSessions",
                columns: new[] { "AcademicSessionNamesAcademicSessionId", "AcademicSessionNamesLanguage" },
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessionNames",
                principalColumns: new[] { "AcademicSessionId", "Language" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
