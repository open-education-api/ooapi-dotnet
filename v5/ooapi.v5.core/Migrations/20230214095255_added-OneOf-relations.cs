using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedOneOfrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProgramResultId",
                schema: "ooapiv5",
                table: "Associations",
                newName: "ResultId");

            migrationBuilder.RenameColumn(
                name: "CourseResultId",
                schema: "ooapiv5",
                table: "Associations",
                newName: "ProgramResultResultId");

            migrationBuilder.RenameColumn(
                name: "ComponentResultId",
                schema: "ooapiv5",
                table: "Associations",
                newName: "ProgramOfferingOfferingId");

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ComponentResults",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentResults", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "CourseResults",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResults", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "ProgramResults",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramResults", x => x.ResultId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrganizationId",
                schema: "ooapiv5",
                table: "Groups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Courses",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrganizationId",
                schema: "ooapiv5",
                table: "Courses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_CourseId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "ProgramOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_OrganizationId",
                schema: "ooapiv5",
                table: "Components",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_CourseId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ComponentResultResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "CourseResultResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_PersonId",
                schema: "ooapiv5",
                table: "Associations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ProgramResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ProgramResultResultId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessions_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "YearId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_ComponentResults_ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ComponentResultResultId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentResults",
                principalColumn: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_CourseResults_CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "CourseResultResultId",
                principalSchema: "ooapiv5",
                principalTable: "CourseResults",
                principalColumn: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Persons_PersonId",
                schema: "ooapiv5",
                table: "Associations",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_ProgramResults_ProgramResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ProgramResultResultId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramResults",
                principalColumn: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_Components_ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_Courses_CourseId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Components",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Courses_CourseId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_ProgramOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "ProgramOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Courses",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Courses",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Groups",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ParentId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferings_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferings_Programs_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_ComponentResults_ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_CourseResults_CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Persons_PersonId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_ProgramResults_ProgramResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_Components_ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_Courses_CourseId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Courses_CourseId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_ProgramOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferings_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferings_Programs_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "ComponentResults",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseResults",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramResults",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BuildingId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfferings_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ParentId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Groups_OrganizationId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Courses_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OrganizationId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_CourseId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Components_OrganizationId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_CourseId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Associations_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Associations_ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Associations_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Associations_CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Associations_PersonId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Associations_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Associations_ProgramResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessions_YearId",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropColumn(
                name: "ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropColumn(
                name: "CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.RenameColumn(
                name: "ResultId",
                schema: "ooapiv5",
                table: "Associations",
                newName: "ProgramResultId");

            migrationBuilder.RenameColumn(
                name: "ProgramResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                newName: "CourseResultId");

            migrationBuilder.RenameColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                newName: "ComponentResultId");
        }
    }
}
