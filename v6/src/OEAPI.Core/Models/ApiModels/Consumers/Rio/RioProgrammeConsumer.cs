using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio;

/// <summary>
///     RIO add-on attributes to a collection of courses that lead to a certifiable learning outcome.
///     Matches source/consumers/RIO/V1/Programme.yaml.
/// </summary>
public class RioProgrammeConsumer
{
    [JsonPropertyName("consumerKey")] public string ConsumerKey { get; set; } = "rio";

    /// <summary>Determines what kind of RIO entity this Programme is mapped to.</summary>
    [JsonPropertyName("type")]
    public RioProgrammeType? Type { get; set; }

    /// <summary>onderwijsaanbiedercode - identifier for an education offerer in RIO.</summary>
    [JsonPropertyName("educationOffererCode")]
    public string EducationOffererCode { get; set; } = string.Empty;

    /// <summary>onderwijslocatiecode - identifier for an education location in RIO.</summary>
    [JsonPropertyName("educationLocationCode")]
    public string? EducationLocationCode { get; set; }

    [JsonPropertyName("consentParticipationSTAP")]
    public RioConsentParticipationStap? ConsentParticipationStap { get; set; }

    /// <summary>samenwerkendeOnderwijsaanbiedercode - codes of education offerers this programme is run jointly with.</summary>
    [JsonPropertyName("jointPartnerCodes")]
    public string[]? JointPartnerCodes { get; set; }

    /// <summary>buitenlandsePartner - description of foreign partner organisations for a Joint Degree.</summary>
    [JsonPropertyName("foreignPartners")]
    public string[]? ForeignPartners { get; set; }

    /// <summary>deficientie - whether enrolment is possible with insufficient prior education.</summary>
    [JsonPropertyName("deficiency")]
    public RioDeficiency? Deficiency { get; set; }

    /// <summary>eisenWerkzaamheden - whether requirements are set for the type of work performed as part of the programme.</summary>
    [JsonPropertyName("requirementsActivities")]
    public RioRequirementsActivities? RequirementsActivities { get; set; }

    /// <summary>propadeutischeFase - whether the programme has a propaedeutic phase and whether it concludes with an exam.</summary>
    [JsonPropertyName("propaedeuticPhase")]
    public RioPropaedeuticPhase? PropaedeuticPhase { get; set; }

    /// <summary>studiekeuzecheck - whether and how a suitability check is performed for prospective students.</summary>
    [JsonPropertyName("studyChoiceCheck")]
    public RioStudyChoiceCheck? StudyChoiceCheck { get; set; }

    /// <summary>versneldTraject - whether a student follows an accelerated route through the programme.</summary>
    [JsonPropertyName("acceleratedRoute")]
    public RioAcceleratedRoute? AcceleratedRoute { get; set; }

    /// <summary>The sector for this programme.</summary>
    [JsonPropertyName("sector")]
    public RioSector? Sector { get; set; }

    /// <summary>A classification for programmes in non-formal education, used for ParticuliereOpleiding in RIO.</summary>
    [JsonPropertyName("privateCategory")]
    public RioPrivateCategory[]? PrivateCategory { get; set; }
}
