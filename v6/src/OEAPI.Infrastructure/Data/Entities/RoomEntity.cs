using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Room.
/// </summary>
[Table("Rooms")]
[Index(nameof(RoomId), IsUnique = true)]
public class RoomEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the room unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string RoomId { get; set; } = string.Empty;

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
    [StringLength(4096)]
    public string? NameJson { get; set; }

    /// <summary>
    ///     Gets or sets the description as JSON.
    /// </summary>
    [StringLength(4096)]
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the room type.
    /// </summary>
    [StringLength(256)]
    public string? RoomType { get; set; }

    /// <summary>
    ///     Gets or sets the floor.
    /// </summary>
    [StringLength(256)]
    public string? Floor { get; set; }

    /// <summary>
    ///     Gets or sets the wing.
    /// </summary>
    [StringLength(256)]
    public string? Wing { get; set; }

    /// <summary>
    ///     Gets or sets the latitude.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    ///     Gets or sets the longitude.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    ///     Gets or sets the total number of seats.
    /// </summary>
    public int? TotalSeats { get; set; }

    /// <summary>
    ///     Gets or sets the available seats.
    /// </summary>
    public int? AvailableSeats { get; set; }

    /// <summary>
    ///     Gets or sets the capacity.
    /// </summary>
    public int? Capacity { get; set; }

    /// <summary>
    ///     Gets or sets the surface in square meters.
    /// </summary>
    public double? SurfaceInSquareMeters { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    [StringLength(4096)]
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for this room.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets consumer information as JSON.
    /// </summary>
    [StringLength(2048)]
    public string? ConsumerJson { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the building foreign key.
    /// </summary>
    public Guid? BuildingEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the building.
    /// </summary>
    [ForeignKey(nameof(BuildingEntityId))]
    public virtual BuildingEntity? Building { get; set; }

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
