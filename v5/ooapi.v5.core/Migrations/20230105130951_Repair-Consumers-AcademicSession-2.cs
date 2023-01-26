using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumersAcademicSession2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
