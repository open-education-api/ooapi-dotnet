using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_ext_to_group_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_ext",
                schema: "ooapiv5",
                table: "Groups",
                newName: "Ext");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ext",
                schema: "ooapiv5",
                table: "Groups",
                newName: "_ext");
        }
    }
}
