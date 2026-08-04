using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.EduXchange;

/// <summary>
///     eduXchange add-on attributes for a Course or Programme. The eduXchange Course.yaml and
///     Programme.yaml schemas are identical, so both entities share this one DTO.
/// </summary>
public class EduXchangeAllianceConsumer
{
    [JsonPropertyName("consumerKey")] public string ConsumerKey { get; set; } = "eduxchange";

    [JsonPropertyName("alliances")]
    public EduXchangeAlliance[] Alliances { get; set; } = [];
}
