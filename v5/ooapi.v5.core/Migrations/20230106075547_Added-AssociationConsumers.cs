using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedAssociationConsumers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationsConsumers",
                schema: "ooapiv5");

            migrationBuilder.CreateTable(
                name: "AssociationConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationConsumers", x => new { x.AssociationId, x.ConsumerKey });
                    table.ForeignKey(
                        name: "FK_AssociationConsumers_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Associations",
                        principalColumn: "AssociationId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssociationConsumers",
                schema: "ooapiv5");

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

            migrationBuilder.CreateIndex(
                name: "IX_AssociationsConsumers_ConsumerId",
                schema: "ooapiv5",
                table: "AssociationsConsumers",
                column: "ConsumerId");
        }
    }
}
