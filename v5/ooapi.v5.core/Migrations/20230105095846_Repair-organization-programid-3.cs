using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Repairorganizationprogramid3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationsPrograms",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsPrograms", x => new { x.OrganizationId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_OrganizationsPrograms_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationsPrograms_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsPrograms_ProgramId",
                schema: "ooapiv5",
                table: "OrganizationsPrograms",
                column: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationsPrograms",
                schema: "ooapiv5");
        }
    }
}
