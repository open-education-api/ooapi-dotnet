using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class AddedConsumerPropertyTypeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceConsumers",
                schema: "ooapiv5",
                table: "ServiceConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomConsumers",
                schema: "ooapiv5",
                table: "RoomConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramOfferingConsumers",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramConsumers",
                schema: "ooapiv5",
                table: "ProgramConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonConsumers",
                schema: "ooapiv5",
                table: "PersonConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationConsumers",
                schema: "ooapiv5",
                table: "OrganizationConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsItemConsumers",
                schema: "ooapiv5",
                table: "NewsItemConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsFeedConsumers",
                schema: "ooapiv5",
                table: "NewsFeedConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupConsumers",
                schema: "ooapiv5",
                table: "GroupConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationConsumers",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOfferingConsumers",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseConsumers",
                schema: "ooapiv5",
                table: "CourseConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentOfferingConsumers",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentConsumers",
                schema: "ooapiv5",
                table: "ComponentConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingConsumers",
                schema: "ooapiv5",
                table: "BuildingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociationConsumers",
                schema: "ooapiv5",
                table: "AssociationConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "RoomConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "RoomConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ProgramConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ProgramConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "PersonConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "PersonConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "OrganizationConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "OrganizationConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "NewsItemConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "NewsItemConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "NewsFeedConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "NewsFeedConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "GroupConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "GroupConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "CourseConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "CourseConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ComponentConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ComponentConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "BuildingConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "BuildingConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AssociationConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "AssociationConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceConsumers",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                columns: new[] { "ServiceId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomConsumers",
                schema: "ooapiv5",
                table: "RoomConsumers",
                columns: new[] { "RoomId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramOfferingConsumers",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers",
                columns: new[] { "ProgramOfferingId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramConsumers",
                schema: "ooapiv5",
                table: "ProgramConsumers",
                columns: new[] { "ProgramId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonConsumers",
                schema: "ooapiv5",
                table: "PersonConsumers",
                columns: new[] { "PersonId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationConsumers",
                schema: "ooapiv5",
                table: "OrganizationConsumers",
                columns: new[] { "OrganizationId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsItemConsumers",
                schema: "ooapiv5",
                table: "NewsItemConsumers",
                columns: new[] { "NewsItemId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsFeedConsumers",
                schema: "ooapiv5",
                table: "NewsFeedConsumers",
                columns: new[] { "NewsFeedId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupConsumers",
                schema: "ooapiv5",
                table: "GroupConsumers",
                columns: new[] { "GroupId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationConsumers",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers",
                columns: new[] { "EducationSpecificationId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOfferingConsumers",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers",
                columns: new[] { "CourseOfferingId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseConsumers",
                schema: "ooapiv5",
                table: "CourseConsumers",
                columns: new[] { "CourseId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentOfferingConsumers",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers",
                columns: new[] { "ComponentOfferingId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentConsumers",
                schema: "ooapiv5",
                table: "ComponentConsumers",
                columns: new[] { "ComponentId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingConsumers",
                schema: "ooapiv5",
                table: "BuildingConsumers",
                columns: new[] { "BuildingId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociationConsumers",
                schema: "ooapiv5",
                table: "AssociationConsumers",
                columns: new[] { "AssociationId", "ConsumerKey", "PropertyName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                columns: new[] { "AcademicSessionId", "ConsumerKey", "PropertyName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceConsumers",
                schema: "ooapiv5",
                table: "ServiceConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomConsumers",
                schema: "ooapiv5",
                table: "RoomConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramOfferingConsumers",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramConsumers",
                schema: "ooapiv5",
                table: "ProgramConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonConsumers",
                schema: "ooapiv5",
                table: "PersonConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrganizationConsumers",
                schema: "ooapiv5",
                table: "OrganizationConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsItemConsumers",
                schema: "ooapiv5",
                table: "NewsItemConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsFeedConsumers",
                schema: "ooapiv5",
                table: "NewsFeedConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupConsumers",
                schema: "ooapiv5",
                table: "GroupConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationSpecificationConsumers",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOfferingConsumers",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseConsumers",
                schema: "ooapiv5",
                table: "CourseConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentOfferingConsumers",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentConsumers",
                schema: "ooapiv5",
                table: "ComponentConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingConsumers",
                schema: "ooapiv5",
                table: "BuildingConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssociationConsumers",
                schema: "ooapiv5",
                table: "AssociationConsumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ServiceConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "RoomConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ProgramConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "PersonConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "OrganizationConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "NewsItemConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "NewsFeedConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "GroupConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "CourseConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "ComponentConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "BuildingConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "AssociationConsumers");

            migrationBuilder.DropColumn(
                name: "PropertyType",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "RoomConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ProgramConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "PersonConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "OrganizationConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "NewsItemConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "NewsFeedConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "GroupConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "CourseConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "ComponentConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "BuildingConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AssociationConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyName",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceConsumers",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                columns: new[] { "ServiceId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomConsumers",
                schema: "ooapiv5",
                table: "RoomConsumers",
                columns: new[] { "RoomId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramOfferingConsumers",
                schema: "ooapiv5",
                table: "ProgramOfferingConsumers",
                columns: new[] { "ProgramOfferingId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramConsumers",
                schema: "ooapiv5",
                table: "ProgramConsumers",
                columns: new[] { "ProgramId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonConsumers",
                schema: "ooapiv5",
                table: "PersonConsumers",
                columns: new[] { "PersonId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrganizationConsumers",
                schema: "ooapiv5",
                table: "OrganizationConsumers",
                columns: new[] { "OrganizationId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsItemConsumers",
                schema: "ooapiv5",
                table: "NewsItemConsumers",
                columns: new[] { "NewsItemId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsFeedConsumers",
                schema: "ooapiv5",
                table: "NewsFeedConsumers",
                columns: new[] { "NewsFeedId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupConsumers",
                schema: "ooapiv5",
                table: "GroupConsumers",
                columns: new[] { "GroupId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationSpecificationConsumers",
                schema: "ooapiv5",
                table: "EducationSpecificationConsumers",
                columns: new[] { "EducationSpecificationId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOfferingConsumers",
                schema: "ooapiv5",
                table: "CourseOfferingConsumers",
                columns: new[] { "CourseOfferingId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseConsumers",
                schema: "ooapiv5",
                table: "CourseConsumers",
                columns: new[] { "CourseId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentOfferingConsumers",
                schema: "ooapiv5",
                table: "ComponentOfferingConsumers",
                columns: new[] { "ComponentOfferingId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentConsumers",
                schema: "ooapiv5",
                table: "ComponentConsumers",
                columns: new[] { "ComponentId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingConsumers",
                schema: "ooapiv5",
                table: "BuildingConsumers",
                columns: new[] { "BuildingId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssociationConsumers",
                schema: "ooapiv5",
                table: "AssociationConsumers",
                columns: new[] { "AssociationId", "ConsumerKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionConsumers",
                columns: new[] { "AcademicSessionId", "ConsumerKey" });
        }
    }
}
