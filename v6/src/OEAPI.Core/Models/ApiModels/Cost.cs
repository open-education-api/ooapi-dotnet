using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Cost information for an offering.
/// </summary>
public class Cost
{
    /// <summary>
    ///     The type of cost.
    /// </summary>
    [JsonPropertyName("costType")]
    [ExtensibleEnum("costType")]
    public string CostType { get; set; } = string.Empty;

    /// <summary>
    ///     The total amount of the cost as a string. Use a '.' (dot) as an optional separator.
    /// </summary>
    /// <example>340.84</example>
    [JsonPropertyName("amount")]
    [RegexPattern("amount")]
    public string? Amount { get; set; }

    /// <summary>
    ///     The part of the cost that is VAT, as a string.
    /// </summary>
    [JsonPropertyName("vatAmount")]
    [RegexPattern("amount")]
    public string? VatAmount { get; set; }

    /// <summary>
    ///     The part of the cost that is non-VAT, as a string.
    /// </summary>
    [JsonPropertyName("amountWithoutVat")]
    [RegexPattern("amount")]
    public string? AmountWithoutVat { get; set; }

    /// <summary>
    ///     The currency this cost is in. Should correspond to one of the currency codes from ISO 4217.
    /// </summary>
    /// <example>EUR</example>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    ///     An array of optional pre-formatted strings in different locales.
    /// </summary>
    [JsonPropertyName("displayAmount")]
    public LanguageTypedString[]? DisplayAmount { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}
