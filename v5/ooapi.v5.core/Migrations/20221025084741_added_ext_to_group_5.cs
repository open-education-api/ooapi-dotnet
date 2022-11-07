using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_ext_to_group_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extend",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "TTTT",
                schema: "ooapiv5",
                table: "Groups",
                newName: "Extension");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Extension",
                schema: "ooapiv5",
                table: "Groups",
                newName: "TTTT");

            migrationBuilder.AddColumn<string>(
                name: "Extend",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
