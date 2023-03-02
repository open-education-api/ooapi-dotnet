using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedExpandsToProgramsAndEducationspecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecifications_OrganizationId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecifications_ParentId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecifications_EducationSpecifications_ParentId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                column: "ParentId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecifications_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecifications_EducationSpecifications_ParentId",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecifications_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_EducationSpecifications_OrganizationId",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_EducationSpecifications_ParentId",
                schema: "ooapiv5",
                table: "EducationSpecifications");
        }
    }
}
