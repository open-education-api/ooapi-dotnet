using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Response body for <c>POST /persons</c> - the spec's <c>PersonId</c> + <c>PostResponse</c>
///     composed shape, not the full <see cref="Person" /> resource. Mirrors
///     <see cref="AssociationWriteResponse" />'s shape for the equivalent write-response schemas,
///     minus <c>state</c> (not part of <c>PersonId</c>/<c>PostResponse</c>).
/// </summary>
public class PostResponse
{
    /// <summary>
    ///     Unique id of the newly created person.
    /// </summary>
    [JsonPropertyName("personId")]
    public string PersonId { get; set; } = string.Empty;

    /// <summary>
    ///     Information displayed to the user (spec requires at least one entry).
    /// </summary>
    [JsonPropertyName("message")]
    public LanguageTypedString[] Message { get; set; } = [];

    /// <summary>
    ///     URL where additional information can be found, e.g. by use of a deep link.
    /// </summary>
    [JsonPropertyName("redirect")]
    public string? Redirect { get; set; }
}
