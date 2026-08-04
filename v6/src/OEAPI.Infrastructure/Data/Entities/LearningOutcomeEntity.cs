using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for LearningOutcome.
/// </summary>
[Table("LearningOutcomes")]
[Index(nameof(LearningOutcomeId), IsUnique = true)]
[Index(nameof(PrimaryCode), IsUnique = false)]
public class LearningOutcomeEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the learning outcome unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string LearningOutcomeId { get; set; } = string.Empty;

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
    ///     Gets or sets the abbreviation.
    /// </summary>
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Gets or sets the description (serialized as JSON for multi-language).
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the fields of study.
    /// </summary>
    [StringLength(6)]
    public string? FieldsOfStudy { get; set; }

    /// <summary>
    ///     Gets or sets the complexity level type.
    /// </summary>
    [StringLength(256)]
    public string? ComplexityLevelType { get; set; }

    /// <summary>
    ///     Gets or sets the complexity level value.
    /// </summary>
    [StringLength(256)]
    public string? ComplexityLevel { get; set; }

    /// <summary>
    ///     Gets or sets the valid from date string.
    /// </summary>
    [StringLength(256)]
    public string? ValidFrom { get; set; }

    /// <summary>
    ///     Gets or sets the valid to date string.
    /// </summary>
    [StringLength(256)]
    public string? ValidTo { get; set; }

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
    ///     Gets or sets the organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the organisation that provides this learning outcome.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets the courses that have this learning outcome.
    /// </summary>
    public virtual ICollection<CourseEntity> Courses { get; set; } = [];

    /// <summary>
    ///     Gets or sets the programmes that have this learning outcome.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> Programmes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the learning components that have this learning outcome.
    /// </summary>
    public virtual ICollection<LearningComponentEntity> LearningComponents { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the test components that have this learning outcome.
    /// </summary>
    public virtual ICollection<TestComponentEntity> TestComponents { get; set; } = [];

    /// <summary>
    ///     Gets or sets the parent learning outcomes (a learning outcome can have more than one parent).
    /// </summary>
    public virtual ICollection<LearningOutcomeEntity> Parents { get; set; } = [];

    /// <summary>
    ///     Gets or sets the child learning outcomes.
    /// </summary>
    public virtual ICollection<LearningOutcomeEntity> Children { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
