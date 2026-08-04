using System.Text.Json;
using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Serializes <see cref="Identifier" /> as a bare JSON string (its <see cref="Identifier.Value" />
///     directly) instead of System.Text.Json's default object-wrapper shape, to match the spec's own
///     <c>Identifier</c> schema (<c>type: string, format: uuid</c>) - see <see cref="Identifier" />'s
///     own doc comment for the full rationale. Applied as a class-level attribute on
///     <see cref="Identifier" /> rather than registered on a specific <see cref="JsonSerializerOptions" />
///     instance, so it applies uniformly everywhere the type is (de)serialized - response output,
///     request-body model binding for write actions, and the manual <c>fields=</c>-selection path
///     alike - without needing to remember to wire it into each one individually.
/// </summary>
public sealed class IdentifierJsonConverter : JsonConverter<Identifier>
{
    public override Identifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;

        if (reader.TokenType != JsonTokenType.String)
            throw new JsonException($"Expected a JSON string for {nameof(Identifier)}, got {reader.TokenType}.");

        return new Identifier { Value = reader.GetString() ?? string.Empty };
    }

    public override void Write(Utf8JsonWriter writer, Identifier value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
