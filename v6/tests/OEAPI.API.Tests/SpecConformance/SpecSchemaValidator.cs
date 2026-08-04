using System.Text.Json;
using System.Text.Json.Nodes;
using Json.Schema;

namespace OEAPI.API.Tests.SpecConformance;

/// <summary>
///     Validates a real HTTP response body against the spec's own JSON Schema for that operation - not
///     just status codes/shape spot-checks (phases A-F), the actual declared schema (required fields,
///     types, nested <c>$ref</c>s). OpenAPI 3.1 schemas are genuine JSON Schema draft 2020-12 (confirmed
///     by inspection: no OpenAPI-3.0-isms like <c>nullable: true</c>), so <c>JsonSchema.Net</c> can
///     evaluate them directly with no preprocessing.
/// </summary>
public static class SpecSchemaValidator
{
    /// <summary>
    ///     Evaluates <paramref name="responseBody" /> against <paramref name="operation" />'s
    ///     <see cref="SpecOperation.ResponseSchema" />. Throws if the operation has no JSON response
    ///     schema to validate against (call sites should only invoke this for JSON operations).
    /// </summary>
    public static EvaluationResults Validate(SpecOperation operation, JsonNode? responseBody)
    {
        if (operation.ResponseSchema is not { } responseSchemaElement)
            throw new InvalidOperationException($"{operation} has no responseSchema to validate against.");

        // Combines the operation's own response schema with the spec's full components.schemas
        // dictionary into one document, so the response schema's internal "$ref"s resolve without a
        // custom resolver. "components.schemas" is an OpenAPI wrapper concept, not real JSON Schema
        // vocabulary - JsonSchema.Net rejects "components" as an unrecognized keyword - so this uses
        // the standard "$defs" keyword instead, rewriting every "#/components/schemas/X" ref (both
        // in the response schema and, recursively, within the component schemas themselves) to
        // "#/$defs/X" via a plain string replace before parsing.
        JsonObject combined = new()
        {
            ["$defs"] = JsonNode.Parse(SpecManifest.ComponentSchemas.GetRawText())
        };

        JsonObject responseSchemaNode = JsonNode.Parse(responseSchemaElement.GetRawText())!.AsObject();
        foreach (string key in responseSchemaNode.Select(kvp => kvp.Key).ToList())
        {
            JsonNode? value = responseSchemaNode[key];
            responseSchemaNode.Remove(key);
            combined[key] = value;
        }

        // The spec carries a couple of OpenAPI-era annotation keywords JSON Schema draft 2020-12
        // itself doesn't define (singular "example" instead of the standard plural "examples", and
        // the custom "x-ooapi-extensible-enum" vocabulary extension) - JsonSchema.Net's dialect
        // rejects unrecognized keywords outright by default, so they're stripped before parsing
        // rather than fought at the library-configuration level.
        StripAnnotationKeywords(combined);

        string combinedText = combined.ToJsonString()
            .Replace("#/components/schemas/", "#/$defs/", StringComparison.Ordinal);
        JsonSchema schema = JsonSchema.FromText(combinedText);
        JsonElement instance = responseBody == null
            ? default
            : JsonSerializer.Deserialize<JsonElement>(responseBody.ToJsonString());
        return schema.Evaluate(instance, new EvaluationOptions { OutputFormat = OutputFormat.List });
    }

    private static void StripAnnotationKeywords(JsonNode? node)
    {
        switch (node)
        {
            case JsonObject obj:
                foreach (string key in obj.Select(kvp => kvp.Key).ToList())
                    if (key == "example" || key.StartsWith("x-", StringComparison.Ordinal))
                        obj.Remove(key);

                foreach ((string _, JsonNode? value) in obj) StripAnnotationKeywords(value);
                break;

            case JsonArray array:
                foreach (JsonNode? item in array) StripAnnotationKeywords(item);
                break;
        }
    }

    /// <summary>
    ///     Flattens a (possibly nested) evaluation result into a human-readable summary of
    ///     every failed sub-schema, for use in test failure messages.
    /// </summary>
    public static string DescribeErrors(EvaluationResults results)
    {
        List<string> lines = [];
        Collect(results);
        return string.Join("\n", lines);

        void Collect(EvaluationResults node)
        {
            if (node.Errors is { Count: > 0 })
                foreach ((string keyword, string message) in node.Errors)
                    lines.Add($"  at {node.InstanceLocation}: [{keyword}] {message}");

            if (node.Details != null)
                foreach (EvaluationResults child in node.Details)
                    Collect(child);
        }
    }
}
