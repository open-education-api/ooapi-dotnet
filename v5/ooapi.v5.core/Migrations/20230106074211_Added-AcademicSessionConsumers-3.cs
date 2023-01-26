using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedAcademicSessionConsumers3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                newName: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers");

            migrationBuilder.RenameColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                newName: "RefId");
        }
    }
}
