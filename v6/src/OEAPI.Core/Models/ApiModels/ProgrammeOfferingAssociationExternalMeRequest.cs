using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     The request body for <c>POST /programme-offering-associations/external/me</c>. Enrols the
///     authenticated caller (resolved via <see cref="OEAPI.Core.Interfaces.ICurrentPersonProvider" />,
///     not from this body) into a programme offering. Matches the spec's
///     <c>ProgrammeOfferingAssociationExternalMe</c> schema (<c>AssociationId</c> + <c>AssociationProperties</c>
///     + <c>programmeOfferingId</c>/<c>programmeOffering</c>) plus the endpoint's own <c>issuer</c>
///     property. Deliberately excludes <c>personId</c>/<c>person</c> (the person is always the
///     authenticated caller).
/// </summary>
public class ProgrammeOfferingAssociationExternalMeRequest
{
    /// <summary>
    ///     The primary human readable identifier for this association. Optional per spec (not in this
    ///     resource's `required` list).
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry? PrimaryCode { get; set; }

    /// <summary>
    ///     The role of the person associated with the offering.
    /// </summary>
    [JsonPropertyName("role")]
    [ExtensibleEnum("associationRole")]
    [StringLength(64)]
    public string Role { get; set; } = string.Empty;

    /// <summary>
    ///     The start date and time the person is intended to start participating in the offering.
    /// </summary>
    [JsonPropertyName("startDateTime")]
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     The expected end date and time the person is intended to stop participating in the offering.
    /// </summary>
    [JsonPropertyName("expectedEndDateTime")]
    [StringLength(256)]
    public string? ExpectedEndDateTime { get; set; }

    /// <summary>
    ///     The actual end date and time the person stopped participating in the offering.
    /// </summary>
    [JsonPropertyName("actualEndDateTime")]
    [StringLength(256)]
    public string? ActualEndDateTime { get; set; }

    /// <summary>
    ///     The state of this association.
    /// </summary>
    [JsonPropertyName("state")]
    [StringLength(256)]
    public string State { get; set; } = string.Empty;

    /// <summary>
    ///     The state of this association for the organisation performing this request. Required as a
    ///     key by this endpoint's schema, but the value itself may explicitly be `null`
    ///     (`oneOf: [remoteAssociationState, null]`) - nullable here so ASP.NET Core's automatic
    ///     implicit-required-for-non-nullable-reference-type validation doesn't reject that
    ///     spec-valid `null` as if the key were absent.
    /// </summary>
    [JsonPropertyName("remoteState")]
    [StringLength(256)]
    public string? RemoteState { get; set; }

    /// <summary>
    ///     The result of this association.
    /// </summary>
    [JsonPropertyName("result")]
    public Result? Result { get; set; }

    /// <summary>
    ///     The identifier of the programme offering to enrol into. Either this or <see cref="ProgrammeOffering" /> is
    ///     required.
    /// </summary>
    [JsonPropertyName("programmeOfferingId")]
    public Identifier? ProgrammeOfferingId { get; set; }

    /// <summary>
    ///     The expanded programme offering to enrol into. Either this or <see cref="ProgrammeOfferingId" /> is required.
    /// </summary>
    [JsonPropertyName("programmeOffering")]
    public ProgrammeOffering? ProgrammeOffering { get; set; }

    /// <summary>
    ///     The organisation (type=root) issuing this association. Optional per spec - nullable so an
    ///     omitted write-request value stays `null` rather than a default-constructed
    ///     <see cref="Organisation" /> with an empty, validation-failing <c>organisationType</c>.
    /// </summary>
    [JsonPropertyName("issuer")]
    public Organisation? Issuer { get; set; }

    /// <summary>
    ///     Other identifiers for this association.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

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
