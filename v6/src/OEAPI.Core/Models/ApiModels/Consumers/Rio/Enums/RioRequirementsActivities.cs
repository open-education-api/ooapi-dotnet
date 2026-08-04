using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     eisenWerkzaamheden - whether requirements are set for the type of work performed as part of
///     the programme.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioRequirementsActivities
{
    [JsonStringEnumMemberName("requirements")]
    Requirements,

    [JsonStringEnumMemberName("no_requirements")]
    NoRequirements
}
