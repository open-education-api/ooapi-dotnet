using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for TestComponentOfferingAssociation.
/// </summary>
[Table("TestComponentOfferingAssociations")]
[Index(nameof(TestComponentOfferingAssociationIdValue), IsUnique = true)]
public class TestComponentOfferingAssociationEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the test component offering association unique identifier value.
    /// </summary>
    [Required]
    [StringLength(36)]
    public string TestComponentOfferingAssociationIdValue { get; set; } = string.Empty;

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
    ///     Gets or sets the additional facilities or resources needed by the person, as JSON (spec's
    ///     <c>requiredPersonalNeeds</c>).
    /// </summary>
    public string? RequiredPersonalNeedsJson { get; set; }

    /// <summary>
    ///     Gets or sets the extra duration granted to this person on this association as a personal need.
    /// </summary>
    [StringLength(256)]
    public string? ExtraDuration { get; set; }

    /// <summary>
    ///     Gets or sets the first attempt to be consumed within this association.
    /// </summary>
    public int? InitialAttemptOnAssociation { get; set; }

    /// <summary>
    ///     Gets or sets the maximum number of attempts allowed for this person on this association.
    /// </summary>
    public int? MaximumNumberOfAttemptsOnAssociation { get; set; }

    /// <summary>
    ///     Gets or sets additional information about external disturbances or irregularities, as JSON.
    /// </summary>
    public string? IrregularitiesJson { get; set; }

    /// <summary>
    ///     Gets or sets the documents related to this association (embedded, full <c>Document</c>
    ///     objects per spec - not identifier references) as JSON.
    /// </summary>
    public string? DocumentsJson { get; set; }

    /// <summary>
    ///     Gets or sets the attempts made under this association, as JSON (denormalized, mirroring
    ///     <c>TestComponentOfferingAssociationAttemptEntity.DocumentsJson</c>'s existing precedent -
    ///     <c>attemptIds</c> is projected separately from the real
    ///     <see cref="TestComponentOfferingAssociationAttemptEntity" /> relationship, not from this
    ///     column).
    /// </summary>
    public string? AttemptsJson { get; set; }

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
    ///     Gets or sets the consumer key for this test component offering association.
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

    /// <summary>
    ///     Gets or sets the URL of the test tool to start this specific attempt (spec's dedicated
    ///     <c>GET /test-component-offering-associations/{id}/url</c> sub-resource - not a property of
    ///     this association's own response body). Required and non-nullable per the spec's <c>Url</c>
    ///     schema (a plain <c>format: uri</c> string, no null option), so this column carries no
    ///     backfill risk once seeded - existing rows just need a real value, same as any other
    ///     required string column.
    /// </summary>
    [Required]
    [StringLength(2048)]
    public string Url { get; set; } = string.Empty;

    // Foreign keys
    /// <summary>
    ///     Gets or sets the test component offering foreign key.
    /// </summary>
    public Guid? TestComponentOfferingEntityId { get; set; }

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
    ///     Gets or sets the test component offering.
    /// </summary>
    [ForeignKey(nameof(TestComponentOfferingEntityId))]
    public virtual TestComponentOfferingEntity? TestComponentOffering { get; set; }

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
    ///     Gets or sets the attempts made under this association (real relationship, used to project
    ///     <c>attemptIds</c> - the full expanded <c>attempts</c> array is served from the denormalized
    ///     <see cref="AttemptsJson" /> column instead, per this codebase's existing Document-embedding
    ///     precedent).
    /// </summary>
    public virtual ICollection<TestComponentOfferingAssociationAttemptEntity> Attempts { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
