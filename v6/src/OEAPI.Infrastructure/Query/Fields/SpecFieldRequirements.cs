using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OEAPI.Infrastructure.Query.Fields;

/// <summary>
///     Loads the pre-generated, per-schema <c>required</c> field sets and nested object-property
///     schema map (embedded resource, produced by <c>scripts/extract-spec-manifest.py</c> from the
///     specification's published OpenAPI document), so <see cref="FieldPruner" /> can source "what
///     must always survive a <c>fields=</c> selection" from the spec itself instead of a
///     hand-maintained heuristic. Re-run the script (and rebuild) if the spec changes.
/// </summary>
public static class SpecFieldRequirements
{
    private static readonly Lazy<FieldRequirementsDocument> LazyDocument = new(Load);
    private static readonly SchemaEntry Empty = new();

    /// <summary>The set of field names the given schema marks <c>required</c> (empty if the schema is unknown).</summary>
    public static IReadOnlySet<string> RequiredFields(string schemaName)
    {
        return LazyDocument.Value.Schemas.TryGetValue(ResolveManifestKey(schemaName), out SchemaEntry? entry)
            ? entry.Required
            : Empty.Required;
    }

    /// <summary>
    ///     Whether <paramref name="schemaName" /> has a manifest entry at all - distinguishes "this
    ///     schema genuinely has zero required fields" (an empty, but real, <see cref="RequiredFields" />
    ///     result) from "this name isn't a spec schema" (also an empty result, but nothing is known
    ///     about it). Callers that would otherwise treat "no required fields" as license to touch every
    ///     property need this check first - see <c>OeapiJsonSerializerOptions</c>.
    /// </summary>
    public static bool IsKnownSchema(string schemaName)
    {
        return LazyDocument.Value.Schemas.ContainsKey(ResolveManifestKey(schemaName));
    }

    /// <summary>
    ///     The named component schema <paramref name="propertyName" /> resolves to on
    ///     <paramref name="schemaName" />, or <see langword="null" /> if it's a scalar field or
    ///     <paramref name="schemaName" />/<paramref name="propertyName" /> isn't a known object-valued
    ///     property.
    /// </summary>
    public static string? NestedSchemaName(string schemaName, string propertyName)
    {
        if (!LazyDocument.Value.Schemas.TryGetValue(ResolveManifestKey(schemaName), out SchemaEntry? entry))
            return null;

        return entry.Properties.TryGetValue(propertyName, out string? nestedSchemaName)
            ? nestedSchemaName
            : null;
    }

    private static FieldRequirementsDocument Load()
    {
        Assembly assembly = typeof(SpecFieldRequirements).Assembly;
        string resourceName = assembly.GetManifestResourceNames()
                                  .SingleOrDefault(name =>
                                      name.EndsWith("field-requirements.json", StringComparison.Ordinal))
                              ?? throw new InvalidOperationException(
                                  "field-requirements.json embedded resource not found - was it built as an EmbeddedResource?");

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        return JsonSerializer.Deserialize<FieldRequirementsDocument>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Failed to deserialize field-requirements.json.");
    }

    /// <summary>
    ///     Resolves the manifest key to actually look a schema name up under. Almost every schema name
    ///     is matched exactly (this codebase deliberately keeps API model class names aligned 1:1 with
    ///     their spec schema name - see <c>omit-null-optional-fields</c>'s <c>design.md</c>), but the
    ///     spec's own <c>personalNeed</c> schema is the one object schema in the whole manifest that
    ///     isn't PascalCase (every other ~113 are) - matching it needs a targeted fallback, not a
    ///     case-insensitive comparer on the whole dictionary: the manifest genuinely contains distinct,
    ///     independently-meaningful entries that differ only by case elsewhere (e.g. <c>CourseOfferingId</c>
    ///     - the standalone id-wrapper component schema - and <c>courseOfferingId</c> - an inline
    ///     property-shaped entry from a different generation path in <c>extract-spec-manifest.py</c>), so
    ///     a blanket case-insensitive lookup would silently collapse those into one and lose data.
    ///     Confirmed safe: none of this codebase's other API model class names collide with the
    ///     lowercase-first-letter form of another manifest entry - this fallback only ever adds
    ///     <c>PersonalNeed</c>/<c>personalNeed</c>, never risks matching the wrong schema.
    /// </summary>
    private static string ResolveManifestKey(string schemaName)
    {
        if (LazyDocument.Value.Schemas.ContainsKey(schemaName)) return schemaName;

        string camelCase = char.ToLowerInvariant(schemaName[0]) + schemaName[1..];
        return LazyDocument.Value.Schemas.ContainsKey(camelCase) ? camelCase : schemaName;
    }

    // ReSharper disable once CollectionNeverUpdated.Local - populated by JSON deserialization, not by
    // in-code Add calls.
    private sealed class SchemaEntry
    {
        public HashSet<string> Required { get; set; } = new(StringComparer.OrdinalIgnoreCase);
        public Dictionary<string, string> Properties { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }

    private sealed class FieldRequirementsDocument
    {
        public string SourceSpec { get; set; } = string.Empty;

        [JsonPropertyName("schemas")]
        public Dictionary<string, SchemaEntry> Schemas { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    }
}
