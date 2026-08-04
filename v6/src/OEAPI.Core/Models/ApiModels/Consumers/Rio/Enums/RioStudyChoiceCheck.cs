using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     studiekeuzecheck - specifies whether, and how, a suitability check is performed for a
///     prospective student's chosen programme.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioStudyChoiceCheck
{
    [JsonStringEnumMemberName("no_study_choice_check")]
    NoStudyChoiceCheck,

    [JsonStringEnumMemberName("study_choice_check_available")]
    StudyChoiceCheckAvailable,

    [JsonStringEnumMemberName("study_choice_check_mandatory")]
    StudyChoiceCheckMandatory
}
