using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An area within a building where education can take place.
/// </summary>
public class Room
{
    /// <summary>
    ///     Unique id for this room.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-332114174000</example>
    [JsonPropertyName("roomId")]
    public string RoomId { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for the room. This is often the source identifier as defined by the
    ///     institution.
    /// </summary>
    /// <example>roomCode: Bb4.54</example>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The type of this room.
    /// </summary>
    [JsonPropertyName("roomType")]
    [ExtensibleEnum("roomType")]
    public string RoomType { get; set; } = string.Empty;

    /// <summary>
    ///     The abbreviation of the name of this room.
    /// </summary>
    /// <example>Bb4.54</example>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The name of this room.
    /// </summary>
    /// <example>Beatrix building room 4.54</example>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The description of this room.
    /// </summary>
    /// <example>External education and exam room 4.54</example>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The total number of seats located in the room.
    /// </summary>
    /// <example>300</example>
    [JsonPropertyName("totalSeats")]
    public int? TotalSeats { get; set; }

    /// <summary>
    ///     The total number of available (=non-reserved) seats in the room.
    /// </summary>
    /// <example>200</example>
    [JsonPropertyName("availableSeats")]
    public int? AvailableSeats { get; set; }

    /// <summary>
    ///     The floor on which this room is located.
    /// </summary>
    /// <example>4</example>
    [JsonPropertyName("floor")]
    public string? Floor { get; set; }

    /// <summary>
    ///     The wing in which this room is located.
    /// </summary>
    /// <example>None</example>
    [JsonPropertyName("wing")]
    public string? Wing { get; set; }

    /// <summary>
    ///     Geolocation of the entrance of this room (WGS84 coordinate reference system).
    /// </summary>
    [JsonPropertyName("geolocation")]
    public Geolocation? Geolocation { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     The identifier of the building in which the room is located.
    ///     When the client does not request expansion of `building`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("buildingId")]
    public Identifier? BuildingId { get; set; }

    /// <summary>
    ///     The expanded building object in which the room is located.
    ///     When the client requests expansion of `building`, the full building object MUST be returned
    ///     here instead of only the identifier. If no building is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("building")]
    public Building? Building { get; set; }

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
