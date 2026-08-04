using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Learning component offering association information.
/// </summary>
public class LearningComponentOfferingAssociation
{
    /// <summary>
    ///     The primary human readable identifier for this association. Optional per spec (not in this
    ///     resource's `required` list) - nullable so an omitted write-request value stays `null` rather
    ///     than a default-constructed <see cref="IdentifierEntry" /> with an empty, validation-failing
    ///     <c>codeType</c>.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry? PrimaryCode { get; set; }

    /// <summary>
    ///     Unique id for this learning component offering association.
    /// </summary>
    [JsonPropertyName("associationId")]
    public string AssociationIdValue { get; set; } = string.Empty;

    /// <summary>
    ///     The role of the person associated with the offering.
    /// </summary>
    [JsonPropertyName("role")]
    [ExtensibleEnum("associationRole")]
    [StringLength(64)]
    public string? Role { get; set; }

    /// <summary>
    ///     The attendance status of the person's association with the offering.
    /// </summary>
    [JsonPropertyName("attendance")]
    [ExtensibleEnum("associationAttendance")]
    [StringLength(64)]
    public string? Attendance { get; set; }

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
    [ExtensibleEnum("associationState")]
    [StringLength(256)]
    public string? State { get; set; }

    /// <summary>
    ///     The state of this association for the organisation performing the request.
    /// </summary>
    [JsonPropertyName("remoteState")]
    [ExtensibleEnum("remoteAssociationState")]
    [StringLength(256)]
    public string? RemoteState { get; set; }

    /// <summary>
    ///     The result of this association.
    /// </summary>
    [JsonPropertyName("result")]
    public Result? Result { get; set; }

    /// <summary>
    ///     The identifier of the learning component offering associated with this association.
    /// </summary>
    [JsonPropertyName("learningComponentOfferingId")]
    public Identifier? LearningComponentOfferingId { get; set; }

    /// <summary>
    ///     The expanded learning component offering object associated with this association.
    /// </summary>
    [JsonPropertyName("learningComponentOffering")]
    public LearningComponentOffering? LearningComponentOffering { get; set; }

    /// <summary>
    ///     The identifier of the person associated with this association.
    /// </summary>
    [JsonPropertyName("personId")]
    public Identifier? PersonId { get; set; }

    /// <summary>
    ///     The expanded person object associated with this association.
    /// </summary>
    [JsonPropertyName("person")]
    public Person? Person { get; set; }

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
