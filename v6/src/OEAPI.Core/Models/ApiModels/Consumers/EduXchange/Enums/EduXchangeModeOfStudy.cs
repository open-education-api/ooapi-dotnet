using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

/// <summary>
///     The mode of study of the programme for a person's enrolment.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EduXchangeModeOfStudy
{
    [JsonStringEnumMemberName("full-time")]
    FullTime,

    [JsonStringEnumMemberName("part-time")]
    PartTime,

    [JsonStringEnumMemberName("dual training")]
    DualTraining,

    [JsonStringEnumMemberName("self-paced")]
    SelfPaced
}
