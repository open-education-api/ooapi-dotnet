using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using OEAPI.API.Tests.Infrastructure;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Covers the genuine JSON Merge Patch (RFC 7396) contract on the 4
///     offering-association resources - <c>{remoteState?, result?}</c> in,
///     <see cref="AssociationWriteResponse" /> out. Unlike the full-representation <c>PUT</c>/generic
///     <c>PATCH</c> most other resources use, this narrow contract only ever updates an *existing*
///     association (no upsert - a missing id is a real <c>404</c>, confirmed by reading the
///     controllers), so every test here first <c>PUT</c>s the association into existence.
/// </summary>
[Collection(SqlServerCollection.Name)]
public class PatchAssociationConformanceTests(SqlServerContainerFixture fixture)
{
    private readonly SqlServerContainerFixture _fixture = fixture;

    [Fact]
    public async Task PatchCourseOfferingAssociation_ExistingAssociation_UpdatesRemoteStateAndResult()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/course-offering-associations/{id}", new CourseOfferingAssociation
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry { CodeType = "identifier", Code = $"COAD-{id[..8]}" },
            Role = "student",
            State = "pending"
        });

        HttpResponseMessage response = await PatchMergeAsync(client, $"/course-offering-associations/{id}",
            new AssociationPatchRequest { RemoteState = "associated", Result = new Result { State = "completed", Score = "A", ResultDateTime = "2026-01-01T00:00:00+01:00" } });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        AssociationWriteResponse? body = await response.Content.ReadFromJsonAsync<AssociationWriteResponse>();
        Assert.Equal(id, body!.AssociationId);
        Assert.NotEmpty(body.Message);
        Assert.Equal("pending",
            body.State); // the *normal* state, untouched by this patch - remoteState is a separate field

        CourseOfferingAssociation? afterPatch =
            await client.GetFromJsonAsync<CourseOfferingAssociation>($"/course-offering-associations/{id}");
        Assert.Equal("associated", afterPatch!.RemoteState);
        Assert.NotNull(afterPatch.Result);
    }

    [Fact]
    public async Task PatchCourseOfferingAssociation_NonExistentAssociation_ReturnsNotFound()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();

        HttpResponseMessage response = await PatchMergeAsync(client, $"/course-offering-associations/{Guid.NewGuid()}",
            new AssociationPatchRequest { RemoteState = "associated" });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PatchProgrammeOfferingAssociation_ExistingAssociation_UpdatesRemoteStateAndResult()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/programme-offering-associations/{id}", new ProgrammeOfferingAssociation
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"POAD-{id[..8]}" },
            Role = "student",
            State = "pending"
        });

        HttpResponseMessage response = await PatchMergeAsync(client, $"/programme-offering-associations/{id}",
            new AssociationPatchRequest { RemoteState = "associated", Result = new Result { State = "completed", Score = "A", ResultDateTime = "2026-01-01T00:00:00+01:00" } });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        AssociationWriteResponse? body = await response.Content.ReadFromJsonAsync<AssociationWriteResponse>();
        Assert.Equal(id, body!.AssociationId);
        Assert.NotEmpty(body.Message);

        ProgrammeOfferingAssociation? afterPatch =
            await client.GetFromJsonAsync<ProgrammeOfferingAssociation>($"/programme-offering-associations/{id}");
        Assert.Equal("associated", afterPatch!.RemoteState);
        Assert.NotNull(afterPatch.Result);
    }

    [Fact]
    public async Task PatchLearningComponentOfferingAssociation_ExistingAssociation_UpdatesRemoteStateAndResult()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/learning-component-offering-associations/{id}",
            new LearningComponentOfferingAssociation
            {
                AssociationIdValue = id,
                PrimaryCode = new IdentifierEntry
                { CodeType = "identifier", Code = $"LCOAD-{id[..8]}" },
                Role = "student",
                State = "pending"
            });

        HttpResponseMessage response = await PatchMergeAsync(client, $"/learning-component-offering-associations/{id}",
            new AssociationPatchRequest { RemoteState = "associated", Result = new Result { State = "completed", Score = "A", ResultDateTime = "2026-01-01T00:00:00+01:00" } });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        AssociationWriteResponse? body = await response.Content.ReadFromJsonAsync<AssociationWriteResponse>();
        Assert.Equal(id, body!.AssociationId);
        Assert.NotEmpty(body.Message);

        LearningComponentOfferingAssociation? afterPatch =
            await client.GetFromJsonAsync<LearningComponentOfferingAssociation>(
                $"/learning-component-offering-associations/{id}");
        Assert.Equal("associated", afterPatch!.RemoteState);
        Assert.NotNull(afterPatch.Result);
    }

    [Fact]
    public async Task PatchTestComponentOfferingAssociation_ExistingAssociation_UpdatesRemoteStateAndResult()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/test-component-offering-associations/{id}", new TestComponentOfferingAssociation
        {
            AssociationIdValue = id,
            PrimaryCode = new IdentifierEntry
            { CodeType = "identifier", Code = $"TCOAD-{id[..8]}" },
            Role = "student",
            State = "pending"
        });

        HttpResponseMessage response = await PatchMergeAsync(client, $"/test-component-offering-associations/{id}",
            new AssociationPatchRequest { RemoteState = "associated", Result = new Result { State = "completed", Score = "A", ResultDateTime = "2026-01-01T00:00:00+01:00" } });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        AssociationWriteResponse? body = await response.Content.ReadFromJsonAsync<AssociationWriteResponse>();
        Assert.Equal(id, body!.AssociationId);
        Assert.NotEmpty(body.Message);

        TestComponentOfferingAssociation? afterPatch =
            await client.GetFromJsonAsync<TestComponentOfferingAssociation>(
                $"/test-component-offering-associations/{id}");
        Assert.Equal("associated", afterPatch!.RemoteState);
        Assert.NotNull(afterPatch.Result);
    }

    [Fact]
    public async Task PatchTestComponentOfferingAssociationAttempt_NoState_OmitsStateFieldRatherThanNull()
    {
        // Regression test: TestComponentOfferingAssociationAttempt's own canonical `state` field
        // explicitly allows null (oneOf: [attemptState, null]), but AssociationWriteResponse.State
        // (this PATCH endpoint's own response type, shared with the 4 sibling association PATCH
        // endpoints) maps to `associationState`, which does not - the response schema does mark
        // `state` optional though, so the fix is to omit the key entirely rather than serialize it
        // as literal `null`, which the schema-conformant server must do to avoid violating it.
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/test-component-offering-associations-attempt/{id}",
            new TestComponentOfferingAssociationAttempt { AttemptId = id });

        HttpResponseMessage response = await client.PatchAsync($"/test-component-offering-associations-attempt/{id}",
            JsonContent.Create(new AssociationPatchRequest
            {
                Result = new Result { State = "completed", Score = "A", ResultDateTime = "2026-01-01T00:00:00+01:00" }
            }, mediaType: new MediaTypeHeaderValue("application/merge-patch+json")));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        JsonDocument body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        Assert.False(body.RootElement.TryGetProperty("state", out _), "state must be omitted, not null");
    }

    [Fact]
    public async Task PatchCourseOfferingAssociation_WrongContentType_ReturnsUnsupportedMediaType()
    {
        using OeapiWebApplicationFactory factory = _fixture.CreateFactory();
        using HttpClient client = factory.CreateClient();
        string id = Guid.NewGuid().ToString();

        await client.PutAsJsonAsync($"/course-offering-associations/{id}", new CourseOfferingAssociation
        {
            AssociationIdValue = id,
            PrimaryCode =
                new IdentifierEntry { CodeType = "identifier", Code = $"COAD2-{id[..8]}" },
            Role = "student",
            State = "pending"
        });

        // application/json instead of the spec-required application/merge-patch+json.
        using StringContent content =
            new(JsonSerializer.Serialize(new AssociationPatchRequest { RemoteState = "associated" }));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await client.PatchAsync($"/course-offering-associations/{id}", content);

        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
    }

    private static async Task<HttpResponseMessage> PatchMergeAsync(HttpClient client, string path,
        AssociationPatchRequest request)
    {
        using StringContent content = new(JsonSerializer.Serialize(request));
        content.Headers.ContentType = new MediaTypeHeaderValue("application/merge-patch+json");
        return await client.PatchAsync(path, content);
    }
}
