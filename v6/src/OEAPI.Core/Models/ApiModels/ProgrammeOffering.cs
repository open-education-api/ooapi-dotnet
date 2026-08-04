using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A programme offering which describes the programme in time.
/// </summary>
public class ProgrammeOffering
{
    /// <summary>
    ///     Unique id for this programme offering.
    /// </summary>
    [JsonPropertyName("programmeOfferingId")]
    public string ProgrammeOfferingIdValue { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for this programme offering.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The name of this programme offering.
    /// </summary>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The state of this offering, e.g. active, inactive, archived.
    /// </summary>
    [JsonPropertyName("state")]
    [ExtensibleEnum("offeringState")]
    [StringLength(256)]
    public string? State { get; set; }

    /// <summary>
    ///     The rostering state of this offering, indicating the state in relation to planning.
    /// </summary>
    [JsonPropertyName("rosteringState")]
    [ExtensibleEnum("rosteringState")]
    [StringLength(256)]
    public string? RosteringState { get; set; }

    /// <summary>
    ///     The abbreviation or internal code used to identify this offering.
    /// </summary>
    [JsonPropertyName("abbreviation")]
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The description of this offering.
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The languages in which this offering is given.
    /// </summary>
    [JsonPropertyName("teachingLanguages")]
    [RegexPattern("language")]
    public string[]? TeachingLanguages { get; set; }

    /// <summary>
    ///     The modes of delivery of this offering.
    /// </summary>
    [JsonPropertyName("modesOfDelivery")]
    [ExtensibleEnum("modeOfDelivery")]
    public string[]? ModesOfDelivery { get; set; }

    /// <summary>
    ///     The maximum number of students allowed to enrol for this offering.
    /// </summary>
    [JsonPropertyName("maxNumberStudents")]
    public int? MaxNumberStudents { get; set; }

    /// <summary>
    ///     The number of students who have already enrolled for this offering.
    /// </summary>
    [JsonPropertyName("enrolledNumberStudents")]
    public int? EnrolledNumberStudents { get; set; }

    /// <summary>
    ///     The number of students who have a pending enrolment request for this offering.
    /// </summary>
    [JsonPropertyName("pendingNumberStudents")]
    public int? PendingNumberStudents { get; set; }

    /// <summary>
    ///     The minimum number of students needed for this offering to proceed.
    /// </summary>
    [JsonPropertyName("minNumberStudents")]
    public int? MinNumberStudents { get; set; }

    /// <summary>
    ///     Indicates whether a result is expected for this offering.
    /// </summary>
    [JsonPropertyName("resultExpected")]
    public bool? ResultExpected { get; set; }

    /// <summary>
    ///     The result value type for this offering.
    /// </summary>
    [JsonPropertyName("resultValueType")]
    [ExtensibleEnum("resultValueType")]
    [StringLength(256)]
    public string? ResultValueType { get; set; }

    /// <summary>
    ///     URL of this offering's webpage.
    /// </summary>
    [JsonPropertyName("link")]
    [StringLength(2048)]
    public string? Link { get; set; }

    /// <summary>
    ///     An array of periods that a person can enrol into this offering.
    /// </summary>
    [JsonPropertyName("enrolmentPeriods")]
    public EnrolmentPeriods[]? EnrolmentPeriods { get; set; }

    /// <summary>
    ///     Optional supplementary information associated with this offering.
    /// </summary>
    [JsonPropertyName("supplementaryInformation")]
    public SupplementaryInformation[]? SupplementaryInformation { get; set; }

    /// <summary>
    ///     The moment on which this offering starts, RFC3339 (date-time).
    /// </summary>
    [JsonPropertyName("startDateTime")]
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     The moment on which this offering ends, RFC3339 (date-time).
    /// </summary>
    [JsonPropertyName("endDateTime")]
    [StringLength(256)]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     Flexible entry period start date time.
    /// </summary>
    [JsonPropertyName("flexibleEntryPeriodStartDateTime")]
    [StringLength(256)]
    public string? FlexibleEntryPeriodStartDateTime { get; set; }

    /// <summary>
    ///     Flexible entry period end date time.
    /// </summary>
    [JsonPropertyName("flexibleEntryPeriodEndDateTime")]
    [StringLength(256)]
    public string? FlexibleEntryPeriodEndDateTime { get; set; }

    /// <summary>
    ///     Addresses for this offering.
    /// </summary>
    [JsonPropertyName("addresses")]
    public Address[]? Addresses { get; set; }

    /// <summary>
    ///     Price information for this offering.
    /// </summary>
    [JsonPropertyName("priceInformation")]
    public Cost[]? PriceInformation { get; set; }

    /// <summary>
    ///     The identifier of the programme that is offered in this programmeOffering.
    /// </summary>
    [JsonPropertyName("programmeId")]
    public Identifier? ProgrammeId { get; set; }

    /// <summary>
    ///     The expanded programme object that is offered in this programmeOffering.
    /// </summary>
    [JsonPropertyName("programme")]
    public Programme? Programme { get; set; }

    /// <summary>
    ///     The identifier of the organisation that manages this programmeOffering.
    /// </summary>
    [JsonPropertyName("organisationId")]
    public Identifier? OrganisationId { get; set; }

    /// <summary>
    ///     The expanded organisation object that manages this programmeOffering.
    /// </summary>
    [JsonPropertyName("organisation")]
    public Organisation? Organisation { get; set; }

    /// <summary>
    ///     The identifier of the academic session for which this programme offering is intended.
    ///     When the client does not request expansion of `academicSession`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("academicSessionId")]
    public Identifier? AcademicSessionId { get; set; }

    /// <summary>
    ///     The expanded academic session object for which this programme offering is intended.
    ///     When the client requests expansion of `academicSession`, the full expanded academic session object
    ///     MUST be returned here instead of only the identifier. If no academic session is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("academicSession")]
    public AcademicSession? AcademicSession { get; set; }

    /// <summary>
    ///     The identifiers (0..N) of the groups associated with this offering.
    /// </summary>
    [JsonPropertyName("groupIds")]
    public Identifier[]? GroupIds { get; set; }

    /// <summary>
    ///     Other identifiers for this offering.
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
