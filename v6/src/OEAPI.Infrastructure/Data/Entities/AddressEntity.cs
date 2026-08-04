using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Address.
/// </summary>
[Table("Addresses")]
[Index(nameof(AddressId), IsUnique = true)]
public class AddressEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the address unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string AddressId { get; set; } = string.Empty;

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
    ///     Gets or sets the address type.
    /// </summary>
    [StringLength(256)]
    public string? AddressType { get; set; }

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    [StringLength(256)]
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the street.
    /// </summary>
    [StringLength(256)]
    public string? Street { get; set; }

    /// <summary>
    ///     Gets or sets the house number.
    /// </summary>
    [StringLength(256)]
    public string? StreetNumber { get; set; }

    /// <summary>
    ///     Gets or sets the house number suffix.
    /// </summary>
    [StringLength(256)]
    public string? HouseNumberSuffix { get; set; }

    /// <summary>
    ///     Gets or sets the house number addition.
    /// </summary>
    [StringLength(256)]
    public string? HouseNumberAddition { get; set; }

    /// <summary>
    ///     Gets or sets the postal code.
    /// </summary>
    [StringLength(256)]
    public string? PostCode { get; set; }

    /// <summary>
    ///     Gets or sets the city.
    /// </summary>
    [StringLength(256)]
    public string? City { get; set; }

    /// <summary>
    ///     Gets or sets the country code.
    /// </summary>
    [StringLength(2)]
    public string? CountryCode { get; set; }

    /// <summary>
    ///     Gets or sets the country.
    /// </summary>
    [StringLength(256)]
    public string? Country { get; set; }

    /// <summary>
    ///     Gets or sets the geolocation as JSON.
    /// </summary>
    public string? GeoJson { get; set; }

    /// <summary>
    ///     Gets or sets the latitude.
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    ///     Gets or sets the longitude.
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
