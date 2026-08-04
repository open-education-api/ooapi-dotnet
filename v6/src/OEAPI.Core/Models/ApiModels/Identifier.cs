using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An identifier of another resource. Serializes as a bare JSON string (per the spec's own
///     <c>Identifier</c> schema, <c>type: string, format: uuid</c>) via
///     <see cref="IdentifierJsonConverter" />, not as an object wrapper - this class's own shape
///     (a single <see cref="Value" /> property) is purely a C#-side convenience, never reflected in
///     the actual JSON on the wire.
/// </summary>
[JsonConverter(typeof(IdentifierJsonConverter))]
public class Identifier
{
    /// <summary>
    ///     The identifier value.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    public string Value { get; set; } = string.Empty;
}
