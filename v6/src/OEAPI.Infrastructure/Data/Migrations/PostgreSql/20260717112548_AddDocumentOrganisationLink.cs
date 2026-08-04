using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class AddDocumentOrganisationLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganisationEntityId",
                table: "Documents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OrganisationEntityId",
                table: "Documents",
                column: "OrganisationEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Organisations_OrganisationEntityId",
                table: "Documents",
                column: "OrganisationEntityId",
                principalTable: "Organisations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Organisations_OrganisationEntityId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_OrganisationEntityId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "OrganisationEntityId",
                table: "Documents");
        }
    }
}
