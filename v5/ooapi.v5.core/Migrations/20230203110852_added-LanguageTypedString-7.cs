using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                columns: new[] { "EducationSpecificationId", "Language" });

            migrationBuilder.CreateTable(
                name: "BuildingDescriptions",
                schema: "ooapiv5",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingDescriptions", x => new { x.BuildingId, x.Language });
                    table.ForeignKey(
                        name: "FK_BuildingDescriptions_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingNames",
                schema: "ooapiv5",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingNames", x => new { x.BuildingId, x.Language });
                    table.ForeignKey(
                        name: "FK_BuildingNames_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionNames_AcademicSessions_AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.DropTable(
                name: "BuildingDescriptions",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "BuildingNames",
                schema: "ooapiv5");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessionNames_AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "AcademicSessionNames");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                columns: new[] { "EducationSpecificationId", "PropertyName", "Language" });
        }
    }
}
