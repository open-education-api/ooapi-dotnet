using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class Added_EducationSpecificationConsumer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationSpecificationConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecificationConsumers", x => new { x.EducationSpecificationId, x.ConsumerKey });
                    table.ForeignKey(
                        name: "FK_EducationSpecificationConsumers_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationSpecificationConsumers",
                schema: "ooapiv5");
        }
    }
}
