using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     The full street address.
/// </summary>
public class Address
{
    /// <summary>
    ///     The type of address, indicating its intended use.
    /// </summary>
    /// <example>postal</example>
    [JsonPropertyName("addressType")]
    [ExtensibleEnum("addressType")]
    public string AddressType { get; set; } = string.Empty;

    /// <summary>
    ///     The street name.
    /// </summary>
    /// <example>Moreelsepark</example>
    [JsonPropertyName("street")]
    public string? Street { get; set; }

    /// <summary>
    ///     The street number.
    /// </summary>
    /// <example>48</example>
    [JsonPropertyName("streetNumber")]
    public string? StreetNumber { get; set; }

    /// <summary>
    ///     Further details like building name, suite, apartment number, etc.
    /// </summary>
    /// <example>On the other side of the road</example>
    [JsonPropertyName("additional")]
    public LanguageTypedString[]? Additional { get; set; }

    /// <summary>
    ///     Code to help sort and deliver mail also known as Postal code and ZIP code.
    /// </summary>
    /// <example>3511 EP</example>
    [JsonPropertyName("postCode")]
    public string? PostCode { get; set; }

    /// <summary>
    ///     Name of the city / locality.
    /// </summary>
    /// <example>Utrecht</example>
    [JsonPropertyName("city")]
    public string? City { get; set; }

    /// <summary>
    ///     Country information, based on at least one ISO-3166 code.
    /// </summary>
    [JsonPropertyName("countryCode")]
    public Country? CountryCode { get; set; }

    /// <summary>
    ///     Geolocation of the entrance of this address (WGS84 coordinate reference system).
    /// </summary>
    [JsonPropertyName("geolocation")]
    public Geolocation? Geolocation { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}

/// <summary>
///     Geolocation coordinates.
/// </summary>
public class Geolocation
{
    /// <summary>
    ///     Latitude coordinate.
    /// </summary>
    /// <example>52.089123</example>
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    /// <summary>
    ///     Longitude coordinate.
    /// </summary>
    /// <example>5.113337</example>
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
