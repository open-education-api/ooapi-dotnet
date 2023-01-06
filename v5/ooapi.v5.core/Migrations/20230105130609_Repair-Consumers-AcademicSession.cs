using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumersAcademicSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "ConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessions_Consumers_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "ConsumerId",
                principalSchema: "ooapiv5",
                principalTable: "Consumers",
                principalColumn: "ConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_Consumers_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");
        }
    }
}
