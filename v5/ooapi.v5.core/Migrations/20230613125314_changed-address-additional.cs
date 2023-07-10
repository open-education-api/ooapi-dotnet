using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changedaddressadditional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Additional",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AddressId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_AddressId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.AddColumn<string>(
                name: "Additional",
                schema: "ooapiv5",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
