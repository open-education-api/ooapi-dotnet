using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An object indicating a country based on at least one ISO-3166 code. When more than one code is
///     provided, the codes must refer to the same country.
/// </summary>
public class Country
{
    /// <summary>
    ///     A country code based on ISO 3166-1 alpha-2.
    /// </summary>
    /// <example>NL</example>
    [JsonPropertyName("iso3166-1-alpha2")]
    public string? Iso3166Alpha2 { get; set; }

    /// <summary>
    ///     A country code based on ISO 3166-1 alpha-3.
    /// </summary>
    /// <example>NLD</example>
    [JsonPropertyName("iso3166-1-alpha3")]
    public string? Iso3166Alpha3 { get; set; }

    /// <summary>
    ///     A country subdivision code based on ISO 3166-2.
    /// </summary>
    /// <example>BQ-BO</example>
    [JsonPropertyName("iso3166-2")]
    public string? Iso31662 { get; set; }

    /// <summary>
    ///     A code for a country that no longer exists, based on ISO 3166-3.
    /// </summary>
    /// <example>ANHH</example>
    [JsonPropertyName("iso3166-3")]
    public string? Iso31663 { get; set; }
}
