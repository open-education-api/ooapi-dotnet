using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changemoreparentchildrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecifications_EducationSpecifications_ParentId",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_EducationSpecifications_ParentId",
                schema: "ooapiv5",
                table: "EducationSpecifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Programs_ParentId",
                schema: "ooapiv5",
                table: "Programs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ParentId");

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
                name: "FK_Organizations_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ParentId",
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
    }
}
