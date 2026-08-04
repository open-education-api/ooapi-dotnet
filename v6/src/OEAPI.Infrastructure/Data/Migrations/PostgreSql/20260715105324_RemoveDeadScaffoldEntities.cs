#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql;

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
                Id = table.Column<Guid>("uuid", nullable: false),
                CourseIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_CourseIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "LearningComponentIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ComponentIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_LearningComponentIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "ProgrammeIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ProgrammeIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ProgrammeIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "Services",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ContactEmail = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                Documentation = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                Specification = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: false),
                SupportedConsumersJson = table.Column<string>("text", nullable: true),
                SupportedExpandsJson = table.Column<string>("text", nullable: true),
                SupportedOperationsJson = table.Column<string>("text", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Services", x => x.Id); });

        migrationBuilder.CreateTable(
            "TestComponentIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ComponentIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true)
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
