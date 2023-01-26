using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumersAcademicSession3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_Consumers_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AcademicSessionsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    acadamicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionsConsumers", x => new { x.acadamicSessionId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_AcademicSessionsConsumers_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSessionsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessionsConsumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessionsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                column: "ConsumerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Consumers_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "ConsumerId",
                principalSchema: "ooapiv5",
                principalTable: "Consumers",
                principalColumn: "ConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Consumers_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "AcademicSessionsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");
        }
    }
}
