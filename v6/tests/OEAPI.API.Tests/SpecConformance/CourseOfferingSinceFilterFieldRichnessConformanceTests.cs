using System.Text.Json.Nodes;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Guards <c>GET /courses/{courseId}/course-offerings</c>' <c>since</c> filter: a past-dated
///     <see cref="CourseOfferingEntity" /> is excluded by default but included, and fully populated
///     (composite <c>addresses</c>/<c>priceInformation</c>/<c>enrolmentPeriods</c> sub-fields,
///     <c>groupIds</c>), once queried with an explicit <c>since</c>.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class CourseOfferingSinceFilterFieldRichnessConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task PastDatedCourseOffering_ExcludedByDefault_IncludedWithSinceAndFullyPopulated()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        Guid orgId = Guid.CreateVersion7();
        Guid courseId = Guid.CreateVersion7();
        Guid academicSessionId = Guid.CreateVersion7();
        Guid groupId = Guid.CreateVersion7();
        Guid courseOfferingId = Guid.CreateVersion7();

        using (IServiceScope scope = factory.Services.CreateScope())
        {
            SqlServerOEAPIDbContext dbContext = scope.ServiceProvider.GetRequiredService<SqlServerOEAPIDbContext>();

            OrganisationEntity org = new()
            {
                Id = orgId,
                OrganisationId = orgId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"COFR-ORG-{orgId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"FieldRichness Org\"}]"
            };
            dbContext.Organisations.Add(org);

            CourseEntity course = new()
            {
                Id = courseId,
                CourseId = courseId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"COFR-CRS-{courseId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"FieldRichness Course\"}]"
            };
            dbContext.Courses.Add(course);

            // Already started - excluded by the since=<today> default, exactly like the conformance
            // tool's own blind discovery misses this deployment's real seeded data.
            AcademicSessionEntity academicSession = new()
            {
                Id = academicSessionId,
                AcademicSessionId = academicSessionId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"COFR-AS-{academicSessionId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Already Started Session\"}]",
                AcademicSessionType = "semester",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00"
            };
            dbContext.AcademicSessions.Add(academicSession);

            GroupEntity group = new()
            {
                Id = groupId,
                GroupId = groupId.ToString(),
                GroupType = "course",
                PrimaryCodeType = "identifier",
                PrimaryCode = $"COFR-GRP-{groupId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"FieldRichness Group\"}]",
                OrganisationEntityId = org.Id
            };
            dbContext.Groups.Add(group);

            CourseOfferingEntity courseOffering = new()
            {
                Id = courseOfferingId,
                CourseOfferingId = courseOfferingId.ToString(),
                PrimaryCodeType = "identifier",
                PrimaryCode = $"COFR-CO-{courseOfferingId.ToString()[..8]}",
                NameJson = "[{\"language\":\"en\",\"value\":\"Already Started Offering\"}]",
                StartDateTime = "2020-09-01T00:00:00+01:00",
                EndDateTime = "2020-12-01T00:00:00+01:00",
                CourseEntityId = course.Id,
                OrganisationEntityId = org.Id,
                AcademicSessionEntityId = academicSession.Id,
                AddressesJson =
                    "[{\"addressType\":\"visit\",\"street\":\"Moreelsepark\",\"streetNumber\":\"48\"," +
                    "\"additional\":[{\"language\":\"en\",\"value\":\"Main entrance, room 1.05\"}]," +
                    "\"postCode\":\"3511 EP\",\"city\":\"Utrecht\"," +
                    "\"countryCode\":{\"iso3166-1-alpha2\":\"NL\"}," +
                    "\"geolocation\":{\"latitude\":52.089123,\"longitude\":5.113337}," +
                    "\"ext\":{\"x-test-note\":\"field-richness regression fixture\"}}]",
                PriceInformationJson =
                    "[{\"costType\":\"total_costs\",\"amount\":\"250.00\",\"vatAmount\":\"43.39\"," +
                    "\"amountWithoutVat\":\"206.61\",\"currency\":\"EUR\"," +
                    "\"displayAmount\":[{\"language\":\"en-GB\",\"value\":\"250.00 EUR\"}]}]",
                EnrolmentPeriodsJson =
                    "[{\"startDateTime\":\"2020-06-01T00:00:00+01:00\"," +
                    "\"endDateTime\":\"2020-08-31T23:59:59+01:00\"," +
                    "\"targetGroups\":[\"prospective_student\"],\"enrolmentType\":\"broker\"," +
                    "\"enrolmentUrl\":\"https://example.org/enrol\",\"queueEnabled\":true}]",
                ExtJson = "{\"x-test-note\":\"field-richness regression fixture\"}",
                OtherCodes = { new OtherCodeEntity { CodeType = "system_id", Code = "COFR-1" } }
            };
            courseOffering.Groups.Add(group);
            dbContext.CourseOfferings.Add(courseOffering);

            await dbContext.SaveChangesAsync();
        }

        using HttpClient client = factory.CreateClient();

        // Half 1: without since=, the past-dated offering is excluded by the default - the exact
        // condition that starves the conformance tool's own blind fixture discovery.
        JsonObject defaulted = await GetJsonAsync(client, $"/courses/{courseId}/course-offerings");
        Assert.DoesNotContain(defaulted["items"]!.AsArray(),
            i => i!["courseOfferingId"]!.GetValue<string>() == courseOfferingId.ToString());

        // Half 2: with an explicit, early since=, the same offering appears and is fully populated -
        // proving the field data itself was never the problem, only the tool's own discovery strategy.
        JsonObject explicitSince = await GetJsonAsync(client,
            $"/courses/{courseId}/course-offerings?since=2000-01-01T00:00:00%2B00:00");
        JsonObject offering = explicitSince["items"]!.AsArray()
            .Single(i => i!["courseOfferingId"]!.GetValue<string>() == courseOfferingId.ToString())!
            .AsObject();

        JsonObject address = offering["addresses"]!.AsArray()[0]!.AsObject();
        Assert.Equal("Main entrance, room 1.05", address["additional"]![0]!["value"]!.GetValue<string>());
        Assert.Equal("3511 EP", address["postCode"]!.GetValue<string>());
        Assert.Equal(52.089123, address["geolocation"]!["latitude"]!.GetValue<double>());
        Assert.NotNull(address["ext"]);

        JsonObject cost = offering["priceInformation"]!.AsArray()[0]!.AsObject();
        Assert.Equal("43.39", cost["vatAmount"]!.GetValue<string>());
        Assert.Equal("206.61", cost["amountWithoutVat"]!.GetValue<string>());
        Assert.Equal("250.00 EUR", cost["displayAmount"]![0]!["value"]!.GetValue<string>());

        JsonObject enrolmentPeriod = offering["enrolmentPeriods"]!.AsArray()[0]!.AsObject();
        Assert.Equal("prospective_student", enrolmentPeriod["targetGroups"]![0]!.GetValue<string>());
        Assert.Equal("https://example.org/enrol", enrolmentPeriod["enrolmentUrl"]!.GetValue<string>());
        Assert.True(enrolmentPeriod["queueEnabled"]!.GetValue<bool>());

        Assert.Contains(offering["groupIds"]!.AsArray(), g => g!.GetValue<string>() == groupId.ToString());
        Assert.NotNull(offering["ext"]);
    }

    private static async Task<JsonObject> GetJsonAsync(HttpClient client, string requestUri)
    {
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return JsonNode.Parse(await response.Content.ReadAsStringAsync())!.AsObject();
    }
}
