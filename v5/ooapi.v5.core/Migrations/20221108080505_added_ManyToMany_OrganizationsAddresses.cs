using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_ManyToMany_OrganizationsAddresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Additional",
                schema: "ooapiv5",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "OrganizationsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsAddresses", x => new { x.OrganizationId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_OrganizationsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationsAddresses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsAddresses_AddressId",
                schema: "ooapiv5",
                table: "OrganizationsAddresses",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationsAddresses",
                schema: "ooapiv5");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Additional",
                schema: "ooapiv5",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");
        }
    }
}
