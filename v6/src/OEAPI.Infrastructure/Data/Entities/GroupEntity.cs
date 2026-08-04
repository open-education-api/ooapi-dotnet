using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Group.
/// </summary>
[Table("Groups")]
[Index(nameof(GroupId), IsUnique = true)]
public class GroupEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the group unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string GroupId { get; set; } = string.Empty;

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
    ///     Gets or sets the group type.
    /// </summary>
    [StringLength(256)]
    public string? GroupType { get; set; }

    /// <summary>
    ///     Gets or sets the name (serialized as JSON for multi-language).
    /// </summary>
    public string NameJson { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the description (serialized as JSON for multi-language).
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

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
    ///     Gets or sets the person count.
    /// </summary>
    public int? PersonCount { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the academic session foreign key.
    /// </summary>
    public Guid? AcademicSessionEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the organisation.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets the academic session this group is intended for.
    /// </summary>
    [ForeignKey(nameof(AcademicSessionEntityId))]
    public virtual AcademicSessionEntity? AcademicSession { get; set; }

    /// <summary>
    ///     Gets or sets the collection of memberships for this group.
    /// </summary>
    public virtual ICollection<MembershipEntity> Memberships { get; set; } = [];

    /// <summary>
    ///     Gets or sets the course offerings this group is associated with. The spec's
    ///     <c>offeringIds</c> is a polymorphic 0..N reference (one of 4 offering types per entry) - this
    ///     is one of 4 separate typed many-to-many collections that together represent it, rather than a
    ///     single generic/discriminated relationship, so each keeps a real FK-enforced join table. See
    ///     <c>OEAPIDbContext.ConfigureGroupOfferingRelationships</c>.
    /// </summary>
    public virtual ICollection<CourseOfferingEntity> CourseOfferings { get; set; } = [];

    /// <summary>
    ///     Gets or sets the programme offerings this group is associated with. See
    ///     <see cref="CourseOfferings" /> for why this is a separate typed collection rather than one
    ///     polymorphic relationship.
    /// </summary>
    public virtual ICollection<ProgrammeOfferingEntity> ProgrammeOfferings { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the learning component offerings this group is associated with. See
    ///     <see cref="CourseOfferings" /> for why this is a separate typed collection rather than one
    ///     polymorphic relationship.
    /// </summary>
    public virtual ICollection<LearningComponentOfferingEntity> LearningComponentOfferings { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the test component offerings this group is associated with. See
    ///     <see cref="CourseOfferings" /> for why this is a separate typed collection rather than one
    ///     polymorphic relationship.
    /// </summary>
    public virtual ICollection<TestComponentOfferingEntity> TestComponentOfferings { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets the consumer as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
