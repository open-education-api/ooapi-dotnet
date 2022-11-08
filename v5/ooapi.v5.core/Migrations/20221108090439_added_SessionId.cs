using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_SessionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "ooapiv5",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Documentation = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers");
        }
    }
}
