using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_ext_to_group_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "testje",
                schema: "ooapiv5",
                table: "Groups",
                newName: "TTTT");

            migrationBuilder.RenameColumn(
                name: "Ext",
                schema: "ooapiv5",
                table: "Groups",
                newName: "Extend");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TTTT",
                schema: "ooapiv5",
                table: "Groups",
                newName: "testje");

            migrationBuilder.RenameColumn(
                name: "Extend",
                schema: "ooapiv5",
                table: "Groups",
                newName: "Ext");
        }
    }
}
