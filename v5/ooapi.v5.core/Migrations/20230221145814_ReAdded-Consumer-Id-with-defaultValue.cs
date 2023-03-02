using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ReAddedConsumerIdwithdefaultValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                newName: "EducationSpecificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                newName: "Id");
        }
    }
}
