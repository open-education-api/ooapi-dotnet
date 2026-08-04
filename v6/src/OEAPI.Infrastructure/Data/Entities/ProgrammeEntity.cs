using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Programme.
/// </summary>
[Table("Programmes")]
[Index(nameof(ProgrammeId), IsUnique = true)]
public class ProgrammeEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the programme unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string ProgrammeId { get; set; } = string.Empty;

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
    ///     Gets or sets the qualification awarded (extensible enum, e.g. "bachelor"/"master").
    /// </summary>
    [StringLength(64)]
    public string? QualificationAwarded { get; set; }

    /// <summary>
    ///     Gets or sets the qualification designations (serialized as JSON array) - multiple
    ///     designations may apply to interdisciplinary programmes, per the spec.
    /// </summary>
    public string? QualificationDesignationsJson { get; set; }

    /// <summary>
    ///     Gets or sets the teaching languages (serialized as JSON).
    /// </summary>
    public string? TeachingLanguagesJson { get; set; }

    /// <summary>
    ///     Gets or sets the programme type (extensible enum, e.g. "programme"/"minor"/"honours").
    /// </summary>
    [StringLength(64)]
    public string? ProgrammeType { get; set; }

    /// <summary>
    ///     Gets or sets the mode of study (extensible enum, e.g. "full_time"/"part_time").
    /// </summary>
    [StringLength(64)]
    public string? ModeOfStudy { get; set; }

    /// <summary>
    ///     Gets or sets the modes of delivery (serialized as JSON).
    /// </summary>
    public string? ModesOfDeliveryJson { get; set; }

    /// <summary>
    ///     Gets or sets the level of qualification (extensible enum, e.g. "eqf_6"/"eqf_7").
    /// </summary>
    [StringLength(64)]
    public string? LevelOfQualification { get; set; }

    /// <summary>
    ///     Gets or sets the type of formal document obtained upon completion (extensible enum, e.g.
    ///     "diploma"/"certificate").
    /// </summary>
    [StringLength(64)]
    public string? FormalDocument { get; set; }

    /// <summary>
    ///     Gets or sets the fields of study.
    /// </summary>
    [StringLength(6)]
    [MinLength(2)]
    public string? FieldsOfStudy { get; set; }

    /// <summary>
    ///     Gets or sets the duration.
    /// </summary>
    [StringLength(256)]
    public string? Duration { get; set; }

    /// <summary>
    ///     Gets or sets the first start date.
    /// </summary>
    public DateTime? FirstStartDateTime { get; set; }

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
    ///     Gets or sets the valid from date.
    /// </summary>
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    ///     Gets or sets the valid to date.
    /// </summary>
    public DateTime? ValidTo { get; set; }

    /// <summary>
    ///     Gets or sets the URL of the programme's website.
    /// </summary>
    [StringLength(2048)]
    public string? Link { get; set; }

    /// <summary>
    ///     Gets or sets the addresses for this programme (serialized as JSON).
    /// </summary>
    public string? AddressesJson { get; set; }

    /// <summary>
    ///     Gets or sets the level of this programme (ECTS year of study if applicable).
    /// </summary>
    [StringLength(64)]
    public string? Level { get; set; }

    /// <summary>
    ///     Gets or sets the resources for this programme (serialized as JSON).
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
    ///     Gets or sets the consumer key for this programme.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets consumer information as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the parent programme foreign key.
    /// </summary>
    public Guid? ParentEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the organisation.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets the parent programme.
    /// </summary>
    [ForeignKey(nameof(ParentEntityId))]
    public virtual ProgrammeEntity? Parent { get; set; }

    /// <summary>
    ///     Gets or sets the child programmes.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> Children { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of programme offerings for this programme.
    /// </summary>
    public virtual ICollection<ProgrammeOfferingEntity> ProgrammeOfferings { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the persons who coordinate this programme.
    /// </summary>
    public virtual ICollection<PersonEntity> Coordinators { get; set; } = [];

    /// <summary>
    ///     Gets or sets the persons who instruct this programme.
    /// </summary>
    public virtual ICollection<PersonEntity> Instructors { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of courses in this programme.
    /// </summary>
    public virtual ICollection<CourseEntity> Courses { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the learning outcomes related to this programme.
    /// </summary>
    public virtual ICollection<LearningOutcomeEntity> LearningOutcomes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the historical/future versions of this programme (the spec's
    ///     <c>timelineOverrides</c> mechanism).
    /// </summary>
    public virtual ICollection<TimelineOverrideProgrammeEntity> TimelineOverrides { get; set; } = [];
}
