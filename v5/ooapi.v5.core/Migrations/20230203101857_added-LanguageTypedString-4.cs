using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionLanguageTypedStrings", x => new { x.AcademicSessionId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_AcademicSessionLanguageTypedStrings_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicSessionLanguageTypedStrings",
                schema: "ooapiv5");
        }
    }
}
