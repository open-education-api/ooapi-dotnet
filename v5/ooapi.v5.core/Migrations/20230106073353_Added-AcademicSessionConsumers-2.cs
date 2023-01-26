using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedAcademicSessionConsumers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicSessionsConsumers",
                schema: "ooapiv5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicSessionsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionsConsumers", x => new { x.AcademicSessionId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_AcademicSessionsConsumers_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicSessionsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessionsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                column: "ConsumerId");
        }
    }
}
