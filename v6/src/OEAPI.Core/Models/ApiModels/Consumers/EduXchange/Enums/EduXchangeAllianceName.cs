using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange.Enums;

/// <summary>
///     The name of the alliance a Programme or Course belongs to.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EduXchangeAllianceName
{
    [JsonStringEnumMemberName("ewuu")] Ewuu,

    [JsonStringEnumMemberName("lde")] Lde
}
