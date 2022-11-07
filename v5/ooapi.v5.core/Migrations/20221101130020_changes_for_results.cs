using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changes_for_results : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropTable(
                name: "ProgramResultStudyLoad",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Program_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Course_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                newName: "StudyLoadDescriptorId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationSpecification_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                newName: "IX_EducationSpecification_StudyLoadDescriptorId");

            migrationBuilder.RenameColumn(
                name: "Year",
                schema: "ooapiv5",
                table: "AcademicSession",
                newName: "YearId");

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Program",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Course",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                schema: "ooapiv5",
                table: "AcademicSession",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                columns: table => new
                {
                    StudyLoadDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLoadDescriptor", x => x.StudyLoadDescriptorId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptor_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadDescriptorId",
                principalSchema: "ooapiv5",
                principalTable: "StudyLoadDescriptor",
                principalColumn: "StudyLoadDescriptorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptor_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropTable(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "ooapiv5",
                table: "AcademicSession");

            migrationBuilder.RenameColumn(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                newName: "StudyLoadProgramResultStudyLoadId");

            migrationBuilder.RenameIndex(
                name: "IX_EducationSpecification_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                newName: "IX_EducationSpecification_StudyLoadProgramResultStudyLoadId");

            migrationBuilder.RenameColumn(
                name: "YearId",
                schema: "ooapiv5",
                table: "AcademicSession",
                newName: "Year");

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ProgramResultStudyLoad",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramResultStudyLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramResultStudyLoad", x => x.ProgramResultStudyLoadId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Program_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program",
                column: "StudyLoadProgramResultStudyLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course",
                column: "StudyLoadProgramResultStudyLoadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course",
                column: "StudyLoadProgramResultStudyLoadId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramResultStudyLoad",
                principalColumn: "ProgramResultStudyLoadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadProgramResultStudyLoadId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramResultStudyLoad",
                principalColumn: "ProgramResultStudyLoadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program",
                column: "StudyLoadProgramResultStudyLoadId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramResultStudyLoad",
                principalColumn: "ProgramResultStudyLoadId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
