using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ChangedAddressComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponent_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponent_Components_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent");

            migrationBuilder.RenameColumn(
                name: "ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "ComponentsRefComponentId");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressComponent_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "IX_AddressComponent_ComponentsRefComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponent_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponent_Components_ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "ComponentsRefComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponent_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponent_Components_ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent");

            migrationBuilder.RenameColumn(
                name: "ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "ComponentsComponentId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressComponent_ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "IX_AddressComponent_ComponentsComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponent_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponent_Components_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "ComponentsComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
