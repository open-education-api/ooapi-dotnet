using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class AddProgrammeQualificationAwarded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationJson",
                table: "Programmes");

            migrationBuilder.AddColumn<string>(
                name: "QualificationAwarded",
                table: "Programmes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationAwarded",
                table: "Programmes");

            migrationBuilder.AddColumn<string>(
                name: "QualificationJson",
                table: "Programmes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
