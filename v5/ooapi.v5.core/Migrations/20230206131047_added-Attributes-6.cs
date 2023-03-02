using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CourseId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Components_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentId1",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Courses_CourseId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseId1",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Components_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Courses_CourseId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CourseId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                schema: "ooapiv5",
                table: "Attributes");
        }
    }
}
