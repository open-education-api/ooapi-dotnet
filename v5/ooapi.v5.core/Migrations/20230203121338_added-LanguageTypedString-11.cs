using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "BuildingNames",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "BuildingDescriptions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BuildingNames_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingNames",
                column: "BuildingId1");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingDescriptions_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingDescriptions",
                column: "BuildingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingDescriptions_Buildings_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingDescriptions",
                column: "BuildingId1",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingNames_Buildings_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingNames",
                column: "BuildingId1",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BuildingDescriptions_Buildings_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingNames_Buildings_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingNames");

            migrationBuilder.DropIndex(
                name: "IX_BuildingNames_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingNames");

            migrationBuilder.DropIndex(
                name: "IX_BuildingDescriptions_BuildingId1",
                schema: "ooapiv5",
                table: "BuildingDescriptions");

            migrationBuilder.DropColumn(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "BuildingNames");

            migrationBuilder.DropColumn(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "BuildingDescriptions");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
