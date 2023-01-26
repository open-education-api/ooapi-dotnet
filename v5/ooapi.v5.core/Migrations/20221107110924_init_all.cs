using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ooapi.v5.core.Migrations
{
    public partial class init_all : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ooapiv5");

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
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.AssociationId);
                });

            migrationBuilder.CreateTable(
                name: "Geolocations",
                schema: "ooapiv5",
                columns: table => new
                {
                    GeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geolocations", x => x.GeolocationId);
                });

            migrationBuilder.CreateTable(
                name: "NewsFeeds",
                schema: "ooapiv5",
                columns: table => new
                {
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsFeedType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItems", x => x.NewsItemId);
                });

            migrationBuilder.CreateTable(
                name: "PrimaryCodes",
                schema: "ooapiv5",
                columns: table => new
                {
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrimaryCodes", x => x.PrimaryCodeId);
                });

            migrationBuilder.CreateTable(
                name: "StudyLoadDescriptors",
                schema: "ooapiv5",
                columns: table => new
                {
                    StudyLoadDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudyLoadUnit = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLoadDescriptors", x => x.StudyLoadDescriptorId);
                });

            migrationBuilder.CreateTable(
                name: "AcademicSessions",
                schema: "ooapiv5",
                columns: table => new
                {
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicSessionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicSessions", x => x.AcademicSessionId);
                    table.ForeignKey(
                        name: "FK_AcademicSessions_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComponentOfferings",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultWeight = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: true),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentOfferings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_ComponentOfferings_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                schema: "ooapiv5",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComponentType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Enrollment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assessment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_Components_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferings",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: true),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "ooapiv5",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    StudyLoadDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    LearningOutcomes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmissionRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QualificationRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Enrollment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resources = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assessment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "ooapiv5",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "ooapiv5",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "FK_Organizations_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgramOfferings",
                schema: "ooapiv5",
                columns: table => new
                {
                    OfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlexibleEntryPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FlexibleEntryPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfferingType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EnrolledNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PendingNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MinNumberStudents = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ResultExpected = table.Column<bool>(type: "bit", nullable: true),
                    ResultValueType = table.Column<int>(type: "int", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnrollEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramOfferings", x => x.OfferingId);
                    table.ForeignKey(
                        name: "FK_ProgramOfferings_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                schema: "ooapiv5",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingLanguage = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    StudyLoadDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QualificationAwarded = table.Column<int>(type: "int", nullable: true),
                    ModeOfStudy = table.Column<int>(type: "int", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LevelOfQualification = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Enrollment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Assessment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdmissionRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QualificationRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Programs_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "ooapiv5",
                columns: table => new
                {
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: true),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: true),
                    AvailableSeats = table.Column<int>(type: "int", nullable: true),
                    Floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeolocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                    table.ForeignKey(
                        name: "FK_Rooms_Geolocations_GeolocationId",
                        column: x => x.GeolocationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Geolocations",
                        principalColumn: "GeolocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rooms_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationSpecification",
                schema: "ooapiv5",
                columns: table => new
                {
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationSpecificationType = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormalDocument = table.Column<int>(type: "int", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Sector = table.Column<int>(type: "int", nullable: true),
                    LevelOfQualification = table.Column<int>(type: "int", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    StudyLoadDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationSpecification", x => x.EducationSpecificationId);
                    table.ForeignKey(
                        name: "FK_EducationSpecification_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationSpecification_StudyLoadDescriptors_StudyLoadDescriptorId",
                        column: x => x.StudyLoadDescriptorId,
                        principalSchema: "ooapiv5",
                        principalTable: "StudyLoadDescriptors",
                        principalColumn: "StudyLoadDescriptorId",
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
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypedString", x => x.LanguageTypedStringId);
                    table.ForeignKey(
                        name: "FK_LanguageTypedString_AcademicSessions_AcademicSessionId",
                        column: x => x.AcademicSessionId,
                        principalSchema: "ooapiv5",
                        principalTable: "AcademicSessions",
                        principalColumn: "AcademicSessionId");
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
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.CostId);
                    table.ForeignKey(
                        name: "FK_Costs_ComponentOfferings_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Costs_CourseOfferings_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Costs_ProgramOfferings_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "ooapiv5",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Additional = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Addresses_ComponentOfferings_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Addresses_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_Addresses_CourseOfferings_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Addresses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Addresses_Geolocations_GeolocationId",
                        column: x => x.GeolocationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Geolocations",
                        principalColumn: "GeolocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "ooapiv5",
                        principalTable: "Organizations",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_Addresses_ProgramOfferings_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Addresses_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId");
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                schema: "ooapiv5",
                columns: table => new
                {
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrimaryCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.BuildingId);
                    table.ForeignKey(
                        name: "FK_Buildings_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "ooapiv5",
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Buildings_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
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
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                        name: "FK_Persons_PrimaryCodes_PrimaryCodeId",
                        column: x => x.PrimaryCodeId,
                        principalSchema: "ooapiv5",
                        principalTable: "PrimaryCodes",
                        principalColumn: "PrimaryCodeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                schema: "ooapiv5",
                columns: table => new
                {
                    ConsumerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AssociationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CourseOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EducationSpecificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsFeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewsItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgramOfferingOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.ConsumerId);
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
                        name: "FK_Consumers_ComponentOfferings_ComponentOfferingOfferingId",
                        column: x => x.ComponentOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ComponentOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Consumers_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalSchema: "ooapiv5",
                        principalTable: "Components",
                        principalColumn: "ComponentId");
                    table.ForeignKey(
                        name: "FK_Consumers_CourseOfferings_CourseOfferingOfferingId",
                        column: x => x.CourseOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "CourseOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Consumers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "ooapiv5",
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_Consumers_EducationSpecification_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecification",
                        principalColumn: "EducationSpecificationId");
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
                        name: "FK_Consumers_ProgramOfferings_ProgramOfferingOfferingId",
                        column: x => x.ProgramOfferingOfferingId,
                        principalSchema: "ooapiv5",
                        principalTable: "ProgramOfferings",
                        principalColumn: "OfferingId");
                    table.ForeignKey(
                        name: "FK_Consumers_Programs_ProgramId",
                        column: x => x.ProgramId,
                        principalSchema: "ooapiv5",
                        principalTable: "Programs",
                        principalColumn: "ProgramId");
                    table.ForeignKey(
                        name: "FK_Consumers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "ooapiv5",
                        principalTable: "Rooms",
                        principalColumn: "RoomId");
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
                        name: "FK_OtherCodes_EducationSpecification_EducationSpecificationId",
                        column: x => x.EducationSpecificationId,
                        principalSchema: "ooapiv5",
                        principalTable: "EducationSpecification",
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

            migrationBuilder.CreateIndex(
                name: "IX_AcademicSessions_PrimaryCodeId",
                schema: "ooapiv5",
                table: "AcademicSessions",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ComponentId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CourseId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_GeolocationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OrganizationId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProgramId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Addresses",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_AddressId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Buildings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ComponentOfferings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Components",
                column: "PrimaryCodeId");

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
                name: "IX_Consumers_EducationSpecificationId",
                schema: "ooapiv5",
                table: "Consumers",
                column: "EducationSpecificationId");

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
                name: "IX_Costs_ComponentOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ComponentOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_CourseOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "CourseOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ProgramOfferingOfferingId",
                schema: "ooapiv5",
                table: "Costs",
                column: "ProgramOfferingOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "CourseOfferings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Courses",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecification_PrimaryCodeId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationSpecification_StudyLoadDescriptorId",
                schema: "ooapiv5",
                table: "EducationSpecification",
                column: "StudyLoadDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Groups",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageOfChoices_PersonId",
                schema: "ooapiv5",
                table: "LanguageOfChoices",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypedString_AcademicSessionId",
                schema: "ooapiv5",
                table: "LanguageTypedString",
                column: "AcademicSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Organizations",
                column: "PrimaryCodeId");

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
                name: "IX_Persons_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Persons",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramOfferings_PrimaryCodeId",
                schema: "ooapiv5",
                table: "ProgramOfferings",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Programs",
                column: "PrimaryCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_GeolocationId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "GeolocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PrimaryCodeId",
                schema: "ooapiv5",
                table: "Rooms",
                column: "PrimaryCodeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consumers",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Costs",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "LanguageOfChoices",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "LanguageTypedString",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "OtherCodes",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Associations",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsFeeds",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "NewsItems",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "AcademicSessions",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Buildings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "EducationSpecification",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Persons",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "StudyLoadDescriptors",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ComponentOfferings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Components",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "CourseOfferings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Geolocations",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "ProgramOfferings",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "Programs",
                schema: "ooapiv5");

            migrationBuilder.DropTable(
                name: "PrimaryCodes",
                schema: "ooapiv5");
        }
    }
}
