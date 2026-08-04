using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     deficientie - whether enrolment is possible with insufficient prior education.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioDeficiency
{
    [JsonStringEnumMemberName("deficiencies")]
    Deficiencies,

    [JsonStringEnumMemberName("no_deficiencies")]
    NoDeficiencies
}
