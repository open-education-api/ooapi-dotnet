using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Course offering association information.
/// </summary>
public class CourseOfferingAssociation
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
    ///     Unique id for this course offering association.
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
    ///     Only relevant when the study load for this individual student/enrolment differs from the
    ///     study load of the course offering.
    /// </summary>
    [JsonPropertyName("studyLoad")]
    public StudyLoadDescriptor? StudyLoad { get; set; }

    /// <summary>
    ///     The identifier of the course offering associated with this association.
    /// </summary>
    [JsonPropertyName("courseOfferingId")]
    public Identifier? CourseOfferingId { get; set; }

    /// <summary>
    ///     The expanded course offering object associated with this association.
    /// </summary>
    [JsonPropertyName("courseOffering")]
    public CourseOffering? CourseOffering { get; set; }

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
    ///     The expanded academic session of this association's course offering. Only ever populated by
    ///     <c>GET /persons/{personId}/course-offering-associations?expand=academicSession</c> - the spec
    ///     adds this field to that one nested endpoint's response shape only;
    ///     <see cref="CourseOfferingAssociation" /> itself has no direct academic session relationship of
    ///     its own (it's reached only via <see cref="CourseOffering" />).
    /// </summary>
    [JsonPropertyName("academicSession")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AcademicSession? AcademicSession { get; set; }

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
