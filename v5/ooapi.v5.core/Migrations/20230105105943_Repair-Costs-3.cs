using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairCosts3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.CreateTable(
                name: "CourseOfferingsCosts",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingsCosts", x => new { x.CourseOfferingId, x.CostId });
                    table.ForeignKey(
                        name: "FK_CourseOfferingsCosts_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseOfferingsCosts_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "CourseOfferingsCosts",
                column: "CostId");
        }
    }
}
