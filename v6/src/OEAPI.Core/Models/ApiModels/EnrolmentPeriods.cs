using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Describes the moment at which an offering is available for enrolment, supplemented with
///     information regarding the intended target group and how the enrolment is handled.
/// </summary>
public class EnrolmentPeriods
{
    /// <summary>
    ///     The moment from which the enrolment should be available, RFC3339 (date-time).
    /// </summary>
    [JsonPropertyName("startDateTime")]
    public string StartDateTime { get; set; } = string.Empty;

    /// <summary>
    ///     The moment until which the enrolment should be available, RFC3339 (date-time).
    /// </summary>
    [JsonPropertyName("endDateTime")]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     The people for whom this enrolment is available.
    /// </summary>
    [JsonPropertyName("targetGroups")]
    public string[]? TargetGroups { get; set; }

    /// <summary>
    ///     The way the enrolment process should be handled for this period and target group
    ///     (e.g. "url", "broker").
    /// </summary>
    [JsonPropertyName("enrolmentType")]
    public string? EnrolmentType { get; set; }

    /// <summary>
    ///     The URL where a person of this target group can enrol him or herself.
    /// </summary>
    [JsonPropertyName("enrolmentUrl")]
    public string? EnrolmentUrl { get; set; }

    /// <summary>
    ///     Indicates whether enrolment is queued.
    /// </summary>
    [JsonPropertyName("queueEnabled")]
    public bool? QueueEnabled { get; set; }

    /// <summary>
    ///     The number of students that have a queued enrolment state for this offering.
    /// </summary>
    [JsonPropertyName("queuedNumberStudents")]
    public int? QueuedNumberStudents { get; set; }

    /// <summary>
    ///     The maximum number of students allowed in the queue for this offering.
    /// </summary>
    [JsonPropertyName("maxQueuedNumberStudents")]
    public int? MaxQueuedNumberStudents { get; set; }

    /// <summary>
    ///     Additional information regarding this enrolment period that can be shared with the persons
    ///     in the target groups.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

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
