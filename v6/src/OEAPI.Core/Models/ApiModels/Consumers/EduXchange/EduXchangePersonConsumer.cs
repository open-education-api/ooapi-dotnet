using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange;

/// <summary>
///     eduXchange add-on attributes for a Person. Matches source/consumers/EDUXCHANGE/V1/Person.yaml.
/// </summary>
public class EduXchangePersonConsumer
{
    [JsonPropertyName("consumerKey")] public string ConsumerKey { get; set; } = "eduxchange";

    [JsonPropertyName("enrolments")]
    public EduXchangeEnrolment[] Enrolments { get; set; } = [];

    /// <summary>The BRIN code of the institution, e.g. "12AB".</summary>
    [JsonPropertyName("institutionBRINCode")]
    public string InstitutionBrinCode { get; set; } = string.Empty;
}
