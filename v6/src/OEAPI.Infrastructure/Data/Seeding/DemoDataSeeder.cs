using System.Text;
using Microsoft.EntityFrameworkCore;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;

namespace OEAPI.Infrastructure.Data.Seeding;

/// <summary>
///     Seeds a small but genuinely connected demo dataset - covering every top-level resource this API
///     exposes, with real hierarchy (organisation parent/child/root, academic year/semester, learning
///     outcome parent/child), real many-to-many links (course/programme coordinators and instructors,
///     group-to-offering links, room assignments, cross-entity <c>otherCodes</c>), and at least two rows
///     on every resource that's actually surfaced by the API - not just one isolated row per table. This
///     is deliberately more than the minimum needed to "have something to look at": it's meant to be
///     enough for an external spec-conformance prober (which discovers data by walking the graph - list
///     an endpoint, follow a nested/expand link, list the next one) to find real data everywhere it
///     looks, not dead ends. Opt-in via <c>Database:SeedDemoData</c> (see <c>Program.cs</c>). Every
///     resource's own identifier field is a real GUID (spec's <c>Identifier</c>/<c>AssociationId</c>
///     schemas declare <c>format: uuid</c>) rather than a human-readable string; the <c>DEMO-*</c>
///     primary codes are what makes this data recognizable as sample data instead, since
///     <c>primaryCode</c> is spec'd as a free-form business code, not a UUID.
/// </summary>
public static class DemoDataSeeder
{
    /// <summary>
    ///     Seeds the demo dataset if the database has no data yet. Idempotent: checks for any existing
    ///     <see cref="OrganisationEntity" /> row first and does nothing if one is found, so this is safe
    ///     to call on every startup without duplicating rows or overwriting real data.
    /// </summary>
    public static async Task SeedAsync(OEAPIDbContext dbContext, CancellationToken cancellationToken = default)
    {
        if (await dbContext.Organisations.AnyAsync(cancellationToken).ConfigureAwait(false)) return;

        // since=<today> is now the implicit default on every offering-collection endpoint (spec's
        // own documented behaviour - see add-since-default-today-filter) - at least one real
        // offering of each type needs a linked academic session that starts in the future, or every
        // one of those endpoints would return an empty list by default against this fixture data,
        // which would also starve every other conformance check that resolves a baseline item from
        // an unfiltered GET. Anchored to "next 1 September" rather than a fixed date so this keeps
        // working no matter when the seeder actually runs - always within the next 12 months, so
        // always in the future relative to "now".
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset nextAcademicYearStart = new DateTimeOffset(
            now.Month >= 9 ? now.Year + 1 : now.Year, 9, 1, 0, 0, 0, TimeSpan.FromHours(1));

        // ---------------------------------------------------------------------------------------
        // Organisations - a root university and a child faculty, so parent/children/root expand
        // all have something real to return.
        // ---------------------------------------------------------------------------------------
        Guid orgId = Guid.CreateVersion7();
        OrganisationEntity org = new()
        {
            Id = orgId,
            OrganisationId = orgId.ToString(),
            PrimaryCodeType = "organisation_id",
            PrimaryCode = "DEMO-UNI",
            OrganisationType = "root",
            NameJson = "[{\"language\":\"en\",\"value\":\"Demo University\"}]",
            // Address.additional/postCode/geolocation/ext have no example anywhere else in the
            // seeder (faculty's own address below only sets the base fields) - covered here.
            AddressesJson =
                "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Main entrance, north wing\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            OtherCodes = { new OtherCodeEntity { CodeType = "institution_code", Code = "1AB2" } },
            // Fixture data for the ext/consumer read-only checks planned for the external OOAPI
            // conformance test tool - every other seeded entity leaves both null, so without this
            // there is nothing for those checks to exercise.
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            // Both ConsumerKey (row-level visibility, checked by GenericEntityController.
            // IsConsumerMatchAsync) and ConsumerJson's own embedded consumerKey (gates the
            // `consumer` field itself, via ConsumerDataResolver) need to agree - two different,
            // independently-checked places that store the same value.
            ConsumerKey = "nl-edusites",
            ConsumerJson =
                "{\"consumerKey\":\"nl-edusites\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}"
        };
        dbContext.Organisations.Add(org);

        Guid facultyId = Guid.CreateVersion7();
        OrganisationEntity faculty = new()
        {
            Id = facultyId,
            OrganisationId = facultyId.ToString(),
            PrimaryCodeType = "organisation_id",
            PrimaryCode = "DEMO-FAC-CS",
            OrganisationType = "institute",
            NameJson = "[{\"language\":\"en\",\"value\":\"Faculty of Computer Science\"}]",
            ShortName = "FCS",
            DescriptionJson =
                "[{\"language\":\"en\",\"value\":\"Faculty of Computer Science conducts education and research in computing, software engineering, and data science.\"}]",
            Link = "https://demo-university.example/faculty-of-computer-science",
            Logo = "https://demo-university.example/assets/fcs-logo.svg",
            AddressesJson =
                "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"}}]",
            ParentEntityId = org.Id,
            RootEntityId = org.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "CS-100" } }
        };
        dbContext.Organisations.Add(faculty);

        // ---------------------------------------------------------------------------------------
        // Academic sessions - a year and one of its semesters, so parent/children/year expand all
        // have something real to return.
        // ---------------------------------------------------------------------------------------
        Guid academicSessionId = Guid.CreateVersion7();
        AcademicSessionEntity academicSession = new()
        {
            Id = academicSessionId,
            AcademicSessionId = academicSessionId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-AS1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Academic Year 2025-2026\"}]",
            AcademicSessionType = "academic_year",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2026-08-31T23:59:59+01:00",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "AY2526" } }
        };
        dbContext.AcademicSessions.Add(academicSession);

        Guid semesterId = Guid.CreateVersion7();
        AcademicSessionEntity semester = new()
        {
            Id = semesterId,
            AcademicSessionId = semesterId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-AS2",
            NameJson = "[{\"language\":\"en\",\"value\":\"Semester 1, 2025-2026\"}]",
            AcademicSessionType = "semester",
            Abbreviation = "FALL-2025",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2026-01-31T23:59:59+01:00",
            ParentEntityId = academicSession.Id,
            YearEntityId = academicSession.Id,
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            ConsumerKey = "nl-edusites",
            ConsumerJson =
                "{\"consumerKey\":\"nl-edusites\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}"
        };
        dbContext.AcademicSessions.Add(semester);

        // An upcoming session, always in the future relative to "now" (see nextAcademicYearStart
        // above) - the anchor everything below in the "Offerings" section reuses to guarantee at
        // least one real offering of every type satisfies the new since=<today> default.
        Guid upcomingSemesterId = Guid.CreateVersion7();
        AcademicSessionEntity upcomingSemester = new()
        {
            Id = upcomingSemesterId,
            AcademicSessionId = upcomingSemesterId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-AS3",
            NameJson =
                $"[{{\"language\":\"en\",\"value\":\"Semester 1, {nextAcademicYearStart.Year}-{nextAcademicYearStart.Year + 1}\"}}]",
            AcademicSessionType = "semester",
            StartDateTime = nextAcademicYearStart.ToString("yyyy-MM-ddTHH:mm:sszzz"),
            EndDateTime = nextAcademicYearStart.AddMonths(5).AddSeconds(-1).ToString("yyyy-MM-ddTHH:mm:sszzz"),
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "UPCOMING-SEM" } }
        };
        dbContext.AcademicSessions.Add(upcomingSemester);

        // ---------------------------------------------------------------------------------------
        // Persons - three, so coordinators/instructors and student associations aren't all the
        // same one person.
        // ---------------------------------------------------------------------------------------
        string eduXchangePersonConsumerJson =
            "{\"consumerKey\":\"eduxchange\",\"enrolments\":[{\"crohoCreboCode\":\"34401\",\"name\":\"Computer Science\",\"phase\":\"bachelor\",\"modeOfStudy\":\"full-time\"}],\"institutionBRINCode\":\"12AB\"}";

        Guid personId = Guid.CreateVersion7();
        PersonEntity person = new()
        {
            Id = personId,
            PersonId = personId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PER1",
            GivenName = "Sam",
            Surname = "Karimi",
            Email = "sam.karimi@example.org",
            ConsumerJson = eduXchangePersonConsumerJson,
            ConsumerKey = "eduxchange",
            ActiveEnrolment = true,
            DateOfBirth = "2003-09-30",
            CityOfBirth = "Groningen",
            Gender = "f",
            LanguageOfChoiceJson = "[\"en-GB\",\"nl-NL\"]",
            CountryOfBirthJson = "{\"iso3166-1-alpha2\":\"NL\",\"iso3166-1-alpha3\":\"NLD\"}",
            NationalityJson = "{\"iso3166-1-alpha2\":\"NL\",\"iso3166-1-alpha3\":\"NLD\"}",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs - covered here.
            AddressJson =
                "{\"addressType\":\"postal\",\"street\":\"Zernikecomplex\",\"streetNumber\":\"12\",\"additional\":[{\"language\":\"en\",\"value\":\"Student housing, block C\"}],\"postCode\":\"9747 AG\",\"city\":\"Groningen\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":53.219494,\"longitude\":6.567925},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}",
            AssignedNeedsJson =
                "[{\"code\":\"ExtraTimeOnlyMaths25%\",\"description\":[{\"language\":\"en-GB\",\"value\":\"Extra time for tests\"}],\"startDateTime\":\"2025-09-01T00:00:00+01:00\",\"endDateTime\":\"2026-07-01T00:00:00+01:00\"}]",
            AffiliationsJson = "[\"student\"]",
            OtherCodes = { new OtherCodeEntity { CodeType = "student_number", Code = "S1234567" } }
        };
        dbContext.Persons.Add(person);

        // Second person with a distinct name/affiliation, doubling as the course/programme
        // coordinator and instructor below, so the q/affiliations filters have something to
        // discriminate between and coordinators/instructors expand has real data.
        Guid person2Id = Guid.CreateVersion7();
        PersonEntity person2 = new()
        {
            Id = person2Id,
            PersonId = person2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PER2",
            GivenName = "Jordan",
            Surname = "Lee",
            AlternateName = "Jordy",
            PreferredName = "Jordan",
            DisplayName = "Jordan Lee",
            Initials = "J.L.",
            TitlePrefix = "dr",
            TitleSuffix = "PhD",
            Office = "Snijders Building, room 2.14",
            Email = "jordan.lee@example.org",
            TelephoneNumber = "+31 43 388 1234",
            MobileNumber = "+31 6 1234 5678",
            AffiliationsJson = "[\"employee\"]",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "E7654321" } }
        };
        dbContext.Persons.Add(person2);

        // Third person - a second student, so a second course offering/group/association isn't
        // just a duplicate of the first person's data.
        Guid person3Id = Guid.CreateVersion7();
        PersonEntity person3 = new()
        {
            Id = person3Id,
            PersonId = person3Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PER3",
            GivenName = "Priya",
            Surname = "Singh",
            SurnamePrefix = "de",
            IdCheckName = "de Singh, Priya, s7654321",
            ActiveEnrolment = true,
            DateOfNationality = "2010-06-15",
            Email = "priya.singh@example.org",
            SecondaryEmail = "priya.singh.private@example.org",
            PhotoSocial = "https://example.org/photos/priya-singh-social.jpg",
            PhotoOfficial = "https://example.org/photos/priya-singh-official.jpg",
            IceName = "Raj Singh",
            IcePhoneNumber = "+31 6 8765 4321",
            IceRelationJson = "\"parent\"",
            AffiliationsJson = "[\"student\"]",
            OtherCodes = { new OtherCodeEntity { CodeType = "student_number", Code = "S7654321" } }
        };
        dbContext.Persons.Add(person3);

        // ---------------------------------------------------------------------------------------
        // Courses.
        // ---------------------------------------------------------------------------------------
        string rioCourseConsumerJson =
            "{\"consumerKey\":\"rio\",\"educationOffererCode\":\"123A321\",\"educationLocationCode\":\"334X123\",\"consentParticipationSTAP\":\"permission_granted\",\"jointPartnerCodes\":[\"123A123\"],\"foreignPartners\":[\"Harvard University\"]}";

        Guid courseId = Guid.CreateVersion7();
        CourseEntity course = new()
        {
            Id = courseId,
            CourseId = courseId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-CRS1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Introduction to Data Science\"}]",
            Abbreviation = "DS101",
            DescriptionJson =
                "[{\"language\":\"en\",\"value\":\"An introduction to statistical methods, data wrangling, and machine learning fundamentals, with hands-on assignments using real datasets.\"}]",
            StudyLoadJson = "[{\"studyLoadUnit\":\"ects\",\"value\":6}]",
            ModesOfDeliveryJson = "[\"blended\"]",
            Duration = "P16W",
            FirstStartDate = new DateTime(2020, 9, 28, 8, 30, 0, DateTimeKind.Utc),
            TeachingLanguagesJson = "[\"en-GB\",\"nl-NL\"]",
            FieldsOfStudy = "0613",
            OrganisationEntityId = org.Id,
            ConsumerJson = rioCourseConsumerJson,
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            Link = "https://osiris.example.org/course/DEMO-CRS1",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs on Course specifically.
            AddressesJson =
                "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Lecture wing, ground floor\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // No example of Course's own validFrom/validTo anywhere else.
            ValidFrom = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            ValidTo = new DateTime(2030, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            Level = "master",
            ResourcesJson = "[\"Course reader (PDF)\",\"Online exercise platform\"]",
            AssessmentJson = "[{\"language\":\"en-GB\",\"value\":\"Written exam on campus\"}]",
            EnrolmentJson = "[{\"language\":\"en-GB\",\"value\":\"Enrolment through the student information system\"}]",
            AdmissionRequirementsJson =
                "[{\"language\":\"en-GB\",\"value\":\"Students need to be enrolled at a qualifying institution\"}]",
            QualificationRequirementsJson =
                "[{\"language\":\"en-GB\",\"value\":\"Students must obtain the required number of credits\"}]",
            SupplementaryInformationJson =
                "[{\"role\":\"badge\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Top rated course\"}]}]",
            FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc),
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "INFOB3DS" } }
        };
        course.CourseCoordinatorEntities.Add(person2);
        course.CourseInstructorEntities.Add(person2);
        dbContext.Courses.Add(course);

        // Second course tagged for a different consumer, to demonstrate the consumer-specific-data
        // (RIO/eduXchange) feature discriminating between consumers.
        Guid course2Id = Guid.CreateVersion7();
        CourseEntity course2 = new()
        {
            Id = course2Id,
            CourseId = course2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-CRS2",
            NameJson = "[{\"language\":\"en\",\"value\":\"Sustainability and Global Challenges (eduXchange)\"}]",
            OrganisationEntityId = faculty.Id,
            ConsumerJson =
                "{\"consumerKey\":\"eduxchange\",\"alliances\":[{\"name\":\"ewuu\",\"theme\":\"sustainability\"}]}",
            ConsumerKey = "eduxchange",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "INFOB3SC" } }
        };
        dbContext.Courses.Add(course2);

        string eduXchangeProgrammeConsumerJson =
            "{\"consumerKey\":\"eduxchange\",\"alliances\":[{\"name\":\"ewuu\",\"theme\":\"sustainability\",\"selection\":true,\"type\":\"broadening\",\"visibleForOwnStudents\":true,\"enrolmentForOwnStudents\":\"broker\"}]}";

        Guid programmeId = Guid.CreateVersion7();
        ProgrammeEntity programme = new()
        {
            Id = programmeId,
            ProgrammeId = programmeId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PRG1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Data Science\"}]",
            Abbreviation = "DS-BSc",
            DescriptionJson =
                "[{\"language\":\"en\",\"value\":\"A three-year bachelor's programme covering statistics, computer science, and their application to real-world data-driven problems.\"}]",
            TeachingLanguagesJson = "[\"en-GB\",\"nl-NL\"]",
            StudyLoadJson = "[{\"studyLoadUnit\":\"ects\",\"value\":180}]",
            QualificationDesignationsJson = "[\"of Science\"]",
            Duration = "P3Y",
            FirstStartDateTime = new DateTime(2020, 9, 1, 8, 30, 0, DateTimeKind.Utc),
            LevelOfQualification = "eqf_6",
            FieldsOfStudy = "0613",
            EnrolmentJson =
                "[{\"language\":\"en-GB\",\"value\":\"Enrolment through the student information system\"}]",
            ResourcesJson = "[\"Programme handbook\",\"Online study guide\"]",
            AssessmentJson = "[{\"language\":\"en-GB\",\"value\":\"Combination of written exams and project assessments\"}]",
            AdmissionRequirementsJson =
                "[{\"language\":\"en-GB\",\"value\":\"Students need a relevant pre-university diploma or equivalent\"}]",
            QualificationRequirementsJson =
                "[{\"language\":\"en-GB\",\"value\":\"Students must obtain the required number of credits\"}]",
            FormalDocument = "diploma",
            Link = "https://osiris.example.org/programme/DEMO-PRG1",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs on Programme specifically.
            AddressesJson =
                "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Programme office, second floor\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // No example of Programme's own validFrom/validTo anywhere else.
            ValidFrom = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            ValidTo = new DateTime(2030, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            SupplementaryInformationJson =
                "[{\"role\":\"badge\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Top rated programme\"}]}]",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OrganisationEntityId = org.Id,
            ProgrammeType = "programme",
            ModeOfStudy = "full_time",
            ModesOfDeliveryJson = "[\"presential\"]",
            QualificationAwarded = "bachelor",
            ConsumerJson = eduXchangeProgrammeConsumerJson,
            ConsumerKey = "eduxchange",
            Level = "master",
            FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc),
            OtherCodes = { new OtherCodeEntity { CodeType = "programme_code", Code = "60301" } }
        };
        programme.Coordinators.Add(person2);
        programme.Instructors.Add(person2);
        dbContext.Programmes.Add(programme);

        // Sub-programme, demonstrating GET /programmes/{id}/programmes.
        Guid subProgrammeId = Guid.CreateVersion7();
        ProgrammeEntity subProgramme = new()
        {
            Id = subProgrammeId,
            ProgrammeId = subProgrammeId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PRG2",
            NameJson = "[{\"language\":\"en\",\"value\":\"Data Science - Year 1\"}]",
            OrganisationEntityId = org.Id,
            ParentEntityId = programme.Id,
            ProgrammeType = "specialisation",
            OtherCodes = { new OtherCodeEntity { CodeType = "programme_code", Code = "60301-Y1" } }
        };
        dbContext.Programmes.Add(subProgramme);

        // Link both courses to the programme, demonstrating GET /programmes/{id}/courses.
        course.ProgrammeEntities.Add(programme);
        course2.ProgrammeEntities.Add(subProgramme);

        // ---------------------------------------------------------------------------------------
        // Timeline overrides - historical/future alternate snapshots, demonstrating
        // GET /courses/{id}?returnTimelineOverrides=true and the programme equivalent.
        // ---------------------------------------------------------------------------------------
        TimelineOverrideCourseEntity courseTimelineOverride = new()
        {
            CourseEntityId = course.Id,
            ValidFrom = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            ValidTo = new DateTime(2023, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-CRS1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Introduction to Data Analysis\"}]",
            Abbreviation = "OLD-DS101",
            Level = "bachelor",
            StudyLoadJson = "[{\"studyLoadUnit\":\"ects\",\"value\":5}]",
            OrganisationEntityId = org.Id
        };
        courseTimelineOverride.Coordinators.Add(person2);
        courseTimelineOverride.Instructors.Add(person2);
        courseTimelineOverride.Programmes.Add(programme);
        dbContext.CourseTimelineOverrides.Add(courseTimelineOverride);

        TimelineOverrideProgrammeEntity programmeTimelineOverride = new()
        {
            ProgrammeEntityId = programme.Id,
            ValidFrom = new DateTime(2027, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PRG1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Data Science and Artificial Intelligence\"}]",
            ProgrammeType = "programme",
            QualificationAwarded = "master",
            // Genuinely multi-value example - demonstrates the spec's own documented
            // interdisciplinary-programme case, not just a single-element array.
            QualificationDesignationsJson = "[\"of Science\",\"of Artificial Intelligence\"]",
            LevelOfQualification = "eqf_7",
            Duration = "P1Y",
            OrganisationEntityId = org.Id
        };
        programmeTimelineOverride.Coordinators.Add(person2);
        programmeTimelineOverride.Instructors.Add(person2);
        programmeTimelineOverride.Children.Add(subProgramme);
        dbContext.ProgrammeTimelineOverrides.Add(programmeTimelineOverride);

        // ---------------------------------------------------------------------------------------
        // Groups - one linked to every offering type (below, once those exist) and a second,
        // simpler group, so /groups always returns more than one row.
        // ---------------------------------------------------------------------------------------
        Guid groupId = Guid.CreateVersion7();
        GroupEntity group = new()
        {
            Id = groupId,
            GroupId = groupId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-GRP1",
            GroupType = "group",
            NameJson = "[{\"language\":\"en\",\"value\":\"Data Science Cohort 2026\"}]",
            DescriptionJson =
                "[{\"language\":\"en\",\"value\":\"All students enrolled in the Data Science bachelor's programme for the 2025-2026 academic year.\"}]",
            // No example of Group.startDateTime anywhere else.
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2026-08-31T23:59:59+01:00",
            PersonCount = 2,
            ConsumerJson = "{\"consumerKey\":\"nl-edusites\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "nl-edusites",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = academicSession.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "DS-2026" } }
        };
        dbContext.Groups.Add(group);

        Guid group2Id = Guid.CreateVersion7();
        GroupEntity group2 = new()
        {
            Id = group2Id,
            GroupId = group2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-GRP2",
            GroupType = "group",
            NameJson = "[{\"language\":\"en\",\"value\":\"Sustainability Cohort 2026\"}]",
            OrganisationEntityId = faculty.Id,
            AcademicSessionEntityId = semester.Id
        };
        dbContext.Groups.Add(group2);

        Guid membershipId = Guid.CreateVersion7();
        MembershipEntity membership = new()
        {
            Id = membershipId,
            MembershipIdValue = membershipId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-MEM1",
            Role = "student",
            State = "active",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            GroupId = group.Id,
            PersonId = person.Id
        };
        dbContext.Memberships.Add(membership);

        Guid membership2Id = Guid.CreateVersion7();
        MembershipEntity membership2 = new()
        {
            Id = membership2Id,
            MembershipIdValue = membership2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-MEM2",
            Role = "student",
            State = "active",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2026-01-31T23:59:59+01:00",
            ConsumerJson = "{\"consumerKey\":\"eduxchange\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "eduxchange",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            GroupId = group2.Id,
            PersonId = person3.Id
        };
        dbContext.Memberships.Add(membership2);

        // ---------------------------------------------------------------------------------------
        // Buildings, addresses and rooms.
        // ---------------------------------------------------------------------------------------
        Guid addressId = Guid.CreateVersion7();
        AddressEntity address = new()
        {
            Id = addressId,
            AddressId = addressId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-ADDR1",
            AddressType = "visit",
            Name = "Building entrance, ground floor",
            Street = "Moreelsepark",
            StreetNumber = "48",
            PostCode = "3511 EP",
            City = "Utrecht",
            CountryCode = "NL",
            Country = "Netherlands",
            Latitude = 52.089123,
            Longitude = 5.113337,
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}"
        };
        dbContext.Addresses.Add(address);

        Guid buildingId = Guid.CreateVersion7();
        BuildingEntity building = new()
        {
            Id = buildingId,
            BuildingId = buildingId.ToString(),
            PrimaryCodeType = "building_id",
            PrimaryCode = "DEMO-BLD1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Central Campus Building\"}]",
            Abbreviation = "CAMPUS-A",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"The main lecture and seminar building on the central campus.\"}]",
            AddressEntityId = address.Id,
            ConsumerJson = "{\"consumerKey\":\"nl-edusites\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "nl-edusites",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "CAMPUS-A" } }
        };
        dbContext.Buildings.Add(building);

        Guid address2Id = Guid.CreateVersion7();
        AddressEntity address2 = new()
        {
            Id = address2Id,
            AddressId = address2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-ADDR2",
            AddressType = "visit",
            Street = "Padualaan",
            StreetNumber = "8",
            PostCode = "3584 CH",
            City = "Utrecht",
            CountryCode = "NL",
            Country = "Netherlands"
        };
        dbContext.Addresses.Add(address2);

        Guid building2Id = Guid.CreateVersion7();
        BuildingEntity building2 = new()
        {
            Id = building2Id,
            BuildingId = building2Id.ToString(),
            PrimaryCodeType = "building_id",
            PrimaryCode = "DEMO-BLD2",
            NameJson = "[{\"language\":\"en\",\"value\":\"Science Park Building\"}]",
            AddressEntityId = address2.Id
        };
        dbContext.Buildings.Add(building2);

        Guid roomId = Guid.CreateVersion7();
        RoomEntity room = new()
        {
            Id = roomId,
            RoomId = roomId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-ROOM1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Lecture Hall A\"}]",
            // Room.roomType is required with an extensible enum; without an explicit value here the
            // mapping falls back to "general_purpose" for every seeded room. Set explicitly so at
            // least one non-default enum value has a real example too.
            RoomType = "lecture_room",
            Abbreviation = "A1.01",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"Tiered lecture hall with fixed seating and full AV equipment.\"}]",
            TotalSeats = 300,
            AvailableSeats = 280,
            Floor = "1",
            Wing = "A",
            Latitude = 52.089147,
            Longitude = 5.113298,
            BuildingEntityId = building.Id,
            ConsumerJson = "{\"consumerKey\":\"nl-edusites\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "nl-edusites",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "room_code", Code = "A1.01" } }
        };
        dbContext.Rooms.Add(room);

        Guid room2Id = Guid.CreateVersion7();
        RoomEntity room2 = new()
        {
            Id = room2Id,
            RoomId = room2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-ROOM2",
            NameJson = "[{\"language\":\"en\",\"value\":\"Seminar Room B\"}]",
            BuildingEntityId = building2.Id
        };
        dbContext.Rooms.Add(room2);

        // ---------------------------------------------------------------------------------------
        // Learning outcomes - a parent and a child, so children/parents expand has real data, and
        // linked directly to a course too (GET /courses doesn't itself expose this today, but the
        // relational link exists for institutions/tools that query it directly).
        // ---------------------------------------------------------------------------------------
        Guid learningOutcomeId = Guid.CreateVersion7();
        LearningOutcomeEntity learningOutcome = new()
        {
            Id = learningOutcomeId,
            LearningOutcomeId = learningOutcomeId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LO1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Apply statistical analysis techniques\"}]",
            Abbreviation = "LO-STAT",
            DescriptionJson =
                "[{\"language\":\"en\",\"value\":\"The student can select and apply appropriate statistical methods to analyse a real-world dataset.\"}]",
            FieldsOfStudy = "0613",
            ComplexityLevel = "bloom_3",
            ValidFrom = "2025-09-01T09:00:00+01:00",
            OrganisationEntityId = org.Id,
            ConsumerJson = "{\"consumerKey\":\"nl-edusites\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "nl-edusites",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "LO-STAT-1" } }
        };
        dbContext.LearningOutcomes.Add(learningOutcome);

        Guid learningOutcome2Id = Guid.CreateVersion7();
        LearningOutcomeEntity learningOutcome2 = new()
        {
            Id = learningOutcome2Id,
            LearningOutcomeId = learningOutcome2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LO2",
            NameJson = "[{\"language\":\"en\",\"value\":\"Apply regression analysis techniques\"}]",
            ValidFrom = "2025-09-01T09:00:00+01:00",
            ValidTo = "2030-09-01T09:00:00+01:00",
            OrganisationEntityId = org.Id
        };
        learningOutcome2.Parents.Add(learningOutcome);
        dbContext.LearningOutcomes.Add(learningOutcome2);

        course.LearningOutcomes.Add(learningOutcome);
        programme.LearningOutcomes.Add(learningOutcome);
        courseTimelineOverride.LearningOutcomes.Add(learningOutcome);

        // ---------------------------------------------------------------------------------------
        // Learning components / test components - parent/child pairs.
        // ---------------------------------------------------------------------------------------
        Guid learningComponentParentGuid = Guid.CreateVersion7();
        LearningComponentEntity learningComponentParent = new()
        {
            Id = learningComponentParentGuid,
            ComponentId = learningComponentParentGuid.ToString(),
            ComponentType = "lecture",
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LC-PARENT1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Lectures\"}]",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"Weekly lectures covering the core theory for this course.\"}]",
            ModesOfDeliveryJson = "[\"presential\"]",
            Duration = "PT2H",
            FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc),
            TeachingLanguagesJson = "[\"en-GB\"]",
            FieldsOfStudy = "0613",
            ValidFrom = new DateTime(2025, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "LC-LEC" } }
        };
        dbContext.LearningComponents.Add(learningComponentParent);

        Guid learningComponentChildGuid = Guid.CreateVersion7();
        LearningComponentEntity learningComponentChild = new()
        {
            Id = learningComponentChildGuid,
            ComponentId = learningComponentChildGuid.ToString(),
            ComponentType = "tutorial",
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LC-CHILD1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Weekly Tutorial\"}]",
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            ParentEntityId = learningComponentParent.Id,
            Abbreviation = "TUT1",
            EnrolmentJson = "[{\"language\":\"en-GB\",\"value\":\"Enrolment through the student information system\"}]",
            ResourcesJson = "[\"Weekly exercise sheets\"]",
            AssessmentJson = "[{\"language\":\"en-GB\",\"value\":\"Participation-based\"}]",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs on LearningComponent specifically.
            AddressesJson =
                "[{\"addressType\":\"teaching\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Tutorial room, third floor\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            ValidTo = new DateTime(2030, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "LC-TUT" } }
        };
        learningComponentChild.LearningOutcomes.Add(learningOutcome);
        dbContext.LearningComponents.Add(learningComponentChild);

        Guid testComponentParentGuid = Guid.CreateVersion7();
        TestComponentEntity testComponentParent = new()
        {
            Id = testComponentParentGuid,
            ComponentId = testComponentParentGuid.ToString(),
            ComponentType = "digital_test",
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-TC-PARENT1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Written Exam\"}]",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"Closed-book written exam covering all lecture material.\"}]",
            ModesOfDeliveryJson = "[\"presential\"]",
            Duration = "PT3H",
            FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
            LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc),
            TeachingLanguagesJson = "[\"en-GB\"]",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "TC-EXAM" } }
        };
        dbContext.TestComponents.Add(testComponentParent);

        Guid testComponentChildGuid = Guid.CreateVersion7();
        TestComponentEntity testComponentChild = new()
        {
            Id = testComponentChildGuid,
            ComponentId = testComponentChildGuid.ToString(),
            ComponentType = "oral_test",
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-TC-CHILD1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Oral Resit\"}]",
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            ParentEntityId = testComponentParent.Id,
            Abbreviation = "RESIT1",
            ExtraDuration = "PT30M",
            ResultValueType = "pass_or_fail",
            ResultExpected = true,
            Attempts = 2,
            PassFrom = "5.5",
            State = "active",
            EnrolmentJson = "[{\"language\":\"en-GB\",\"value\":\"Enrolment through the student information system\"}]",
            ResourcesJson = "[\"Resit registration form\"]",
            AssessmentJson = "[{\"language\":\"en-GB\",\"value\":\"Oral exam on campus\"}]",
            // additional/postCode/geolocation/ext/countryCode have no example anywhere else in the
            // seeder's various embedded Address-shaped blobs on TestComponent specifically.
            AddressesJson =
                "[{\"addressType\":\"teaching\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Oral exam room, second floor\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "TC-RESIT" } }
        };
        testComponentChild.LearningOutcomes.Add(learningOutcome);
        dbContext.TestComponents.Add(testComponentChild);

        // ---------------------------------------------------------------------------------------
        // Offerings - a course offering and programme offering in each academic session, so
        // "offered across multiple sessions" has real data, plus learning/test component offerings
        // for the fall session.
        // ---------------------------------------------------------------------------------------
        string rioOfferingConsumerJson =
            "{\"consumerKey\":\"rio\",\"registrationStatus\":\"open\",\"requiredPermissionRegistration\":\"yes\",\"explanationRequiredPermission\":\"Limited capacity\",\"modeOfDelivery\":[\"coaching\"]}";

        Guid courseOfferingId = Guid.CreateVersion7();
        CourseOfferingEntity courseOffering = new()
        {
            Id = courseOfferingId,
            CourseOfferingId = courseOfferingId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-CO1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Introduction to Data Science - Fall 2025\"}]",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2026-01-31T23:59:59+01:00",
            FlexibleEntryPeriodStartDateTime = "2025-09-01T09:00:00+01:00",
            FlexibleEntryPeriodEndDateTime = "2025-09-14T22:59:59+01:00",
            State = "active",
            RosteringState = "definitive",
            Abbreviation = "DS101-25",
            DescriptionJson =
                "[{\"language\":\"en\",\"value\":\"Fall 2025 run of the Introduction to Data Science course.\"}]",
            TeachingLanguagesJson = "[\"en-GB\"]",
            ModesOfDeliveryJson = "[\"blended\"]",
            MaxNumberStudents = 200,
            EnrolledNumberStudents = 150,
            PendingNumberStudents = 5,
            MinNumberStudents = 15,
            ResultExpected = true,
            ResultValueType = "grade_0_10",
            Link = "https://osiris.example.org/offering/DEMO-CO1",
            // targetGroups/enrolmentUrl/queueEnabled/queuedNumberStudents/maxQueuedNumberStudents/
            // comment/consumer/ext have no example anywhere else in the seeder's enrolmentPeriods
            // blobs on CourseOffering specifically.
            EnrolmentPeriodsJson =
                "[{\"startDateTime\":\"2025-06-01T00:00:00+01:00\",\"endDateTime\":\"2025-08-31T23:59:59+01:00\",\"targetGroups\":[\"prospective_student\"],\"enrolmentType\":\"broker\",\"enrolmentUrl\":\"https://osiris.example.org/enrol/DEMO-CO1\",\"queueEnabled\":true,\"queuedNumberStudents\":8,\"maxQueuedNumberStudents\":25,\"comment\":\"Enrolment opens six weeks before the semester starts.\",\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            SupplementaryInformationJson =
                "[{\"role\":\"badge\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Popular this semester\"}]}]",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs on CourseOffering specifically.
            AddressesJson =
                "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Main entrance, room 1.05\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // vatAmount/amountWithoutVat/displayAmount/ext have no example anywhere else in the
            // seeder's priceInformation blobs on CourseOffering specifically.
            PriceInformationJson =
                "[{\"costType\":\"total_costs\",\"amount\":\"250.00\",\"vatAmount\":\"43.39\",\"amountWithoutVat\":\"206.61\",\"currency\":\"EUR\",\"displayAmount\":[{\"language\":\"en-GB\",\"value\":\"\u20AC250.00\"}],\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = semester.Id,
            ConsumerJson = rioOfferingConsumerJson,
            ConsumerKey = "rio",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "INFOB3DS-25" } }
        };
        dbContext.CourseOfferings.Add(courseOffering);

        // Second course offering - same course, upcoming academic year (see upcomingSemester
        // above) - a "concept" stage offering fits a not-yet-open session well, and this is what
        // demonstrates GET /course-offerings' since=<today> default actually returning something.
        Guid courseOffering2Id = Guid.CreateVersion7();
        CourseOfferingEntity courseOffering2 = new()
        {
            Id = courseOffering2Id,
            CourseOfferingId = courseOffering2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-CO2",
            NameJson =
                $"[{{\"language\":\"en\",\"value\":\"Sustainability and Global Challenges - {nextAcademicYearStart.Year}-{nextAcademicYearStart.Year + 1}\"}}]",
            StartDateTime = upcomingSemester.StartDateTime,
            EndDateTime = upcomingSemester.EndDateTime,
            State = "concept",
            ConsumerJson = "{\"consumerKey\":\"eduxchange\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "eduxchange",
            CourseEntityId = course2.Id,
            OrganisationEntityId = faculty.Id,
            AcademicSessionEntityId = upcomingSemester.Id
        };
        dbContext.CourseOfferings.Add(courseOffering2);

        Guid programmeOfferingId = Guid.CreateVersion7();
        ProgrammeOfferingEntity programmeOffering = new()
        {
            Id = programmeOfferingId,
            ProgrammeOfferingIdValue = programmeOfferingId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PO1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Data Science - Fall 2025\"}]",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2029-08-31T23:59:59+01:00",
            State = "active",
            RosteringState = "definitive",
            Abbreviation = "DS-BSc-25",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"2025 cohort of the Data Science bachelor's programme.\"}]",
            TeachingLanguagesJson = "[\"en-GB\",\"nl-NL\"]",
            ModesOfDeliveryJson = "[\"presential\"]",
            MaxNumberStudents = 120,
            EnrolledNumberStudents = 95,
            // No example of ProgrammeOffering.pendingNumberStudents anywhere else.
            PendingNumberStudents = 10,
            MinNumberStudents = 20,
            ResultExpected = true,
            ResultValueType = "grade_0_10",
            Link = "https://osiris.example.org/offering/DEMO-PO1",
            // No example of ProgrammeOffering's own flexibleEntryPeriodStartDateTime/EndDateTime
            // anywhere else.
            FlexibleEntryPeriodStartDateTime = "2025-09-01T09:00:00+01:00",
            FlexibleEntryPeriodEndDateTime = "2025-09-14T22:59:59+01:00",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs on ProgrammeOffering specifically.
            AddressesJson =
                "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Programme office, second floor\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // vatAmount/amountWithoutVat/displayAmount/ext have no example anywhere else in the
            // seeder's priceInformation blobs on ProgrammeOffering specifically.
            PriceInformationJson =
                "[{\"costType\":\"total_costs\",\"amount\":\"2530.00\",\"vatAmount\":\"438.68\",\"amountWithoutVat\":\"2091.32\",\"currency\":\"EUR\",\"displayAmount\":[{\"language\":\"en-GB\",\"value\":\"\u20AC2,530.00\"}],\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // queuedNumberStudents/maxQueuedNumberStudents/consumer/ext have no example anywhere else
            // in the seeder's enrolmentPeriods blobs on ProgrammeOffering specifically.
            EnrolmentPeriodsJson =
                "[{\"startDateTime\":\"2025-06-01T00:00:00+01:00\",\"endDateTime\":\"2025-08-31T23:59:59+01:00\",\"targetGroups\":[\"prospective_student\"],\"enrolmentType\":\"broker\",\"enrolmentUrl\":\"https://osiris.example.org/enrol/DEMO-PO1\",\"queueEnabled\":false,\"queuedNumberStudents\":0,\"maxQueuedNumberStudents\":15,\"comment\":\"Enrolment opens six weeks before the semester starts.\",\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // No example of ProgrammeOffering's own supplementaryInformation anywhere else.
            SupplementaryInformationJson =
                "[{\"role\":\"badge\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Popular this year\"}]}]",
            ConsumerJson = "{\"consumerKey\":\"eduxchange\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "eduxchange",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            ProgrammeEntityId = programme.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = semester.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "DS-PRG-25" } }
        };
        courseOffering.ProgrammeOfferings.Add(programmeOffering);
        dbContext.ProgrammeOfferings.Add(programmeOffering);

        // Second programme offering - the sub-programme, upcoming academic year (see
        // upcomingSemester above).
        Guid programmeOffering2Id = Guid.CreateVersion7();
        ProgrammeOfferingEntity programmeOffering2 = new()
        {
            Id = programmeOffering2Id,
            ProgrammeOfferingIdValue = programmeOffering2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-PO2",
            NameJson =
                $"[{{\"language\":\"en\",\"value\":\"Data Science - Year 1 - {nextAcademicYearStart.Year}-{nextAcademicYearStart.Year + 1}\"}}]",
            ProgrammeEntityId = subProgramme.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = upcomingSemester.Id
        };
        dbContext.ProgrammeOfferings.Add(programmeOffering2);

        Guid learningComponentOfferingId = Guid.CreateVersion7();
        LearningComponentOfferingEntity learningComponentOffering = new()
        {
            Id = learningComponentOfferingId,
            LearningComponentOfferingIdValue = learningComponentOfferingId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LCO1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Weekly Tutorial - Fall 2025\"}]",
            StartDateTime = "2025-09-01T00:00:00+01:00",
            EndDateTime = "2026-01-31T23:59:59+01:00",
            State = "active",
            RosteringState = "definitive",
            Abbreviation = "TUT1-25",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"Weekly tutorial sessions accompanying the Fall 2025 lectures.\"}]",
            TeachingLanguagesJson = "[\"en-GB\"]",
            ModesOfDeliveryJson = "[\"presential\"]",
            MaxNumberStudents = 30,
            EnrolledNumberStudents = 25,
            // No example of LearningComponentOffering.pendingNumberStudents anywhere else.
            PendingNumberStudents = 3,
            MinNumberStudents = 5,
            ResultValueType = "pass_or_fail",
            Link = "https://osiris.example.org/offering/DEMO-LCO1",
            // targetGroups/enrolmentUrl/queueEnabled/comment/ext have no example anywhere else in
            // the seeder's enrolmentPeriods blobs on LearningComponentOffering specifically.
            EnrolmentPeriodsJson =
                "[{\"startDateTime\":\"2025-06-01T00:00:00+01:00\",\"endDateTime\":\"2025-08-31T23:59:59+01:00\",\"targetGroups\":[\"prospective_student\"],\"enrolmentType\":\"broker\",\"enrolmentUrl\":\"https://osiris.example.org/enrol/DEMO-LCO1\",\"queueEnabled\":true,\"queuedNumberStudents\":2,\"maxQueuedNumberStudents\":8,\"comment\":\"Enrolment opens six weeks before the semester starts.\",\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            AddressesJson =
                "[{\"addressType\":\"teaching\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\",\"additional\":[{\"language\":\"en\",\"value\":\"Tutorial room, third floor\"}],\"postCode\":\"3511 EP\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            PriceInformationJson =
                "[{\"costType\":\"total_costs\",\"amount\":\"0.00\",\"vatAmount\":\"0.00\",\"amountWithoutVat\":\"0.00\",\"currency\":\"EUR\",\"displayAmount\":[{\"language\":\"en-GB\",\"value\":\"\u20AC0.00\"}],\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            // No example of LearningComponentOffering's own supplementaryInformation anywhere else.
            SupplementaryInformationJson =
                "[{\"role\":\"announcement\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Bring a laptop to every session\"}]}]",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            LearningComponentEntityId = learningComponentChild.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = semester.Id,
            ResultExpected = true,
            ResultWeight = 30,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "TUT1-25" } }
        };
        learningComponentOffering.CourseOfferings.Add(courseOffering);
        learningComponentOffering.Rooms.Add(room);
        dbContext.LearningComponentOfferings.Add(learningComponentOffering);

        Guid testComponentOfferingId = Guid.CreateVersion7();
        TestComponentOfferingEntity testComponentOffering = new()
        {
            Id = testComponentOfferingId,
            TestComponentOfferingIdValue = testComponentOfferingId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-TCO1",
            NameJson = "[{\"language\":\"en\",\"value\":\"Oral Resit - Fall 2025\"}]",
            StartDateTime = "2026-02-01T00:00:00+01:00",
            EndDateTime = "2026-02-15T23:59:59+01:00",
            State = "active",
            RosteringState = "tentative",
            Abbreviation = "RESIT1-25",
            DescriptionJson = "[{\"language\":\"en\",\"value\":\"Oral resit examination for students who did not pass the written exam.\"}]",
            TeachingLanguagesJson = "[\"en-GB\"]",
            ModesOfDeliveryJson = "[\"presential\"]",
            MaxNumberStudents = 20,
            EnrolledNumberStudents = 3,
            PendingNumberStudents = 1,
            // No example of TestComponentOffering.minNumberStudents anywhere else.
            MinNumberStudents = 2,
            ResultValueType = "pass_or_fail",
            Link = "https://osiris.example.org/offering/DEMO-TCO1",
            // targetGroups/enrolmentUrl/queueEnabled/queuedNumberStudents/maxQueuedNumberStudents/
            // comment/consumer/ext have no example anywhere else in the seeder's enrolmentPeriods
            // blobs on TestComponentOffering specifically.
            EnrolmentPeriodsJson =
                "[{\"startDateTime\":\"2026-01-01T00:00:00+01:00\",\"endDateTime\":\"2026-01-25T23:59:59+01:00\",\"targetGroups\":[\"resit_candidate\"],\"enrolmentType\":\"url\",\"enrolmentUrl\":\"https://osiris.example.org/enrol/DEMO-TCO1\",\"queueEnabled\":false,\"queuedNumberStudents\":0,\"maxQueuedNumberStudents\":10,\"comment\":\"Resit registration closes one week before the exam.\",\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            SupplementaryInformationJson =
                "[{\"role\":\"badge\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Resit opportunity\"}]}]",
            FlexibleEntryPeriodStartDateTime = "2026-02-01T09:00:00+01:00",
            FlexibleEntryPeriodEndDateTime = "2026-02-01T22:59:59+01:00",
            // additional/postCode/geolocation/ext have no example anywhere else in the seeder's
            // various embedded Address-shaped blobs on TestComponentOffering specifically.
            AddressesJson =
                "[{\"addressType\":\"teaching\",\"street\":\"Padualaan\",\"streetNumber\":\"8\",\"additional\":[{\"language\":\"en\",\"value\":\"Exam hall, ground floor\"}],\"postCode\":\"3584 CH\",\"city\":\"Utrecht\",\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"},\"geolocation\":{\"latitude\":52.086146,\"longitude\":5.171958},\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            PriceInformationJson =
                "[{\"costType\":\"total_costs\",\"amount\":\"0.00\",\"vatAmount\":\"0.00\",\"amountWithoutVat\":\"0.00\",\"currency\":\"EUR\",\"displayAmount\":[{\"language\":\"en-GB\",\"value\":\"\u20AC0.00\"}],\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}]",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            TestComponentEntityId = testComponentChild.Id,
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = semester.Id,
            ResultExpected = true,
            ResultWeight = 70,
            DocumentsJson =
                "[{\"documentId\":\"" + Guid.CreateVersion7() +
                "\",\"documentType\":\"instructions\",\"documentName\":\"Resit instructions.pdf\"}]",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "RESIT1-25" } }
        };
        testComponentOffering.CourseOfferings.Add(courseOffering);
        testComponentOffering.Rooms.Add(room2);
        dbContext.TestComponentOfferings.Add(testComponentOffering);

        // Upcoming learning/test component offerings (see upcomingSemester above) - the same
        // since=<today> default coverage rationale as courseOffering2/programmeOffering2.
        Guid learningComponentOffering2Id = Guid.CreateVersion7();
        LearningComponentOfferingEntity learningComponentOffering2 = new()
        {
            Id = learningComponentOffering2Id,
            LearningComponentOfferingIdValue = learningComponentOffering2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LCO2",
            NameJson =
                $"[{{\"language\":\"en\",\"value\":\"Weekly Tutorial - {nextAcademicYearStart.Year}-{nextAcademicYearStart.Year + 1}\"}}]",
            StartDateTime = upcomingSemester.StartDateTime,
            EndDateTime = upcomingSemester.EndDateTime,
            State = "concept",
            LearningComponentEntityId = learningComponentChild.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = upcomingSemester.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "TUT1-UPCOMING" } }
        };
        dbContext.LearningComponentOfferings.Add(learningComponentOffering2);

        Guid testComponentOffering2Id = Guid.CreateVersion7();
        TestComponentOfferingEntity testComponentOffering2 = new()
        {
            Id = testComponentOffering2Id,
            TestComponentOfferingIdValue = testComponentOffering2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-TCO2",
            NameJson =
                $"[{{\"language\":\"en\",\"value\":\"Written Exam - {nextAcademicYearStart.Year}-{nextAcademicYearStart.Year + 1}\"}}]",
            StartDateTime = upcomingSemester.StartDateTime,
            EndDateTime = upcomingSemester.EndDateTime,
            State = "concept",
            TestComponentEntityId = testComponentParent.Id,
            CourseEntityId = course.Id,
            OrganisationEntityId = org.Id,
            AcademicSessionEntityId = upcomingSemester.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "EXAM-UPCOMING" } }
        };
        dbContext.TestComponentOfferings.Add(testComponentOffering2);

        // Link the first group to one offering of every type, so a single group demonstrates all
        // 4 polymorphic offering-reference collections at once.
        group.CourseOfferings.Add(courseOffering);
        group.ProgrammeOfferings.Add(programmeOffering);
        group.LearningComponentOfferings.Add(learningComponentOffering);
        group.TestComponentOfferings.Add(testComponentOffering);

        // ---------------------------------------------------------------------------------------
        // Associations - two of each type, tying person/person3 to the offerings above.
        // ---------------------------------------------------------------------------------------
        Guid courseOfferingAssociationId = Guid.CreateVersion7();
        CourseOfferingAssociationEntity courseOfferingAssociation = new()
        {
            Id = courseOfferingAssociationId,
            CourseOfferingAssociationIdValue = courseOfferingAssociationId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-COA1",
            Role = "student",
            State = "associated",
            // No example of CourseOfferingAssociation.startDateTime anywhere else.
            StartDateTime = "2025-09-01T08:30:00+01:00",
            CourseOfferingEntityId = courseOffering.Id,
            PersonEntityId = person.Id,
            OrganisationEntityId = org.Id,
            StudyLoadJson = "{\"studyLoadUnit\":\"hour\",\"value\":8}",
            // consumer/ext inside the Result object itself (distinct from the association's own
            // top-level consumer/ext below) have no example anywhere else in the seeder's various
            // Result-shaped blobs.
            ResultJson = "{\"state\":\"completed\",\"pass\":\"passed\",\"comment\":\"Strong performance overall.\"," +
                "\"score\":\"8\",\"resultvaluetype\":\"grade_0_10\",\"rawScore\":80,\"maxRawScore\":100," +
                "\"final\":true,\"assessorId\":\"" + person2.Id + "\"," +
                "\"resultDateTime\":\"2026-02-10T09:00:00+01:00\"," +
                "\"documents\":[{\"documentId\":\"" + Guid.CreateVersion7() + "\",\"documentType\":\"assessment_form\",\"documentName\":\"course-assessment-form.pdf\"}]," +
                "\"studyLoad\":{\"studyLoadUnit\":\"hour\",\"value\":8}," +
                "\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}," +
                "\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "ENR-COA-1" } }
        };
        dbContext.CourseOfferingAssociations.Add(courseOfferingAssociation);

        Guid courseOfferingAssociation2Id = Guid.CreateVersion7();
        CourseOfferingAssociationEntity courseOfferingAssociation2 = new()
        {
            Id = courseOfferingAssociation2Id,
            CourseOfferingAssociationIdValue = courseOfferingAssociation2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-COA2",
            Role = "student",
            State = "associated",
            CourseOfferingEntityId = courseOffering2.Id,
            PersonEntityId = person3.Id,
            OrganisationEntityId = faculty.Id,
            RemoteState = "associated",
            ExpectedEndDateTime = "2026-08-31T23:59:59+01:00",
            ActualEndDateTime = "2026-08-15T17:00:00+01:00"
        };
        dbContext.CourseOfferingAssociations.Add(courseOfferingAssociation2);

        Guid learningComponentOfferingAssociationId = Guid.CreateVersion7();
        LearningComponentOfferingAssociationEntity learningComponentOfferingAssociation = new()
        {
            Id = learningComponentOfferingAssociationId,
            LearningComponentOfferingAssociationIdValue = learningComponentOfferingAssociationId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LCOA1",
            Role = "student",
            State = "associated",
            // startDateTime/expectedEndDateTime/actualEndDateTime/remoteState/consumer/ext have no
            // example anywhere else in the seeder on LearningComponentOfferingAssociation specifically
            // (unlike CourseOfferingAssociation/ProgrammeOfferingAssociation, which already cover
            // these shared AssociationProperties fields across their own two rows).
            StartDateTime = "2025-09-01T08:30:00+01:00",
            ExpectedEndDateTime = "2026-01-31T23:59:59+01:00",
            ActualEndDateTime = "2026-01-31T18:00:00+01:00",
            RemoteState = "associated",
            LearningComponentOfferingEntityId = learningComponentOffering.Id,
            PersonEntityId = person.Id,
            OrganisationEntityId = org.Id,
            Attendance = "present",
            ResultJson = "{\"state\":\"completed\",\"pass\":\"passed\",\"comment\":\"Actively participated in every session.\"," +
                "\"score\":\"pass\",\"resultvaluetype\":\"pass_or_fail\",\"final\":true," +
                "\"resultDateTime\":\"2026-01-30T09:00:00+01:00\",\"weight\":30," +
                "\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}," +
                "\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "ENR-LCOA-1" } }
        };
        dbContext.LearningComponentOfferingAssociations.Add(learningComponentOfferingAssociation);

        Guid learningComponentOfferingAssociation2Id = Guid.CreateVersion7();
        LearningComponentOfferingAssociationEntity learningComponentOfferingAssociation2 = new()
        {
            Id = learningComponentOfferingAssociation2Id,
            LearningComponentOfferingAssociationIdValue = learningComponentOfferingAssociation2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-LCOA2",
            Role = "student",
            State = "associated",
            LearningComponentOfferingEntityId = learningComponentOffering.Id,
            PersonEntityId = person3.Id,
            OrganisationEntityId = org.Id
        };
        dbContext.LearningComponentOfferingAssociations.Add(learningComponentOfferingAssociation2);

        Guid programmeOfferingAssociationId = Guid.CreateVersion7();
        ProgrammeOfferingAssociationEntity programmeOfferingAssociation = new()
        {
            Id = programmeOfferingAssociationId,
            ProgrammeOfferingAssociationIdValue = programmeOfferingAssociationId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-POA1",
            Role = "student",
            State = "associated",
            ProgrammeOfferingEntityId = programmeOffering.Id,
            PersonEntityId = person.Id,
            OrganisationEntityId = org.Id,
            RemoteState = "associated",
            // No example of ProgrammeOfferingAssociation.startDateTime anywhere else.
            StartDateTime = "2025-09-01T08:30:00+01:00",
            ExpectedEndDateTime = "2029-08-31T23:59:59+01:00",
            ResultJson = "{\"state\":\"in_progress\",\"comment\":\"On track, second year underway.\"," +
                "\"resultDateTime\":\"2026-02-10T09:00:00+01:00\"," +
                "\"studyLoad\":{\"studyLoadUnit\":\"ects\",\"value\":180}}",
            ConsumerJson = "{\"consumerKey\":\"eduxchange\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "eduxchange",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "ENR-POA-1" } }
        };
        dbContext.ProgrammeOfferingAssociations.Add(programmeOfferingAssociation);

        Guid programmeOfferingAssociation2Id = Guid.CreateVersion7();
        ProgrammeOfferingAssociationEntity programmeOfferingAssociation2 = new()
        {
            Id = programmeOfferingAssociation2Id,
            ProgrammeOfferingAssociationIdValue = programmeOfferingAssociation2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-POA2",
            Role = "student",
            State = "associated",
            ProgrammeOfferingEntityId = programmeOffering2.Id,
            PersonEntityId = person3.Id,
            OrganisationEntityId = org.Id,
            // No example of ProgrammeOfferingAssociation.actualEndDateTime anywhere else - doesn't fit
            // programmeOfferingAssociation's own in-progress narrative, so seeded here instead.
            ActualEndDateTime = "2026-01-15T17:00:00+01:00"
        };
        dbContext.ProgrammeOfferingAssociations.Add(programmeOfferingAssociation2);

        Guid testComponentOfferingAssociationId = Guid.CreateVersion7();
        TestComponentOfferingAssociationEntity testComponentOfferingAssociation = new()
        {
            Id = testComponentOfferingAssociationId,
            TestComponentOfferingAssociationIdValue = testComponentOfferingAssociationId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-TCOA1",
            Role = "student",
            State = "associated",
            // startDateTime/expectedEndDateTime/actualEndDateTime/remoteState/consumer/ext have no
            // example anywhere else in the seeder on TestComponentOfferingAssociation specifically
            // (unlike CourseOfferingAssociation/ProgrammeOfferingAssociation, which already cover
            // these shared AssociationProperties fields across their own two rows).
            StartDateTime = "2026-02-01T09:00:00+01:00",
            ExpectedEndDateTime = "2026-02-01T11:00:00+01:00",
            ActualEndDateTime = "2026-02-01T10:45:00+01:00",
            RemoteState = "associated",
            TestComponentOfferingEntityId = testComponentOffering.Id,
            PersonEntityId = person.Id,
            OrganisationEntityId = org.Id,
            Attendance = "present",
            RequiredPersonalNeedsJson = "[\"extra_time\"]",
            ExtraDuration = "PT20M",
            InitialAttemptOnAssociation = 1,
            MaximumNumberOfAttemptsOnAssociation = 2,
            IrregularitiesJson = "[\"Fire alarm interrupted the session for 5 minutes\"]",
            DocumentsJson =
                "[{\"documentId\":\"" + Guid.CreateVersion7() +
                "\",\"documentType\":\"handed_in_document\",\"documentName\":\"resit-answers.pdf\"}]",
            ResultJson = "{\"state\":\"completed\",\"pass\":\"passed\",\"comment\":\"Solid oral defence of the written work.\"," +
                "\"score\":\"7\",\"resultvaluetype\":\"pass_or_fail\",\"final\":true,\"assessorId\":\"" + person2.Id + "\"," +
                "\"resultDateTime\":\"2026-02-16T09:00:00+01:00\",\"weight\":70," +
                "\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}," +
                "\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio",
            ExtJson = "{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}",
            Url = "https://osiris.example.org/test-tool/start/" + testComponentOfferingAssociationId,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "ENR-TCOA-1" } }
        };
        dbContext.TestComponentOfferingAssociations.Add(testComponentOfferingAssociation);

        Guid testComponentOfferingAssociation2Id = Guid.CreateVersion7();
        TestComponentOfferingAssociationEntity testComponentOfferingAssociation2 = new()
        {
            Id = testComponentOfferingAssociation2Id,
            TestComponentOfferingAssociationIdValue = testComponentOfferingAssociation2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-TCOA2",
            Role = "student",
            State = "associated",
            TestComponentOfferingEntityId = testComponentOffering.Id,
            PersonEntityId = person3.Id,
            OrganisationEntityId = org.Id,
            Url = "https://osiris.example.org/test-tool/start/" + testComponentOfferingAssociation2Id
        };
        dbContext.TestComponentOfferingAssociations.Add(testComponentOfferingAssociation2);

        // ---------------------------------------------------------------------------------------
        // Attempts - one completed, one scheduled, so State has real variety and both Rooms and
        // Coordinator expand have data on more than one row.
        // ---------------------------------------------------------------------------------------
        Guid attemptId = Guid.CreateVersion7();
        TestComponentOfferingAssociationAttemptEntity attempt = new()
        {
            Id = attemptId,
            AttemptIdValue = attemptId.ToString(),
            Opportunity = "2025Semester1Resit",
            Attempt = 1,
            State = "finished",
            Attendance = "present",
            StartDateTime = "2026-02-01T09:00:00+01:00",
            EndDateTime = "2026-02-01T11:00:00+01:00",
            TestComponentOfferingAssociationEntityId = testComponentOfferingAssociation.Id,
            CourseOfferingAssociationEntityId = courseOfferingAssociation.Id,
            CoordinatorEntityId = person2.Id,
            DocumentsJson =
                "[{\"documentId\":\"" + Guid.CreateVersion7() +
                "\",\"documentType\":\"test_made\",\"documentName\":\"resit-result.txt\"}]",
            // consumer/ext inside the Result object itself have no example anywhere else in the
            // seeder's various Result-shaped blobs on TestComponentOfferingAssociationAttempt
            // specifically.
            ResultJson = "{\"state\":\"completed\",\"pass\":\"passed\",\"comment\":\"Solid oral defence of the written work.\"," +
                "\"score\":\"7\",\"resultvaluetype\":\"pass_or_fail\",\"final\":true,\"assessorId\":\"" + person2.Id + "\"," +
                "\"resultDateTime\":\"2026-02-01T11:30:00+01:00\"," +
                "\"consumer\":{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}," +
                "\"ext\":{\"x-demo-note\":\"Example institution-specific field, for local conformance testing.\"}}",
            ConsumerJson = "{\"consumerKey\":\"rio\",\"x-demo-field\":\"Example consumer-scoped value, for local conformance testing.\"}",
            ConsumerKey = "rio"
        };
        attempt.Rooms.Add(room);
        dbContext.TestComponentOfferingAssociationAttempts.Add(attempt);

        Guid attempt2Id = Guid.CreateVersion7();
        TestComponentOfferingAssociationAttemptEntity attempt2 = new()
        {
            Id = attempt2Id,
            AttemptIdValue = attempt2Id.ToString(),
            Attempt = 1,
            State = "associated",
            StartDateTime = "2026-02-08T09:00:00+01:00",
            EndDateTime = "2026-02-08T11:00:00+01:00",
            Irregularities = "Student requested a seat near the exit due to a medical need.",
            TestComponentOfferingAssociationEntityId = testComponentOfferingAssociation2.Id,
            CourseOfferingAssociationEntityId = courseOfferingAssociation2.Id,
            CoordinatorEntityId = person2.Id
        };
        attempt2.Rooms.Add(room2);
        dbContext.TestComponentOfferingAssociationAttempts.Add(attempt2);

        // ---------------------------------------------------------------------------------------
        // Documents - real content, so GET /documents/{documentId} has more than one row to
        // download.
        // ---------------------------------------------------------------------------------------
        Guid documentId = Guid.CreateVersion7();
        DocumentEntity document = new()
        {
            Id = documentId,
            DocumentId = documentId.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-DOC1",
            Name = "course-syllabus.txt",
            DocumentType = "additional_document",
            MimeType = "text/plain",
            Content = Encoding.UTF8.GetBytes(
                "Introduction to Data Science - course syllabus (demo document)."),
            OrganisationEntityId = org.Id,
            OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "DOC-SYL-1" } }
        };
        dbContext.Documents.Add(document);

        Guid document2Id = Guid.CreateVersion7();
        DocumentEntity document2 = new()
        {
            Id = document2Id,
            DocumentId = document2Id.ToString(),
            PrimaryCodeType = "identifier",
            PrimaryCode = "DEMO-DOC2",
            Name = "exam-regulations.txt",
            DocumentType = "instructions",
            MimeType = "text/plain",
            Content = Encoding.UTF8.GetBytes(
                "Introduction to Data Science - exam regulations (demo document)."),
            OrganisationEntityId = org.Id
        };
        dbContext.Documents.Add(document2);

        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
