using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Membership.
/// </summary>
[Table("Memberships")]
[Index(nameof(MembershipIdValue), IsUnique = true)]
public class MembershipEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the membership unique identifier value.
    /// </summary>
    [Required]
    [StringLength(36)]
    public string MembershipIdValue { get; set; } = string.Empty;

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
    ///     Gets or sets the role.
    /// </summary>
    [StringLength(256)]
    public string? Role { get; set; }

    /// <summary>
    ///     Gets or sets the membership state.
    /// </summary>
    [StringLength(64)]
    public string? State { get; set; }

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
    ///     Gets or sets the consumer key for this membership.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets consumer information as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the group foreign key.
    /// </summary>
    public Guid? GroupId { get; set; }

    /// <summary>
    ///     Gets or sets the person foreign key.
    /// </summary>
    public Guid? PersonId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the group.
    /// </summary>
    [ForeignKey(nameof(GroupId))]
    public virtual GroupEntity? Group { get; set; }

    /// <summary>
    ///     Gets or sets the person.
    /// </summary>
    [ForeignKey(nameof(PersonId))]
    public virtual PersonEntity? Person { get; set; }

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
