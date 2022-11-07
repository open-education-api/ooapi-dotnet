using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class changes_for_ListLanguageTypedString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_AcademicSession_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Address_AddressId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Building_BuildingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Building_BuildingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId2",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_ComponentOffering_ComponentOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Course_CourseId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Course_CourseId4",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Course_CourseId5",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_CourseOffering_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_EducationSpecification_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Groups_GroupId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Groups_GroupId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_NewsFeed_NewsFeedId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_NewsFeed_NewsFeedId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_NewsItem_NewsItemId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Offering_OfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Offering_OfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Organization_OrganizationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Program_ProgramId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Program_ProgramId4",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Program_ProgramId5",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_ProgramOffering_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Room_RoomId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropForeignKey(
                name: "FK_LanguageTypedString_Room_RoomId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_AddressId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_BuildingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_BuildingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ComponentId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ComponentId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ComponentId2",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ComponentId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ComponentOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_CourseId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_CourseId4",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_CourseId5",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_EducationSpecificationId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_GroupId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_GroupId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_NewsFeedId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_NewsFeedId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_NewsItemId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_OfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_OfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_OrganizationId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_OrganizationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ProgramId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ProgramId4",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ProgramId5",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_RoomId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropIndex(
                name: "IX_LanguageTypedString_RoomId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ComponentId2",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ComponentId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "CourseId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "CourseId4",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "CourseId5",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "NewsFeedId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "NewsItemId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "OfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ProgramId3",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ProgramId4",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ProgramId5",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                schema: "ooapiv5",
                table: "LanguageTypedString");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "ProgramOffering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "ProgramOffering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Program",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Program",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Program",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Program",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Program",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Program",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Organization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Organization",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Offering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Offering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "ooapiv5",
                table: "NewsItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "NewsFeed",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsFeed",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "EducationSpecification",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "CourseOffering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "CourseOffering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DisplayAmount",
                schema: "ooapiv5",
                table: "Cost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "ComponentOffering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "ComponentOffering",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Component",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Component",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Component",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Component",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "ooapiv5",
                table: "Building",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "Building",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Additional",
                schema: "ooapiv5",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSession",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "ProgramOffering");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "ProgramOffering");

            migrationBuilder.DropColumn(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Organization");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Offering");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Offering");

            migrationBuilder.DropColumn(
                name: "Content",
                schema: "ooapiv5",
                table: "NewsItem");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsItem");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "NewsFeed");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "NewsFeed");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "EducationSpecification");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "CourseOffering");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "CourseOffering");

            migrationBuilder.DropColumn(
                name: "AdmissionRequirements",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "QualificationRequirements",
                schema: "ooapiv5",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "DisplayAmount",
                schema: "ooapiv5",
                table: "Cost");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "ComponentOffering");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "ComponentOffering");

            migrationBuilder.DropColumn(
                name: "Assessment",
                schema: "ooapiv5",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                schema: "ooapiv5",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Component");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "ooapiv5",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "Building");

            migrationBuilder.DropColumn(
                name: "Additional",
                schema: "ooapiv5",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "ooapiv5",
                table: "AcademicSession");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId2",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId4",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId5",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsFeedId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId4",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId5",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_AddressId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_BuildingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_BuildingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "BuildingId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ComponentId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ComponentId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ComponentId2",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId2");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ComponentId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId3");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ComponentOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentOfferingOfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId3");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseId4",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId4");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseId5",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId5");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseOfferingOfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_EducationSpecificationId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "EducationSpecificationId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_GroupId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_GroupId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "GroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_NewsFeedId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_NewsFeedId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsFeedId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_NewsItemId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_OfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_OfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_OrganizationId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_OrganizationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId3");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramId4",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId4");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramId5",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId5");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramOfferingOfferingId1");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_RoomId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_RoomId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_AcademicSession_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSession",
                principalColumn: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Address_AddressId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AddressId",
                principalSchema: "ooapiv5",
                principalTable: "Address",
                principalColumn: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Building_BuildingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "BuildingId",
                principalSchema: "ooapiv5",
                principalTable: "Building",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Building_BuildingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "BuildingId1",
                principalSchema: "ooapiv5",
                principalTable: "Building",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId1",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId2",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId2",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Component_ComponentId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentId3",
                principalSchema: "ooapiv5",
                principalTable: "Component",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_ComponentOffering_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_ComponentOffering_ComponentOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ComponentOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Course_CourseId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId3",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Course_CourseId4",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId4",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Course_CourseId5",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseId5",
                principalSchema: "ooapiv5",
                principalTable: "Course",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_CourseOffering_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_CourseOffering_CourseOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "CourseOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "CourseOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_EducationSpecification_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "EducationSpecificationId1",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Groups_GroupId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Groups_GroupId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "GroupId1",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_NewsFeed_NewsFeedId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsFeedId",
                principalSchema: "ooapiv5",
                principalTable: "NewsFeed",
                principalColumn: "NewsFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_NewsFeed_NewsFeedId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsFeedId1",
                principalSchema: "ooapiv5",
                principalTable: "NewsFeed",
                principalColumn: "NewsFeedId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_NewsItem_NewsItemId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsItemId1",
                principalSchema: "ooapiv5",
                principalTable: "NewsItem",
                principalColumn: "NewsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Offering_OfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OfferingId",
                principalSchema: "ooapiv5",
                principalTable: "Offering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Offering_OfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "Offering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Organization_OrganizationId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OrganizationId",
                principalSchema: "ooapiv5",
                principalTable: "Organization",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Organization_OrganizationId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "OrganizationId1",
                principalSchema: "ooapiv5",
                principalTable: "Organization",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Program_ProgramId3",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId3",
                principalSchema: "ooapiv5",
                principalTable: "Program",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Program_ProgramId4",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId4",
                principalSchema: "ooapiv5",
                principalTable: "Program",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Program_ProgramId5",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramId5",
                principalSchema: "ooapiv5",
                principalTable: "Program",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_ProgramOffering_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramOfferingOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_ProgramOffering_ProgramOfferingOfferingId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "ProgramOfferingOfferingId1",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOffering",
                principalColumn: "OfferingId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Room_RoomId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "RoomId",
                principalSchema: "ooapiv5",
                principalTable: "Room",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageTypedString_Room_RoomId1",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "RoomId1",
                principalSchema: "ooapiv5",
                principalTable: "Room",
                principalColumn: "RoomId");
        }
    }
}
