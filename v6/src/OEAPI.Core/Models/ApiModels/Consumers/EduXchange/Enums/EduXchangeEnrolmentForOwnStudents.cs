using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

/// <summary>
///     Which enrolment process should be followed for students of the offering institution. Only
///     used when visibleForOwnStudents is true.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EduXchangeEnrolmentForOwnStudents
{
    [JsonStringEnumMemberName("broker")] Broker,

    [JsonStringEnumMemberName("url")] Url
}
