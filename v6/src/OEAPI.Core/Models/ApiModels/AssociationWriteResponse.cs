using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Shared response body for the spec's 4 offering-association PATCH endpoints and the 2
///     <c>external/me</c> POST endpoints (<c>AssociationId</c> + <c>PostResponse</c> + a <c>state</c>
///     property, per the spec). All of these share this exact shape, so one shared response type
///     covers them rather than several near-duplicates.
/// </summary>
public class AssociationWriteResponse
{
    /// <summary>
    ///     Unique id of this association.
    /// </summary>
    [JsonPropertyName("associationId")]
    public string AssociationId { get; set; } = string.Empty;

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

    /// <summary>
    ///     The state of this association after applying the write. Optional in the spec's response
    ///     schema for all 5 PATCH endpoints sharing this type (not in any of their `required` lists) -
    ///     omitted entirely when there's no value, rather than serialized as `null`, since at least one
    ///     of them (<c>TestComponentOfferingAssociationAttempt</c>) has a schema whose own canonical
    ///     `state` field explicitly allows `null` (<c>oneOf: [attemptState, null]</c>), while this
    ///     shared write-response type's own `state` property (<c>associationState</c>) does not.
    /// </summary>
    [JsonPropertyName("state")]
    [ExtensibleEnum("associationState")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; set; }
}
