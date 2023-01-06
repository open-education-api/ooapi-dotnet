using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairCosts4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "CostId1");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_CostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CostId1",
                principalSchema: "ooapiv5",
                principalTable: "Costs",
                principalColumn: "CostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.RenameColumn(
                name: "CostId1",
                schema: "ooapiv5",
                table: "Costs",
                newName: "CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_CostId1",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_CourseOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");
        }
    }
}
