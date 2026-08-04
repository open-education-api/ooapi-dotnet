using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for a single historical/future version of a <see cref="CourseEntity" /> (the
///     spec's <c>timelineOverrides</c> mechanism). Mirrors <see cref="CourseEntity" />'s own column
///     shape exactly - see that class for the rationale behind each individual field - plus its own
///     <see cref="ValidFrom" />/<see cref="ValidTo" /> window and its own, independent set of
///     relationships (organisation/coordinators/instructors/programmes/learning-outcomes/other-codes):
///     per the spec's <c>CourseProperties</c> schema, an override entry's relationships are
///     independent of the parent course's current ones and of every other override entry, not shared.
/// </summary>
[Table("CourseTimelineOverrides")]
public class TimelineOverrideCourseEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the owning course's foreign key.
    /// </summary>
    [Required]
    public Guid CourseEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the owning course.
    /// </summary>
    [ForeignKey(nameof(CourseEntityId))]
    public virtual CourseEntity? Course { get; set; }

    /// <summary>
    ///     Gets or sets the day on which this override starts being valid (inclusive).
    /// </summary>
    [Required]
    public DateTime ValidFrom { get; set; }

    /// <summary>
    ///     Gets or sets the day on which this override ceases to be valid (exclusive).
    /// </summary>
    public DateTime? ValidTo { get; set; }

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
    ///     Gets or sets the abbreviation.
    /// </summary>
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Gets or sets the name (serialized as JSON for multi-language).
    /// </summary>
    [Required]
    public string NameJson { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the description (serialized as JSON for multi-language).
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the study load (serialized as JSON).
    /// </summary>
    public string? StudyLoadJson { get; set; }

    /// <summary>
    ///     Gets or sets the modes of delivery (serialized as JSON).
    /// </summary>
    public string? ModesOfDeliveryJson { get; set; }

    /// <summary>
    ///     Gets or sets the duration.
    /// </summary>
    [StringLength(256)]
    public string? Duration { get; set; }

    /// <summary>
    ///     Gets or sets the first start date.
    /// </summary>
    public DateTime? FirstStartDate { get; set; }

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
    ///     Gets or sets the URL of the course's website.
    /// </summary>
    [StringLength(2048)]
    public string? Link { get; set; }

    /// <summary>
    ///     Gets or sets the addresses for this course (serialized as JSON).
    /// </summary>
    public string? AddressesJson { get; set; }

    /// <summary>
    ///     Gets or sets the level of this course (ECTS year of study if applicable).
    /// </summary>
    [StringLength(64)]
    public string? Level { get; set; }

    /// <summary>
    ///     Gets or sets the resources for this course (serialized as JSON).
    /// </summary>
    public string? ResourcesJson { get; set; }

    /// <summary>
    ///     Gets or sets the assessment description (serialized as JSON for multi-language).
    /// </summary>
    public string? AssessmentJson { get; set; }

    /// <summary>
    ///     Gets or sets the enrolment information (serialized as JSON for multi-language).
    /// </summary>
    public string? EnrolmentJson { get; set; }

    /// <summary>
    ///     Gets or sets the admission requirements (serialized as JSON for multi-language).
    /// </summary>
    public string? AdmissionRequirementsJson { get; set; }

    /// <summary>
    ///     Gets or sets the qualification requirements (serialized as JSON for multi-language).
    /// </summary>
    public string? QualificationRequirementsJson { get; set; }

    /// <summary>
    ///     Gets or sets the supplementary information (serialized as JSON).
    /// </summary>
    public string? SupplementaryInformationJson { get; set; }

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
    ///     Gets or sets this override's own organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets this override's own organisation.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets this override's own coordinators.
    /// </summary>
    public virtual ICollection<PersonEntity> Coordinators { get; set; } = [];

    /// <summary>
    ///     Gets or sets this override's own instructors.
    /// </summary>
    public virtual ICollection<PersonEntity> Instructors { get; set; } = [];

    /// <summary>
    ///     Gets or sets this override's own programmes.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> Programmes { get; set; } = [];

    /// <summary>
    ///     Gets or sets this override's own learning outcomes.
    /// </summary>
    public virtual ICollection<LearningOutcomeEntity> LearningOutcomes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
