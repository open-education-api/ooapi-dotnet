using System.Reflection;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.API.OpenApi;

/// <summary>
///     Reflects <see cref="RegexPatternAttribute" />/<see cref="ExtensibleEnumAttribute" /> - two custom
///     <c>ValidationAttribute</c>s the built-in OpenAPI generator has no knowledge of, since it only
///     recognises a fixed allowlist of <c>System.ComponentModel.DataAnnotations</c> types via
///     <c>JsonSchemaExporter</c> - onto the generated schema, so a client reading only
///     <c>/openapi/v1.json</c> can discover both constraints instead of just the ones expressed through
///     recognised attributes like <c>[StringLength]</c>. Runs once per property (via
///     <see cref="OpenApiSchemaTransformerContext.JsonPropertyInfo" />, non-null exactly when the schema
///     being transformed corresponds to a single property rather than a whole type/array-items schema),
///     covering both a plain string property and a string-array property (e.g.
///     <c>Course.ModesOfDelivery</c>, <c>PersonProperties.LanguageOfChoice</c>) by writing onto the
///     array's <c>items</c> schema instead of the array schema itself in the latter case.
/// </summary>
public sealed class ValidationAttributeSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.JsonPropertyInfo?.AttributeProvider is not { } attributeProvider)
            return Task.CompletedTask;

        // JsonSchemaType is a [Flags] enum - a nullable array property's schema reports
        // `Null | Array`, not `Array` alone, so this has to test the flag rather than compare equal.
        bool isArray = schema.Type is { } type && type.HasFlag(JsonSchemaType.Array);

        OpenApiSchema? targetSchema = isArray ? schema.Items as OpenApiSchema : schema;

        if (targetSchema == null)
            return Task.CompletedTask;

        if (GetSingleAttribute<RegexPatternAttribute>(attributeProvider) is { } regexPattern)
            targetSchema.Pattern = RegexPatterns.Values[regexPattern.SchemaName];

        if (GetSingleAttribute<ExtensibleEnumAttribute>(attributeProvider) is { } extensibleEnum)
        {
            string[] knownValues = ExtensibleEnumValues.Values[extensibleEnum.SchemaName];

            // Expressed as "one of these known values, or an institution-defined x-prefixed custom
            // value" via anyOf, rather than a single `enum` (which can't also allow the x-prefixed
            // case) or a single `pattern` (which would have to hand-encode case-insensitive
            // alternation of every known value rather than reusing `enum`'s own semantics).
            targetSchema.AnyOf =
            [
                new OpenApiSchema { Enum = [.. knownValues.Select(v => (JsonNode)v)] },
                new OpenApiSchema { Pattern = "^x-" }
            ];
        }

        return Task.CompletedTask;
    }

    private static TAttribute? GetSingleAttribute<TAttribute>(ICustomAttributeProvider attributeProvider)
        where TAttribute : Attribute
    {
        object[] attributes = attributeProvider.GetCustomAttributes(typeof(TAttribute), true);
        return attributes.Length > 0 ? (TAttribute)attributes[0] : null;
    }
}
