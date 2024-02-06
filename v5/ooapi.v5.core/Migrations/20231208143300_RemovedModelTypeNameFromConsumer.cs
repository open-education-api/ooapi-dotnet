using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    /// <inheritdoc />
    public partial class RemovedModelTypeNameFromConsumer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ModelTypeName",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "Id", "ConsumerKey", "PropertyName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AddColumn<string>(
                name: "ModelTypeName",
                schema: "ooapiv5",
                table: "Consumers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "Id", "ModelTypeName", "ConsumerKey", "PropertyName" });
        }
    }
}
