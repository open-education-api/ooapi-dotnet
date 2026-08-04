using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange;

/// <summary>
///     A single enrolment entry for a Person, per the eduXchange consumer.
/// </summary>
public class EduXchangeEnrolment
{
    /// <summary>The crohoCreboCode for this programme - a five character string, e.g. "34401".</summary>
    [JsonPropertyName("crohoCreboCode")]
    public string CrohoCreboCode { get; set; } = string.Empty;

    /// <summary>The name of the programme this enrolment is for.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("phase")] public EduXchangeEnrolmentPhase? Phase { get; set; }

    [JsonPropertyName("modeOfStudy")] public EduXchangeModeOfStudy? ModeOfStudy { get; set; }

    /// <summary>The start moment for this enrolment (RFC3339 full-date).</summary>
    [JsonPropertyName("startDateTime")]
    public string? StartDateTime { get; set; }

    /// <summary>The end moment for this enrolment (RFC3339 full-date).</summary>
    [JsonPropertyName("endDateTime")]
    public string? EndDateTime { get; set; }
}
