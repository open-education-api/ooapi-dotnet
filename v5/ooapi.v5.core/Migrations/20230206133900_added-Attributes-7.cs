using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class addedAttributes7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Components_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Courses_CourseId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceConsumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "ServiceConsumers");

            migrationBuilder.DropTable(
                name: "AcademicSessionConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AssociationConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "BuildingConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferingConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferingConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecificationConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "GroupConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeedConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItemConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "OrganizationConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "PersonConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferingConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "RoomConsumers",
                schema: "ooapiv5");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CourseId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceConsumers",
                schema: "ooapiv5",
                table: "ServiceConsumers");

            migrationBuilder.DropColumn(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                schema: "ooapiv5",
                table: "Attributes");

            migrationBuilder.RenameTable(
                name: "ServiceConsumers",
                schema: "ooapiv5",
                newName: "Consumers",
                newSchema: "ooapiv5");

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ModelTypeName",
                schema: "ooapiv5",
                table: "Consumers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssociationId",
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
                name: "BuildingId",
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
                name: "ComponentId",
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
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
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

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
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
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
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
                name: "NewsItemId",
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
                name: "OrganizationId",
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
                name: "PersonId",
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
                name: "ProgramId",
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
                name: "ProgramOfferingOfferingId",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers",
                columns: new[] { "Id", "ModelTypeName", "ConsumerKey", "PropertyName" });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AssociationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AssociationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BuildingId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId");

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
                name: "IX_Consumers_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_CourseId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId");

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
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_GroupId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "GroupId");

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
                name: "IX_Consumers_NewsItemId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "NewsItemId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "OrganizationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_PersonId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_PersonId1",
                schema: "ooapiv5",
                table: "Consumers",
                column: "PersonId1");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ProgramId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId");

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
                name: "IX_Consumers_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_Consumers_ComponentOfferings_ComponentOfferingOfferingId",
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
                name: "FK_Consumers_CourseOfferings_CourseOfferingOfferingId",
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
                name: "FK_Consumers_ProgramOfferings_ProgramOfferingOfferingId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumers",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AssociationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AssociationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_BuildingId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ComponentId",
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
                name: "IX_Consumers_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_CourseId",
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
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_GroupId",
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
                name: "IX_Consumers_NewsItemId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_PersonId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_PersonId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ProgramId",
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
                name: "IX_Consumers_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ServiceId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ModelTypeName",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AssociationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "BuildingId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ComponentId",
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
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "CourseId",
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
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "GroupId",
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
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "NewsItemId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "OrganizationId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ProgramId",
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
                name: "ProgramOfferingOfferingId",
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

            migrationBuilder.RenameTable(
                name: "Consumers",
                schema: "ooapiv5",
                newName: "ServiceConsumers",
                newSchema: "ooapiv5");

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId1",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId1",
                schema: "ooapiv5",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ServiceId",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceConsumers",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                columns: new[] { "ServiceId", "ConsumerKey", "PropertyName" });

            migrationBuilder.CreateTable(
                name: "AcademicSessionConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessionConsumers", x => new { x.AcademicSessionId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_AcademicSessionConsumers_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssociationConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssociationConsumers", x => new { x.AssociationId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_AssociationConsumers_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Associations",
                        principalColumn: "AssociationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingConsumers", x => new { x.BuildingId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_BuildingConsumers_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentConsumers", x => new { x.ComponentId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_ComponentConsumers_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferingConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingConsumers", x => new { x.ComponentOfferingId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingConsumers_ComponentOfferings_ComponentOfferingId",
                        column: x => x.ComponentOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseConsumers", x => new { x.CourseId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_CourseConsumers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingConsumers", x => new { x.CourseOfferingId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_CourseOfferingConsumers_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSpecificationConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecificationConsumers", x => new { x.EducationSpecificationId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_EducationSpecificationConsumers_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupConsumers", x => new { x.GroupId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_GroupConsumers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeedConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedConsumers", x => new { x.NewsFeedId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_NewsFeedConsumers_NewsFeeds_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsItemConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItemConsumers", x => new { x.NewsItemId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_NewsItemConsumers_NewsItems_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationConsumers", x => new { x.OrganizationId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_OrganizationConsumers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonConsumers", x => new { x.PersonId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_PersonConsumers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramConsumers", x => new { x.ProgramId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_ProgramConsumers_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferingConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferingConsumers", x => new { x.ProgramOfferingId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_ProgramOfferingConsumers_ProgramOfferings_ProgramOfferingId",
                        column: x => x.ProgramOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomConsumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomConsumers", x => new { x.RoomId, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_RoomConsumers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Rooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CourseId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Components_ComponentId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentId1",
                principalSchema: "ooapiv5",
                principalTable: "Components",
                principalColumn: "ComponentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Courses_CourseId1",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseId1",
                principalSchema: "ooapiv5",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceConsumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "ServiceConsumers",
                column: "ServiceId",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
