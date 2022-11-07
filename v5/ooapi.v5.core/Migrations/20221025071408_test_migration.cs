using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class test_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrimaryCode",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryCode", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Organization = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Group_PrimaryCode_PrimaryCodeCode",
                        column: x => x.PrimaryCodeCode,
                        principalTable: "PrimaryCode",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumer",
                columns: table => new
                {
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumer", x => x.ConsumerKey);
                    table.ForeignKey(
                        name: "FK_Consumer_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateTable(
                name: "OtherCodes",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherCodes", x => x.Code);
                    table.ForeignKey(
                        name: "FK_OtherCodes_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "GroupId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_GroupId",
                table: "Consumer",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_PrimaryCodeCode",
                table: "Group",
                column: "PrimaryCodeCode");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_GroupId",
                table: "OtherCodes",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumer");

            migrationBuilder.DropTable(
                name: "OtherCodes");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "PrimaryCode");
        }
    }
}
