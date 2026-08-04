using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     versneldTraject - whether a student follows an accelerated route through the programme.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioAcceleratedRoute
{
    [JsonStringEnumMemberName("accelerated_route")]
    AcceleratedRoute,

    [JsonStringEnumMemberName("no_accelerated_route")]
    NoAcceleratedRoute
}
