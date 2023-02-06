using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicSessionAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "BuildingAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferingAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingAttributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecificationAttributes",
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

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_BuildingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_GroupId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "NewsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_NewsItemId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_OrganizationId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ProgramId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Groups_GroupId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "NewsFeedId",
                principalSchema: "ooapiv5",
                principalTable: "NewsFeeds",
                principalColumn: "NewsFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItems",
                principalColumn: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Groups_GroupId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_BuildingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_GroupId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_NewsItemId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_OrganizationId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ProgramId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.CreateTable(
                name: "AcademicSessionAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionAttributes", x => new { x.AcademicSessionId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_AcademicSessionAttributes_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingAttributes", x => new { x.BuildingId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_BuildingAttributes_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferingAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "EducationSpecificationAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecificationAttributes", x => new { x.EducationSpecificationId, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_EducationSpecificationAttributes_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupAttributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
    }
}
