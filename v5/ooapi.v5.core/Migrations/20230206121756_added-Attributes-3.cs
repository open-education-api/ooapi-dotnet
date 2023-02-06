using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Content",
                schema: "ooapiv5",
                table: "NewsItems");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsItems");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.CreateTable(
                name: "ComponentAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentAttributes", x => new { x.ComponentId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_ComponentAttributes_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferingAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingAttributes", x => new { x.ComponentOfferingId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingAttributes_ComponentOfferings_ComponentOfferingId",
                        column: x => x.ComponentOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingAttributes", x => new { x.CourseOfferingId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_CourseOfferingAttributes_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAttributes", x => new { x.GroupId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_GroupAttributes_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeedAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedAttributes", x => new { x.NewsFeedId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_NewsFeedAttributes_NewsFeeds_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsItemAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemAttributes", x => new { x.NewsItemId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_NewsItemAttributes_NewsItems_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationAttributes", x => new { x.OrganizationId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_OrganizationAttributes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramAttributes", x => new { x.ProgramId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_ProgramAttributes_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferingAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferingAttributes", x => new { x.ProgramOfferingId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_ProgramOfferingAttributes_ProgramOfferings_ProgramOfferingId",
                        column: x => x.ProgramOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferingAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "GroupAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeedAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItemAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "OrganizationAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferingAttributes",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Programs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "ooapiv5",
                table: "NewsItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "NewsFeeds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsFeeds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
