using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Changedtest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Courses_CourseId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Courses_CourseId",
                schema: "ooapiv5",
                table: "Components",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Courses_CourseId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.AlterColumn<Guid>(
                name: "AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Courses_CourseId",
                schema: "ooapiv5",
                table: "Components",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
