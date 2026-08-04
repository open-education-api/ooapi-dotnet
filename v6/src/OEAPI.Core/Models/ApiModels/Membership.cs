using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Membership information.
/// </summary>
public class Membership
{
    /// <summary>
    ///     Unique id for this membership - the id of the person, since there's a 1-1 relationship
    ///     between a membership and a person (spec: plain UUID string, not expandable). Not
    ///     <c>[Required]</c> despite being spec-required in the response: the one write endpoint for
    ///     this resource (<c>PUT /groups/{groupId}/memberships/{personId}</c>) takes both ids from the
    ///     URL, never from the body, matching every other resource's own id field in this codebase.
    /// </summary>
    [JsonPropertyName("personId")]
    public string PersonId { get; set; } = string.Empty;

    /// <summary>
    ///     The id of the group this membership belongs to (spec: plain UUID string, not expandable).
    ///     See <see cref="PersonId" /> for why this isn't <c>[Required]</c>.
    /// </summary>
    [JsonPropertyName("groupId")]
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    ///     The start date and time of this membership.
    /// </summary>
    [JsonPropertyName("startDateTime")]
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     The end date and time of this membership.
    /// </summary>
    [JsonPropertyName("endDateTime")]
    [StringLength(256)]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     The state of this membership.
    /// </summary>
    [JsonPropertyName("state")]
    [ExtensibleEnum("membershipState")]
    [StringLength(64)]
    [Required]
    public string State { get; set; } = string.Empty;

    /// <summary>
    ///     The role of the person in this membership.
    /// </summary>
    [JsonPropertyName("role")]
    [ExtensibleEnum("membershipRole")]
    [StringLength(256)]
    [Required]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}
