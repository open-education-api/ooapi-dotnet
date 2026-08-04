#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer;

/// <inheritdoc />
public partial class AddDocumentContent : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<byte[]>(
            "Content",
            "Documents",
            "varbinary(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "Content",
            "Documents");
    }
}
