using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql
{
    /// <inheritdoc />
    public partial class AddCourseProgrammeTimelineOverrides : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseTimelineOverrides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PrimaryCodeType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PrimaryCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NameJson = table.Column<string>(type: "text", nullable: false),
                    DescriptionJson = table.Column<string>(type: "text", nullable: true),
                    StudyLoadJson = table.Column<string>(type: "text", nullable: true),
                    ModesOfDeliveryJson = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FirstStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TeachingLanguagesJson = table.Column<string>(type: "text", nullable: true),
                    FieldsOfStudy = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true),
                    Link = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    AddressesJson = table.Column<string>(type: "text", nullable: true),
                    Level = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ResourcesJson = table.Column<string>(type: "text", nullable: true),
                    AssessmentJson = table.Column<string>(type: "text", nullable: true),
                    EnrolmentJson = table.Column<string>(type: "text", nullable: true),
                    AdmissionRequirementsJson = table.Column<string>(type: "text", nullable: true),
                    QualificationRequirementsJson = table.Column<string>(type: "text", nullable: true),
                    SupplementaryInformationJson = table.Column<string>(type: "text", nullable: true),
                    ExtJson = table.Column<string>(type: "text", nullable: true),
                    ConsumerKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConsumerJson = table.Column<string>(type: "text", nullable: true),
                    OrganisationEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTimelineOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrides_Courses_CourseEntityId",
                        column: x => x.CourseEntityId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrides_Organisations_OrganisationEntityId",
                        column: x => x.OrganisationEntityId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeTimelineOverrides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgrammeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PrimaryCodeType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PrimaryCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Abbreviation = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NameJson = table.Column<string>(type: "text", nullable: false),
                    DescriptionJson = table.Column<string>(type: "text", nullable: true),
                    StudyLoadJson = table.Column<string>(type: "text", nullable: true),
                    QualificationAwarded = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    QualificationLevels = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TeachingLanguagesJson = table.Column<string>(type: "text", nullable: true),
                    ProgrammeType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ModeOfStudy = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ModesOfDeliveryJson = table.Column<string>(type: "text", nullable: true),
                    LevelOfQualification = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    FormalDocument = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Duration = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExtJson = table.Column<string>(type: "text", nullable: true),
                    ConsumerKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConsumerJson = table.Column<string>(type: "text", nullable: true),
                    OrganisationEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeTimelineOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrides_Organisations_OrganisationEntity~",
                        column: x => x.OrganisationEntityId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrides_Programmes_ParentEntityId",
                        column: x => x.ParentEntityId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrides_Programmes_ProgrammeEntityId",
                        column: x => x.ProgrammeEntityId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTimelineOverrideCoordinators",
                columns: table => new
                {
                    TimelineOverrideCourseEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTimelineOverrideCoordinators", x => new { x.TimelineOverrideCourseEntityId, x.PersonEntityId });
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideCoordinators_CourseTimelineOverrides_~",
                        column: x => x.TimelineOverrideCourseEntityId,
                        principalTable: "CourseTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideCoordinators_Persons_PersonEntityId",
                        column: x => x.PersonEntityId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTimelineOverrideInstructors",
                columns: table => new
                {
                    TimelineOverrideCourseEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTimelineOverrideInstructors", x => new { x.TimelineOverrideCourseEntityId, x.PersonEntityId });
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideInstructors_CourseTimelineOverrides_T~",
                        column: x => x.TimelineOverrideCourseEntityId,
                        principalTable: "CourseTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideInstructors_Persons_PersonEntityId",
                        column: x => x.PersonEntityId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTimelineOverrideLearningOutcomes",
                columns: table => new
                {
                    TimelineOverrideCourseEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    LearningOutcomeEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTimelineOverrideLearningOutcomes", x => new { x.TimelineOverrideCourseEntityId, x.LearningOutcomeEntityId });
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideLearningOutcomes_CourseTimelineOverri~",
                        column: x => x.TimelineOverrideCourseEntityId,
                        principalTable: "CourseTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideLearningOutcomes_LearningOutcomes_Lea~",
                        column: x => x.LearningOutcomeEntityId,
                        principalTable: "LearningOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTimelineOverrideOtherCodes",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodeType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTimelineOverrideOtherCodes", x => new { x.OwnerId, x.Id });
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideOtherCodes_CourseTimelineOverrides_Ow~",
                        column: x => x.OwnerId,
                        principalTable: "CourseTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTimelineOverrideProgrammes",
                columns: table => new
                {
                    TimelineOverrideCourseEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgrammeEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTimelineOverrideProgrammes", x => new { x.TimelineOverrideCourseEntityId, x.ProgrammeEntityId });
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideProgrammes_CourseTimelineOverrides_Ti~",
                        column: x => x.TimelineOverrideCourseEntityId,
                        principalTable: "CourseTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseTimelineOverrideProgrammes_Programmes_ProgrammeEntity~",
                        column: x => x.ProgrammeEntityId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeTimelineOverrideChildren",
                columns: table => new
                {
                    TimelineOverrideProgrammeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgrammeEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeTimelineOverrideChildren", x => new { x.TimelineOverrideProgrammeEntityId, x.ProgrammeEntityId });
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideChildren_ProgrammeTimelineOverride~",
                        column: x => x.TimelineOverrideProgrammeEntityId,
                        principalTable: "ProgrammeTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideChildren_Programmes_ProgrammeEntit~",
                        column: x => x.ProgrammeEntityId,
                        principalTable: "Programmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeTimelineOverrideCoordinators",
                columns: table => new
                {
                    TimelineOverrideProgrammeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeTimelineOverrideCoordinators", x => new { x.TimelineOverrideProgrammeEntityId, x.PersonEntityId });
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideCoordinators_Persons_PersonEntityId",
                        column: x => x.PersonEntityId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideCoordinators_ProgrammeTimelineOver~",
                        column: x => x.TimelineOverrideProgrammeEntityId,
                        principalTable: "ProgrammeTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeTimelineOverrideInstructors",
                columns: table => new
                {
                    TimelineOverrideProgrammeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeTimelineOverrideInstructors", x => new { x.TimelineOverrideProgrammeEntityId, x.PersonEntityId });
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideInstructors_Persons_PersonEntityId",
                        column: x => x.PersonEntityId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideInstructors_ProgrammeTimelineOverr~",
                        column: x => x.TimelineOverrideProgrammeEntityId,
                        principalTable: "ProgrammeTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgrammeTimelineOverrideOtherCodes",
                columns: table => new
                {
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodeType = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgrammeTimelineOverrideOtherCodes", x => new { x.OwnerId, x.Id });
                    table.ForeignKey(
                        name: "FK_ProgrammeTimelineOverrideOtherCodes_ProgrammeTimelineOverri~",
                        column: x => x.OwnerId,
                        principalTable: "ProgrammeTimelineOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseTimelineOverrideCoordinators_PersonEntityId",
                table: "CourseTimelineOverrideCoordinators",
                column: "PersonEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTimelineOverrideInstructors_PersonEntityId",
                table: "CourseTimelineOverrideInstructors",
                column: "PersonEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTimelineOverrideLearningOutcomes_LearningOutcomeEntit~",
                table: "CourseTimelineOverrideLearningOutcomes",
                column: "LearningOutcomeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTimelineOverrideProgrammes_ProgrammeEntityId",
                table: "CourseTimelineOverrideProgrammes",
                column: "ProgrammeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTimelineOverrides_CourseEntityId",
                table: "CourseTimelineOverrides",
                column: "CourseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTimelineOverrides_OrganisationEntityId",
                table: "CourseTimelineOverrides",
                column: "OrganisationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTimelineOverrideChildren_ProgrammeEntityId",
                table: "ProgrammeTimelineOverrideChildren",
                column: "ProgrammeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTimelineOverrideCoordinators_PersonEntityId",
                table: "ProgrammeTimelineOverrideCoordinators",
                column: "PersonEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTimelineOverrideInstructors_PersonEntityId",
                table: "ProgrammeTimelineOverrideInstructors",
                column: "PersonEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTimelineOverrides_OrganisationEntityId",
                table: "ProgrammeTimelineOverrides",
                column: "OrganisationEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTimelineOverrides_ParentEntityId",
                table: "ProgrammeTimelineOverrides",
                column: "ParentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgrammeTimelineOverrides_ProgrammeEntityId",
                table: "ProgrammeTimelineOverrides",
                column: "ProgrammeEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTimelineOverrideCoordinators");

            migrationBuilder.DropTable(
                name: "CourseTimelineOverrideInstructors");

            migrationBuilder.DropTable(
                name: "CourseTimelineOverrideLearningOutcomes");

            migrationBuilder.DropTable(
                name: "CourseTimelineOverrideOtherCodes");

            migrationBuilder.DropTable(
                name: "CourseTimelineOverrideProgrammes");

            migrationBuilder.DropTable(
                name: "ProgrammeTimelineOverrideChildren");

            migrationBuilder.DropTable(
                name: "ProgrammeTimelineOverrideCoordinators");

            migrationBuilder.DropTable(
                name: "ProgrammeTimelineOverrideInstructors");

            migrationBuilder.DropTable(
                name: "ProgrammeTimelineOverrideOtherCodes");

            migrationBuilder.DropTable(
                name: "CourseTimelineOverrides");

            migrationBuilder.DropTable(
                name: "ProgrammeTimelineOverrides");
        }
    }
}
