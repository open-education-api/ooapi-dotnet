#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer;

/// <inheritdoc />
public partial class AddMembershipState : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "State",
            "Memberships",
            "nvarchar(64)",
            maxLength: 64,
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "State",
            "Memberships");
    }
}
