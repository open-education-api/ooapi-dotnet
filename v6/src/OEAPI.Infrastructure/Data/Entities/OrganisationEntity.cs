using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Organisation.
/// </summary>
[Table("Organisations")]
[Index(nameof(OrganisationId), IsUnique = true)]
public class OrganisationEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the organisation unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string OrganisationId { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the parent organisation foreign key.
    /// </summary>
    public Guid? ParentEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the root organisation foreign key.
    /// </summary>
    public Guid? RootEntityId { get; set; }

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
    ///     Gets or sets the short name.
    /// </summary>
    [StringLength(256)]
    public string? ShortName { get; set; }

    /// <summary>
    ///     Gets or sets the name (serialized as JSON for multi-language).
    /// </summary>
    public string NameJson { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the description (serialized as JSON for multi-language).
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the logo URL.
    /// </summary>
    [StringLength(2048)]
    public string? Logo { get; set; }

    /// <summary>
    ///     Gets or sets the website URL.
    /// </summary>
    [StringLength(2048)]
    public string? Link { get; set; }

    /// <summary>
    ///     Gets or sets the contact email.
    /// </summary>
    [StringLength(256)]
    public string? ContactEmail { get; set; }

    /// <summary>
    ///     Gets or sets the contact telephone.
    /// </summary>
    [StringLength(256)]
    public string? ContactTelephone { get; set; }

    /// <summary>
    ///     Gets or sets the organisation type.
    /// </summary>
    [StringLength(256)]
    public string? OrganisationType { get; set; }

    /// <summary>
    ///     Gets or sets the addresses for this organisation (serialized as JSON).
    /// </summary>
    public string? AddressesJson { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the valid from date.
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    ///     Gets or sets the valid to date.
    /// </summary>
    public DateTime? ValidTo { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets the consumer as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the parent organisation.
    /// </summary>
    [ForeignKey(nameof(ParentEntityId))]
    public virtual OrganisationEntity? Parent { get; set; }

    /// <summary>
    ///     Gets or sets the root organisation.
    /// </summary>
    [ForeignKey(nameof(RootEntityId))]
    public virtual OrganisationEntity? Root { get; set; }

    /// <summary>
    ///     Gets or sets the child organisations.
    /// </summary>
    public virtual ICollection<OrganisationEntity> Children { get; set; } = [];

    // Reverse navigation properties
    /// <summary>
    ///     Gets or sets the collection of courses for this organisation.
    /// </summary>
    public virtual ICollection<CourseEntity> Courses { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of programmes for this organisation.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> Programmes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of course offerings for this organisation.
    /// </summary>
    public virtual ICollection<CourseOfferingEntity> CourseOfferings { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of programme offerings for this organisation.
    /// </summary>
    public virtual ICollection<ProgrammeOfferingEntity> ProgrammeOfferings { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the collection of groups for this organisation.
    /// </summary>
    public virtual ICollection<GroupEntity> Groups { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of course offering associations for this organisation.
    /// </summary>
    public virtual ICollection<CourseOfferingAssociationEntity> CourseOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the collection of learning component offering associations for this organisation.
    /// </summary>
    public virtual ICollection<LearningComponentOfferingAssociationEntity> LearningComponentOfferingAssociations
    {
        get;
        set;
    } = [];

    /// <summary>
    ///     Gets or sets the collection of programme offering associations for this organisation.
    /// </summary>
    public virtual ICollection<ProgrammeOfferingAssociationEntity> ProgrammeOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the collection of test component offering associations for this organisation.
    /// </summary>
    public virtual ICollection<TestComponentOfferingAssociationEntity> TestComponentOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
