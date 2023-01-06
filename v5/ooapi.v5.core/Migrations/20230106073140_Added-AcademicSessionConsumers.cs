using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedAcademicSessionConsumers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsFeeds_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropTable(
                name: "LanguageTypedString",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.CreateTable(
                name: "AcademicSessionConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionConsumers", x => new { x.RefId, x.ConsumerKey });
                });

            migrationBuilder.CreateTable(
                name: "ConsumerRegistrations",
                schema: "ooapiv5",
                columns: table => new
                {
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerRegistrations", x => x.ConsumerKey);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicSessionConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ConsumerRegistrations",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItemsNewsFeeds",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LanguageTypedString",
                schema: "ooapiv5",
                columns: table => new
                {
                    LanguageTypedStringId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypedString", x => x.LanguageTypedStringId);
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AcademicSessionId");

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
