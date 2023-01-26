using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "acadamicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                newName: "AcadamicSessionId");

            migrationBuilder.CreateTable(
                name: "AssociationsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationsConsumers", x => new { x.AssociationId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_AssociationsConsumers_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Associations",
                        principalColumn: "AssociationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssociationsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferingsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingsConsumers", x => new { x.ComponentOfferingId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsConsumers_ComponentOfferings_ComponentOfferingId",
                        column: x => x.ComponentOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentsConsumers", x => new { x.ComponentId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ComponentsConsumers_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingsConsumers", x => new { x.CourseOfferingId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_CourseOfferingsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseOfferingsConsumers_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursesConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesConsumers", x => new { x.CourseId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_CoursesConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesConsumers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsConsumers", x => new { x.GroupId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_GroupsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsConsumers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeedsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedsConsumers", x => new { x.NewsFeedId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_NewsFeedsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsFeedsConsumers_NewsFeeds_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsItemsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemsConsumers", x => new { x.NewsItemId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_NewsItemsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsItemsConsumers_NewsItems_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsConsumers", x => new { x.OrganizationId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_OrganizationsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationsConsumers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsConsumers", x => new { x.PersonId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_PersonsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsConsumers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferingsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferingsConsumers", x => new { x.ProgramOfferingId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsConsumers_ProgramOfferings_ProgramOfferingId",
                        column: x => x.ProgramOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramsConsumers", x => new { x.ProgramId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ProgramsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramsConsumers_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomsConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomsConsumers", x => new { x.RoomId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_RoomsConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomsConsumers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesConsumers", x => new { x.ServiceId, x.ConsumerId });
                    table.ForeignKey(
                        name: "FK_ServicesConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalSchema: "ooapiv5",
                        principalTable: "Consumers",
                        principalColumn: "ConsumerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesConsumers_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ooapiv5",
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssociationsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "AssociationsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferingsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "ComponentOfferingsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "ComponentsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "CourseOfferingsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "CoursesConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "GroupsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "NewsFeedsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItemsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "NewsItemsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "OrganizationsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "PersonsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferingsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "ProgramOfferingsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "ProgramsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "RoomsConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "ServicesConsumers",
                column: "ConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferingsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CoursesConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "GroupsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeedsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItemsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "OrganizationsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "PersonsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferingsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "RoomsConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ServicesConsumers",
                schema: "ooapiv5");

            migrationBuilder.RenameColumn(
                name: "AcadamicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                newName: "acadamicSessionId");
        }
    }
}
