using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio;

/// <summary>
///     RIO add-on attributes to a course that leads to a certifiable learning outcome.
///     Matches source/consumers/RIO/V1/Course.yaml.
/// </summary>
public class RioCourseConsumer
{
    [JsonPropertyName("consumerKey")] public string ConsumerKey { get; set; } = "rio";

    /// <summary>onderwijsaanbiedercode - identifier for an education offerer in RIO.</summary>
    [JsonPropertyName("educationOffererCode")]
    public string EducationOffererCode { get; set; } = string.Empty;

    /// <summary>onderwijslocatiecode - identifier for an education location in RIO.</summary>
    [JsonPropertyName("educationLocationCode")]
    public string? EducationLocationCode { get; set; }

    [JsonPropertyName("consentParticipationSTAP")]
    public RioConsentParticipationStap? ConsentParticipationStap { get; set; }

    /// <summary>samenwerkendeOnderwijsaanbiedercode - codes of education offerers this course is run jointly with.</summary>
    [JsonPropertyName("jointPartnerCodes")]
    public string[]? JointPartnerCodes { get; set; }

    /// <summary>buitenlandsePartner - description of foreign partner organisations for a Joint Degree.</summary>
    [JsonPropertyName("foreignPartners")]
    public string[]? ForeignPartners { get; set; }
}
