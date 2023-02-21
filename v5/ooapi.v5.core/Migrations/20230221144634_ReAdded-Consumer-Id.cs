using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ReAddedConsumerId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.RenameColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "Id", "ModelTypeName", "ConsumerKey", "PropertyName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                newName: "EducationSpecificationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "ModelTypeName", "ConsumerKey", "PropertyName" });
        }
    }
}
