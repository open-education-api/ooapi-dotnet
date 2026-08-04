using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class AddOrganisationAddressesAndRoomGeolocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Rooms",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Rooms",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressesJson",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AddressesJson",
                table: "Organisations");
        }
    }
}
