using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairCosts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
