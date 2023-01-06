using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedAcademicSessionConsumers4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");
        }
    }
}
