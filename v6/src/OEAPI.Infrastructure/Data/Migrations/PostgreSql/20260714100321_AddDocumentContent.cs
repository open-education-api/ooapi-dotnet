#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql;

/// <inheritdoc />
public partial class AddDocumentContent : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<byte[]>(
            "Content",
            "Documents",
            "bytea",
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
