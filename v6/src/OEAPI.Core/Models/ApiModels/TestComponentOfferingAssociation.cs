using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Test component offering association information.
/// </summary>
public class TestComponentOfferingAssociation
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
    ///     Unique id for this test component offering association.
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
    ///     The additional facilities or resources needed by a person to make the component accessible
    ///     and usable.
    /// </summary>
    [JsonPropertyName("requiredPersonalNeeds")]
    [ExtensibleEnum("personalNeed")]
    public string[]? RequiredPersonalNeeds { get; set; }

    /// <summary>
    ///     The extra duration of this component for this specific candidate. ISO 8601 duration format.
    /// </summary>
    [JsonPropertyName("extraDuration")]
    [StringLength(256)]
    public string? ExtraDuration { get; set; }

    /// <summary>
    ///     The first attempt to be consumed within this association (persons are often allowed only a
    ///     limited number of attempts on a specific test, tracked across associations/offerings).
    /// </summary>
    [JsonPropertyName("initialAttemptOnAssociation")]
    public int? InitialAttemptOnAssociation { get; set; }

    /// <summary>
    ///     The maximum number of attempts allowed on this association. `null` means unlimited.
    /// </summary>
    [JsonPropertyName("maximumNumberOfAttemptsOnAssociation")]
    public int? MaximumNumberOfAttemptsOnAssociation { get; set; }

    /// <summary>
    ///     The irregularities that are reported for this association.
    /// </summary>
    [JsonPropertyName("irregularities")]
    public string[]? Irregularities { get; set; }

    /// <summary>
    ///     Documents that are related to this association, e.g. handed in documents, plagiarism
    ///     reports, test made, etc.
    /// </summary>
    [JsonPropertyName("documents")]
    public Document[]? Documents { get; set; }

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
    ///     The identifier of the test component offering associated with this association.
    /// </summary>
    [JsonPropertyName("testComponentOfferingId")]
    public Identifier? TestComponentOfferingId { get; set; }

    /// <summary>
    ///     The expanded test component offering object associated with this association.
    /// </summary>
    [JsonPropertyName("testComponentOffering")]
    public TestComponentOffering? TestComponentOffering { get; set; }

    /// <summary>
    ///     The identifiers of the attempts related to this association.
    ///     When the client does not request expansion of `attempts`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("attemptIds")]
    public Identifier[]? AttemptIds { get; set; }

    /// <summary>
    ///     The expanded attempt objects related to this association.
    ///     When the client requests expansion of `attempts`, the full expanded attempt objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("attempts")]
    public TestComponentOfferingAssociationAttempt[]? Attempts { get; set; }

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
