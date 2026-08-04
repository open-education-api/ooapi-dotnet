using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Planning and execution information on an attempt belonging to a
///     <see cref="TestComponentOfferingAssociation" />. Matches the spec's
///     <c>TestComponentOfferingAssociationAttemptFull</c> (the base attempt shape plus
///     <c>courseOfferingAssociationId</c>/<c>testComponentOfferingAssociationId</c>) - used both for the
///     direct <c>/test-component-offering-associations-attempt/{id}</c> resource and the nested
///     <c>/test-component-offering-associations/{id}/test-component-offering-association-attempts</c>
///     list, since the base schema doesn't restrict extra properties. Unlike most entities in this
///     codebase, the spec defines no <c>primaryCode</c> for this resource.
/// </summary>
public class TestComponentOfferingAssociationAttempt
{
    /// <summary>
    ///     Unique id of this attempt.
    /// </summary>
    [JsonPropertyName("attemptId")]
    public string AttemptId { get; set; } = string.Empty;

    /// <summary>
    ///     The opportunity during which this attempt can be fulfilled. Only relevant when only one
    ///     attempt is allowed per association.
    /// </summary>
    [JsonPropertyName("opportunity")]
    [StringLength(256)]
    public string? Opportunity { get; set; }

    /// <summary>
    ///     Which attempt this is for the given person on the given offering.
    /// </summary>
    [JsonPropertyName("attempt")]
    public int? Attempt { get; set; }

    /// <summary>
    ///     The state of this attempt.
    /// </summary>
    [JsonPropertyName("state")]
    [ExtensibleEnum("attemptState")]
    [StringLength(256)]
    public string? State { get; set; }

    /// <summary>
    ///     Moment (date and time) of the start of the actual attempt.
    /// </summary>
    [JsonPropertyName("startDateTime")]
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     Moment (date and time) of the end of the actual attempt.
    /// </summary>
    [JsonPropertyName("endDateTime")]
    [StringLength(256)]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     The identifiers of the rooms for this attempt.
    ///     When the client does not request expansion of `rooms`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("roomIds")]
    public Identifier[]? RoomIds { get; set; }

    /// <summary>
    ///     The expanded room objects for this attempt.
    ///     When the client requests expansion of `rooms`, the full expanded room objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("rooms")]
    public Room[]? Rooms { get; set; }

    /// <summary>
    ///     The attendance status of the person for this attempt.
    /// </summary>
    [JsonPropertyName("attendance")]
    [ExtensibleEnum("attendance")]
    [StringLength(256)]
    public string? Attendance { get; set; }

    /// <summary>
    ///     Additional information about external disturbances or (potentially) illegal actions by the
    ///     student, before, during or after the test.
    /// </summary>
    [JsonPropertyName("irregularities")]
    [StringLength(2048)]
    public string? Irregularities { get; set; }

    /// <summary>
    ///     The identifier of the coordinator responsible for overseeing the test.
    ///     When the client does not request expansion of `coordinator`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("coordinatorId")]
    public Identifier? CoordinatorId { get; set; }

    /// <summary>
    ///     The expanded person object representing the coordinator responsible for overseeing the test.
    ///     When the client requests expansion of `coordinator`, the full person object MUST be returned
    ///     here instead of only the identifier. If no coordinator is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("coordinator")]
    public Person? Coordinator { get; set; }

    /// <summary>
    ///     Documents that are related to this attempt (e.g. test completed, work handed in).
    /// </summary>
    [JsonPropertyName("documents")]
    public Document[]? Documents { get; set; }

    /// <summary>
    ///     The result of this attempt.
    /// </summary>
    [JsonPropertyName("result")]
    public Result? Result { get; set; }

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     The unique identifier of the student's enrolment in a course offering to which the current
    ///     association relates.
    /// </summary>
    [JsonPropertyName("courseOfferingAssociationId")]
    public string? CourseOfferingAssociationId { get; set; }

    /// <summary>
    ///     The associationId under which this attempt was made.
    /// </summary>
    [JsonPropertyName("testComponentOfferingAssociationId")]
    public string? TestComponentOfferingAssociationId { get; set; }
}
