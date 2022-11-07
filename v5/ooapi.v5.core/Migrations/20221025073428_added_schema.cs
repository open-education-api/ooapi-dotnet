using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Group_GroupId",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_PrimaryCode_PrimaryCodeCode",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Group_GroupId",
                table: "OtherCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.EnsureSchema(
                name: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "PrimaryCode",
                newName: "PrimaryCode",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "OtherCodes",
                newName: "OtherCodes",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Consumer",
                newName: "Consumer",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups",
                newSchema: "ooapiv5");

            migrationBuilder.RenameIndex(
                name: "IX_Group_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups",
                newName: "IX_Groups_PrimaryCodeCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                schema: "ooapiv5",
                table: "Groups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_PrimaryCode_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeCode",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Groups_GroupId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_PrimaryCode_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Groups_GroupId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "PrimaryCode",
                schema: "ooapiv5",
                newName: "PrimaryCode");

            migrationBuilder.RenameTable(
                name: "OtherCodes",
                schema: "ooapiv5",
                newName: "OtherCodes");

            migrationBuilder.RenameTable(
                name: "Consumer",
                schema: "ooapiv5",
                newName: "Consumer");

            migrationBuilder.RenameTable(
                name: "Groups",
                schema: "ooapiv5",
                newName: "Group");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_PrimaryCodeCode",
                table: "Group",
                newName: "IX_Group_PrimaryCodeCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Group_GroupId",
                table: "Consumer",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_PrimaryCode_PrimaryCodeCode",
                table: "Group",
                column: "PrimaryCodeCode",
                principalTable: "PrimaryCode",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Group_GroupId",
                table: "OtherCodes",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId");
        }
    }
}
