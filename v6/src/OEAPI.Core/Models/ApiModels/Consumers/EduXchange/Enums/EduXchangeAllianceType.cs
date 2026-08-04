using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

/// <summary>
///     Whether a Programme or Course is broadening or deepening within the alliance.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EduXchangeAllianceType
{
    [JsonStringEnumMemberName("broadening")]
    Broadening,

    [JsonStringEnumMemberName("deepening")]
    Deepening
}
