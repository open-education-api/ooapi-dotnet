using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Changed_Many2Many_Costs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferingsCosts",
                schema: "ooapiv5");

            migrationBuilder.CreateTable(
                name: "ComponentOfferingCost",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostsCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingCost", x => new { x.ComponentOfferingsOfferingId, x.CostsCostId });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingCost_ComponentOfferings_ComponentOfferingsOfferingId",
                        column: x => x.ComponentOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOfferingCost_Costs_CostsCostId",
                        column: x => x.CostsCostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostCourseOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    CostsCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCourseOffering", x => new { x.CostsCostId, x.CourseOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_CostCourseOffering_Costs_CostsCostId",
                        column: x => x.CostsCostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostCourseOffering_CourseOfferings_CourseOfferingsOfferingId",
                        column: x => x.CourseOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostProgramOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    CostsCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostProgramOffering", x => new { x.CostsCostId, x.ProgramOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_CostProgramOffering_Costs_CostsCostId",
                        column: x => x.CostsCostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostProgramOffering_ProgramOfferings_ProgramOfferingsOfferingId",
                        column: x => x.ProgramOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferingCost_CostsCostId",
                schema: "ooapiv5",
                table: "ComponentOfferingCost",
                column: "CostsCostId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCourseOffering_CourseOfferingsOfferingId",
                schema: "ooapiv5",
                table: "CostCourseOffering",
                column: "CourseOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CostProgramOffering_ProgramOfferingsOfferingId",
                schema: "ooapiv5",
                table: "CostProgramOffering",
                column: "ProgramOfferingsOfferingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentOfferingCost",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CostCourseOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CostProgramOffering",
                schema: "ooapiv5");

            migrationBuilder.CreateTable(
                name: "ComponentOfferingsCosts",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingsCosts", x => new { x.ComponentOfferingId, x.CostId });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsCosts_ComponentOfferings_ComponentOfferingId",
                        column: x => x.ComponentOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOfferingsCosts_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingsCosts",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingsCosts", x => new { x.CourseOfferingId, x.CostId });
                    table.ForeignKey(
                        name: "FK_CourseOfferingsCosts_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseOfferingsCosts_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferingsCosts",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferingsCosts", x => new { x.ProgramOfferingId, x.CostId });
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsCosts_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgramOfferingsCosts_ProgramOfferings_ProgramOfferingId",
                        column: x => x.ProgramOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "ComponentOfferingsCosts",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "CourseOfferingsCosts",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferingsCosts_CostId",
                schema: "ooapiv5",
                table: "ProgramOfferingsCosts",
                column: "CostId");
        }
    }
}
