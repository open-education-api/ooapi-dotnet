using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changed_EducationSpecification_to_EducationSpecifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecification",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.RenameTable(
                name: "EducationSpecification",
                schema: "ooapiv5",
                newName: "EducationSpecifications",
                newSchema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "RefId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecifications",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                column: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecifications",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.DropColumn(
                name: "RefId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.RenameTable(
                name: "EducationSpecifications",
                schema: "ooapiv5",
                newName: "EducationSpecification",
                newSchema: "ooapiv5");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecification",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");
        }
    }
}
