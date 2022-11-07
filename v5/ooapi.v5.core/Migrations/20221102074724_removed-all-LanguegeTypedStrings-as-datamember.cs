using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class removedallLanguegeTypedStringsasdatamember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSession_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSession");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Component_ComponentId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Course_CourseId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Geolocation_GeolocationId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Program_ProgramId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_Address_AddressId",
                schema: "ooapiv5",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOffering_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_AcademicSession_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Association_AssociationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Building_BuildingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Component_ComponentId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Course_CourseId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_NewsFeed_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_NewsItem_NewsItemId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Offering_OfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Person_PersonId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Program_ProgramId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumer_Room_RoomId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropForeignKey(
                name: "FK_Cost_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Cost_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Cost_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOffering_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptor_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageOfChoice_Person_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Organization_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organization");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_AcademicSession_AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Building_BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Component_ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Course_CourseId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Offering_OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Person_PersonId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Program_ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Room_RoomId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Address_AddressId",
                schema: "ooapiv5",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Program_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOffering_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOffering");

            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Course_CourseId",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomGeolocation_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Author",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "LanguageTypedString",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "List<LanguageTypedString>",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "List<string>",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Offering",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "StudyLoadDescriptor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomGeolocation",
                schema: "ooapiv5",
                table: "RoomGeolocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                schema: "ooapiv5",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resource",
                schema: "ooapiv5",
                table: "Resource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramOffering",
                schema: "ooapiv5",
                table: "ProgramOffering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Program",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrimaryCode",
                schema: "ooapiv5",
                table: "PrimaryCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                schema: "ooapiv5",
                table: "Person");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                schema: "ooapiv5",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsItem",
                schema: "ooapiv5",
                table: "NewsItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsFeed",
                schema: "ooapiv5",
                table: "NewsFeed");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageOfChoice",
                schema: "ooapiv5",
                table: "LanguageOfChoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Geolocation",
                schema: "ooapiv5",
                table: "Geolocation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOffering",
                schema: "ooapiv5",
                table: "CourseOffering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cost",
                schema: "ooapiv5",
                table: "Cost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumer",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_OfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentOffering",
                schema: "ooapiv5",
                table: "ComponentOffering");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Component",
                schema: "ooapiv5",
                table: "Component");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Building",
                schema: "ooapiv5",
                table: "Building");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Association",
                schema: "ooapiv5",
                table: "Association");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSession",
                schema: "ooapiv5",
                table: "AcademicSession");

            migrationBuilder.DropColumn(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.RenameTable(
                name: "StudyLoadDescriptor",
                schema: "ooapiv5",
                newName: "StudyLoadDescriptors",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "RoomGeolocation",
                schema: "ooapiv5",
                newName: "RoomGeolocations",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Room",
                schema: "ooapiv5",
                newName: "Rooms",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Resource",
                schema: "ooapiv5",
                newName: "Resources",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "ProgramOffering",
                schema: "ooapiv5",
                newName: "ProgramOfferings",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Program",
                schema: "ooapiv5",
                newName: "Programs",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "PrimaryCode",
                schema: "ooapiv5",
                newName: "PrimaryCodes",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Person",
                schema: "ooapiv5",
                newName: "Persons",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Organization",
                schema: "ooapiv5",
                newName: "Organizations",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "NewsItem",
                schema: "ooapiv5",
                newName: "NewsItems",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "NewsFeed",
                schema: "ooapiv5",
                newName: "NewsFeeds",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "LanguageOfChoice",
                schema: "ooapiv5",
                newName: "LanguageOfChoices",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Geolocation",
                schema: "ooapiv5",
                newName: "Geolocations",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "CourseOffering",
                schema: "ooapiv5",
                newName: "CourseOfferings",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Course",
                schema: "ooapiv5",
                newName: "Courses",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Cost",
                schema: "ooapiv5",
                newName: "Costs",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Consumer",
                schema: "ooapiv5",
                newName: "Consumers",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "ComponentOffering",
                schema: "ooapiv5",
                newName: "ComponentOfferings",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Component",
                schema: "ooapiv5",
                newName: "Components",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Building",
                schema: "ooapiv5",
                newName: "Buildings",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Association",
                schema: "ooapiv5",
                newName: "Associations",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Address",
                schema: "ooapiv5",
                newName: "Addresses",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "AcademicSession",
                schema: "ooapiv5",
                newName: "AcademicSessions",
                newSchema: "ooapiv5");

            migrationBuilder.RenameIndex(
                name: "IX_Room_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms",
                newName: "IX_Rooms_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                newName: "IX_Rooms_GeolocationRoomGeolocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Resource_CourseId",
                schema: "ooapiv5",
                table: "Resources",
                newName: "IX_Resources_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramOffering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                newName: "IX_ProgramOfferings_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Program_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs",
                newName: "IX_Programs_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons",
                newName: "IX_Persons_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Person_AddressId",
                schema: "ooapiv5",
                table: "Persons",
                newName: "IX_Persons_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Organization_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations",
                newName: "IX_Organizations_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageOfChoice_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoices",
                newName: "IX_LanguageOfChoices_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOffering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                newName: "IX_CourseOfferings_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses",
                newName: "IX_Courses_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_ProgramOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Cost_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                newName: "IX_Costs_ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_ProgramOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_ProgramId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_PersonId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_NewsItemId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_NewsItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_NewsFeedId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_GroupId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_EducationSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_CourseId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_AssociationId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_AssociationId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumer_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                newName: "IX_Consumers_AcademicSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentOffering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                newName: "IX_ComponentOfferings_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Component_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components",
                newName: "IX_Components_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Building_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings",
                newName: "IX_Buildings_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Building_AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                newName: "IX_Buildings_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_ProgramOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ProgramId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_GeolocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CourseId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Address_ComponentId",
                schema: "ooapiv5",
                table: "Addresses",
                newName: "IX_Addresses_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSession_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                newName: "IX_AcademicSessions_PrimaryCodeId");

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                schema: "ooapiv5",
                table: "NewsItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyLoadDescriptors",
                schema: "ooapiv5",
                table: "StudyLoadDescriptors",
                column: "StudyLoadDescriptorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomGeolocations",
                schema: "ooapiv5",
                table: "RoomGeolocations",
                column: "RoomGeolocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                schema: "ooapiv5",
                table: "Rooms",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                schema: "ooapiv5",
                table: "Resources",
                column: "ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramOfferings",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "OfferingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Programs",
                schema: "ooapiv5",
                table: "Programs",
                column: "ProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrimaryCodes",
                schema: "ooapiv5",
                table: "PrimaryCodes",
                column: "PrimaryCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                schema: "ooapiv5",
                table: "Persons",
                column: "PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                schema: "ooapiv5",
                table: "Organizations",
                column: "OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsItems",
                schema: "ooapiv5",
                table: "NewsItems",
                column: "NewsItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsFeeds",
                schema: "ooapiv5",
                table: "NewsFeeds",
                column: "NewsFeedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageOfChoices",
                schema: "ooapiv5",
                table: "LanguageOfChoices",
                column: "LanguageOfChoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Geolocations",
                schema: "ooapiv5",
                table: "Geolocations",
                column: "GeolocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOfferings",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "OfferingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                schema: "ooapiv5",
                table: "Courses",
                column: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Costs",
                schema: "ooapiv5",
                table: "Costs",
                column: "CostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ConsumerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentOfferings",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "OfferingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Components",
                schema: "ooapiv5",
                table: "Components",
                column: "ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buildings",
                schema: "ooapiv5",
                table: "Buildings",
                column: "BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Associations",
                schema: "ooapiv5",
                table: "Associations",
                column: "AssociationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                schema: "ooapiv5",
                table: "Addresses",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessions",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessions_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Addresses_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "GeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "Geolocations",
                principalColumn: "GeolocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Components_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Associations_AssociationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId",
                principalSchema: "ooapiv5",
                principalTable: "Associations",
                principalColumn: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Components_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Courses_CourseId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_NewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsFeedId",
                principalSchema: "ooapiv5",
                principalTable: "NewsFeeds",
                principalColumn: "NewsFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItems",
                principalColumn: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Persons_PersonId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Rooms_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptors_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadDescriptorId",
                principalSchema: "ooapiv5",
                principalTable: "StudyLoadDescriptors",
                principalColumn: "StudyLoadDescriptorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageOfChoices_Persons_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoices",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Components_ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Courses_CourseId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organizations",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Persons_PersonId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Programs_ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Rooms_RoomId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Persons",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Courses_CourseId",
                schema: "ooapiv5",
                table: "Resources",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCodes",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomGeolocations_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "GeolocationRoomGeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "RoomGeolocations",
                principalColumn: "RoomGeolocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessions_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions");

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
                name: "FK_Addresses_Geolocations_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Organizations_OrganizationId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Associations_AssociationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Components_ComponentId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Courses_CourseId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_NewsFeeds_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Persons_PersonId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Rooms_RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptors_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageOfChoices_Persons_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_ComponentOfferings_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Components_ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_CourseOfferings_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Courses_CourseId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Persons_PersonId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_ProgramOfferings_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Programs_ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_OtherCodes_Rooms_RoomId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramOfferings_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Courses_CourseId",
                schema: "ooapiv5",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_PrimaryCodes_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomGeolocations_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyLoadDescriptors",
                schema: "ooapiv5",
                table: "StudyLoadDescriptors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                schema: "ooapiv5",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomGeolocations",
                schema: "ooapiv5",
                table: "RoomGeolocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                schema: "ooapiv5",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Programs",
                schema: "ooapiv5",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramOfferings",
                schema: "ooapiv5",
                table: "ProgramOfferings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrimaryCodes",
                schema: "ooapiv5",
                table: "PrimaryCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                schema: "ooapiv5",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                schema: "ooapiv5",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsItems",
                schema: "ooapiv5",
                table: "NewsItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsFeeds",
                schema: "ooapiv5",
                table: "NewsFeeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LanguageOfChoices",
                schema: "ooapiv5",
                table: "LanguageOfChoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Geolocations",
                schema: "ooapiv5",
                table: "Geolocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                schema: "ooapiv5",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOfferings",
                schema: "ooapiv5",
                table: "CourseOfferings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Costs",
                schema: "ooapiv5",
                table: "Costs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Components",
                schema: "ooapiv5",
                table: "Components");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComponentOfferings",
                schema: "ooapiv5",
                table: "ComponentOfferings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buildings",
                schema: "ooapiv5",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Associations",
                schema: "ooapiv5",
                table: "Associations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                schema: "ooapiv5",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessions",
                schema: "ooapiv5",
                table: "AcademicSessions");

            migrationBuilder.DropColumn(
                name: "Authors",
                schema: "ooapiv5",
                table: "NewsItems");

            migrationBuilder.RenameTable(
                name: "StudyLoadDescriptors",
                schema: "ooapiv5",
                newName: "StudyLoadDescriptor",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Rooms",
                schema: "ooapiv5",
                newName: "Room",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "RoomGeolocations",
                schema: "ooapiv5",
                newName: "RoomGeolocation",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Resources",
                schema: "ooapiv5",
                newName: "Resource",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Programs",
                schema: "ooapiv5",
                newName: "Program",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "ProgramOfferings",
                schema: "ooapiv5",
                newName: "ProgramOffering",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "PrimaryCodes",
                schema: "ooapiv5",
                newName: "PrimaryCode",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Persons",
                schema: "ooapiv5",
                newName: "Person",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Organizations",
                schema: "ooapiv5",
                newName: "Organization",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "NewsItems",
                schema: "ooapiv5",
                newName: "NewsItem",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "NewsFeeds",
                schema: "ooapiv5",
                newName: "NewsFeed",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "LanguageOfChoices",
                schema: "ooapiv5",
                newName: "LanguageOfChoice",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Geolocations",
                schema: "ooapiv5",
                newName: "Geolocation",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "ooapiv5",
                newName: "Course",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "CourseOfferings",
                schema: "ooapiv5",
                newName: "CourseOffering",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Costs",
                schema: "ooapiv5",
                newName: "Cost",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Consumers",
                schema: "ooapiv5",
                newName: "Consumer",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Components",
                schema: "ooapiv5",
                newName: "Component",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "ComponentOfferings",
                schema: "ooapiv5",
                newName: "ComponentOffering",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Buildings",
                schema: "ooapiv5",
                newName: "Building",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Associations",
                schema: "ooapiv5",
                newName: "Association",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "Addresses",
                schema: "ooapiv5",
                newName: "Address",
                newSchema: "ooapiv5");

            migrationBuilder.RenameTable(
                name: "AcademicSessions",
                schema: "ooapiv5",
                newName: "AcademicSession",
                newSchema: "ooapiv5");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Room",
                newName: "IX_Room_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Room",
                newName: "IX_Room_GeolocationRoomGeolocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_CourseId",
                schema: "ooapiv5",
                table: "Resource",
                newName: "IX_Resource_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Programs_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Program",
                newName: "IX_Program_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOffering",
                newName: "IX_ProgramOffering_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Person",
                newName: "IX_Person_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_AddressId",
                schema: "ooapiv5",
                table: "Person",
                newName: "IX_Person_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Organizations_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organization",
                newName: "IX_Organization_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_LanguageOfChoices_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoice",
                newName: "IX_LanguageOfChoice_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Course",
                newName: "IX_Course_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOffering",
                newName: "IX_CourseOffering_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                newName: "IX_Cost_ProgramOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                newName: "IX_Cost_CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Costs_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                newName: "IX_Cost_ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_ProgramOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_ProgramId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_PersonId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_OrganizationId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_NewsItemId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_NewsItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_NewsFeedId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_GroupId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_EducationSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_CourseId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_ComponentId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_BuildingId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_AssociationId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_AssociationId");

            migrationBuilder.RenameIndex(
                name: "IX_Consumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer",
                newName: "IX_Consumer_AcademicSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Components_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Component",
                newName: "IX_Component_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_ComponentOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOffering",
                newName: "IX_ComponentOffering_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Building",
                newName: "IX_Building_PrimaryCodeId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_AddressId",
                schema: "ooapiv5",
                table: "Building",
                newName: "IX_Building_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_ProgramOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ProgramId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_GeolocationId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_GeolocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_CourseOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CourseId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_ComponentOfferingOfferingId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_ComponentId",
                schema: "ooapiv5",
                table: "Address",
                newName: "IX_Address_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSessions_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSession",
                newName: "IX_AcademicSession_PrimaryCodeId");

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyLoadDescriptor",
                schema: "ooapiv5",
                table: "StudyLoadDescriptor",
                column: "StudyLoadDescriptorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                schema: "ooapiv5",
                table: "Room",
                column: "RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomGeolocation",
                schema: "ooapiv5",
                table: "RoomGeolocation",
                column: "RoomGeolocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resource",
                schema: "ooapiv5",
                table: "Resource",
                column: "ResourceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Program",
                schema: "ooapiv5",
                table: "Program",
                column: "ProgramId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramOffering",
                schema: "ooapiv5",
                table: "ProgramOffering",
                column: "OfferingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrimaryCode",
                schema: "ooapiv5",
                table: "PrimaryCode",
                column: "PrimaryCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                schema: "ooapiv5",
                table: "Person",
                column: "PersonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                schema: "ooapiv5",
                table: "Organization",
                column: "OrganizationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsItem",
                schema: "ooapiv5",
                table: "NewsItem",
                column: "NewsItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsFeed",
                schema: "ooapiv5",
                table: "NewsFeed",
                column: "NewsFeedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LanguageOfChoice",
                schema: "ooapiv5",
                table: "LanguageOfChoice",
                column: "LanguageOfChoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Geolocation",
                schema: "ooapiv5",
                table: "Geolocation",
                column: "GeolocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                schema: "ooapiv5",
                table: "Course",
                column: "CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOffering",
                schema: "ooapiv5",
                table: "CourseOffering",
                column: "OfferingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cost",
                schema: "ooapiv5",
                table: "Cost",
                column: "CostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumer",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ConsumerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Component",
                schema: "ooapiv5",
                table: "Component",
                column: "ComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComponentOffering",
                schema: "ooapiv5",
                table: "ComponentOffering",
                column: "OfferingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Building",
                schema: "ooapiv5",
                table: "Building",
                column: "BuildingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Association",
                schema: "ooapiv5",
                table: "Association",
                column: "AssociationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                schema: "ooapiv5",
                table: "Address",
                column: "AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSession",
                schema: "ooapiv5",
                table: "AcademicSession",
                column: "AcademicSessionId");

            migrationBuilder.CreateTable(
                name: "Author",
                schema: "ooapiv5",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Author_NewsItem_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItem",
                        principalColumn: "NewsItemId");
                });

            migrationBuilder.CreateTable(
                name: "LanguageTypedString",
                schema: "ooapiv5",
                columns: table => new
                {
                    LanguageTypedStringId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypedString", x => x.LanguageTypedStringId);
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Cost_CostId",
                        column: x => x.CostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Cost",
                        principalColumn: "CostId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Course_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Course_CourseId1",
                        column: x => x.CourseId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Course_CourseId2",
                        column: x => x.CourseId2,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_NewsItem_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItem",
                        principalColumn: "NewsItemId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Program_ProgramId1",
                        column: x => x.ProgramId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Program_ProgramId2",
                        column: x => x.ProgramId2,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                });

            migrationBuilder.CreateTable(
                name: "List<LanguageTypedString>",
                schema: "ooapiv5",
                columns: table => new
                {
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "List<string>",
                schema: "ooapiv5",
                columns: table => new
                {
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Offering",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: true),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offering", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_Offering_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_OfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Author_NewsItemId",
                schema: "ooapiv5",
                table: "Author",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CostId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseId2",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId2");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_NewsItemId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramId2",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId2");

            migrationBuilder.CreateIndex(
                name: "IX_Offering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Offering",
                column: "PrimaryCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSession_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSession",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Component_ComponentId",
                schema: "ooapiv5",
                table: "Address",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Course_CourseId",
                schema: "ooapiv5",
                table: "Address",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Geolocation_GeolocationId",
                schema: "ooapiv5",
                table: "Address",
                column: "GeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "Geolocation",
                principalColumn: "GeolocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "Address",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organization",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Program_ProgramId",
                schema: "ooapiv5",
                table: "Address",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Program",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Building_Address_AddressId",
                schema: "ooapiv5",
                table: "Building",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Building_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Building",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Component",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentOffering_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOffering",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_AcademicSession_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSession",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Association_AssociationId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "AssociationId",
                principalSchema: "ooapiv5",
                principalTable: "Association",
                principalColumn: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Building_BuildingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Building",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Component_ComponentId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Course_CourseId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_NewsFeed_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "NewsFeedId",
                principalSchema: "ooapiv5",
                principalTable: "NewsFeed",
                principalColumn: "NewsFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_NewsItem_NewsItemId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItem",
                principalColumn: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Offering_OfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "OfferingId",
                principalSchema: "ooapiv5",
                principalTable: "Offering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organization",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Person_PersonId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Program_ProgramId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Program",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumer_Room_RoomId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Room",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cost_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Course",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOffering_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOffering",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationSpecification_StudyLoadDescriptor_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadDescriptorId",
                principalSchema: "ooapiv5",
                principalTable: "StudyLoadDescriptor",
                principalColumn: "StudyLoadDescriptorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageOfChoice_Person_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoice",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organization_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organization",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_AcademicSession_AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSession",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Building_BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Building",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Component_ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Course_CourseId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Offering_OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OfferingId",
                principalSchema: "ooapiv5",
                principalTable: "Offering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organization",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Person_PersonId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "PersonId",
                principalSchema: "ooapiv5",
                principalTable: "Person",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Program_ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Program",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OtherCodes_Room_RoomId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Room",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Address_AddressId",
                schema: "ooapiv5",
                table: "Person",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Person",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Program_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Program",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramOffering_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOffering",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Course_CourseId",
                schema: "ooapiv5",
                table: "Resource",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Room",
                column: "PrimaryCodeId",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "PrimaryCodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomGeolocation_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Room",
                column: "GeolocationRoomGeolocationId",
                principalSchema: "ooapiv5",
                principalTable: "RoomGeolocation",
                principalColumn: "RoomGeolocationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
