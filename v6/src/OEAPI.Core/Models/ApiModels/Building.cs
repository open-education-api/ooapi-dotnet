using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An object describing a building and the properties of a building.
/// </summary>
public class Building
{
    /// <summary>
    ///     Unique id of this building.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-331214174000</example>
    [JsonPropertyName("buildingId")]
    public string BuildingId { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for this building. This is often the source identifier as defined by the
    ///     institution.
    /// </summary>
    /// <example>buildingId: 45</example>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The abbreviation of the name of this building.
    /// </summary>
    /// <example>Bb</example>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The name of this building.
    /// </summary>
    /// <example>Beatrix building</example>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The description of this building.
    /// </summary>
    /// <example>external rooms location for exams</example>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The address of this building.
    /// </summary>
    [JsonPropertyName("address")]
    public Address? Address { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <example>bagId: 0344100000139910</example>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}
