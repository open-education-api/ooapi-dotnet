using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class RenameQualificationLevelsToDesignationsJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationLevels",
                table: "ProgrammeTimelineOverrides");

            migrationBuilder.DropColumn(
                name: "QualificationLevels",
                table: "Programmes");

            migrationBuilder.AddColumn<string>(
                name: "QualificationDesignationsJson",
                table: "ProgrammeTimelineOverrides",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationDesignationsJson",
                table: "Programmes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QualificationDesignationsJson",
                table: "ProgrammeTimelineOverrides");

            migrationBuilder.DropColumn(
                name: "QualificationDesignationsJson",
                table: "Programmes");

            migrationBuilder.AddColumn<string>(
                name: "QualificationLevels",
                table: "ProgrammeTimelineOverrides",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationLevels",
                table: "Programmes",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
