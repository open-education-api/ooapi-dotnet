using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    /// <inheritdoc />
    public partial class InitTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ooapiv5");

            migrationBuilder.CreateTable(
                name: "AcademicSessions",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicSessionType = table.Column<int>(type: "int", nullable: true),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    YearId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessions", x => x.AcademicSessionId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "ComponentResults",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentResults", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                schema: "ooapiv5",
                columns: table => new
                {
                    CostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VatAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountWithoutVat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.CostId);
                });

            migrationBuilder.CreateTable(
                name: "CourseResults",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseResults", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeeds",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedType = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeeds", x => x.NewsFeedId);
                });

            migrationBuilder.CreateTable(
                name: "NewsItems",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsItemType = table.Column<int>(type: "int", nullable: true),
                    Authors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItems", x => x.NewsItemId);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationType = table.Column<int>(type: "int", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "ProgramResults",
                schema: "ooapiv5",
                columns: table => new
                {
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    Pass = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramResults", x => x.ResultId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "ooapiv5",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Documentation = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Buildings_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "NewsFeedNewsItem",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedsNewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsItemsNewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsFeedNewsItem", x => new { x.NewsFeedsNewsFeedId, x.NewsItemsNewsItemId });
                    table.ForeignKey(
                        name: "FK_NewsFeedNewsItem_NewsFeeds_NewsFeedsNewsFeedId",
                        column: x => x.NewsFeedsNewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NewsFeedNewsItem_NewsItems_NewsItemsNewsItemId",
                        column: x => x.NewsItemsNewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressOrganization",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationsOrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressOrganization", x => new { x.AddressId, x.OrganizationsOrganizationId });
                    table.ForeignKey(
                        name: "FK_AddressOrganization_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressOrganization_Organizations_OrganizationsOrganizationId",
                        column: x => x.OrganizationsOrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSpecifications",
                schema: "ooapiv5",
                columns: table => new
                {
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationSpecificationType = table.Column<int>(type: "int", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FormalDocument = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: true),
                    LevelOfQualification = table.Column<int>(type: "int", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecifications", x => x.EducationSpecificationId);
                    table.ForeignKey(
                        name: "FK_EducationSpecifications_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "ooapiv5",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupType = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "ConsumerRegistrations",
                schema: "ooapiv5",
                columns: table => new
                {
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumerRegistrations", x => x.ConsumerKey);
                    table.ForeignKey(
                        name: "FK_ConsumerRegistrations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "ooapiv5",
                        principalTable: "Services",
                        principalColumn: "ServiceId");
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomType = table.Column<int>(type: "int", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: true),
                    AvailableSeats = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(8,6)", precision: 8, scale: 6, nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId");
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    LearningOutcomes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId");
                    table.ForeignKey(
                        name: "FK_Courses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "AddressCourse",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCourse", x => new { x.AddressId, x.CoursesCourseId });
                    table.ForeignKey(
                        name: "FK_AddressCourse_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressCourse_Courses_CoursesCourseId",
                        column: x => x.CoursesCourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentType = table.Column<int>(type: "int", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_Components_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Components_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StudyLoadUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyLoadValue = table.Column<int>(type: "int", nullable: true),
                    QualificationAwarded = table.Column<int>(type: "int", nullable: true),
                    ModeOfStudy = table.Column<int>(type: "int", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LevelOfQualification = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Programs_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Programs_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId");
                    table.ForeignKey(
                        name: "FK_Programs_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "AddressComponent",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentsComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressComponent", x => new { x.AddressId, x.ComponentsComponentId });
                    table.ForeignKey(
                        name: "FK_AddressComponent_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressComponent_Components_ComponentsComponentId",
                        column: x => x.ComponentsComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModelTypeName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => new { x.Id, x.ModelTypeName, x.PropertyName, x.Language });
                    table.ForeignKey(
                        name: "FK_Attributes_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_Attributes_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId");
                    table.ForeignKey(
                        name: "FK_Attributes_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_Attributes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Attributes_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_Attributes_NewsFeeds_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId");
                    table.ForeignKey(
                        name: "FK_Attributes_NewsItems_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId");
                    table.ForeignKey(
                        name: "FK_Attributes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferings",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultWeight = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxNumberStudents = table.Column<int>(type: "int", nullable: false),
                    EnrolledNumberStudents = table.Column<int>(type: "int", nullable: false),
                    PendingNumberStudents = table.Column<int>(type: "int", nullable: true),
                    MinNumberStudents = table.Column<int>(type: "int", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_ComponentOfferings_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_ComponentOfferings_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_ComponentOfferings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_ComponentOfferings_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_ComponentOfferings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "AddressProgram",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramsProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressProgram", x => new { x.AddressId, x.ProgramsProgramId });
                    table.ForeignKey(
                        name: "FK_AddressProgram_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressProgram_Programs_ProgramsProgramId",
                        column: x => x.ProgramsProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                schema: "ooapiv5",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GivenName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    SurnamePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveEnrollment = table.Column<bool>(type: "bit", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CityOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfNationality = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Affiliations = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    ICERelation = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Persons_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId");
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferings",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxNumberStudents = table.Column<int>(type: "int", nullable: false),
                    EnrolledNumberStudents = table.Column<int>(type: "int", nullable: false),
                    PendingNumberStudents = table.Column<int>(type: "int", nullable: true),
                    MinNumberStudents = table.Column<int>(type: "int", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_ProgramOfferings_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_ProgramOfferings_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_ProgramOfferings_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId");
                });

            migrationBuilder.CreateTable(
                name: "AddressComponentOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressComponentOffering", x => new { x.AddressId, x.ComponentOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_AddressComponentOffering_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressComponentOffering_ComponentOfferings_ComponentOfferingsOfferingId",
                        column: x => x.ComponentOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferingCost",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CostsCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferingCost", x => new { x.ComponentOfferingsOfferingId, x.CostsCostId });
                    table.ForeignKey(
                        name: "FK_ComponentOfferingCost_ComponentOfferings_ComponentOfferingsOfferingId",
                        column: x => x.ComponentOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentOfferingCost_Costs_CostsCostId",
                        column: x => x.CostsCostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupPerson",
                schema: "ooapiv5",
                columns: table => new
                {
                    GroupsGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonsPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPerson", x => new { x.GroupsGroupId, x.PersonsPersonId });
                    table.ForeignKey(
                        name: "FK_GroupPerson_Groups_GroupsGroupId",
                        column: x => x.GroupsGroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupPerson_Persons_PersonsPersonId",
                        column: x => x.PersonsPersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageOfChoices",
                schema: "ooapiv5",
                columns: table => new
                {
                    LanguageOfChoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageOfChoices", x => x.LanguageOfChoiceId);
                    table.ForeignKey(
                        name: "FK_LanguageOfChoices_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "AddressProgramOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressProgramOffering", x => new { x.AddressId, x.ProgramOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_AddressProgramOffering_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressProgramOffering_ProgramOfferings_ProgramOfferingsOfferingId",
                        column: x => x.ProgramOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CostProgramOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    CostsCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostProgramOffering", x => new { x.CostsCostId, x.ProgramOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_CostProgramOffering_Costs_CostsCostId",
                        column: x => x.CostsCostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostProgramOffering_ProgramOfferings_ProgramOfferingsOfferingId",
                        column: x => x.ProgramOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferings",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCodeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxNumberStudents = table.Column<int>(type: "int", nullable: false),
                    EnrolledNumberStudents = table.Column<int>(type: "int", nullable: false),
                    PendingNumberStudents = table.Column<int>(type: "int", nullable: true),
                    MinNumberStudents = table.Column<int>(type: "int", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: false),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_CourseOfferings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CourseOfferings_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_CourseOfferings_ProgramOfferings_ProgramOfferingId",
                        column: x => x.ProgramOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                });

            migrationBuilder.CreateTable(
                name: "AddressCourseOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressCourseOffering", x => new { x.AddressId, x.CourseOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_AddressCourseOffering_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressCourseOffering_CourseOfferings_CourseOfferingsOfferingId",
                        column: x => x.CourseOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Associations",
                schema: "ooapiv5",
                columns: table => new
                {
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociationType = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    RemoteState = table.Column<int>(type: "int", nullable: true),
                    ResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramResultResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseResultResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentResultResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.AssociationId);
                    table.ForeignKey(
                        name: "FK_Associations_ComponentOfferings_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Associations_ComponentResults_ComponentResultResultId",
                        column: x => x.ComponentResultResultId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentResults",
                        principalColumn: "ResultId");
                    table.ForeignKey(
                        name: "FK_Associations_CourseOfferings_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Associations_CourseResults_CourseResultResultId",
                        column: x => x.CourseResultResultId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseResults",
                        principalColumn: "ResultId");
                    table.ForeignKey(
                        name: "FK_Associations_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Associations_ProgramOfferings_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Associations_ProgramResults_ProgramResultResultId",
                        column: x => x.ProgramResultResultId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramResults",
                        principalColumn: "ResultId");
                });

            migrationBuilder.CreateTable(
                name: "CostCourseOffering",
                schema: "ooapiv5",
                columns: table => new
                {
                    CostsCostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingsOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCourseOffering", x => new { x.CostsCostId, x.CourseOfferingsOfferingId });
                    table.ForeignKey(
                        name: "FK_CostCourseOffering_Costs_CostsCostId",
                        column: x => x.CostsCostId,
                        principalSchema: "ooapiv5",
                        principalTable: "Costs",
                        principalColumn: "CostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostCourseOffering_CourseOfferings_CourseOfferingsOfferingId",
                        column: x => x.CourseOfferingsOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherCodes",
                schema: "ooapiv5",
                columns: table => new
                {
                    OtherCodesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherCodes", x => x.OtherCodesId);
                    table.ForeignKey(
                        name: "FK_OtherCodes_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_ComponentOfferings_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_CourseOfferings_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_EducationSpecifications_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecifications",
                        principalColumn: "EducationSpecificationId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_ProgramOfferings_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_OtherCodes_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ConsumerKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => new { x.Id, x.ConsumerKey, x.PropertyName });
                    table.ForeignKey(
                        name: "FK_Consumers_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
                    table.ForeignKey(
                        name: "FK_Consumers_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Associations",
                        principalColumn: "AssociationId");
                    table.ForeignKey(
                        name: "FK_Consumers_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalSchema: "ooapiv5",
                        principalTable: "Buildings",
                        principalColumn: "BuildingId");
                    table.ForeignKey(
                        name: "FK_Consumers_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_Consumers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Consumers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "ooapiv5",
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_Consumers_NewsFeeds_NewsFeedId",
                        column: x => x.NewsFeedId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsFeeds",
                        principalColumn: "NewsFeedId");
                    table.ForeignKey(
                        name: "FK_Consumers_NewsItems_NewsItemId",
                        column: x => x.NewsItemId,
                        principalSchema: "ooapiv5",
                        principalTable: "NewsItems",
                        principalColumn: "NewsItemId");
                    table.ForeignKey(
                        name: "FK_Consumers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_Consumers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "ooapiv5",
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                    table.ForeignKey(
                        name: "FK_Consumers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressComponent_ComponentsComponentId",
                schema: "ooapiv5",
                table: "AddressComponent",
                column: "ComponentsComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressComponentOffering_ComponentOfferingsOfferingId",
                schema: "ooapiv5",
                table: "AddressComponentOffering",
                column: "ComponentOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressCourse_CoursesCourseId",
                schema: "ooapiv5",
                table: "AddressCourse",
                column: "CoursesCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressCourseOffering_CourseOfferingsOfferingId",
                schema: "ooapiv5",
                table: "AddressCourseOffering",
                column: "CourseOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressOrganization_OrganizationsOrganizationId",
                schema: "ooapiv5",
                table: "AddressOrganization",
                column: "OrganizationsOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressProgram_ProgramsProgramId",
                schema: "ooapiv5",
                table: "AddressProgram",
                column: "ProgramsProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressProgramOffering_ProgramOfferingsOfferingId",
                schema: "ooapiv5",
                table: "AddressProgramOffering",
                column: "ProgramOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ComponentResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ComponentResultResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_CourseResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "CourseResultResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_PersonId",
                schema: "ooapiv5",
                table: "Associations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_ProgramResultResultId",
                schema: "ooapiv5",
                table: "Associations",
                column: "ProgramResultResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AcademicSessionId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_BuildingId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ComponentId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CourseId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_GroupId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_NewsFeedId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "NewsFeedId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_NewsItemId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "NewsItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_OrganizationId",
                schema: "ooapiv5",
                table: "Attributes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferingCost_CostsCostId",
                schema: "ooapiv5",
                table: "ComponentOfferingCost",
                column: "CostsCostId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_ComponentId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_CourseId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_RoomId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_CourseId",
                schema: "ooapiv5",
                table: "Components",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_OrganizationId",
                schema: "ooapiv5",
                table: "Components",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumerRegistrations_ServiceId",
                schema: "ooapiv5",
                table: "ConsumerRegistrations",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AcademicSessionId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_AssociationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_BuildingId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_ComponentId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_CourseId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "CourseId");

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
                name: "IX_Consumers_RoomId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCourseOffering_CourseOfferingsOfferingId",
                schema: "ooapiv5",
                table: "CostCourseOffering",
                column: "CourseOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CostProgramOffering_ProgramOfferingsOfferingId",
                schema: "ooapiv5",
                table: "CostProgramOffering",
                column: "ProgramOfferingsOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_CourseId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_ProgramOfferingId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "ProgramOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Courses",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OrganizationId",
                schema: "ooapiv5",
                table: "Courses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecifications_OrganizationId",
                schema: "ooapiv5",
                table: "EducationSpecifications",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupPerson_PersonsPersonId",
                schema: "ooapiv5",
                table: "GroupPerson",
                column: "PersonsPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrganizationId",
                schema: "ooapiv5",
                table: "Groups",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageOfChoices_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoices",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsFeedNewsItem_NewsItemsNewsItemId",
                schema: "ooapiv5",
                table: "NewsFeedNewsItem",
                column: "NewsItemsNewsItemId");

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
                name: "IX_OtherCodes_GroupId",
                schema: "ooapiv5",
                table: "OtherCodes",
                column: "GroupId");

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
                name: "IX_Persons_AddressId",
                schema: "ooapiv5",
                table: "Persons",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CourseId",
                schema: "ooapiv5",
                table: "Persons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ProgramId",
                schema: "ooapiv5",
                table: "Persons",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_AcademicSessionId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_OrganizationId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_ProgramId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_CourseId",
                schema: "ooapiv5",
                table: "Programs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Programs",
                column: "EducationSpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_OrganizationId",
                schema: "ooapiv5",
                table: "Programs",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "BuildingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressComponent",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressComponentOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressCourse",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressCourseOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressOrganization",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressProgram",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AddressProgramOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Attributes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferingCost",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ConsumerRegistrations",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Consumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CostCourseOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CostProgramOffering",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "GroupPerson",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "LanguageOfChoices",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeedNewsItem",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "OtherCodes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Associations",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Costs",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeeds",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItems",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentResults",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseResults",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramResults",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Components",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Buildings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AcademicSessions",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Programs",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecifications",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "ooapiv5");
        }
    }
}
