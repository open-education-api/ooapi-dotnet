using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ReAddedAttributeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes",
                columns: new[] { "Id", "ModelTypeName", "PropertyName", "Language" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes",
                columns: new[] { "ModelTypeName", "PropertyName", "Language" });
        }
    }
}
