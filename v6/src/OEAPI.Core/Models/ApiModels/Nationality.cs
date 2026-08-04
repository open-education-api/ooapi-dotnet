using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An object indicating nationality based on at least one ISO-3166 code. When more than one code is
///     provided, the codes must refer to the same country.
/// </summary>
public class Nationality
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
    ///     A nationality code for a country that no longer exists, based on ISO 3166-3. It is possible
    ///     for a person to hold the nationality of a country that no longer exists (e.g. after a country
    ///     split) without having applied for the nationality of one of the successor countries.
    /// </summary>
    /// <example>ANHH</example>
    [JsonPropertyName("iso3166-3")]
    public string? Iso31663 { get; set; }
}
