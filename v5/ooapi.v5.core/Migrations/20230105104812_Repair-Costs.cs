using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComponentOfferingsCosts",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingsCosts", x => new { x.ComponentOfferingId, x.CostId });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsCosts_ComponentOfferings_ComponentOfferingId",
                        column: x => x.ComponentOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsCosts_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "ComponentOfferingsCosts",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "CourseOfferingsCosts",
                column: "CostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropTable(
                name: "ComponentOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings");
        }
    }
}
