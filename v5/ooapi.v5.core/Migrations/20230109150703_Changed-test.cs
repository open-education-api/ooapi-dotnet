using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Changedtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Components_CourseId",
                schema: "ooapiv5",
                table: "Components",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Courses_CourseId",
                schema: "ooapiv5",
                table: "Components",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Courses_CourseId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_CourseId",
                schema: "ooapiv5",
                table: "Components");
        }
    }
}
