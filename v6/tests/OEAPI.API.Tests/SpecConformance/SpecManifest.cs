using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OEAPI.API.Tests.SpecConformance;

public sealed class SpecPathParam
{
    public string Name { get; set; } = string.Empty;
    public string? Format { get; set; }
}

public sealed class SpecRequestBody
{
    public string ContentType { get; set; } = string.Empty;
    public bool Required { get; set; }
}

/// <summary>
///     One operation (path + HTTP method) from the spec, as extracted by
///     <c>scripts/extract-spec-manifest.py</c>.
/// </summary>
public sealed class SpecOperation
{
    public string Path { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string? OperationId { get; set; }
    public string[] Tags { get; set; } = [];
    public SpecPathParam[] PathParams { get; set; } = [];
    public SpecRequestBody? RequestBody { get; set; }
    public int[] SuccessStatusCodes { get; set; } = [];
    public bool IsPagedResponse { get; set; }
    public bool IsTopLevelListGet { get; set; }

    /// <summary>
    ///     The raw JSON Schema (OpenAPI 3.1 = genuine JSON Schema draft 2020-12) for this operation's
    ///     success response body, or <see langword="null" /> for operations with no JSON response (e.g.
    ///     <c>GET /documents/{documentId}</c>, a binary file download). Internal <c>$ref</c>s point at
    ///     <c>#/components/schemas/...</c>, resolved against <see cref="SpecManifest.ComponentSchemas" /> -
    ///     see <c>SpecSchemaValidator</c> for how the two are combined into one document to validate
    ///     against.
    /// </summary>
    public JsonElement? ResponseSchema { get; set; }

    public override string ToString()
    {
        return $"{Method} {Path}";
    }
}

/// <summary>
///     Loads the pre-generated spec manifest (embedded resource, produced by
///     <c>scripts/extract-spec-manifest.py</c> from the specification's published OpenAPI document) so
///     the generic conformance tests can iterate every real spec operation without parsing the 640KB
///     source spec at test time. Re-run the script (and rebuild) if the spec changes.
/// </summary>
public static class SpecManifest
{
    private const string PlaceholderId = "00000000-0000-0000-0000-000000000001";

    private static readonly Lazy<SpecManifestDocument> LazyDocument = new(Load);

    public static IReadOnlyList<SpecOperation> Operations => LazyDocument.Value.OperationsList;

    /// <summary>
    ///     The spec's full <c>components.schemas</c> dictionary, shared by every operation's
    ///     <see cref="SpecOperation.ResponseSchema" /> via <c>$ref</c>.
    /// </summary>
    public static JsonElement ComponentSchemas => LazyDocument.Value.ComponentSchemasValue;

    private static SpecManifestDocument Load()
    {
        Assembly assembly = typeof(SpecManifest).Assembly;
        string resourceName = assembly.GetManifestResourceNames()
                                  .SingleOrDefault(name =>
                                      name.EndsWith("spec-manifest.json", StringComparison.Ordinal))
                              ?? throw new InvalidOperationException(
                                  "spec-manifest.json embedded resource not found - was it built as an EmbeddedResource?");

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        return JsonSerializer.Deserialize<SpecManifestDocument>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Failed to deserialize spec-manifest.json.");
    }

    /// <summary>Builds a concrete request path, substituting every path parameter with a fixed placeholder GUID.</summary>
    public static string BuildConcretePath(SpecOperation operation)
    {
        string path = operation.Path;
        foreach (SpecPathParam param in operation.PathParams)
            path = path.Replace($"{{{param.Name}}}", PlaceholderId, StringComparison.Ordinal);

        return path;
    }

    private sealed class SpecManifestDocument
    {
        public string SourceSpec { get; set; } = string.Empty;
        public int OperationCount { get; set; }

        [JsonPropertyName("operations")]
        public SpecOperation[] OperationsList { get; set; } = [];

        [JsonPropertyName("componentSchemas")]
        public JsonElement ComponentSchemasValue { get; set; }
    }
}
