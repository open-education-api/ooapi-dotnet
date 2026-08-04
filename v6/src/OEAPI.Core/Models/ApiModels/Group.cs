using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A group is simply a collection of persons. Groups can be used to accommodate various use cases.
///     Groups MAY optionally have a relation to an offering, however the meaning of such relations is left unspecified.
/// </summary>
public class Group
{
    /// <summary>
    ///     The unique ID of the group.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-134564174000</example>
    [JsonPropertyName("groupId")]
    public string GroupIdValue { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for this group. This is often the source identifier as defined by the
    ///     institution.
    /// </summary>
    /// <example>groupCode: group-abc987</example>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The type of this group.
    /// </summary>
    [JsonPropertyName("groupType")]
    [ExtensibleEnum("groupType")]
    [StringLength(256)]
    public string GroupType { get; set; } = string.Empty;

    /// <summary>
    ///     The name of this group.
    /// </summary>
    /// <example>statistics students</example>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The description of this group.
    /// </summary>
    /// <example>The group of students that follow statistics classes</example>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The moment on which this group starts being active, RFC3339 (date-time).
    /// </summary>
    /// <example>2025-05-30T20:00:00+01:00</example>
    [JsonPropertyName("startDateTime")]
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     The moment on which this group ends being active, RFC3339 (date-time).
    /// </summary>
    /// <example>2025-06-30T20:00:00+01:00</example>
    [JsonPropertyName("endDateTime")]
    [StringLength(256)]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     The number of persons that are member of this group.
    /// </summary>
    /// <example>183</example>
    [JsonPropertyName("personCount")]
    public int? PersonCount { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     The identifier of the organisation that manages this group.
    ///     When the client does not request expansion of `organisation`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("organisationId")]
    public Identifier? OrganisationId { get; set; }

    /// <summary>
    ///     The expanded organisation object that manages this group.
    ///     When the client requests expansion of `organisation`, the full expanded organisation object
    ///     MUST be returned here instead of only the identifier. If no organisation is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("organisation")]
    public Organisation? Organisation { get; set; }

    /// <summary>
    ///     The identifier of the academicSession for which this group is intended.
    ///     When the client does not request expansion of `academicSession`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("academicSessionId")]
    public Identifier? AcademicSessionId { get; set; }

    /// <summary>
    ///     The expanded academicSession object for which this group is intended.
    ///     When the client requests expansion of `academicSession`, the full expanded academicSession object
    ///     MUST be returned here instead of only the identifier. If no academicSession is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("academicSession")]
    public AcademicSession? AcademicSession { get; set; }

    /// <summary>
    ///     The offering identifiers (0..N) associated with this group. Each entry references exactly
    ///     one offering, of exactly one of the four offering types - see
    ///     <see cref="GroupOfferingReference" />. The spec leaves the meaning of such relations
    ///     unspecified and up to the implementer.
    /// </summary>
    [JsonPropertyName("offeringIds")]
    public GroupOfferingReference[]? OfferingIds { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}
