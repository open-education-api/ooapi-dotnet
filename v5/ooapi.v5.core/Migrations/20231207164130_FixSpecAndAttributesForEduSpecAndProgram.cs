using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    /// <inheritdoc />
    public partial class FixSpecAndAttributesForEduSpecAndProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProgramType",
                schema: "ooapiv5",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue:"[]");

            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                type: "nvarchar(max)",
                nullable: false, 
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Attributes",
                schema: "ooapiv5",
                table: "EducationSpecifications");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramType",
                schema: "ooapiv5",
                table: "Programs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryCodeType",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PrimaryCode",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
