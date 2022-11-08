using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class removed_geolocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Geolocations",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "GeolocationId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "GeolocationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                schema: "ooapiv5",
                table: "Rooms",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                schema: "ooapiv5",
                table: "Rooms",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                schema: "ooapiv5",
                table: "Addresses",
                type: "decimal(8,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                schema: "ooapiv5",
                table: "Addresses",
                type: "decimal(8,6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GeolocationId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Geolocations",
                schema: "ooapiv5",
                columns: table => new
                {
                    GeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.GeolocationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "GeolocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "GeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "Geolocations",
                principalColumn: "GeolocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "GeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "Geolocations",
                principalColumn: "GeolocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
