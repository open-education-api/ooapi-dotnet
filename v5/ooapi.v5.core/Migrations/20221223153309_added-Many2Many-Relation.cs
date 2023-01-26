using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedMany2ManyRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropTable(
                name: "ComponentOfferingsAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentsAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingsAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CoursesAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "OrganizationsAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferingsAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramsAddresses",
                schema: "ooapiv5");

            migrationBuilder.RenameColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Programs",
                newName: "EducationSpecificationId");

            migrationBuilder.RenameColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "ProgramOfferingOfferingId1");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_ProgramOfferingOfferingId1");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                newName: "CourseId");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Programs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Persons",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                schema: "ooapiv5",
                table: "Associations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressComponent",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentsComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressComponent", x => new { x.AddressesRefAddressId, x.ComponentsComponentId });
                    table.ForeignKey(
                        name: "FK_AddressComponent_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressComponent_Components_ComponentsComponentId",
                        column: x => x.ComponentsComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressComponentOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressComponentOffering", x => new { x.AddressesRefAddressId, x.ComponentOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_AddressComponentOffering_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressComponentOffering_ComponentOfferings_ComponentOfferingsOfferingId",
                        column: x => x.ComponentOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressCourse",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCourse", x => new { x.AddressesRefAddressId, x.CoursesCourseId });
                    table.ForeignKey(
                        name: "FK_AddressCourse_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressCourse_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressCourseOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCourseOffering", x => new { x.AddressesRefAddressId, x.CourseOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_AddressCourseOffering_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressCourseOffering_CourseOfferings_CourseOfferingsOfferingId",
                        column: x => x.CourseOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressOrganization",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationsOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressOrganization", x => new { x.AddressesRefAddressId, x.OrganizationsOrganizationId });
                    table.ForeignKey(
                        name: "FK_AddressOrganization_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressOrganization_Organizations_OrganizationsOrganizationId",
                        column: x => x.OrganizationsOrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressProgram",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramsProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressProgram", x => new { x.AddressesRefAddressId, x.ProgramsProgramId });
                    table.ForeignKey(
                        name: "FK_AddressProgram_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressProgram_Programs_ProgramsProgramId",
                        column: x => x.ProgramsProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressProgramOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressesRefAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressProgramOffering", x => new { x.AddressesRefAddressId, x.ProgramOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_AddressProgramOffering_Addresses_AddressesRefAddressId",
                        column: x => x.AddressesRefAddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressProgramOffering_ProgramOfferings_ProgramOfferingsOfferingId",
                        column: x => x.ProgramOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_CourseId",
                schema: "ooapiv5",
                table: "Programs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CourseId",
                schema: "ooapiv5",
                table: "Persons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProgramId",
                schema: "ooapiv5",
                table: "Persons",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CourseId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_GroupId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ProgramId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_RoomId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressComponent_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "ComponentsComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressComponentOffering_ComponentOfferingsOfferingId",
                schema: "ooapiv5",
                table: "AddressComponentOffering",
                column: "ComponentOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressCourse_CoursesCourseId",
                schema: "ooapiv5",
                table: "AddressCourse",
                column: "CoursesCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressCourseOffering_CourseOfferingsOfferingId",
                schema: "ooapiv5",
                table: "AddressCourseOffering",
                column: "CourseOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressOrganization_OrganizationsOrganizationId",
                schema: "ooapiv5",
                table: "AddressOrganization",
                column: "OrganizationsOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressProgram_ProgramsProgramId",
                schema: "ooapiv5",
                table: "AddressProgram",
                column: "ProgramsProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressProgramOffering_ProgramOfferingsOfferingId",
                schema: "ooapiv5",
                table: "AddressProgramOffering",
                column: "ProgramOfferingsOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_Rooms_RoomId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsFeeds_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItems",
                principalColumn: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Courses_CourseId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Groups_GroupId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Courses_CourseId",
                schema: "ooapiv5",
                table: "Persons",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Persons",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Courses_CourseId",
                schema: "ooapiv5",
                table: "Programs",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_Rooms_RoomId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsFeeds_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Courses_CourseId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Groups_GroupId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Courses_CourseId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferings_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Courses_CourseId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropTable(
                name: "AddressComponent",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressComponentOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressCourse",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressCourseOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressOrganization",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressProgram",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressProgramOffering",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Programs_CourseId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_ProgramOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CourseId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ProgramId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_CourseId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_GroupId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ProgramId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_NewsFeeds_NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropIndex(
                name: "IX_CourseOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropIndex(
                name: "IX_Costs_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropIndex(
                name: "IX_ComponentOfferings_RoomId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.RenameColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Programs",
                newName: "AcademicSessionId");

            migrationBuilder.RenameColumn(
                name: "ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                newName: "ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_ComponentOfferingOfferingId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                newName: "CourseOfferingId");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                newName: "ComponentId");

            migrationBuilder.CreateTable(
                name: "ComponentOfferingsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingsAddresses", x => new { x.ComponentOfferingId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsAddresses_ComponentOfferings_ComponentOfferingId",
                        column: x => x.ComponentOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentsAddresses", x => new { x.ComponentId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_ComponentsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentsAddresses_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingsAddresses", x => new { x.CourseOfferingId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_CourseOfferingsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseOfferingsAddresses_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CoursesAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesAddresses", x => new { x.CourseId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_CoursesAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesAddresses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationsAddresses", x => new { x.OrganizationId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_OrganizationsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationsAddresses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferingsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferingsAddresses", x => new { x.ProgramOfferingId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsAddresses_ProgramOfferings_ProgramOfferingId",
                        column: x => x.ProgramOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramsAddresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramsAddresses", x => new { x.ProgramId, x.AddressId });
                    table.ForeignKey(
                        name: "FK_ProgramsAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramsAddresses_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferingsAddresses_AddressId",
                schema: "ooapiv5",
                table: "ComponentOfferingsAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentsAddresses_AddressId",
                schema: "ooapiv5",
                table: "ComponentsAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingsAddresses_AddressId",
                schema: "ooapiv5",
                table: "CourseOfferingsAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesAddresses_AddressId",
                schema: "ooapiv5",
                table: "CoursesAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationsAddresses_AddressId",
                schema: "ooapiv5",
                table: "OrganizationsAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferingsAddresses_AddressId",
                schema: "ooapiv5",
                table: "ProgramOfferingsAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramsAddresses_AddressId",
                schema: "ooapiv5",
                table: "ProgramsAddresses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");
        }
    }
}
