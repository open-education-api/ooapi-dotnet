using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for TestComponentOfferingAssociationAttempt. Unlike most entities in this
///     codebase, the spec defines no <c>primaryCode</c> for this resource, so there's no
///     <c>PrimaryCodeType</c>/<c>PrimaryCode</c> pair here.
/// </summary>
[Table("TestComponentOfferingAssociationAttempts")]
[Index(nameof(AttemptIdValue), IsUnique = true)]
public class TestComponentOfferingAssociationAttemptEntity : BaseEntity
{
    /// <summary>
    ///     Gets or sets the attempt unique identifier value.
    /// </summary>
    [Required]
    [StringLength(36)]
    public string AttemptIdValue { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the opportunity during which this attempt can be fulfilled.
    /// </summary>
    [StringLength(256)]
    public string? Opportunity { get; set; }

    /// <summary>
    ///     Gets or sets which attempt this is for the given person on the given offering.
    /// </summary>
    public int? Attempt { get; set; }

    /// <summary>
    ///     Gets or sets the state of this attempt.
    /// </summary>
    [StringLength(256)]
    public string? State { get; set; }

    /// <summary>
    ///     Gets or sets the start date time.
    /// </summary>
    [StringLength(256)]
    public string? StartDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the end date time.
    /// </summary>
    [StringLength(256)]
    public string? EndDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the attendance status.
    /// </summary>
    [StringLength(256)]
    public string? Attendance { get; set; }

    /// <summary>
    ///     Gets or sets additional information about external disturbances or irregularities.
    /// </summary>
    [StringLength(2048)]
    public string? Irregularities { get; set; }

    /// <summary>
    ///     Gets or sets the documents related to this attempt (embedded, full <c>Document</c> objects
    ///     per spec - not identifier references) as JSON.
    /// </summary>
    public string? DocumentsJson { get; set; }

    /// <summary>
    ///     Gets or sets the result of this attempt as JSON.
    /// </summary>
    public string? ResultJson { get; set; }

    /// <summary>
    ///     Gets or sets consumer information as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    // Foreign keys
    /// <summary>
    ///     Gets or sets the test component offering association foreign key (the association under
    ///     which this attempt was made).
    /// </summary>
    public Guid? TestComponentOfferingAssociationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the course offering association foreign key (the student's enrolment in a
    ///     course offering to which the current association relates - optional per spec).
    /// </summary>
    public Guid? CourseOfferingAssociationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the coordinator (person) foreign key.
    /// </summary>
    public Guid? CoordinatorEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the test component offering association this attempt was made under.
    /// </summary>
    [ForeignKey(nameof(TestComponentOfferingAssociationEntityId))]
    public virtual TestComponentOfferingAssociationEntity? TestComponentOfferingAssociation { get; set; }

    /// <summary>
    ///     Gets or sets the course offering association this attempt relates to.
    /// </summary>
    [ForeignKey(nameof(CourseOfferingAssociationEntityId))]
    public virtual CourseOfferingAssociationEntity? CourseOfferingAssociation { get; set; }

    /// <summary>
    ///     Gets or sets the coordinator responsible for overseeing the test.
    /// </summary>
    [ForeignKey(nameof(CoordinatorEntityId))]
    public virtual PersonEntity? Coordinator { get; set; }

    /// <summary>
    ///     Gets or sets the rooms used for this attempt.
    /// </summary>
    public virtual ICollection<RoomEntity> Rooms { get; set; } = [];
}
