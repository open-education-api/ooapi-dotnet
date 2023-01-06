using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairprogramofferingCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.CreateTable(
                name: "ProgramOfferingsCosts",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferingsCosts", x => new { x.ProgramId, x.CostId });
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsCosts_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsCosts_ProgramOfferings_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsCosts_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferingsCosts_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "ProgramOfferingOfferingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgramOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");
        }
    }
}
