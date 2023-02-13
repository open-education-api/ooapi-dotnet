using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedOrganizationtoProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Programs_OrganizationId",
                schema: "ooapiv5",
                table: "Programs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Programs",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs",
                column: "ParentId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_OrganizationId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs");
        }
    }
}
