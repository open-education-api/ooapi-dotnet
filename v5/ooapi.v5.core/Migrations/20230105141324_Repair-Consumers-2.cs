using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class RepairConsumers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consumers_Associations_AssociationId",
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
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_AssociationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ComponentId",
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
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_GroupId",
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
                name: "IX_Consumers_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_PersonId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropIndex(
                name: "IX_Consumers_ProgramId",
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionsConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers");

            migrationBuilder.DropIndex(
                name: "IX_AcademicSessionsConsumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ComponentId",
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
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "GroupId",
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
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "ProgramId",
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
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers");

            migrationBuilder.DropColumn(
                name: "AcadamicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionsConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                columns: new[] { "AcademicSessionId", "ConsumerId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionsConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers");

            migrationBuilder.AddColumn<Guid>(
                name: "AssociationId",
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
                name: "CourseOfferingOfferingId",
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
                name: "OrganizationId",
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
                name: "ProgramId",
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
                name: "ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AcadamicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionsConsumers",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                columns: new[] { "AcadamicSessionId", "ConsumerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AssociationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId");

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
                name: "IX_Consumers_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_GroupId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "GroupId");

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
                name: "IX_Consumers_OrganizationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_PersonId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ProgramId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ProgramId");

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
                name: "IX_AcademicSessionsConsumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "AcademicSessionsConsumers",
                column: "AcademicSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumers_Associations_AssociationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId",
                principalSchema: "ooapiv5",
                principalTable: "Associations",
                principalColumn: "AssociationId");

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
                name: "FK_Consumers_Services_ServiceId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ServiceId",
                principalSchema: "ooapiv5",
                principalTable: "Services",
                principalColumn: "ServiceId");
        }
    }
}
