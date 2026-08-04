using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class AddProgrammeTypeModeOfStudyModesOfDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ModeOfStudy",
                table: "Programmes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModesOfDeliveryJson",
                table: "Programmes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgrammeType",
                table: "Programmes",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeOfStudy",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "ModesOfDeliveryJson",
                table: "Programmes");

            migrationBuilder.DropColumn(
                name: "ProgrammeType",
                table: "Programmes");
        }
    }
}
