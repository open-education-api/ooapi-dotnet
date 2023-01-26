using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changed_StudyLoadDescriptor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptors_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropTable(
                name: "StudyLoadDescriptors",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_EducationSpecification_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "Courses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudyLoadDescriptors",
                schema: "ooapiv5",
                columns: table => new
                {
                    StudyLoadDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLoadDescriptors", x => x.StudyLoadDescriptorId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecification_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadDescriptorId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptors_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadDescriptorId",
                principalSchema: "ooapiv5",
                principalTable: "StudyLoadDescriptors",
                principalColumn: "StudyLoadDescriptorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
