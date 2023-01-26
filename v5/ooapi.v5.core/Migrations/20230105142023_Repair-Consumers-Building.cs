using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumersBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingsConsumers", x => new { x.BuildingId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_BuildingsConsumers_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuildingsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuildingsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "BuildingsConsumers",
                column: "ConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingsConsumers",
                schema: "ooapiv5");
        }
    }
}
