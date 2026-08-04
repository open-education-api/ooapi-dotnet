using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     Determines what kind of RIO entity a Programme is mapped to.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioProgrammeType
{
    [JsonStringEnumMemberName("programme")]
    Programme,

    [JsonStringEnumMemberName("variant")] Variant,

    [JsonStringEnumMemberName("cluster")] Cluster,

    [JsonStringEnumMemberName("course")] Course,

    [JsonStringEnumMemberName("private")] Private
}
