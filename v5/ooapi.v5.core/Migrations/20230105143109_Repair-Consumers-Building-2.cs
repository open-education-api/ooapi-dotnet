using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumersBuilding2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Consumers_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "RefId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "Buildings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RefId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ConsumerId",
                schema: "ooapiv5",
                table: "Buildings",
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
    }
}
