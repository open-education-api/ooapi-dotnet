#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.SqlServer;

/// <inheritdoc />
public partial class RemoveDeadScaffoldEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "CourseIds");

        migrationBuilder.DropTable(
            "LearningComponentIds");

        migrationBuilder.DropTable(
            "ProgrammeIds");

        migrationBuilder.DropTable(
            "Services");

        migrationBuilder.DropTable(
            "TestComponentIds");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "CourseIds",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                CourseIdValue = table.Column<string>("nvarchar(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                IsActive = table.Column<bool>("bit", nullable: false),
                ModifiedAt = table.Column<DateTime>("datetime2", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_CourseIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "LearningComponentIds",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                ComponentIdValue = table.Column<string>("nvarchar(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                IsActive = table.Column<bool>("bit", nullable: false),
                ModifiedAt = table.Column<DateTime>("datetime2", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_LearningComponentIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "ProgrammeIds",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                IsActive = table.Column<bool>("bit", nullable: false),
                ModifiedAt = table.Column<DateTime>("datetime2", nullable: true),
                ProgrammeIdValue = table.Column<string>("nvarchar(36)", maxLength: 36, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ProgrammeIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "Services",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                ContactEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                Documentation = table.Column<string>("nvarchar(2048)", maxLength: 2048, nullable: true),
                ExtJson = table.Column<string>("nvarchar(max)", nullable: true),
                IsActive = table.Column<bool>("bit", nullable: false),
                ModifiedAt = table.Column<DateTime>("datetime2", nullable: true),
                Specification = table.Column<string>("nvarchar(2048)", maxLength: 2048, nullable: false),
                SupportedConsumersJson = table.Column<string>("nvarchar(max)", nullable: true),
                SupportedExpandsJson = table.Column<string>("nvarchar(max)", nullable: true),
                SupportedOperationsJson = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Services", x => x.Id); });

        migrationBuilder.CreateTable(
            "TestComponentIds",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                ComponentIdValue = table.Column<string>("nvarchar(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("datetime2", nullable: false),
                IsActive = table.Column<bool>("bit", nullable: false),
                ModifiedAt = table.Column<DateTime>("datetime2", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_TestComponentIds", x => x.Id); });

        migrationBuilder.CreateIndex(
            "IX_CourseIds_CourseIdValue",
            "CourseIds",
            "CourseIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_LearningComponentIds_ComponentIdValue",
            "LearningComponentIds",
            "ComponentIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ProgrammeIds_ProgrammeIdValue",
            "ProgrammeIds",
            "ProgrammeIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Services_ContactEmail",
            "Services",
            "ContactEmail",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponentIds_ComponentIdValue",
            "TestComponentIds",
            "ComponentIdValue",
            unique: true);
    }
}
