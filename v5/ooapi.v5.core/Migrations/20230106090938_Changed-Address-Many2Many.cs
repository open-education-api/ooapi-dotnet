using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ChangedAddressMany2Many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponent_Components_ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponentOffering_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressComponentOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressCourse_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressCourseOffering_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressCourseOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressOrganization_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressProgram_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressProgramOffering_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressProgramOffering");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressProgramOffering",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressProgram",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressOrganization",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressCourseOffering",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressCourse",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressComponentOffering",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "ComponentsComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressComponent_ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "IX_AddressComponent_ComponentsComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponent_Components_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "ComponentsComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponentOffering_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressComponentOffering",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressCourse_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressCourse",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressCourseOffering_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressCourseOffering",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressOrganization_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressOrganization",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressProgram_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressProgram",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressProgramOffering_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressProgramOffering",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponent_Components_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressComponentOffering_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressComponentOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressCourse_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressCourseOffering_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressCourseOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressOrganization_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressProgram_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_AddressProgramOffering_Addresses_AddressId",
                schema: "ooapiv5",
                table: "AddressProgramOffering");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressProgramOffering",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressProgram",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressOrganization",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressCourseOffering",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressCourse",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "AddressComponentOffering",
                newName: "AddressesRefAddressId");

            migrationBuilder.RenameColumn(
                name: "ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "ComponentsRefComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_AddressComponent_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                newName: "IX_AddressComponent_ComponentsRefComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponent_Components_ComponentsRefComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "ComponentsRefComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressComponentOffering_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressComponentOffering",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressCourse_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressCourse",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressCourseOffering_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressCourseOffering",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressOrganization_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressOrganization",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressProgram_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressProgram",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AddressProgramOffering_Addresses_AddressesRefAddressId",
                schema: "ooapiv5",
                table: "AddressProgramOffering",
                column: "AddressesRefAddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
