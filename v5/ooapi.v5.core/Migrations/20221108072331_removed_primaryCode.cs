using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class removed_primaryCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "PrimaryCodes",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Programs_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Persons_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Groups_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_EducationSpecification_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Components_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PrimaryCodes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryCodes", x => x.PrimaryCodeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecification_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "PrimaryCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessions_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Components_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
