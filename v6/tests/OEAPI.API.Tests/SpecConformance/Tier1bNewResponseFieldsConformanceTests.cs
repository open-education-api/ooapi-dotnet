using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Confirms a representative sample of the response fields added by the
///     <c>fix-spec-conformance-tier1b-new-response-fields</c> change actually round-trip - read for
///     every resource touched, plus a PUT round-trip for each of the 3 write-capable resources
///     (<c>CourseOfferingAssociation</c>, the two <c>*Offering</c> types, and
///     <c>TestComponentOfferingAssociation</c>) per that change's tasks.md 7.2. Follows the same
///     seed-via-<see cref="SqlServerOEAPIDbContext" />-then-real-HTTP-request pattern as
///     <see cref="Tier2BindingBugConformanceTests" />.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class Tier1bNewResponseFieldsConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task CourseById_NewOfferingDateFields_ArePresent()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid courseId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Course\"}]",
                FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc)
            };
            dbContext.Courses.Add(course);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client, $"/courses/{courseId}");
        Assert.NotNull(json["firstPossibleOfferingStartDateTime"]);
        Assert.NotNull(json["lastPossibleOfferingStartDateTime"]);
        Assert.NotNull(json["lastPossibleOfferingEndDateTime"]);
    }

    [Fact]
    public async Task ProgrammeById_NewFields_ArePresent()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid programmeId = Guid.CreateVersion7();
        Guid learningOutcomeId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            LearningOutcomeEntity learningOutcome = new()
            {
                Id = learningOutcomeId,
                LearningOutcomeId = learningOutcomeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-LO-{learningOutcomeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Outcome\"}]"
            };
            dbContext.LearningOutcomes.Add(learningOutcome);

            ProgrammeEntity programme = new()
            {
                Id = programmeId,
                ProgrammeId = programmeId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-PRG-{programmeId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Programme\"}]",
                ProgrammeType = "programme",
                Level = "master",
                FieldsOfStudy = "0732",
                Link = "https://example.org/tier1b-programme",
                ResourcesJson = "[\"Reader\"]",
                AssessmentJson = "[{\"language\":\"en-GB\",\"value\":\"Exam\"}]",
                EnrolmentJson = "[{\"language\":\"en-GB\",\"value\":\"Enrol via SIS\"}]",
                AdmissionRequirementsJson = "[{\"language\":\"en-GB\",\"value\":\"Enrolled at qualifying institution\"}]",
                QualificationRequirementsJson = "[{\"language\":\"en-GB\",\"value\":\"Required credits obtained\"}]",
                SupplementaryInformationJson =
                    "[{\"role\":\"badge\",\"type\":\"text_plain\",\"value\":[{\"language\":\"en-GB\",\"value\":\"Top rated\"}]}]",
                FirstStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc)
            };
            programme.LearningOutcomes.Add(learningOutcome);
            dbContext.Programmes.Add(programme);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject json = await GetJsonAsync(client, $"/programmes/{programmeId}");
        Assert.Equal("master", json["level"]!.GetValue<string>());
        Assert.Equal("0732", json["fieldsOfStudy"]!.GetValue<string>());
        Assert.Equal("https://example.org/tier1b-programme", json["link"]!.GetValue<string>());
        Assert.NotNull(json["resources"]);
        Assert.NotNull(json["assessment"]);
        Assert.NotNull(json["enrolment"]);
        Assert.NotNull(json["admissionRequirements"]);
        Assert.NotNull(json["qualificationRequirements"]);
        Assert.NotNull(json["supplementaryInformation"]);
        Assert.NotNull(json["firstStartDateTime"]);
        Assert.NotNull(json["firstPossibleOfferingStartDateTime"]);
        Assert.NotNull(json["lastPossibleOfferingStartDateTime"]);
        Assert.NotNull(json["lastPossibleOfferingEndDateTime"]);
        Assert.Contains(json["learningOutcomeIds"]!.AsArray(),
            i => i!.GetValue<string>() == learningOutcomeId.ToString());
    }

    [Fact]
    public async Task LearningComponentAndTestComponentById_NewOfferingDateFields_ArePresent()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-LC-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Component\"}]",
                FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc)
            };
            dbContext.LearningComponents.Add(learningComponent);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-TC-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Test Component\"}]",
                ResultExpected = true,
                FirstPossibleOfferingStartDateTime = new DateTime(2020, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingStartDateTime = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc),
                LastPossibleOfferingEndDateTime = new DateTime(2027, 8, 11, 23, 59, 0, DateTimeKind.Utc)
            };
            dbContext.TestComponents.Add(testComponent);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject learningComponentJson = await GetJsonAsync(client, $"/learning-components/{learningComponentId}");
        Assert.NotNull(learningComponentJson["firstPossibleOfferingStartDateTime"]);
        Assert.NotNull(learningComponentJson["lastPossibleOfferingStartDateTime"]);
        Assert.NotNull(learningComponentJson["lastPossibleOfferingEndDateTime"]);

        JsonObject testComponentJson = await GetJsonAsync(client, $"/test-components/{testComponentId}");
        Assert.NotNull(testComponentJson["firstPossibleOfferingStartDateTime"]);
        Assert.NotNull(testComponentJson["lastPossibleOfferingStartDateTime"]);
        Assert.NotNull(testComponentJson["lastPossibleOfferingEndDateTime"]);

        // resultExpected has no backing response field (filter-only param) - confirm it's genuinely
        // absent even though the entity that matched it has ResultExpected = true.
        Assert.False(testComponentJson.ContainsKey("resultExpected"));

        // The filter itself: resultExpected=true must match, resultExpected=false must not.
        JsonObject matching = await GetJsonAsync(client, "/test-components?resultExpected=true");
        Assert.Contains(matching["items"]!.AsArray(),
            i => i!["componentId"]!.GetValue<string>() == testComponentId.ToString());

        JsonObject nonMatching = await GetJsonAsync(client, "/test-components?resultExpected=false");
        Assert.DoesNotContain(nonMatching["items"]!.AsArray(),
            i => i!["componentId"]!.GetValue<string>() == testComponentId.ToString());
    }

    [Fact]
    public async Task CourseOfferingAssociationById_StudyLoad_RoundTripsOnReadAndPut()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid courseOfferingId = Guid.CreateVersion7();
        Guid assocId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Org\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-PER-{personId.ToString()[..8]}",
                GivenName = "Tier1b",
                Surname = "Person"
            };
            dbContext.Persons.Add(person);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-CRS2-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Course2\"}]"
            };
            dbContext.Courses.Add(course);

            CourseOfferingEntity courseOffering = new()
            {
                Id = courseOfferingId,
                CourseOfferingId = courseOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-CO-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Offering\"}]",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.CourseOfferings.Add(courseOffering);

            CourseOfferingAssociationEntity association = new()
            {
                Id = assocId,
                CourseOfferingAssociationIdValue = assocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-COA-{assocId.ToString()[..8]}",
                Role = "student",
                CourseOfferingEntityId = courseOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                StudyLoadJson = "{\"studyLoadUnit\":\"hour\",\"value\":8}"
            };
            dbContext.CourseOfferingAssociations.Add(association);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Read.
        JsonObject json = await GetJsonAsync(client, $"/course-offering-associations/{assocId}");
        Assert.Equal("hour", json["studyLoad"]!["studyLoadUnit"]!.GetValue<string>());
        Assert.Equal(8, json["studyLoad"]!["value"]!.GetValue<double>());

        // Write round-trip: PUT with a different studyLoad value, confirm it's reflected on re-read.
        CourseOfferingAssociation putModel = new()
        {
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"T1B-COA-{assocId.ToString()[..8]}" },
            Role = "student",
            State = "associated",
            StudyLoad = new StudyLoadDescriptor { StudyLoadUnit = "ects", Value = 3 },
            CourseOfferingId = new Identifier { Value = courseOfferingId.ToString() },
            PersonId = new Identifier { Value = personId.ToString() }
        };
        HttpResponseMessage putResponse = await client.PutAsJsonAsync($"/course-offering-associations/{assocId}", putModel);
        putResponse.EnsureSuccessStatusCode();

        JsonObject afterPut = await GetJsonAsync(client, $"/course-offering-associations/{assocId}");
        Assert.Equal("ects", afterPut["studyLoad"]!["studyLoadUnit"]!.GetValue<string>());
        Assert.Equal(3, afterPut["studyLoad"]!["value"]!.GetValue<double>());
    }

    [Fact]
    public async Task LearningComponentOfferingAndTestComponentOfferingById_ResultWeight_RoundTripsOnReadAndPut()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid learningComponentId = Guid.CreateVersion7();
        Guid learningComponentOfferingId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();
        Guid testComponentOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-ORG2-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Org2\"}]"
            };
            dbContext.Organisations.Add(org);

            LearningComponentEntity learningComponent = new()
            {
                Id = learningComponentId,
                ComponentId = learningComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-LC2-{learningComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Component2\"}]",
                OrganisationEntityId = org.Id
            };
            dbContext.LearningComponents.Add(learningComponent);

            LearningComponentOfferingEntity learningComponentOffering = new()
            {
                Id = learningComponentOfferingId,
                LearningComponentOfferingIdValue = learningComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-LCO-{learningComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b LC Offering\"}]",
                LearningComponentEntityId = learningComponent.Id,
                OrganisationEntityId = org.Id,
                ResultWeight = 30
            };
            dbContext.LearningComponentOfferings.Add(learningComponentOffering);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-TC2-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Test Component2\"}]",
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponents.Add(testComponent);

            TestComponentOfferingEntity testComponentOffering = new()
            {
                Id = testComponentOfferingId,
                TestComponentOfferingIdValue = testComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-TCO-{testComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b TC Offering\"}]",
                TestComponentEntityId = testComponent.Id,
                OrganisationEntityId = org.Id,
                ResultWeight = 70,
                DocumentsJson =
                    "[{\"documentId\":\"" + Guid.CreateVersion7() +
                    "\",\"documentType\":\"instructions\",\"documentName\":\"Resit instructions.pdf\"}]"
            };
            dbContext.TestComponentOfferings.Add(testComponentOffering);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        JsonObject learningComponentOfferingJson =
            await GetJsonAsync(client, $"/learning-component-offerings/{learningComponentOfferingId}");
        Assert.Equal(30, learningComponentOfferingJson["resultWeight"]!.GetValue<int>());

        JsonObject testComponentOfferingJson =
            await GetJsonAsync(client, $"/test-component-offerings/{testComponentOfferingId}");
        Assert.Equal(70, testComponentOfferingJson["resultWeight"]!.GetValue<int>());
        Assert.Single(testComponentOfferingJson["documents"]!.AsArray());

        // Write round-trip on the learning component offering: PUT a different resultWeight.
        LearningComponentOffering putModel = new()
        {
            LearningComponentOfferingIdValue = learningComponentOfferingId.ToString(),
            PrimaryCode = new IdentifierEntry
            {
                CodeType = "identifier", Code = $"T1B-LCO-{learningComponentOfferingId.ToString()[..8]}"
            },
            Name = [new LanguageTypedString { Language = "en", Value = "Tier1b LC Offering" }],
            ResultWeight = 45,
            LearningComponentId = new Identifier { Value = learningComponentId.ToString() },
            OrganisationId = new Identifier { Value = orgId.ToString() }
        };
        HttpResponseMessage putResponse =
            await client.PutAsJsonAsync($"/learning-component-offerings/{learningComponentOfferingId}", putModel);
        putResponse.EnsureSuccessStatusCode();

        JsonObject afterPut =
            await GetJsonAsync(client, $"/learning-component-offerings/{learningComponentOfferingId}");
        Assert.Equal(45, afterPut["resultWeight"]!.GetValue<int>());
    }

    [Fact]
    public async Task TestComponentOfferingAssociationById_NewFields_RoundTripOnReadAndPut()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid personId = Guid.CreateVersion7();
        Guid testComponentId = Guid.CreateVersion7();
        Guid testComponentOfferingId = Guid.CreateVersion7();
        Guid assocId = Guid.CreateVersion7();
        Guid attemptId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-ORG3-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Org3\"}]"
            };
            dbContext.Organisations.Add(org);

            PersonEntity person = new()
            {
                Id = personId,
                PersonId = personId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-PER2-{personId.ToString()[..8]}",
                GivenName = "Tier1b",
                Surname = "Person2"
            };
            dbContext.Persons.Add(person);

            TestComponentEntity testComponent = new()
            {
                Id = testComponentId,
                ComponentId = testComponentId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-TC3-{testComponentId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b Test Component3\"}]",
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponents.Add(testComponent);

            TestComponentOfferingEntity testComponentOffering = new()
            {
                Id = testComponentOfferingId,
                TestComponentOfferingIdValue = testComponentOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-TCO2-{testComponentOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Tier1b TC Offering2\"}]",
                TestComponentEntityId = testComponent.Id,
                OrganisationEntityId = org.Id
            };
            dbContext.TestComponentOfferings.Add(testComponentOffering);

            TestComponentOfferingAssociationEntity association = new()
            {
                Id = assocId,
                TestComponentOfferingAssociationIdValue = assocId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"T1B-TCOA-{assocId.ToString()[..8]}",
                Role = "student",
                TestComponentOfferingEntityId = testComponentOffering.Id,
                PersonEntityId = person.Id,
                OrganisationEntityId = org.Id,
                ExtraDuration = "PT20M",
                InitialAttemptOnAssociation = 1,
                MaximumNumberOfAttemptsOnAssociation = 2,
                IrregularitiesJson = "[\"Fire alarm interrupted the session\"]",
                DocumentsJson =
                    "[{\"documentId\":\"" + Guid.CreateVersion7() +
                    "\",\"documentType\":\"handed_in_document\",\"documentName\":\"answers.pdf\"}]"
            };
            dbContext.TestComponentOfferingAssociations.Add(association);

            TestComponentOfferingAssociationAttemptEntity attempt = new()
            {
                Id = attemptId,
                AttemptIdValue = attemptId.ToString(),
                TestComponentOfferingAssociationEntityId = assocId
            };
            dbContext.TestComponentOfferingAssociationAttempts.Add(attempt);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Read.
        JsonObject json = await GetJsonAsync(client, $"/test-component-offering-associations/{assocId}");
        Assert.Equal("PT20M", json["extraDuration"]!.GetValue<string>());
        Assert.Equal(1, json["initialAttemptOnAssociation"]!.GetValue<int>());
        Assert.Equal(2, json["maximumNumberOfAttemptsOnAssociation"]!.GetValue<int>());
        Assert.Single(json["irregularities"]!.AsArray());
        Assert.Single(json["documents"]!.AsArray());
        Assert.Contains(json["attemptIds"]!.AsArray(), i => i!.GetValue<string>() == attemptId.ToString());

        // Write round-trip: PUT with different scalar values.
        TestComponentOfferingAssociation putModel = new()
        {
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"T1B-TCOA-{assocId.ToString()[..8]}" },
            Role = "student",
            State = "associated",
            ExtraDuration = "PT45M",
            InitialAttemptOnAssociation = 2,
            MaximumNumberOfAttemptsOnAssociation = 3,
            TestComponentOfferingId = new Identifier { Value = testComponentOfferingId.ToString() },
            PersonId = new Identifier { Value = personId.ToString() }
        };
        HttpResponseMessage putResponse =
            await client.PutAsJsonAsync($"/test-component-offering-associations/{assocId}", putModel);
        putResponse.EnsureSuccessStatusCode();

        JsonObject afterPut = await GetJsonAsync(client, $"/test-component-offering-associations/{assocId}");
        Assert.Equal("PT45M", afterPut["extraDuration"]!.GetValue<string>());
        Assert.Equal(2, afterPut["initialAttemptOnAssociation"]!.GetValue<int>());
        Assert.Equal(3, afterPut["maximumNumberOfAttemptsOnAssociation"]!.GetValue<int>());
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
