using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Associations_AssociationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Associations_AssociationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Buildings_BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Buildings_BuildingId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Components_ComponentId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Components_ComponentId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_CourseOfferings_CourseOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Courses_CourseId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Courses_CourseId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_EducationSpecifications_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Groups_GroupId1",
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
                name: "FK_Consumers_NewsItems_NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Organizations_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Organizations_OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Persons_PersonId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Persons_PersonId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Programs_ProgramId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Rooms_RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Services_ServiceId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AssociationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_BuildingId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ComponentId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ComponentOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_CourseId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_CourseOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_GroupId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_PersonId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ProgramId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ProgramOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ServiceId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AssociationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "CourseOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "GroupId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ProgramId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ServiceId1",
                schema: "ooapiv5",
                table: "Consumers");

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
                name: "FK_Consumers_Components_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Courses_CourseId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
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
                name: "FK_Consumers_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Consumers_Components_ComponentId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Courses_CourseId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Groups_GroupId",
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
                name: "FK_Consumers_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssociationId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Consumers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AssociationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BuildingId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ComponentId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ComponentOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_CourseId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_CourseOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_GroupId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "GroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_PersonId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ProgramId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ProgramOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ServiceId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId",
                principalSchema: "ooapiv5",
                principalTable: "AcademicSessions",
                principalColumn: "AcademicSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_AcademicSessions_AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId1",
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
                principalColumn: "AssociationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Associations_AssociationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId1",
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
                principalColumn: "BuildingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Buildings_BuildingId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId1",
                principalSchema: "ooapiv5",
                principalTable: "Buildings",
                principalColumn: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ComponentOfferings",
                principalColumn: "OfferingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Components_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Components_ComponentId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId1",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_CourseOfferings_CourseOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "CourseOfferings",
                principalColumn: "OfferingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Courses_CourseId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Courses_CourseId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId1",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_EducationSpecifications_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_EducationSpecifications_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId1",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecifications",
                principalColumn: "EducationSpecificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Groups_GroupId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "GroupId",
                principalSchema: "ooapiv5",
                principalTable: "Groups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Groups_GroupId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "GroupId1",
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
                principalColumn: "NewsFeedId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_NewsItems_NewsItemId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsItemId",
                principalSchema: "ooapiv5",
                principalTable: "NewsItems",
                principalColumn: "NewsItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_NewsItems_NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsItemId1",
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
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Organizations_OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "OrganizationId1",
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
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Persons_PersonId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "PersonId1",
                principalSchema: "ooapiv5",
                principalTable: "Persons",
                principalColumn: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramOfferingId",
                principalSchema: "ooapiv5",
                principalTable: "ProgramOfferings",
                principalColumn: "OfferingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Programs_ProgramId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId",
                principalSchema: "ooapiv5",
                principalTable: "Programs",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Programs_ProgramId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId1",
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
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Services_ServiceId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId1",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }
    }
}
