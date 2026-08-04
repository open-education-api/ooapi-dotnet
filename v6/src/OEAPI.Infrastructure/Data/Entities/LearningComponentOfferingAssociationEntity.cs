using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for LearningComponentOfferingAssociation.
/// </summary>
[Table("LearningComponentOfferingAssociations")]
[Index(nameof(LearningComponentOfferingAssociationIdValue), IsUnique = true)]
public class LearningComponentOfferingAssociationEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the learning component offering association unique identifier value.
    /// </summary>
    [Required]
    [StringLength(36)]
    public string LearningComponentOfferingAssociationIdValue { get; set; } = string.Empty;

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
    ///     Gets or sets the state.
    /// </summary>
    [StringLength(256)]
    public string? State { get; set; }

    /// <summary>
    ///     Gets or sets the state of this association for the organisation performing a PATCH request
    ///     (spec's <c>remoteState</c>). Also part of the association's readable representation.
    /// </summary>
    [StringLength(256)]
    public string? RemoteState { get; set; }

    /// <summary>
    ///     Gets or sets the role of the person associated with the offering (spec's <c>role</c>).
    /// </summary>
    [StringLength(64)]
    public string? Role { get; set; }

    /// <summary>
    ///     Gets or sets the attendance status of the person's association with the offering (spec's
    ///     <c>attendance</c>).
    /// </summary>
    [StringLength(64)]
    public string? Attendance { get; set; }

    /// <summary>
    ///     Gets or sets the start date and time the person is intended to start participating in the offering.
    /// </summary>
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the expected end date and time the person is intended to stop participating in the offering.
    /// </summary>
    [StringLength(256)]
    public string? ExpectedEndDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the actual end date and time the person stopped participating in the offering.
    /// </summary>
    [StringLength(256)]
    public string? ActualEndDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the result as JSON.
    /// </summary>
    public string? ResultJson { get; set; }

    /// <summary>
    ///     Gets or sets consumer information as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the learning component offering foreign key.
    /// </summary>
    public Guid? LearningComponentOfferingEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the person foreign key.
    /// </summary>
    public Guid? PersonEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the learning component offering.
    /// </summary>
    [ForeignKey(nameof(LearningComponentOfferingEntityId))]
    public virtual LearningComponentOfferingEntity? LearningComponentOffering { get; set; }

    /// <summary>
    ///     Gets or sets the person.
    /// </summary>
    [ForeignKey(nameof(PersonEntityId))]
    public virtual PersonEntity? Person { get; set; }

    /// <summary>
    ///     Gets or sets the organisation.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
