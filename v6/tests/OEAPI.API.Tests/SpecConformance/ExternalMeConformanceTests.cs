using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Interfaces;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Covers the <c>external/me</c> auth seam. The one genuinely
///     deployment-specific piece of these endpoints is <see cref="ICurrentPersonProvider" /> (see its
///     own doc comment) - real authentication middleware/providers aren't registered in this test host
///     any more than they are in the real dev environment, so "authenticated" here means substituting a
///     <see cref="FakeCurrentPersonProvider" /> via <c>ConfigureTestServices</c>, not faking JWTs/API
///     keys. "No auth" needs no stub at all: with nothing registered, <c>DefaultCurrentPersonProvider</c>
///     already returns <see langword="null" />, exactly like the real unauthenticated-request path.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class ExternalMeConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    private WebApplicationFactory<Program> CreateAuthenticatedFactory(string personId)
    {
        return _fixture.CreateFactory().WithWebHostBuilder(builder =>
            builder.ConfigureTestServices(services =>
                services.AddScoped<ICurrentPersonProvider>(_ => new FakeCurrentPersonProvider(personId))));
    }

    [Fact]
    public async Task PostExternalMe_NoAuth_ReturnsUnauthorized()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                CourseOfferingId = new Identifier { Value = Guid.NewGuid().ToString() },
                Issuer = new Organisation
                {
                    OrganisationId = Guid.NewGuid().ToString(),
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = "EM-NOAUTH" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "No-auth Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PostExternalMe_Authenticated_MissingOfferingReference_ReturnsBadRequest()
    {
        string personId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                // Neither CourseOfferingId nor CourseOffering supplied.
                Issuer = new Organisation
                {
                    OrganisationId = Guid.NewGuid().ToString(),
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = "EM-MISSING-OFF" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "Missing-offering Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostExternalMe_Authenticated_NonExistentOffering_ReturnsNotFound()
    {
        string personId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                CourseOfferingId = new Identifier { Value = Guid.NewGuid().ToString() }, // does not exist
                Issuer = new Organisation
                {
                    OrganisationId = Guid.NewGuid().ToString(),
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = "EM-NOOFFERING" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "No-offering Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostExternalMe_Authenticated_NonExistentIssuer_ReturnsNotFound()
    {
        string personId = Guid.NewGuid().ToString();
        string courseOfferingId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });
        await client.PutAsJsonAsync($"/course-offerings/{courseOfferingId}", new CourseOffering
        {
            CourseOfferingIdValue = courseOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"EM-CO-{courseOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Offering" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                CourseOfferingId = new Identifier { Value = courseOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = Guid.NewGuid().ToString(), // does not exist
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = "EM-NOISSUER" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "Non-existent Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostExternalMe_Authenticated_OmittedIssuer_ReturnsNotFoundNotValidationError()
    {
        // Regression test: Issuer is optional per spec (not in this operation's own required list).
        // Omitting it entirely used to leave a default-constructed Organisation with an empty
        // organisationType, which [ExtensibleEnum] then rejected as "'' is not a recognised value" -
        // a confusing 400 for a field the client never sent. It should reach the same graceful
        // "issuer organisation not found" 404 an explicitly-wrong issuer id already produces.
        string personId = Guid.NewGuid().ToString();
        string courseOfferingId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });
        await client.PutAsJsonAsync($"/course-offerings/{courseOfferingId}", new CourseOffering
        {
            CourseOfferingIdValue = courseOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"EM-CO-{courseOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Offering" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                CourseOfferingId = new Identifier { Value = courseOfferingId }
                // Issuer omitted entirely.
            });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PostExternalMe_Authenticated_ValidRequest_CreatesAssociationForCaller()
    {
        string personId = Guid.NewGuid().ToString();
        string courseOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });
        await client.PutAsJsonAsync($"/course-offerings/{courseOfferingId}", new CourseOffering
        {
            CourseOfferingIdValue = courseOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"EM-CO-{courseOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EM-ORG-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Issuer" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                CourseOfferingId = new Identifier { Value = courseOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EM-ORG-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "External/me Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        AssociationWriteResponse? body = await response.Content.ReadFromJsonAsync<AssociationWriteResponse>();
        Assert.False(string.IsNullOrEmpty(body!.AssociationId));

        CourseOfferingAssociation? created =
            await client.GetFromJsonAsync<CourseOfferingAssociation>(
                $"/course-offering-associations/{body.AssociationId}");
        Assert.Equal(personId, created!.PersonId!.Value);
        Assert.Equal("student", created.Role);
    }

    [Fact]
    public async Task PostExternalMe_Authenticated_ExplicitNullRemoteState_IsAccepted()
    {
        // Regression test: remoteState is required as a *key* by this endpoint's own schema, but its
        // value schema is oneOf: [remoteAssociationState, {type: null}] - an explicit null is
        // spec-valid. RemoteState used to be a plain non-nullable string, so ASP.NET Core's automatic
        // implicit-required-for-non-nullable-reference-type validation rejected an explicit null as
        // "The RemoteState field is required.", even though the key was genuinely present.
        string personId = Guid.NewGuid().ToString();
        string courseOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });
        await client.PutAsJsonAsync($"/course-offerings/{courseOfferingId}", new CourseOffering
        {
            CourseOfferingIdValue = courseOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"EM-CO-{courseOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EM-ORG-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Issuer" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/course-offering-associations/external/me",
            new CourseOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = null,
                CourseOfferingId = new Identifier { Value = courseOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EM-ORG-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "External/me Issuer" }]
                }
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task
        PostProgrammeOfferingAssociationExternalMe_Authenticated_ValidRequest_CreatesAssociationForCaller()
    {
        string personId = Guid.NewGuid().ToString();
        string programmeOfferingId = Guid.NewGuid().ToString();
        string issuerId = Guid.NewGuid().ToString();
        using WebApplicationFactory<Program> factory = CreateAuthenticatedFactory(personId);
        using HttpClient client = factory.CreateClient();
        await client.PutAsJsonAsync($"/persons/{personId}",
            new Person
            {
                PersonIdValue = personId,
                PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"EM-{personId[..8]}" },
                Surname = "ExternalMeTestPerson"
            });
        await client.PutAsJsonAsync($"/programme-offerings/{programmeOfferingId}", new ProgrammeOffering
        {
            ProgrammeOfferingIdValue = programmeOfferingId,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"EM-PO-{programmeOfferingId[..8]}" },
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Programme Offering" }]
        });
        await client.PutAsJsonAsync($"/organisations/{issuerId}", new Organisation
        {
            OrganisationId = issuerId,
            PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EM-ORG2-{issuerId[..8]}" },
            OrganisationType = "root",
            Name = [new LanguageTypedString { Language = "en", Value = "External/me Issuer 2" }]
        });

        HttpResponseMessage response = await client.PostAsJsonAsync("/programme-offering-associations/external/me",
            new ProgrammeOfferingAssociationExternalMeRequest
            {
                Role = "student",
                State = "pending",
                RemoteState = "associated",
                ProgrammeOfferingId = new Identifier { Value = programmeOfferingId },
                Issuer = new Organisation
                {
                    OrganisationId = issuerId,
                    PrimaryCode = new IdentifierEntry { CodeType = "organisation_id", Code = $"EM-ORG2-{issuerId[..8]}" },
                    OrganisationType = "root",
                    Name = [new LanguageTypedString { Language = "en", Value = "External/me Issuer 2" }]
                }
            });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        AssociationWriteResponse? body = await response.Content.ReadFromJsonAsync<AssociationWriteResponse>();
        Assert.False(string.IsNullOrEmpty(body!.AssociationId));

        ProgrammeOfferingAssociation? created =
            await client.GetFromJsonAsync<ProgrammeOfferingAssociation>(
                $"/programme-offering-associations/{body.AssociationId}");
        Assert.Equal(personId, created!.PersonId!.Value);
    }
}
