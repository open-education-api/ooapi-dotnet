using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairprogramofferingCosts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferingsCosts_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferingsCosts_Programs_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfferingsCosts_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                newName: "ProgramOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferingsCosts_ProgramOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "ProgramOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferingsCosts_ProgramOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts");

            migrationBuilder.RenameColumn(
                name: "ProgramOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                newName: "ProgramId");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferingsCosts_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferingsCosts_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferingsCosts_Programs_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
