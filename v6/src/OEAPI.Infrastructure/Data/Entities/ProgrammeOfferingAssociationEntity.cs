using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for ProgrammeOfferingAssociation.
/// </summary>
[Table("ProgrammeOfferingAssociations")]
[Index(nameof(ProgrammeOfferingAssociationIdValue), IsUnique = true)]
public class ProgrammeOfferingAssociationEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the programme offering association unique identifier value.
    /// </summary>
    [Required]
    [StringLength(36)]
    public string ProgrammeOfferingAssociationIdValue { get; set; } = string.Empty;

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
    ///     Gets or sets the consumer key for this programme offering association.
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
    ///     Gets or sets the programme offering foreign key.
    /// </summary>
    public Guid? ProgrammeOfferingEntityId { get; set; }

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
    ///     Gets or sets the programme offering.
    /// </summary>
    [ForeignKey(nameof(ProgrammeOfferingEntityId))]
    public virtual ProgrammeOfferingEntity? ProgrammeOffering { get; set; }

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
