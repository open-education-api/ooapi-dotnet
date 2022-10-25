using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class added_main_models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_PrimaryCode_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrimaryCode",
                schema: "ooapiv5",
                table: "PrimaryCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherCodes",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_Groups_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumer",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ooapiv5",
                table: "PrimaryCode",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "PrimaryCode",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "OtherCodesId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ConsumerKey",
                schema: "ooapiv5",
                table: "Consumer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssociationId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                schema: "ooapiv5",
                table: "Consumer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrimaryCode",
                schema: "ooapiv5",
                table: "PrimaryCode",
                column: "PrimaryCodeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherCodes",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OtherCodesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumer",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ConsumerId");

            migrationBuilder.CreateTable(
                name: "AcademicSession",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicSessionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSession", x => x.AcademicSessionId);
                    table.ForeignKey(
                        name: "FK_AcademicSession_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Association",
                schema: "ooapiv5",
                columns: table => new
                {
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociationType = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RemoteState = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Association", x => x.AssociationId);
                });

            migrationBuilder.CreateTable(
                name: "Author",
                schema: "ooapiv5",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Component",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Course = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Organization = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_Component_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultWeight = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOffering", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_ComponentOffering_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOffering", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_CourseOffering_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Geolocation",
                schema: "ooapiv5",
                columns: table => new
                {
                    GeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocation", x => x.GeolocationId);
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
                name: "NewsFeed",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedType = table.Column<int>(type: "int", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeed", x => x.NewsFeedId);
                });

            migrationBuilder.CreateTable(
                name: "NewsItem",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsItemType = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItem", x => x.NewsItemId);
                });

            migrationBuilder.CreateTable(
                name: "Offering",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Organization",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationType = table.Column<int>(type: "int", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organization_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlexibleEntryPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOffering", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_ProgramOffering_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramResultStudyLoad",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramResultStudyLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramResultStudyLoad", x => x.ProgramResultStudyLoadId);
                });

            migrationBuilder.CreateTable(
                name: "Resource",
                schema: "ooapiv5",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "RoomGeolocation",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomGeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomGeolocation", x => x.RoomGeolocationId);
                });

            migrationBuilder.CreateTable(
                name: "Cost",
                schema: "ooapiv5",
                columns: table => new
                {
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VatAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountWithoutVat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost", x => x.CostId);
                    table.ForeignKey(
                        name: "FK_Cost_ComponentOffering_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Cost_CourseOffering_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Cost_ProgramOffering_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOffering",
                        principalColumn: "OfferingId");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StudyLoadProgramResultStudyLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                        column: x => x.StudyLoadProgramResultStudyLoadId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramResultStudyLoad",
                        principalColumn: "ProgramResultStudyLoadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSpecification",
                schema: "ooapiv5",
                columns: table => new
                {
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationSpecificationType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FormalDocument = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: true),
                    LevelOfQualification = table.Column<int>(type: "int", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    StudyLoadProgramResultStudyLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecification", x => x.EducationSpecificationId);
                    table.ForeignKey(
                        name: "FK_EducationSpecification_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationSpecification_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                        column: x => x.StudyLoadProgramResultStudyLoadId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramResultStudyLoad",
                        principalColumn: "ProgramResultStudyLoadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Program",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StudyLoadProgramResultStudyLoadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QualificationAwarded = table.Column<int>(type: "int", nullable: true),
                    ModeOfStudy = table.Column<int>(type: "int", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LevelOfQualification = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Program", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Program_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Program_ProgramResultStudyLoad_StudyLoadProgramResultStudyLoadId",
                        column: x => x.StudyLoadProgramResultStudyLoadId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramResultStudyLoad",
                        principalColumn: "ProgramResultStudyLoadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: true),
                    AvailableSeats = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeolocationRoomGeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Building = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Room_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_RoomGeolocation_GeolocationRoomGeolocationId",
                        column: x => x.GeolocationRoomGeolocationId,
                        principalSchema: "ooapiv5",
                        principalTable: "RoomGeolocation",
                        principalColumn: "RoomGeolocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Component",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_Address_ComponentOffering_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Address_Course_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Address_CourseOffering_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Address_Geolocation_GeolocationId",
                        column: x => x.GeolocationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Geolocation",
                        principalColumn: "GeolocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_Address_Program_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_Address_ProgramOffering_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOffering",
                        principalColumn: "OfferingId");
                });

            migrationBuilder.CreateTable(
                name: "Building",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Building_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Building_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "ooapiv5",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GivenName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SurnamePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveEnrollment = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CityOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfNationality = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Affiliations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PhotoSocial = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    PhotoOfficial = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    TitlePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitleSuffix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Office = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ICEName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ICEPhoneNumber = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ICERelation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Person_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_PrimaryCode_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCode",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTypedString",
                schema: "ooapiv5",
                columns: table => new
                {
                    LanguageTypedStringId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentOfferingOfferingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId4 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId5 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EducationSpecificationId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsFeedId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsItemId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId4 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId5 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypedString", x => x.LanguageTypedStringId);
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_AcademicSession_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSession",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Address",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Building",
                        principalColumn: "BuildingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Building_BuildingId1",
                        column: x => x.BuildingId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Building",
                        principalColumn: "BuildingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Component",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Component_ComponentId1",
                        column: x => x.ComponentId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Component",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Component_ComponentId2",
                        column: x => x.ComponentId2,
                        principalSchema: "ooapiv5",
                        principalTable: "Component",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Component_ComponentId3",
                        column: x => x.ComponentId3,
                        principalSchema: "ooapiv5",
                        principalTable: "Component",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_ComponentOffering_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_ComponentOffering_ComponentOfferingOfferingId1",
                        column: x => x.ComponentOfferingOfferingId1,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOffering",
                        principalColumn: "OfferingId");
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
                        name: "FK_LanguageTypedString_Course_CourseId3",
                        column: x => x.CourseId3,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Course_CourseId4",
                        column: x => x.CourseId4,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Course_CourseId5",
                        column: x => x.CourseId5,
                        principalSchema: "ooapiv5",
                        principalTable: "Course",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_CourseOffering_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_CourseOffering_CourseOfferingOfferingId1",
                        column: x => x.CourseOfferingOfferingId1,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_EducationSpecification_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecification",
                        principalColumn: "EducationSpecificationId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_EducationSpecification_EducationSpecificationId1",
                        column: x => x.EducationSpecificationId1,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecification",
                        principalColumn: "EducationSpecificationId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_NewsFeed_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeed",
                        principalColumn: "NewsFeedId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_NewsFeed_NewsFeedId1",
                        column: x => x.NewsFeedId1,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeed",
                        principalColumn: "NewsFeedId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_NewsItem_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItem",
                        principalColumn: "NewsItemId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_NewsItem_NewsItemId1",
                        column: x => x.NewsItemId1,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItem",
                        principalColumn: "NewsItemId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Offering_OfferingId",
                        column: x => x.OfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Offering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Offering_OfferingId1",
                        column: x => x.OfferingId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Offering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Organization_OrganizationId1",
                        column: x => x.OrganizationId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
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
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Program_ProgramId3",
                        column: x => x.ProgramId3,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Program_ProgramId4",
                        column: x => x.ProgramId4,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Program_ProgramId5",
                        column: x => x.ProgramId5,
                        principalSchema: "ooapiv5",
                        principalTable: "Program",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_ProgramOffering_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_ProgramOffering_ProgramOfferingOfferingId1",
                        column: x => x.ProgramOfferingOfferingId1,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOffering",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Room_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Room",
                        principalColumn: "RoomId");
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_Room_RoomId1",
                        column: x => x.RoomId1,
                        principalSchema: "ooapiv5",
                        principalTable: "Room",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "LanguageOfChoice",
                schema: "ooapiv5",
                columns: table => new
                {
                    LanguageOfChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageOfChoice", x => x.LanguageOfChoiceId);
                    table.ForeignKey(
                        name: "FK_LanguageOfChoice_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Person",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_CourseId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_PersonId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherCodes_RoomId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_AssociationId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_BuildingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_ComponentId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_CourseId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "NewsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_NewsItemId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_OfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "OfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_OrganizationId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_PersonId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_ProgramId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumer_RoomId",
                schema: "ooapiv5",
                table: "Consumer",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSession_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSession",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ComponentId",
                schema: "ooapiv5",
                table: "Address",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CourseId",
                schema: "ooapiv5",
                table: "Address",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_GeolocationId",
                schema: "ooapiv5",
                table: "Address",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_OrganizationId",
                schema: "ooapiv5",
                table: "Address",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProgramId",
                schema: "ooapiv5",
                table: "Address",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Address",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_AddressId",
                schema: "ooapiv5",
                table: "Building",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Building_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Building",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Component_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Component",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOffering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOffering",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Cost_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Cost",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Course",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Course",
                column: "StudyLoadProgramResultStudyLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOffering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOffering",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecification_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecification_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadProgramResultStudyLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageOfChoice_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoice",
                column: "PersonId");

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
                name: "IX_LanguageTypedString_NewsItemId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "NewsItemId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Offering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Offering",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Organization_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organization",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                schema: "ooapiv5",
                table: "Person",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Person",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Program",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Program_StudyLoadProgramResultStudyLoadId",
                schema: "ooapiv5",
                table: "Program",
                column: "StudyLoadProgramResultStudyLoadId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOffering_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOffering",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_GeolocationRoomGeolocationId",
                schema: "ooapiv5",
                table: "Room",
                column: "GeolocationRoomGeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Room",
                column: "PrimaryCodeId");

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
                name: "FK_Groups_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
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
                name: "FK_OtherCodes_EducationSpecification_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "EducationSpecificationId",
                principalSchema: "ooapiv5",
                principalTable: "EducationSpecification",
                principalColumn: "EducationSpecificationId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Groups_PrimaryCode_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

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
                name: "FK_OtherCodes_EducationSpecification_EducationSpecificationId",
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

            migrationBuilder.DropTable(
                name: "Association",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Author",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "LanguageOfChoice",
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
                name: "Resource",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AcademicSession",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Building",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Cost",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecification",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeed",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItem",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Offering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Room",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "RoomGeolocation",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Component",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Course",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Geolocation",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Organization",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Program",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramResultStudyLoad",
                schema: "ooapiv5");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrimaryCode",
                schema: "ooapiv5",
                table: "PrimaryCode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OtherCodes",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_CourseId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_PersonId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_OtherCodes_RoomId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropIndex(
                name: "IX_Groups_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Consumer",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_AssociationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_BuildingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_ComponentId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_CourseId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_NewsItemId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_OfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_OrganizationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_PersonId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_ProgramId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropIndex(
                name: "IX_Consumer_RoomId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "PrimaryCode");

            migrationBuilder.DropColumn(
                name: "OtherCodesId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ooapiv5",
                table: "OtherCodes");

            migrationBuilder.DropColumn(
                name: "PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ConsumerId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "CourseId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "NewsFeedId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "NewsItemId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "OfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "PersonId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "ooapiv5",
                table: "Consumer");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ooapiv5",
                table: "PrimaryCode",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "ooapiv5",
                table: "OtherCodes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ConsumerKey",
                schema: "ooapiv5",
                table: "Consumer",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrimaryCode",
                schema: "ooapiv5",
                table: "PrimaryCode",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OtherCodes",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Consumer",
                schema: "ooapiv5",
                table: "Consumer",
                column: "ConsumerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_PrimaryCode_PrimaryCodeCode",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeCode",
                principalSchema: "ooapiv5",
                principalTable: "PrimaryCode",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
