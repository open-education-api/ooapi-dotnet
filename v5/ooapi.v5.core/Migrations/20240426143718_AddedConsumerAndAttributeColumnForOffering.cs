using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    /// <inheritdoc />
    public partial class AddedConsumerAndAttributeColumnForOffering : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consumers",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consumers",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Consumers",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "Consumers",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "Attributes",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Consumers",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Attributes",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "Consumers",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Consumers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");
        }
    }
}
