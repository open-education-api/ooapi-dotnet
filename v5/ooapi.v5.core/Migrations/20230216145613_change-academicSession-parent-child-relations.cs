using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changeacademicSessionparentchildrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessions_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "YearId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");
        }
    }
}
