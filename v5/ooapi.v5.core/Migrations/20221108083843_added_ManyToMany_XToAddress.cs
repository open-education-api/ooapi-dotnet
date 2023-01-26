using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_ManyToMany_XToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Components_ComponentId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Courses_CourseId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ComponentId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CourseId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ProgramId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses");

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
                name: "IX_ProgramOfferingsAddresses_AddressId",
                schema: "ooapiv5",
                table: "ProgramOfferingsAddresses",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramsAddresses_AddressId",
                schema: "ooapiv5",
                table: "ProgramsAddresses",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "ProgramOfferingsAddresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramsAddresses",
                schema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ComponentId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CourseId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProgramId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Components_ComponentId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Courses_CourseId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");
        }
    }
}
