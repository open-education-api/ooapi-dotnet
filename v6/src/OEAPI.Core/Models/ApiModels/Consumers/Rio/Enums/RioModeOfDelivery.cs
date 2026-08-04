using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     Optional override of the offering's main modeOfDelivery value, mapped one-on-one to
///     opleidingsvorm in RIO.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioModeOfDelivery
{
    [JsonStringEnumMemberName("online")] Online,

    [JsonStringEnumMemberName("hybrid")] Hybrid,

    [JsonStringEnumMemberName("situated")] Situated,

    [JsonStringEnumMemberName("lecture")] Lecture,

    [JsonStringEnumMemberName("self-study")]
    SelfStudy,

    [JsonStringEnumMemberName("coaching")] Coaching
}
