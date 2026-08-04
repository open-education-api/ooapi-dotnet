using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange;

/// <summary>
///     A single alliance entry for a Programme or Course, per the eduXchange consumer.
/// </summary>
public class EduXchangeAlliance
{
    [JsonPropertyName("name")] public EduXchangeAllianceName Name { get; set; }

    /// <summary>The theme of the Programme or Course within the alliance.</summary>
    [JsonPropertyName("theme")]
    public string? Theme { get; set; }

    /// <summary>
    ///     Whether this Programme or Course is selective, e.g. whether students need to pass extra requirements before
    ///     being allowed to enrol.
    /// </summary>
    [JsonPropertyName("selection")]
    public bool? Selection { get; set; }

    /// <summary>Whether the Programme or Course is broadening or deepening.</summary>
    [JsonPropertyName("type")]
    public EduXchangeAllianceType? Type { get; set; }

    /// <summary>
    ///     Whether this Programme or Course should be visible for students of the offering
    ///     institution. The default value for this attribute is specified outside of this
    ///     specification, on the alliance level.
    /// </summary>
    [JsonPropertyName("visibleForOwnStudents")]
    public bool? VisibleForOwnStudents { get; set; }

    /// <summary>
    ///     Which enrolment process should be followed for students of the offering institution. Only used if
    ///     visibleForOwnStudents is true.
    /// </summary>
    [JsonPropertyName("enrolmentForOwnStudents")]
    public EduXchangeEnrolmentForOwnStudents? EnrolmentForOwnStudents { get; set; }

    /// <summary>URL students will be redirected to if enrolmentForOwnStudents is "url".</summary>
    [JsonPropertyName("enrolmentUrl")]
    public string? EnrolmentUrl { get; set; }
}
