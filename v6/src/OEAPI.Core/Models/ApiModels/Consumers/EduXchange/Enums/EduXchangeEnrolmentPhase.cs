using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

/// <summary>
///     The phase of the programme for a person's enrolment.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EduXchangeEnrolmentPhase
{
    [JsonStringEnumMemberName("bachelor")] Bachelor,

    [JsonStringEnumMemberName("master")] Master
}
