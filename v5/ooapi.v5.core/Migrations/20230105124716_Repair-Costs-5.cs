using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairCosts5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "CostId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.AddColumn<Guid>(
                name: "CostId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_CostId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CostId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Costs_CostId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CostId",
                principalSchema: "ooapiv5",
                principalTable: "Costs",
                principalColumn: "CostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Costs_CostId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_CostId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "CostId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.AddColumn<Guid>(
                name: "CostId1",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CostId1",
                principalSchema: "ooapiv5",
                principalTable: "Costs",
                principalColumn: "CostId");
        }
    }
}
