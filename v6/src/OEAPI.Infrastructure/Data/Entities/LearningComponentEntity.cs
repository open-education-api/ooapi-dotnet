using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for LearningComponent.
/// </summary>
[Table("LearningComponents")]
[Index(nameof(ComponentId), IsUnique = true)]
public class LearningComponentEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the component unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string ComponentId { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the component type.
    /// </summary>
    [StringLength(256)]
    public string? ComponentType { get; set; }

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
    ///     Gets or sets the abbreviation.
    /// </summary>
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Gets or sets the enrolment information (serialized as JSON for multi-language).
    /// </summary>
    public string? EnrolmentJson { get; set; }

    /// <summary>
    ///     Gets or sets the resources (serialized as JSON).
    /// </summary>
    public string? ResourcesJson { get; set; }

    /// <summary>
    ///     Gets or sets the assessment description (serialized as JSON for multi-language).
    /// </summary>
    public string? AssessmentJson { get; set; }

    /// <summary>
    ///     Gets or sets the addresses (serialized as JSON).
    /// </summary>
    public string? AddressesJson { get; set; }

    /// <summary>
    ///     Gets or sets modes of delivery (serialized as JSON).
    /// </summary>
    public string? ModesOfDeliveryJson { get; set; }

    /// <summary>
    ///     Gets or sets the duration.
    /// </summary>
    [StringLength(256)]
    public string? Duration { get; set; }

    /// <summary>
    ///     Gets or sets the first possible offering start date.
    /// </summary>
    public DateTime? FirstPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the last possible offering start date.
    /// </summary>
    public DateTime? LastPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the last possible offering end date.
    /// </summary>
    public DateTime? LastPossibleOfferingEndDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the teaching languages (serialized as JSON).
    /// </summary>
    public string? TeachingLanguagesJson { get; set; }

    /// <summary>
    ///     Gets or sets the fields of study.
    /// </summary>
    [StringLength(6)]
    [MinLength(2)]
    public string? FieldsOfStudy { get; set; }

    /// <summary>
    ///     Gets or sets the valid from date.
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    ///     Gets or sets the valid to date.
    /// </summary>
    public DateTime? ValidTo { get; set; }

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
    public string? ConsumerJson { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the course foreign key.
    /// </summary>
    public Guid? CourseEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the parent learning component foreign key.
    /// </summary>
    public Guid? ParentEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the course this learning component belongs to.
    /// </summary>
    [ForeignKey(nameof(CourseEntityId))]
    public virtual CourseEntity? Course { get; set; }

    /// <summary>
    ///     Gets or sets the organisation that provides this learning component.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets the parent learning component.
    /// </summary>
    [ForeignKey(nameof(ParentEntityId))]
    public virtual LearningComponentEntity? Parent { get; set; }

    /// <summary>
    ///     Gets or sets the child learning components.
    /// </summary>
    public virtual ICollection<LearningComponentEntity> Children { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of learning component offerings.
    /// </summary>
    public virtual ICollection<LearningComponentOfferingEntity> Offerings { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the learning outcomes related to this learning component.
    /// </summary>
    public virtual ICollection<LearningOutcomeEntity> LearningOutcomes { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
