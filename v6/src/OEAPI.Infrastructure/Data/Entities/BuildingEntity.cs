using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Building.
/// </summary>
[Table("Buildings")]
[Index(nameof(BuildingId), IsUnique = true)]
public class BuildingEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the building unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string BuildingId { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the primary code type.
    /// </summary>
    [Required]
    [StringLength(256)]
    public string PrimaryCodeType { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the primary code value.
    /// </summary>
    [Required]
    [StringLength(256)]
    public string PrimaryCode { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    [StringLength(256)]
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the abbreviation.
    /// </summary>
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Gets or sets the description.
    /// </summary>
    [StringLength(2048)]
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the name as JSON.
    /// </summary>
    public string? NameJson { get; set; }

    /// <summary>
    ///     Gets or sets the description as JSON.
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the building type.
    /// </summary>
    [StringLength(256)]
    public string? BuildingType { get; set; }

    /// <summary>
    ///     Gets or sets the floor count.
    /// </summary>
    public int? FloorCount { get; set; }

    /// <summary>
    ///     Gets or sets the room count.
    /// </summary>
    public int? RoomCount { get; set; }

    /// <summary>
    ///     Gets or sets the surface in square meters.
    /// </summary>
    public double? SurfaceInSquareMeters { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets the consumer as JSON.
    /// </summary>
    [StringLength(2048)]
    public string? ConsumerJson { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the address foreign key.
    /// </summary>
    public Guid? AddressEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the address.
    /// </summary>
    [ForeignKey(nameof(AddressEntityId))]
    public virtual AddressEntity? Address { get; set; }

    /// <summary>
    ///     Gets or sets the collection of rooms in this building.
    /// </summary>
    public virtual ICollection<RoomEntity> Rooms { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
