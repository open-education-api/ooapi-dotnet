using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changedcostsforcourseoffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollStartDate",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollStartDate",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollStartDate",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollStartDate",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");
        }
    }
}
