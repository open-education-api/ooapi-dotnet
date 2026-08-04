#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OEAPI.Infrastructure.Data.Migrations.PostgreSql;

/// <inheritdoc />
public partial class InitialCreate_PostgreSQL : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "AcademicSessions",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                AcademicSessionId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: false),
                DescriptionJson = table.Column<string>("text", nullable: true),
                AcademicSessionType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                StartDateTime = table.Column<string>("text", nullable: true),
                EndDateTime = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ParentEntityId = table.Column<Guid>("uuid", nullable: true),
                YearEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AcademicSessions", x => x.Id);
                table.ForeignKey(
                    "FK_AcademicSessions_AcademicSessions_ParentEntityId",
                    x => x.ParentEntityId,
                    "AcademicSessions",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    "FK_AcademicSessions_AcademicSessions_YearEntityId",
                    x => x.YearEntityId,
                    "AcademicSessions",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "Addresses",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                AddressId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                AddressType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Street = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                HouseNumber = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                HouseNumberSuffix = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                HouseNumberAddition = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                PostalCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                City = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                CountryCode = table.Column<string>("character varying(2)", maxLength: 2, nullable: true),
                Country = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                GeoJson = table.Column<string>("text", nullable: true),
                Latitude = table.Column<double>("double precision", nullable: true),
                Longitude = table.Column<double>("double precision", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Addresses", x => x.Id); });

        migrationBuilder.CreateTable(
            "CourseIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                CourseIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_CourseIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "Documents",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                DocumentId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DocumentType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Description = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                Url = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                MimeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Language = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                CreationDate = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Documents", x => x.Id); });

        migrationBuilder.CreateTable(
            "LearningComponentIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ComponentIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_LearningComponentIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "Organisations",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                OrganisationId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                ParentEntityId = table.Column<Guid>("uuid", nullable: true),
                RootEntityId = table.Column<Guid>("uuid", nullable: true),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                NameJson = table.Column<string>("text", nullable: false),
                DescriptionJson = table.Column<string>("text", nullable: true),
                LogoUrl = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                WebsiteUrl = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                ContactEmail = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ContactTelephone = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                OrganisationType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                ValidFrom = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ValidTo = table.Column<DateTime>("timestamp with time zone", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Organisations", x => x.Id);
                table.ForeignKey(
                    "FK_Organisations_Organisations_ParentEntityId",
                    x => x.ParentEntityId,
                    "Organisations",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    "FK_Organisations_Organisations_RootEntityId",
                    x => x.RootEntityId,
                    "Organisations",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "Persons",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                PersonId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                GivenName = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                AlternateName = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                PreferredName = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                SurnamePrefix = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Surname = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DisplayName = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Initials = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                IdCheckName = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                ActiveEnrolment = table.Column<bool>("boolean", nullable: false),
                DateOfBirth = table.Column<string>("text", nullable: true),
                CityOfBirth = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                CountryOfBirth = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Nationality = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DateOfNationality = table.Column<string>("text", nullable: true),
                Gender = table.Column<string>("character varying(1)", maxLength: 1, nullable: true),
                TitlePrefix = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                TitleSuffix = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Office = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                SecondaryEmail = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                TelephoneNumber = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                MobileNumber = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                PhotoSocial = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                PhotoOfficial = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                IceName = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                IcePhoneNumber = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                IceRelation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                AffiliationsJson = table.Column<string>("text", nullable: true),
                LanguageOfChoiceJson = table.Column<string>("text", nullable: true),
                AssignedNeedsJson = table.Column<string>("text", nullable: true),
                AddressJson = table.Column<string>("text", nullable: true),
                IceRelationJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Persons", x => x.Id); });

        migrationBuilder.CreateTable(
            "ProgrammeIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ProgrammeIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ProgrammeIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "Services",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ContactEmail = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Specification = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: false),
                Documentation = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                SupportedConsumersJson = table.Column<string>("text", nullable: true),
                SupportedOperationsJson = table.Column<string>("text", nullable: true),
                SupportedExpandsJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Services", x => x.Id); });

        migrationBuilder.CreateTable(
            "TestComponentIds",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ComponentIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_TestComponentIds", x => x.Id); });

        migrationBuilder.CreateTable(
            "AcademicSessionOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AcademicSessionOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_AcademicSessionOtherCodes_AcademicSessions_OwnerId",
                    x => x.OwnerId,
                    "AcademicSessions",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AddressOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AddressOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_AddressOtherCodes_Addresses_OwnerId",
                    x => x.OwnerId,
                    "Addresses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Buildings",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                BuildingId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Description = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                NameJson = table.Column<string>("text", nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                BuildingType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FloorCount = table.Column<int>("integer", nullable: true),
                RoomCount = table.Column<int>("integer", nullable: true),
                SurfaceInSquareMeters = table.Column<double>("double precision", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                AddressEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Buildings", x => x.Id);
                table.ForeignKey(
                    "FK_Buildings_Addresses_AddressEntityId",
                    x => x.AddressEntityId,
                    "Addresses",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "DocumentOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DocumentOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_DocumentOtherCodes_Documents_OwnerId",
                    x => x.OwnerId,
                    "Documents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Courses",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                CourseId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                NameJson = table.Column<string>("text", nullable: false),
                DescriptionJson = table.Column<string>("text", nullable: true),
                StudyLoadJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                Duration = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FirstStartDate = table.Column<DateTime>("timestamp with time zone", nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                FieldsOfStudy = table.Column<string>("character varying(6)", maxLength: 6, nullable: true),
                ValidFrom = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ValidTo = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Courses", x => x.Id);
                table.ForeignKey(
                    "FK_Courses_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "Groups",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                GroupId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                GroupType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                NameJson = table.Column<string>("text", nullable: false),
                DescriptionJson = table.Column<string>("text", nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                StartDateTime = table.Column<string>("text", nullable: true),
                EndDateTime = table.Column<string>("text", nullable: true),
                PersonCount = table.Column<int>("integer", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                AcademicSessionEntityId = table.Column<Guid>("uuid", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Groups", x => x.Id);
                table.ForeignKey(
                    "FK_Groups_AcademicSessions_AcademicSessionEntityId",
                    x => x.AcademicSessionEntityId,
                    "AcademicSessions",
                    "Id");
                table.ForeignKey(
                    "FK_Groups_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "LearningOutcomes",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                LearningOutcomeId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: false),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                FieldsOfStudy = table.Column<string>("character varying(6)", maxLength: 6, nullable: true),
                ComplexityLevelType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ComplexityLevel = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ValidFrom = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ValidTo = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningOutcomes", x => x.Id);
                table.ForeignKey(
                    "FK_LearningOutcomes_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "OrganisationOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrganisationOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_OrganisationOtherCodes_Organisations_OwnerId",
                    x => x.OwnerId,
                    "Organisations",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Programmes",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ProgrammeId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                NameJson = table.Column<string>("text", nullable: false),
                DescriptionJson = table.Column<string>("text", nullable: true),
                StudyLoadJson = table.Column<string>("text", nullable: true),
                QualificationJson = table.Column<string>("text", nullable: true),
                QualificationLevels = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                FieldsOfStudy = table.Column<string>("character varying(6)", maxLength: 6, nullable: true),
                Duration = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FirstStartDate = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ValidFrom = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ValidTo = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                ParentEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Programmes", x => x.Id);
                table.ForeignKey(
                    "FK_Programmes_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_Programmes_Programmes_ParentEntityId",
                    x => x.ParentEntityId,
                    "Programmes",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "PersonOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PersonOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_PersonOtherCodes_Persons_OwnerId",
                    x => x.OwnerId,
                    "Persons",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "BuildingOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BuildingOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_BuildingOtherCodes_Buildings_OwnerId",
                    x => x.OwnerId,
                    "Buildings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Rooms",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                RoomId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Name = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Description = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                NameJson = table.Column<string>("character varying(4096)", maxLength: 4096, nullable: true),
                DescriptionJson = table.Column<string>("character varying(4096)", maxLength: 4096, nullable: true),
                RoomType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Floor = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Wing = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                TotalSeats = table.Column<int>("integer", nullable: true),
                AvailableSeats = table.Column<int>("integer", nullable: true),
                Capacity = table.Column<int>("integer", nullable: true),
                SurfaceInSquareMeters = table.Column<double>("double precision", nullable: true),
                ExtJson = table.Column<string>("character varying(4096)", maxLength: 4096, nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                BuildingEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Rooms", x => x.Id);
                table.ForeignKey(
                    "FK_Rooms_Buildings_BuildingEntityId",
                    x => x.BuildingEntityId,
                    "Buildings",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "CourseCoordinators",
            table => new
            {
                CourseEntityId = table.Column<Guid>("uuid", nullable: false),
                PersonEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseCoordinators", x => new { x.CourseEntityId, x.PersonEntityId });
                table.ForeignKey(
                    "FK_CourseCoordinators_Courses_CourseEntityId",
                    x => x.CourseEntityId,
                    "Courses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseCoordinators_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CourseInstructors",
            table => new
            {
                CourseEntityId = table.Column<Guid>("uuid", nullable: false),
                PersonEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseInstructors", x => new { x.CourseEntityId, x.PersonEntityId });
                table.ForeignKey(
                    "FK_CourseInstructors_Courses_CourseEntityId",
                    x => x.CourseEntityId,
                    "Courses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseInstructors_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CourseOfferings",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                CourseOfferingId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                StartDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                EndDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodStartDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodEndDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                NameJson = table.Column<string>("text", nullable: true),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RosteringState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                MaxNumberStudents = table.Column<int>("integer", nullable: true),
                EnrolledNumberStudents = table.Column<int>("integer", nullable: true),
                PendingNumberStudents = table.Column<int>("integer", nullable: true),
                MinNumberStudents = table.Column<int>("integer", nullable: true),
                ResultValueType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Link = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                EnrolmentPeriodsJson = table.Column<string>("text", nullable: true),
                SupplementaryInformationJson = table.Column<string>("text", nullable: true),
                AddressesJson = table.Column<string>("text", nullable: true),
                PriceInformationJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                ResultExpected = table.Column<bool>("boolean", nullable: true),
                CourseEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                AcademicSessionEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseOfferings", x => x.Id);
                table.ForeignKey(
                    "FK_CourseOfferings_AcademicSessions_AcademicSessionEntityId",
                    x => x.AcademicSessionEntityId,
                    "AcademicSessions",
                    "Id");
                table.ForeignKey(
                    "FK_CourseOfferings_Courses_CourseEntityId",
                    x => x.CourseEntityId,
                    "Courses",
                    "Id");
                table.ForeignKey(
                    "FK_CourseOfferings_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "CourseOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_CourseOtherCodes_Courses_OwnerId",
                    x => x.OwnerId,
                    "Courses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningComponents",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ComponentId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                ComponentType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: false),
                DescriptionJson = table.Column<string>("text", nullable: true),
                StudyLoadJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                Duration = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                FieldsOfStudy = table.Column<string>("character varying(6)", maxLength: 6, nullable: true),
                ValidFrom = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ValidTo = table.Column<DateTime>("timestamp with time zone", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                CourseEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                ParentEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponents", x => x.Id);
                table.ForeignKey(
                    "FK_LearningComponents_Courses_CourseEntityId",
                    x => x.CourseEntityId,
                    "Courses",
                    "Id");
                table.ForeignKey(
                    "FK_LearningComponents_LearningComponents_ParentEntityId",
                    x => x.ParentEntityId,
                    "LearningComponents",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    "FK_LearningComponents_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "TestComponents",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ComponentId = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                ComponentType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                StudyLoadJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                Duration = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                CourseEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                ParentEntityId = table.Column<Guid>("uuid", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponents", x => x.Id);
                table.ForeignKey(
                    "FK_TestComponents_Courses_CourseEntityId",
                    x => x.CourseEntityId,
                    "Courses",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponents_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponents_TestComponents_ParentEntityId",
                    x => x.ParentEntityId,
                    "TestComponents",
                    "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            "GroupOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_GroupOtherCodes_Groups_OwnerId",
                    x => x.OwnerId,
                    "Groups",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Memberships",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                MembershipIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Role = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                StartDateTime = table.Column<string>("text", nullable: true),
                EndDateTime = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                GroupId = table.Column<Guid>("uuid", nullable: true),
                PersonId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Memberships", x => x.Id);
                table.ForeignKey(
                    "FK_Memberships_Groups_GroupId",
                    x => x.GroupId,
                    "Groups",
                    "Id");
                table.ForeignKey(
                    "FK_Memberships_Persons_PersonId",
                    x => x.PersonId,
                    "Persons",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "CourseEntityLearningOutcomeEntity",
            table => new
            {
                CoursesId = table.Column<Guid>("uuid", nullable: false),
                LearningOutcomesId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseEntityLearningOutcomeEntity",
                    x => new { x.CoursesId, x.LearningOutcomesId });
                table.ForeignKey(
                    "FK_CourseEntityLearningOutcomeEntity_Courses_CoursesId",
                    x => x.CoursesId,
                    "Courses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseEntityLearningOutcomeEntity_LearningOutcomes_Learning~",
                    x => x.LearningOutcomesId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningOutcomeHierarchy",
            table => new
            {
                ParentEntityId = table.Column<Guid>("uuid", nullable: false),
                ChildEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningOutcomeHierarchy", x => new { x.ParentEntityId, x.ChildEntityId });
                table.ForeignKey(
                    "FK_LearningOutcomeHierarchy_LearningOutcomes_ChildEntityId",
                    x => x.ChildEntityId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_LearningOutcomeHierarchy_LearningOutcomes_ParentEntityId",
                    x => x.ParentEntityId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningOutcomeOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningOutcomeOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_LearningOutcomeOtherCodes_LearningOutcomes_OwnerId",
                    x => x.OwnerId,
                    "LearningOutcomes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CourseEntityProgrammeEntity",
            table => new
            {
                CoursesId = table.Column<Guid>("uuid", nullable: false),
                ProgrammeEntitiesId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseEntityProgrammeEntity", x => new { x.CoursesId, x.ProgrammeEntitiesId });
                table.ForeignKey(
                    "FK_CourseEntityProgrammeEntity_Courses_CoursesId",
                    x => x.CoursesId,
                    "Courses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_CourseEntityProgrammeEntity_Programmes_ProgrammeEntitiesId",
                    x => x.ProgrammeEntitiesId,
                    "Programmes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProgrammeCoordinators",
            table => new
            {
                ProgrammeEntityId = table.Column<Guid>("uuid", nullable: false),
                PersonEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeCoordinators", x => new { x.ProgrammeEntityId, x.PersonEntityId });
                table.ForeignKey(
                    "FK_ProgrammeCoordinators_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ProgrammeCoordinators_Programmes_ProgrammeEntityId",
                    x => x.ProgrammeEntityId,
                    "Programmes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProgrammeInstructors",
            table => new
            {
                ProgrammeEntityId = table.Column<Guid>("uuid", nullable: false),
                PersonEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeInstructors", x => new { x.ProgrammeEntityId, x.PersonEntityId });
                table.ForeignKey(
                    "FK_ProgrammeInstructors_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ProgrammeInstructors_Programmes_ProgrammeEntityId",
                    x => x.ProgrammeEntityId,
                    "Programmes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProgrammeOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_ProgrammeOtherCodes_Programmes_OwnerId",
                    x => x.OwnerId,
                    "Programmes",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "RoomOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoomOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_RoomOtherCodes_Rooms_OwnerId",
                    x => x.OwnerId,
                    "Rooms",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CourseOfferingAssociations",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                CourseOfferingAssociationIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RemoteState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ResultJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                CourseOfferingEntityId = table.Column<Guid>("uuid", nullable: true),
                PersonEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseOfferingAssociations", x => x.Id);
                table.ForeignKey(
                    "FK_CourseOfferingAssociations_CourseOfferings_CourseOfferingEn~",
                    x => x.CourseOfferingEntityId,
                    "CourseOfferings",
                    "Id");
                table.ForeignKey(
                    "FK_CourseOfferingAssociations_Organisations_OrganisationEntity~",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_CourseOfferingAssociations_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "CourseOfferingOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseOfferingOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_CourseOfferingOtherCodes_CourseOfferings_OwnerId",
                    x => x.OwnerId,
                    "CourseOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "GroupCourseOfferings",
            table => new
            {
                CourseOfferingsId = table.Column<Guid>("uuid", nullable: false),
                GroupsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupCourseOfferings", x => new { x.CourseOfferingsId, x.GroupsId });
                table.ForeignKey(
                    "FK_GroupCourseOfferings_CourseOfferings_CourseOfferingsId",
                    x => x.CourseOfferingsId,
                    "CourseOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_GroupCourseOfferings_Groups_GroupsId",
                    x => x.GroupsId,
                    "Groups",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProgrammeOfferings",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ProgrammeOfferingIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RosteringState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                MaxNumberStudents = table.Column<int>("integer", nullable: true),
                EnrolledNumberStudents = table.Column<int>("integer", nullable: true),
                PendingNumberStudents = table.Column<int>("integer", nullable: true),
                MinNumberStudents = table.Column<int>("integer", nullable: true),
                ResultExpected = table.Column<bool>("boolean", nullable: true),
                ResultValueType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Link = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                EnrolmentPeriodsJson = table.Column<string>("text", nullable: true),
                SupplementaryInformationJson = table.Column<string>("text", nullable: true),
                StartDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                EndDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodStartDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodEndDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                AddressesJson = table.Column<string>("text", nullable: true),
                PriceInformationJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ProgrammeEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                AcademicSessionEntityId = table.Column<Guid>("uuid", nullable: true),
                CourseOfferingEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeOfferings", x => x.Id);
                table.ForeignKey(
                    "FK_ProgrammeOfferings_AcademicSessions_AcademicSessionEntityId",
                    x => x.AcademicSessionEntityId,
                    "AcademicSessions",
                    "Id");
                table.ForeignKey(
                    "FK_ProgrammeOfferings_CourseOfferings_CourseOfferingEntityId",
                    x => x.CourseOfferingEntityId,
                    "CourseOfferings",
                    "Id");
                table.ForeignKey(
                    "FK_ProgrammeOfferings_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_ProgrammeOfferings_Programmes_ProgrammeEntityId",
                    x => x.ProgrammeEntityId,
                    "Programmes",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "LearningComponentOfferings",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                LearningComponentOfferingIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RosteringState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                MaxNumberStudents = table.Column<int>("integer", nullable: true),
                EnrolledNumberStudents = table.Column<int>("integer", nullable: true),
                PendingNumberStudents = table.Column<int>("integer", nullable: true),
                MinNumberStudents = table.Column<int>("integer", nullable: true),
                ResultExpected = table.Column<bool>("boolean", nullable: true),
                ResultValueType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Link = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                EnrolmentPeriodsJson = table.Column<string>("text", nullable: true),
                SupplementaryInformationJson = table.Column<string>("text", nullable: true),
                StartDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                EndDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodStartDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodEndDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                AddressesJson = table.Column<string>("text", nullable: true),
                PriceInformationJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                LearningComponentEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                AcademicSessionEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOfferings", x => x.Id);
                table.ForeignKey(
                    "FK_LearningComponentOfferings_AcademicSessions_AcademicSession~",
                    x => x.AcademicSessionEntityId,
                    "AcademicSessions",
                    "Id");
                table.ForeignKey(
                    "FK_LearningComponentOfferings_LearningComponents_LearningCompo~",
                    x => x.LearningComponentEntityId,
                    "LearningComponents",
                    "Id");
                table.ForeignKey(
                    "FK_LearningComponentOfferings_Organisations_OrganisationEntity~",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "LearningComponentOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_LearningComponentOtherCodes_LearningComponents_OwnerId",
                    x => x.OwnerId,
                    "LearningComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferings",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                TestComponentOfferingIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                NameJson = table.Column<string>("text", nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RosteringState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Abbreviation = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                DescriptionJson = table.Column<string>("text", nullable: true),
                TeachingLanguagesJson = table.Column<string>("text", nullable: true),
                ModesOfDeliveryJson = table.Column<string>("text", nullable: true),
                MaxNumberStudents = table.Column<int>("integer", nullable: true),
                EnrolledNumberStudents = table.Column<int>("integer", nullable: true),
                PendingNumberStudents = table.Column<int>("integer", nullable: true),
                MinNumberStudents = table.Column<int>("integer", nullable: true),
                ResultExpected = table.Column<bool>("boolean", nullable: true),
                ResultValueType = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Link = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                EnrolmentPeriodsJson = table.Column<string>("text", nullable: true),
                SupplementaryInformationJson = table.Column<string>("text", nullable: true),
                StartDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                EndDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodStartDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                FlexibleEntryPeriodEndDateTime =
                    table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                AddressesJson = table.Column<string>("text", nullable: true),
                PriceInformationJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                CourseEntityId = table.Column<Guid>("uuid", nullable: true),
                TestComponentEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true),
                AcademicSessionEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferings", x => x.Id);
                table.ForeignKey(
                    "FK_TestComponentOfferings_AcademicSessions_AcademicSessionEnti~",
                    x => x.AcademicSessionEntityId,
                    "AcademicSessions",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferings_Courses_CourseEntityId",
                    x => x.CourseEntityId,
                    "Courses",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferings_Organisations_OrganisationEntityId",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferings_TestComponents_TestComponentEntityId",
                    x => x.TestComponentEntityId,
                    "TestComponents",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "TestComponentOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_TestComponentOtherCodes_TestComponents_OwnerId",
                    x => x.OwnerId,
                    "TestComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "MembershipOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MembershipOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_MembershipOtherCodes_Memberships_OwnerId",
                    x => x.OwnerId,
                    "Memberships",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "CourseOfferingAssociationOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CourseOfferingAssociationOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_CourseOfferingAssociationOtherCodes_CourseOfferingAssociati~",
                    x => x.OwnerId,
                    "CourseOfferingAssociations",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "GroupProgrammeOfferings",
            table => new
            {
                GroupsId = table.Column<Guid>("uuid", nullable: false),
                ProgrammeOfferingsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupProgrammeOfferings", x => new { x.GroupsId, x.ProgrammeOfferingsId });
                table.ForeignKey(
                    "FK_GroupProgrammeOfferings_Groups_GroupsId",
                    x => x.GroupsId,
                    "Groups",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_GroupProgrammeOfferings_ProgrammeOfferings_ProgrammeOfferin~",
                    x => x.ProgrammeOfferingsId,
                    "ProgrammeOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProgrammeOfferingAssociations",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ProgrammeOfferingAssociationIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RemoteState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ResultJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ProgrammeOfferingEntityId = table.Column<Guid>("uuid", nullable: true),
                PersonEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeOfferingAssociations", x => x.Id);
                table.ForeignKey(
                    "FK_ProgrammeOfferingAssociations_Organisations_OrganisationEnt~",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_ProgrammeOfferingAssociations_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id");
                table.ForeignKey(
                    "FK_ProgrammeOfferingAssociations_ProgrammeOfferings_ProgrammeO~",
                    x => x.ProgrammeOfferingEntityId,
                    "ProgrammeOfferings",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "ProgrammeOfferingOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeOfferingOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_ProgrammeOfferingOtherCodes_ProgrammeOfferings_OwnerId",
                    x => x.OwnerId,
                    "ProgrammeOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "GroupLearningComponentOfferings",
            table => new
            {
                GroupsId = table.Column<Guid>("uuid", nullable: false),
                LearningComponentOfferingsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupLearningComponentOfferings",
                    x => new { x.GroupsId, x.LearningComponentOfferingsId });
                table.ForeignKey(
                    "FK_GroupLearningComponentOfferings_Groups_GroupsId",
                    x => x.GroupsId,
                    "Groups",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_GroupLearningComponentOfferings_LearningComponentOfferings_~",
                    x => x.LearningComponentOfferingsId,
                    "LearningComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningComponentOfferingAssociations",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                LearningComponentOfferingAssociationIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RemoteState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ResultJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                LearningComponentOfferingEntityId = table.Column<Guid>("uuid", nullable: true),
                PersonEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOfferingAssociations", x => x.Id);
                table.ForeignKey(
                    "FK_LearningComponentOfferingAssociations_LearningComponentOffe~",
                    x => x.LearningComponentOfferingEntityId,
                    "LearningComponentOfferings",
                    "Id");
                table.ForeignKey(
                    "FK_LearningComponentOfferingAssociations_Organisations_Organis~",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_LearningComponentOfferingAssociations_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "LearningComponentOfferingCourseOfferings",
            table => new
            {
                CourseOfferingsId = table.Column<Guid>("uuid", nullable: false),
                LearningComponentOfferingEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOfferingCourseOfferings",
                    x => new { x.CourseOfferingsId, x.LearningComponentOfferingEntityId });
                table.ForeignKey(
                    "FK_LearningComponentOfferingCourseOfferings_CourseOfferings_Co~",
                    x => x.CourseOfferingsId,
                    "CourseOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_LearningComponentOfferingCourseOfferings_LearningComponentO~",
                    x => x.LearningComponentOfferingEntityId,
                    "LearningComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningComponentOfferingOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOfferingOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_LearningComponentOfferingOtherCodes_LearningComponentOfferi~",
                    x => x.OwnerId,
                    "LearningComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningComponentOfferingRooms",
            table => new
            {
                LearningComponentOfferingEntityId = table.Column<Guid>("uuid", nullable: false),
                RoomsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOfferingRooms",
                    x => new { x.LearningComponentOfferingEntityId, x.RoomsId });
                table.ForeignKey(
                    "FK_LearningComponentOfferingRooms_LearningComponentOfferings_L~",
                    x => x.LearningComponentOfferingEntityId,
                    "LearningComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_LearningComponentOfferingRooms_Rooms_RoomsId",
                    x => x.RoomsId,
                    "Rooms",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "GroupTestComponentOfferings",
            table => new
            {
                GroupsId = table.Column<Guid>("uuid", nullable: false),
                TestComponentOfferingsId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GroupTestComponentOfferings", x => new { x.GroupsId, x.TestComponentOfferingsId });
                table.ForeignKey(
                    "FK_GroupTestComponentOfferings_Groups_GroupsId",
                    x => x.GroupsId,
                    "Groups",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_GroupTestComponentOfferings_TestComponentOfferings_TestComp~",
                    x => x.TestComponentOfferingsId,
                    "TestComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingAssociations",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                TestComponentOfferingAssociationIdValue =
                    table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                PrimaryCodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                PrimaryCode = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                RemoteState = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ResultJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ExtJson = table.Column<string>("text", nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                TestComponentOfferingEntityId = table.Column<Guid>("uuid", nullable: true),
                PersonEntityId = table.Column<Guid>("uuid", nullable: true),
                OrganisationEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingAssociations", x => x.Id);
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociations_Organisations_Organisatio~",
                    x => x.OrganisationEntityId,
                    "Organisations",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociations_Persons_PersonEntityId",
                    x => x.PersonEntityId,
                    "Persons",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociations_TestComponentOfferings_Te~",
                    x => x.TestComponentOfferingEntityId,
                    "TestComponentOfferings",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingCourseOfferings",
            table => new
            {
                CourseOfferingsId = table.Column<Guid>("uuid", nullable: false),
                TestComponentOfferingEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingCourseOfferings",
                    x => new { x.CourseOfferingsId, x.TestComponentOfferingEntityId });
                table.ForeignKey(
                    "FK_TestComponentOfferingCourseOfferings_CourseOfferings_Course~",
                    x => x.CourseOfferingsId,
                    "CourseOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_TestComponentOfferingCourseOfferings_TestComponentOfferings~",
                    x => x.TestComponentOfferingEntityId,
                    "TestComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_TestComponentOfferingOtherCodes_TestComponentOfferings_Owne~",
                    x => x.OwnerId,
                    "TestComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingRooms",
            table => new
            {
                RoomsId = table.Column<Guid>("uuid", nullable: false),
                TestComponentOfferingEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingRooms",
                    x => new { x.RoomsId, x.TestComponentOfferingEntityId });
                table.ForeignKey(
                    "FK_TestComponentOfferingRooms_Rooms_RoomsId",
                    x => x.RoomsId,
                    "Rooms",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_TestComponentOfferingRooms_TestComponentOfferings_TestCompo~",
                    x => x.TestComponentOfferingEntityId,
                    "TestComponentOfferings",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ProgrammeOfferingAssociationOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ProgrammeOfferingAssociationOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_ProgrammeOfferingAssociationOtherCodes_ProgrammeOfferingAss~",
                    x => x.OwnerId,
                    "ProgrammeOfferingAssociations",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "LearningComponentOfferingAssociationOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LearningComponentOfferingAssociationOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_LearningComponentOfferingAssociationOtherCodes_LearningComp~",
                    x => x.OwnerId,
                    "LearningComponentOfferingAssociations",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingAssociationAttempts",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                AttemptIdValue = table.Column<string>("character varying(36)", maxLength: 36, nullable: false),
                Opportunity = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Attempt = table.Column<int>("integer", nullable: true),
                State = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                StartDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                EndDateTime = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Attendance = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                Irregularities = table.Column<string>("text", nullable: true),
                DocumentsJson = table.Column<string>("text", nullable: true),
                ResultJson = table.Column<string>("text", nullable: true),
                ConsumerJson = table.Column<string>("text", nullable: true),
                ConsumerKey = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                CreatedAt = table.Column<DateTime>("timestamp with time zone", nullable: false),
                ModifiedAt = table.Column<DateTime>("timestamp with time zone", nullable: true),
                IsActive = table.Column<bool>("boolean", nullable: false),
                TestComponentOfferingAssociationEntityId = table.Column<Guid>("uuid", nullable: true),
                CourseOfferingAssociationEntityId = table.Column<Guid>("uuid", nullable: true),
                CoordinatorEntityId = table.Column<Guid>("uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingAssociationAttempts", x => x.Id);
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociationAttempts_CourseOfferingAsso~",
                    x => x.CourseOfferingAssociationEntityId,
                    "CourseOfferingAssociations",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociationAttempts_Persons_Coordinato~",
                    x => x.CoordinatorEntityId,
                    "Persons",
                    "Id");
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociationAttempts_TestComponentOffer~",
                    x => x.TestComponentOfferingAssociationEntityId,
                    "TestComponentOfferingAssociations",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingAssociationOtherCodes",
            table => new
            {
                OwnerId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodeType = table.Column<string>("character varying(256)", maxLength: 256, nullable: false),
                Code = table.Column<string>("character varying(256)", maxLength: 256, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingAssociationOtherCodes", x => new { x.OwnerId, x.Id });
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociationOtherCodes_TestComponentOff~",
                    x => x.OwnerId,
                    "TestComponentOfferingAssociations",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TestComponentOfferingAssociationAttemptRooms",
            table => new
            {
                RoomsId = table.Column<Guid>("uuid", nullable: false),
                TestComponentOfferingAssociationAttemptEntityId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestComponentOfferingAssociationAttemptRooms",
                    x => new { x.RoomsId, x.TestComponentOfferingAssociationAttemptEntityId });
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociationAttemptRooms_Rooms_RoomsId",
                    x => x.RoomsId,
                    "Rooms",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_TestComponentOfferingAssociationAttemptRooms_TestComponentO~",
                    x => x.TestComponentOfferingAssociationAttemptEntityId,
                    "TestComponentOfferingAssociationAttempts",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_AcademicSessions_AcademicSessionId",
            "AcademicSessions",
            "AcademicSessionId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_AcademicSessions_ParentEntityId",
            "AcademicSessions",
            "ParentEntityId");

        migrationBuilder.CreateIndex(
            "IX_AcademicSessions_YearEntityId",
            "AcademicSessions",
            "YearEntityId");

        migrationBuilder.CreateIndex(
            "IX_Addresses_AddressId",
            "Addresses",
            "AddressId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Buildings_AddressEntityId",
            "Buildings",
            "AddressEntityId");

        migrationBuilder.CreateIndex(
            "IX_Buildings_BuildingId",
            "Buildings",
            "BuildingId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_CourseCoordinators_PersonEntityId",
            "CourseCoordinators",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseEntityLearningOutcomeEntity_LearningOutcomesId",
            "CourseEntityLearningOutcomeEntity",
            "LearningOutcomesId");

        migrationBuilder.CreateIndex(
            "IX_CourseEntityProgrammeEntity_ProgrammeEntitiesId",
            "CourseEntityProgrammeEntity",
            "ProgrammeEntitiesId");

        migrationBuilder.CreateIndex(
            "IX_CourseIds_CourseIdValue",
            "CourseIds",
            "CourseIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_CourseInstructors_PersonEntityId",
            "CourseInstructors",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseOfferingAssociations_CourseOfferingAssociationIdValue",
            "CourseOfferingAssociations",
            "CourseOfferingAssociationIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_CourseOfferingAssociations_CourseOfferingEntityId",
            "CourseOfferingAssociations",
            "CourseOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseOfferingAssociations_OrganisationEntityId",
            "CourseOfferingAssociations",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseOfferingAssociations_PersonEntityId",
            "CourseOfferingAssociations",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseOfferings_AcademicSessionEntityId",
            "CourseOfferings",
            "AcademicSessionEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseOfferings_CourseEntityId",
            "CourseOfferings",
            "CourseEntityId");

        migrationBuilder.CreateIndex(
            "IX_CourseOfferings_CourseOfferingId",
            "CourseOfferings",
            "CourseOfferingId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_CourseOfferings_OrganisationEntityId",
            "CourseOfferings",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_Courses_CourseId",
            "Courses",
            "CourseId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Courses_OrganisationEntityId",
            "Courses",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_Documents_DocumentId",
            "Documents",
            "DocumentId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_GroupCourseOfferings_GroupsId",
            "GroupCourseOfferings",
            "GroupsId");

        migrationBuilder.CreateIndex(
            "IX_GroupLearningComponentOfferings_LearningComponentOfferingsId",
            "GroupLearningComponentOfferings",
            "LearningComponentOfferingsId");

        migrationBuilder.CreateIndex(
            "IX_GroupProgrammeOfferings_ProgrammeOfferingsId",
            "GroupProgrammeOfferings",
            "ProgrammeOfferingsId");

        migrationBuilder.CreateIndex(
            "IX_Groups_AcademicSessionEntityId",
            "Groups",
            "AcademicSessionEntityId");

        migrationBuilder.CreateIndex(
            "IX_Groups_GroupId",
            "Groups",
            "GroupId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Groups_OrganisationEntityId",
            "Groups",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_GroupTestComponentOfferings_TestComponentOfferingsId",
            "GroupTestComponentOfferings",
            "TestComponentOfferingsId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentIds_ComponentIdValue",
            "LearningComponentIds",
            "ComponentIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferingAssociations_LearningComponentOff~1",
            "LearningComponentOfferingAssociations",
            "LearningComponentOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferingAssociations_LearningComponentOffe~",
            "LearningComponentOfferingAssociations",
            "LearningComponentOfferingAssociationIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferingAssociations_OrganisationEntityId",
            "LearningComponentOfferingAssociations",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferingAssociations_PersonEntityId",
            "LearningComponentOfferingAssociations",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferingCourseOfferings_LearningComponentO~",
            "LearningComponentOfferingCourseOfferings",
            "LearningComponentOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferingRooms_RoomsId",
            "LearningComponentOfferingRooms",
            "RoomsId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferings_AcademicSessionEntityId",
            "LearningComponentOfferings",
            "AcademicSessionEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferings_LearningComponentEntityId",
            "LearningComponentOfferings",
            "LearningComponentEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferings_LearningComponentOfferingIdValue",
            "LearningComponentOfferings",
            "LearningComponentOfferingIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_LearningComponentOfferings_OrganisationEntityId",
            "LearningComponentOfferings",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponents_ComponentId",
            "LearningComponents",
            "ComponentId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_LearningComponents_CourseEntityId",
            "LearningComponents",
            "CourseEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponents_OrganisationEntityId",
            "LearningComponents",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningComponents_ParentEntityId",
            "LearningComponents",
            "ParentEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningOutcomeHierarchy_ChildEntityId",
            "LearningOutcomeHierarchy",
            "ChildEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningOutcomes_LearningOutcomeId",
            "LearningOutcomes",
            "LearningOutcomeId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_LearningOutcomes_OrganisationEntityId",
            "LearningOutcomes",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_LearningOutcomes_PrimaryCode",
            "LearningOutcomes",
            "PrimaryCode");

        migrationBuilder.CreateIndex(
            "IX_Memberships_GroupId",
            "Memberships",
            "GroupId");

        migrationBuilder.CreateIndex(
            "IX_Memberships_MembershipIdValue",
            "Memberships",
            "MembershipIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Memberships_PersonId",
            "Memberships",
            "PersonId");

        migrationBuilder.CreateIndex(
            "IX_Organisations_OrganisationId",
            "Organisations",
            "OrganisationId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Organisations_ParentEntityId",
            "Organisations",
            "ParentEntityId");

        migrationBuilder.CreateIndex(
            "IX_Organisations_RootEntityId",
            "Organisations",
            "RootEntityId");

        migrationBuilder.CreateIndex(
            "IX_Persons_PersonId",
            "Persons",
            "PersonId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Persons_PrimaryCodeType_PrimaryCode",
            "Persons",
            ["PrimaryCodeType", "PrimaryCode"],
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ProgrammeCoordinators_PersonEntityId",
            "ProgrammeCoordinators",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeIds_ProgrammeIdValue",
            "ProgrammeIds",
            "ProgrammeIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ProgrammeInstructors_PersonEntityId",
            "ProgrammeInstructors",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferingAssociations_OrganisationEntityId",
            "ProgrammeOfferingAssociations",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferingAssociations_PersonEntityId",
            "ProgrammeOfferingAssociations",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferingAssociations_ProgrammeOfferingAssociationI~",
            "ProgrammeOfferingAssociations",
            "ProgrammeOfferingAssociationIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferingAssociations_ProgrammeOfferingEntityId",
            "ProgrammeOfferingAssociations",
            "ProgrammeOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferings_AcademicSessionEntityId",
            "ProgrammeOfferings",
            "AcademicSessionEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferings_CourseOfferingEntityId",
            "ProgrammeOfferings",
            "CourseOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferings_OrganisationEntityId",
            "ProgrammeOfferings",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferings_ProgrammeEntityId",
            "ProgrammeOfferings",
            "ProgrammeEntityId");

        migrationBuilder.CreateIndex(
            "IX_ProgrammeOfferings_ProgrammeOfferingIdValue",
            "ProgrammeOfferings",
            "ProgrammeOfferingIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Programmes_OrganisationEntityId",
            "Programmes",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_Programmes_ParentEntityId",
            "Programmes",
            "ParentEntityId");

        migrationBuilder.CreateIndex(
            "IX_Programmes_ProgrammeId",
            "Programmes",
            "ProgrammeId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Rooms_BuildingEntityId",
            "Rooms",
            "BuildingEntityId");

        migrationBuilder.CreateIndex(
            "IX_Rooms_RoomId",
            "Rooms",
            "RoomId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Services_ContactEmail",
            "Services",
            "ContactEmail",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponentIds_ComponentIdValue",
            "TestComponentIds",
            "ComponentIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociationAttemptRooms_TestComponentO~",
            "TestComponentOfferingAssociationAttemptRooms",
            "TestComponentOfferingAssociationAttemptEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociationAttempts_AttemptIdValue",
            "TestComponentOfferingAssociationAttempts",
            "AttemptIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociationAttempts_CoordinatorEntityId",
            "TestComponentOfferingAssociationAttempts",
            "CoordinatorEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociationAttempts_CourseOfferingAsso~",
            "TestComponentOfferingAssociationAttempts",
            "CourseOfferingAssociationEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociationAttempts_TestComponentOffer~",
            "TestComponentOfferingAssociationAttempts",
            "TestComponentOfferingAssociationEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociations_OrganisationEntityId",
            "TestComponentOfferingAssociations",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociations_PersonEntityId",
            "TestComponentOfferingAssociations",
            "PersonEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociations_TestComponentOfferingAsso~",
            "TestComponentOfferingAssociations",
            "TestComponentOfferingAssociationIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingAssociations_TestComponentOfferingEnti~",
            "TestComponentOfferingAssociations",
            "TestComponentOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingCourseOfferings_TestComponentOfferingE~",
            "TestComponentOfferingCourseOfferings",
            "TestComponentOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferingRooms_TestComponentOfferingEntityId",
            "TestComponentOfferingRooms",
            "TestComponentOfferingEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferings_AcademicSessionEntityId",
            "TestComponentOfferings",
            "AcademicSessionEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferings_CourseEntityId",
            "TestComponentOfferings",
            "CourseEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferings_OrganisationEntityId",
            "TestComponentOfferings",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferings_TestComponentEntityId",
            "TestComponentOfferings",
            "TestComponentEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponentOfferings_TestComponentOfferingIdValue",
            "TestComponentOfferings",
            "TestComponentOfferingIdValue",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponents_ComponentId",
            "TestComponents",
            "ComponentId",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_TestComponents_CourseEntityId",
            "TestComponents",
            "CourseEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponents_OrganisationEntityId",
            "TestComponents",
            "OrganisationEntityId");

        migrationBuilder.CreateIndex(
            "IX_TestComponents_ParentEntityId",
            "TestComponents",
            "ParentEntityId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "AcademicSessionOtherCodes");

        migrationBuilder.DropTable(
            "AddressOtherCodes");

        migrationBuilder.DropTable(
            "BuildingOtherCodes");

        migrationBuilder.DropTable(
            "CourseCoordinators");

        migrationBuilder.DropTable(
            "CourseEntityLearningOutcomeEntity");

        migrationBuilder.DropTable(
            "CourseEntityProgrammeEntity");

        migrationBuilder.DropTable(
            "CourseIds");

        migrationBuilder.DropTable(
            "CourseInstructors");

        migrationBuilder.DropTable(
            "CourseOfferingAssociationOtherCodes");

        migrationBuilder.DropTable(
            "CourseOfferingOtherCodes");

        migrationBuilder.DropTable(
            "CourseOtherCodes");

        migrationBuilder.DropTable(
            "DocumentOtherCodes");

        migrationBuilder.DropTable(
            "GroupCourseOfferings");

        migrationBuilder.DropTable(
            "GroupLearningComponentOfferings");

        migrationBuilder.DropTable(
            "GroupOtherCodes");

        migrationBuilder.DropTable(
            "GroupProgrammeOfferings");

        migrationBuilder.DropTable(
            "GroupTestComponentOfferings");

        migrationBuilder.DropTable(
            "LearningComponentIds");

        migrationBuilder.DropTable(
            "LearningComponentOfferingAssociationOtherCodes");

        migrationBuilder.DropTable(
            "LearningComponentOfferingCourseOfferings");

        migrationBuilder.DropTable(
            "LearningComponentOfferingOtherCodes");

        migrationBuilder.DropTable(
            "LearningComponentOfferingRooms");

        migrationBuilder.DropTable(
            "LearningComponentOtherCodes");

        migrationBuilder.DropTable(
            "LearningOutcomeHierarchy");

        migrationBuilder.DropTable(
            "LearningOutcomeOtherCodes");

        migrationBuilder.DropTable(
            "MembershipOtherCodes");

        migrationBuilder.DropTable(
            "OrganisationOtherCodes");

        migrationBuilder.DropTable(
            "PersonOtherCodes");

        migrationBuilder.DropTable(
            "ProgrammeCoordinators");

        migrationBuilder.DropTable(
            "ProgrammeIds");

        migrationBuilder.DropTable(
            "ProgrammeInstructors");

        migrationBuilder.DropTable(
            "ProgrammeOfferingAssociationOtherCodes");

        migrationBuilder.DropTable(
            "ProgrammeOfferingOtherCodes");

        migrationBuilder.DropTable(
            "ProgrammeOtherCodes");

        migrationBuilder.DropTable(
            "RoomOtherCodes");

        migrationBuilder.DropTable(
            "Services");

        migrationBuilder.DropTable(
            "TestComponentIds");

        migrationBuilder.DropTable(
            "TestComponentOfferingAssociationAttemptRooms");

        migrationBuilder.DropTable(
            "TestComponentOfferingAssociationOtherCodes");

        migrationBuilder.DropTable(
            "TestComponentOfferingCourseOfferings");

        migrationBuilder.DropTable(
            "TestComponentOfferingOtherCodes");

        migrationBuilder.DropTable(
            "TestComponentOfferingRooms");

        migrationBuilder.DropTable(
            "TestComponentOtherCodes");

        migrationBuilder.DropTable(
            "Documents");

        migrationBuilder.DropTable(
            "LearningComponentOfferingAssociations");

        migrationBuilder.DropTable(
            "LearningOutcomes");

        migrationBuilder.DropTable(
            "Memberships");

        migrationBuilder.DropTable(
            "ProgrammeOfferingAssociations");

        migrationBuilder.DropTable(
            "TestComponentOfferingAssociationAttempts");

        migrationBuilder.DropTable(
            "Rooms");

        migrationBuilder.DropTable(
            "LearningComponentOfferings");

        migrationBuilder.DropTable(
            "Groups");

        migrationBuilder.DropTable(
            "ProgrammeOfferings");

        migrationBuilder.DropTable(
            "CourseOfferingAssociations");

        migrationBuilder.DropTable(
            "TestComponentOfferingAssociations");

        migrationBuilder.DropTable(
            "Buildings");

        migrationBuilder.DropTable(
            "LearningComponents");

        migrationBuilder.DropTable(
            "Programmes");

        migrationBuilder.DropTable(
            "CourseOfferings");

        migrationBuilder.DropTable(
            "Persons");

        migrationBuilder.DropTable(
            "TestComponentOfferings");

        migrationBuilder.DropTable(
            "Addresses");

        migrationBuilder.DropTable(
            "AcademicSessions");

        migrationBuilder.DropTable(
            "TestComponents");

        migrationBuilder.DropTable(
            "Courses");

        migrationBuilder.DropTable(
            "Organisations");
    }
}
