using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     The sector for this programme.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioSector
{
    [JsonStringEnumMemberName("secondary_vocational_education")]
    SecondaryVocationalEducation,

    [JsonStringEnumMemberName("higher_professional_education")]
    HigherProfessionalEducation,

    [JsonStringEnumMemberName("university_education")]
    UniversityEducation
}
