using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for a single historical/future version of a <see cref="ProgrammeEntity" /> (the
///     spec's <c>timelineOverrides</c> mechanism). Mirrors <see cref="ProgrammeEntity" />'s own column
///     shape exactly - see that class for the rationale behind each individual field - plus its own
///     <see cref="ValidFrom" />/<see cref="ValidTo" /> window and its own, independent set of
///     relationships. <see cref="Children" /> is its own relationship, not a reuse of
///     <see cref="ProgrammeEntity.ParentEntityId" />'s live hierarchy column: that column belongs to
///     the child row and reflects only its current, live parent, so it can't also represent this
///     override's children during its own historical window.
/// </summary>
[Table("ProgrammeTimelineOverrides")]
public class TimelineOverrideProgrammeEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the owning programme's foreign key.
    /// </summary>
    [Required]
    public Guid ProgrammeEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the owning programme.
    /// </summary>
    [ForeignKey(nameof(ProgrammeEntityId))]
    public virtual ProgrammeEntity? Programme { get; set; }

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
    ///     Gets or sets the qualification awarded (extensible enum, e.g. "bachelor"/"master").
    /// </summary>
    [StringLength(64)]
    public string? QualificationAwarded { get; set; }

    /// <summary>
    ///     Gets or sets the qualification designations (serialized as JSON array), mirroring
    ///     <see cref="ProgrammeEntity.QualificationDesignationsJson" />.
    /// </summary>
    public string? QualificationDesignationsJson { get; set; }

    /// <summary>
    ///     Gets or sets the teaching languages (serialized as JSON).
    /// </summary>
    public string? TeachingLanguagesJson { get; set; }

    /// <summary>
    ///     Gets or sets the programme type (extensible enum, e.g. "programme"/"minor"/"honours").
    /// </summary>
    [Required]
    [StringLength(64)]
    public string ProgrammeType { get; set; } = string.Empty;

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
    ///     Gets or sets the duration.
    /// </summary>
    [StringLength(256)]
    public string? Duration { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for this override.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets consumer information as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets this override's own organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets this override's own parent programme foreign key.
    /// </summary>
    public Guid? ParentEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets this override's own organisation.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets this override's own parent programme.
    /// </summary>
    [ForeignKey(nameof(ParentEntityId))]
    public virtual ProgrammeEntity? Parent { get; set; }

    /// <summary>
    ///     Gets or sets this override's own child programmes.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> Children { get; set; } = [];

    /// <summary>
    ///     Gets or sets this override's own coordinators.
    /// </summary>
    public virtual ICollection<PersonEntity> Coordinators { get; set; } = [];

    /// <summary>
    ///     Gets or sets this override's own instructors.
    /// </summary>
    public virtual ICollection<PersonEntity> Instructors { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
