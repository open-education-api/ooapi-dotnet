using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ChangesConsumerRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerRegistrations_ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumerRegistrations_Services_ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations",
                column: "ServiceId",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumerRegistrations_Services_ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_ConsumerRegistrations_ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
