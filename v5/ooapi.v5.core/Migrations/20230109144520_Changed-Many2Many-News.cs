using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class ChangedMany2ManyNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsItemsNewsFeeds",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                column: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsFeeds_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItems",
                principalColumn: "NewsItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsFeeds_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.CreateTable(
                name: "NewsItemsNewsFeeds",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemsNewsFeeds", x => new { x.NewsItemId, x.NewsFeedId });
                    table.ForeignKey(
                        name: "FK_NewsItemsNewsFeeds_NewsFeeds_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsItemsNewsFeeds_NewsItems_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsItemsNewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "NewsItemsNewsFeeds",
                column: "NewsFeedId");
        }
    }
}
