using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for AcademicSession.
/// </summary>
[Table("AcademicSessions")]
[Index(nameof(AcademicSessionId), IsUnique = true)]
public class AcademicSessionEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the academic session unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string AcademicSessionId { get; set; } = string.Empty;

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
    ///     Gets or sets the name (serialized as JSON for multi-language).
    /// </summary>
    public string NameJson { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the description (serialized as JSON for multi-language).
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the academic session type.
    /// </summary>
    [StringLength(256)]
    public string? AcademicSessionType { get; set; }

    /// <summary>
    ///     Gets or sets the start date time (RFC3339 date-time format).
    /// </summary>
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the end date time (RFC3339 date-time format).
    /// </summary>
    [StringLength(256)]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the abbreviation.
    /// </summary>
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Gets or sets the consumer as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the parent academic session foreign key (e.g. Autumn term 20xx where this session is week 40).
    /// </summary>
    public Guid? ParentEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the top-level academic session "year" foreign key (e.g. 20xx where this session is week 40 of a
    ///     semester).
    /// </summary>
    public Guid? YearEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the parent academic session.
    /// </summary>
    [ForeignKey(nameof(ParentEntityId))]
    public virtual AcademicSessionEntity? Parent { get; set; }

    /// <summary>
    ///     Gets or sets the child academic sessions.
    /// </summary>
    public virtual ICollection<AcademicSessionEntity> Children { get; set; } = [];

    /// <summary>
    ///     Gets or sets the top-level academic session "year" this session belongs to.
    /// </summary>
    [ForeignKey(nameof(YearEntityId))]
    public virtual AcademicSessionEntity? Year { get; set; }

    /// <summary>
    ///     Gets or sets the collection of course offerings for this academic session.
    /// </summary>
    public virtual ICollection<CourseOfferingEntity> CourseOfferings { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
