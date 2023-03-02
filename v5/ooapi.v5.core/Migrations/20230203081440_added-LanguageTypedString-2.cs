using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedLanguageTypedString2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EducationSpecificationLanguageTypedStrings_EducationSpecificationId",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.DropColumn(
                name: "LanguageTypedKey",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                columns: new[] { "EducationSpecificationId", "PropertyName", "Language" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationLanguageTypedStrings",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings");

            migrationBuilder.AlterColumn<string>(
                name: "Language",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "LanguageTypedKey",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecificationLanguageTypedStrings_EducationSpecificationId",
                schema: "ooapiv5",
                table: "EducationSpecificationLanguageTypedStrings",
                column: "EducationSpecificationId");
        }
    }
}
