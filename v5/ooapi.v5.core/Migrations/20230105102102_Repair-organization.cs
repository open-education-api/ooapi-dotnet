using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Repairorganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Courses_CourseId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Groups_GroupId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "OrganizationsPrograms",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CourseId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_GroupId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FlexibleEntryPeriodStart",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FlexibleEntryPeriodEnd",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollStartDate",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ooapiv5",
                table: "ProgramOfferings",
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
                table: "ProgramOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FlexibleEntryPeriodStart",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FlexibleEntryPeriodEnd",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EnrollStartDate",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrganizationsPrograms",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsPrograms", x => new { x.OrganizationId, x.ProgramId });
                    table.ForeignKey(
                        name: "FK_OrganizationsPrograms_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationsPrograms_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CourseId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_GroupId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsPrograms_ProgramId",
                schema: "ooapiv5",
                table: "OrganizationsPrograms",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Courses_CourseId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Groups_GroupId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }
    }
}
