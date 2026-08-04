using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using OEAPI.Infrastructure.Query.Fields;

namespace OEAPI.API.Serialization;

/// <summary>
///     Shared <see cref="JsonSerializerOptions" />, matching ASP.NET Core's <see cref="JsonSerializerOptions.Web" />
///     defaults (camelCase, etc.) plus two deliberate customizations, both omitting a property from the
///     response entirely when its value is <see langword="null" /> instead of writing an explicit
///     <c>null</c>:
///     <list type="bullet">
///         <item>
///             <description>
///                 Any property not marked <c>required</c> by its declaring type's own spec schema (per
///                 <see cref="SpecFieldRequirements" />) - see <see cref="OmitNullOptionalFields" />. A
///                 <c>required</c> property always stays present, <c>null</c> value included - required-
///                 ness, not runtime nullability, decides this. Types with no spec schema entry (e.g. the
///                 generic <see cref="OEAPI.Infrastructure.Query.PagedResult{T}" /> wrapper) are left
///                 untouched by this modifier rather than guessed at.
///             </description>
///         </item>
///         <item>
///             <description>
///                 Any property serialized as <c>"ext"</c> (the spec's free-form extension anchor - see
///                 <c>oeapi.json</c>'s <c>Ext</c> schema) - see <see cref="OmitNullExt" />. <c>ext</c> is
///                 never spec-required anywhere, so this is now mostly subsumed by the modifier above; kept
///                 as its own independent modifier because it's the only thing still covering <c>ext</c> on
///                 <see cref="OEAPI.Infrastructure.Query.PagedResult{T}" /> and any other type the spec
///                 manifest doesn't recognise.
///             </description>
///         </item>
///     </list>
/// </summary>
/// <remarks>
///     Used both as the MVC output formatter's options (<c>Program.cs</c>) and directly by
///     <c>GenericEntityController</c>/<c>BaseApiController</c>'s manual <c>fields</c>-selection
///     serialization path - both need the same behaviour, and <see cref="JsonSerializerOptions.Web" />
///     itself is a shared, immutable framework singleton that cannot be customized in place.
/// </remarks>
public static class OeapiJsonSerializerOptions
{
    public static readonly JsonSerializerOptions Instance = Build();

    private static JsonSerializerOptions Build()
    {
        return new JsonSerializerOptions(JsonSerializerOptions.Web)
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { OmitNullExt, OmitNullOptionalFields }
            }
        };
    }

    private static void OmitNullExt(JsonTypeInfo typeInfo)
    {
        foreach (JsonPropertyInfo property in typeInfo.Properties)
            if (property.Name == "ext")
                property.ShouldSerialize = (_, value) => value != null;
    }

    private static void OmitNullOptionalFields(JsonTypeInfo typeInfo)
    {
        string schemaName = typeInfo.Type.Name;
        if (!SpecFieldRequirements.IsKnownSchema(schemaName))
            return;

        IReadOnlySet<string> requiredFields = SpecFieldRequirements.RequiredFields(schemaName);

        foreach (JsonPropertyInfo property in typeInfo.Properties)
            if (!requiredFields.Contains(property.Name))
                property.ShouldSerialize = (_, value) => value != null;
    }
}
