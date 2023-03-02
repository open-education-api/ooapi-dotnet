using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class removedConsumerIdandAttributeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsFeeds_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.AddColumn<Guid>(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "ModelTypeName", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes",
                columns: new[] { "ModelTypeName", "PropertyName", "Language" });

            migrationBuilder.CreateTable(
                name: "NewsFeedNewsItem",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedsNewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsItemsNewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedNewsItem", x => new { x.NewsFeedsNewsFeedId, x.NewsItemsNewsItemId });
                    table.ForeignKey(
                        name: "FK_NewsFeedNewsItem_NewsFeeds_NewsFeedsNewsFeedId",
                        column: x => x.NewsFeedsNewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsFeedNewsItem_NewsItems_NewsItemsNewsItemId",
                        column: x => x.NewsItemsNewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedNewsItem_NewsItemsNewsItemId",
                schema: "ooapiv5",
                table: "NewsFeedNewsItem",
                column: "NewsItemsNewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_NewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsFeedId",
                principalSchema: "ooapiv5",
                principalTable: "NewsFeeds",
                principalColumn: "NewsFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Rooms_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Rooms",
                principalColumn: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_NewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Rooms_RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropTable(
                name: "NewsFeedNewsItem",
                schema: "ooapiv5");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "Id", "ModelTypeName", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                schema: "ooapiv5",
                table: "Attributes",
                columns: new[] { "Id", "ModelTypeName", "PropertyName", "Language" });

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
    }
}
