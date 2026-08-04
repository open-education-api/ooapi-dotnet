using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer
{
    /// <inheritdoc />
    public partial class RemoveStudyLoadFromLearningTestComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudyLoadJson",
                table: "TestComponents");

            migrationBuilder.DropColumn(
                name: "StudyLoadJson",
                table: "LearningComponents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudyLoadJson",
                table: "TestComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudyLoadJson",
                table: "LearningComponents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
