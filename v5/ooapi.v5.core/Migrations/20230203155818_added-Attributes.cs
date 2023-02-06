using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicSessionNames",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "BuildingDescriptions",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "BuildingNames",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "AcademicSessionAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionAttributes", x => new { x.AcademicSessionId, x.Language });
                    table.ForeignKey(
                        name: "FK_AcademicSessionAttributes_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingAttributes", x => new { x.BuildingId, x.Language });
                    table.ForeignKey(
                        name: "FK_BuildingAttributes_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseAttributes", x => new { x.CourseId, x.Language });
                    table.ForeignKey(
                        name: "FK_CourseAttributes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSpecificationAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecificationAttributes", x => new { x.EducationSpecificationId, x.Language });
                    table.ForeignKey(
                        name: "FK_EducationSpecificationAttributes_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicSessionAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "BuildingAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecificationAttributes",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AcademicSessionNames",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionNames", x => new { x.AcademicSessionId, x.Language });
                    table.ForeignKey(
                        name: "FK_AcademicSessionNames_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingDescriptions",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                columns: table => new
                {
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecificationLanguageTypedStrings", x => new { x.EducationSpecificationId, x.Language });
                    table.ForeignKey(
                        name: "FK_EducationSpecificationLanguageTypedStrings_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
