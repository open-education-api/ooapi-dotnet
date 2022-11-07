using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_Author_And_Resource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                schema: "ooapiv5",
                table: "Resource",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Resource",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                schema: "ooapiv5",
                table: "Author",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Author",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                schema: "ooapiv5",
                table: "Resource",
                column: "ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                schema: "ooapiv5",
                table: "Author",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_CourseId",
                schema: "ooapiv5",
                table: "Resource",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_NewsItemId",
                schema: "ooapiv5",
                table: "Author",
                column: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Author_NewsItem_NewsItemId",
                schema: "ooapiv5",
                table: "Author",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItem",
                principalColumn: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Course_CourseId",
                schema: "ooapiv5",
                table: "Resource",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Author_NewsItem_NewsItemId",
                schema: "ooapiv5",
                table: "Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Course_CourseId",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Resource_CourseId",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                schema: "ooapiv5",
                table: "Author");

            migrationBuilder.DropIndex(
                name: "IX_Author_NewsItemId",
                schema: "ooapiv5",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                schema: "ooapiv5",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Author");
        }
    }
}
