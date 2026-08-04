using System.Text.Json.Nodes;

namespace OEAPI.Infrastructure.Query.Fields;

/// <summary>
///     Prunes a serialized <see cref="JsonNode" /> tree down to a requested <see cref="FieldSelection" />,
///     recursing into nested objects/arrays that have their own sub-selection. Mutates the given node
///     tree in place (removing unselected properties from each <see cref="JsonObject" />) rather than
///     building a new one - safe here since <see cref="JsonNode" /> instances passed in are always
///     freshly serialized just for this purpose, never a shared/cached tree.
/// </summary>
public static class FieldPruner
{
    /// <summary>
    ///     Prunes <paramref name="node" /> in place to only the fields in <paramref name="selection" />,
    ///     always keeping every field the OOAPI v6 spec marks <c>required</c> for
    ///     <paramref name="schemaName" /> (sourced from <see cref="SpecFieldRequirements" />, generated
    ///     from the canonical spec) even if not explicitly requested - a <c>fields=</c> selection can
    ///     only narrow which optional fields are returned, never drop a spec-mandated one. Recursion into
    ///     a nested selection (e.g. <c>programme(code)</c>) uses that property's own resolved nested
    ///     schema name, so a nested object's required fields are judged against its own schema, not the
    ///     root's.
    /// </summary>
    public static JsonNode? Prune(JsonNode? node, FieldSelection selection, string schemaName)
    {
        switch (node)
        {
            case JsonObject obj:
                List<string> keysToRemove = [.. obj
                    .Select(kv => kv.Key)
                    .Where(key => !IsSelected(key, selection, schemaName))];
                foreach (string key in keysToRemove) obj.Remove(key);

                foreach (string key in obj.Select(kv => kv.Key).ToList())
                    if (selection.Children.TryGetValue(key, out FieldSelection? childSelection) &&
                        childSelection.HasChildren)
                    {
                        string? nestedSchemaName = SpecFieldRequirements.NestedSchemaName(schemaName, key);
                        if (nestedSchemaName != null)
                            Prune(obj[key], childSelection, nestedSchemaName);
                    }

                break;

            case JsonArray array:
                foreach (JsonNode? item in array) Prune(item, selection, schemaName);

                break;
        }

        return node;
    }

    private static bool IsSelected(string jsonPropertyName, FieldSelection selection, string schemaName)
    {
        return selection.Children.ContainsKey(jsonPropertyName)
               || SpecFieldRequirements.RequiredFields(schemaName).Contains(jsonPropertyName);
    }
}
