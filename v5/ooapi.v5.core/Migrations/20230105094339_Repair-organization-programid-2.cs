using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Repairorganizationprogramid2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ProgramId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Organizations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ProgramId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");
        }
    }
}
