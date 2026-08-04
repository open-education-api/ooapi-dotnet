using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     propadeutischeFase - whether the offered programme has a propaedeutic phase and whether it
///     concludes with a propaedeutic exam.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioPropaedeuticPhase
{
    [JsonStringEnumMemberName("no_propaedeutic_phase")]
    NoPropaedeuticPhase,

    [JsonStringEnumMemberName("propaedeutic_phase_exam")]
    PropaedeuticPhaseExam,

    [JsonStringEnumMemberName("propaedeutic_phase_no_exam")]
    PropaedeuticPhaseNoExam
}
