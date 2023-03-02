using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedEducationSpecificationtoProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Programs_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Programs",
                column: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Programs",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Programs");
        }
    }
}
