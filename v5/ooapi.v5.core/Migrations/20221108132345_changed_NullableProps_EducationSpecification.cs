using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changed_NullableProps_EducationSpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "FieldsOfStudy",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Abbreviation",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Link",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FieldsOfStudy",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(4)",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Abbreviation",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");
        }
    }
}
