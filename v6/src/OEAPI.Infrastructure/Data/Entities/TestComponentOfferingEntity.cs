using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for TestComponentOffering.
/// </summary>
[Table("TestComponentOfferings")]
[Index(nameof(TestComponentOfferingIdValue), IsUnique = true)]
public class TestComponentOfferingEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the test component offering unique identifier value.
    /// </summary>
    [Required]
    [StringLength(36)]
    public string TestComponentOfferingIdValue { get; set; } = string.Empty;

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
    ///     Gets or sets the state of this offering (extensible enum, e.g. active, inactive, archived).
    /// </summary>
    [StringLength(256)]
    public string? State { get; set; }

    /// <summary>
    ///     Gets or sets the rostering state of this offering (extensible enum).
    /// </summary>
    [StringLength(256)]
    public string? RosteringState { get; set; }

    /// <summary>
    ///     Gets or sets the abbreviation or internal code used to identify this offering.
    /// </summary>
    [StringLength(256)]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Gets or sets the description as JSON.
    /// </summary>
    public string? DescriptionJson { get; set; }

    /// <summary>
    ///     Gets or sets the teaching languages as JSON.
    /// </summary>
    public string? TeachingLanguagesJson { get; set; }

    /// <summary>
    ///     Gets or sets the modes of delivery as JSON.
    /// </summary>
    public string? ModesOfDeliveryJson { get; set; }

    /// <summary>
    ///     Gets or sets the maximum number of students allowed to enrol for this offering.
    /// </summary>
    public int? MaxNumberStudents { get; set; }

    /// <summary>
    ///     Gets or sets the number of students who have already enrolled for this offering.
    /// </summary>
    public int? EnrolledNumberStudents { get; set; }

    /// <summary>
    ///     Gets or sets the number of students who have a pending enrolment request for this offering.
    /// </summary>
    public int? PendingNumberStudents { get; set; }

    /// <summary>
    ///     Gets or sets the minimum number of students needed for this offering to proceed.
    /// </summary>
    public int? MinNumberStudents { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether a result is expected for this offering.
    /// </summary>
    public bool? ResultExpected { get; set; }

    /// <summary>
    ///     Gets or sets the result value type for this offering (extensible enum).
    /// </summary>
    [StringLength(256)]
    public string? ResultValueType { get; set; }

    /// <summary>
    ///     Gets or sets the weight of the result for this offering.
    /// </summary>
    public int? ResultWeight { get; set; }

    /// <summary>
    ///     Gets or sets the documents related to this offering (embedded, full <c>Document</c> objects
    ///     per spec - not identifier references) as JSON.
    /// </summary>
    public string? DocumentsJson { get; set; }

    /// <summary>
    ///     Gets or sets the URL of this offering's webpage.
    /// </summary>
    [StringLength(2048)]
    public string? Link { get; set; }

    /// <summary>
    ///     Gets or sets the enrolment periods as JSON.
    /// </summary>
    public string? EnrolmentPeriodsJson { get; set; }

    /// <summary>
    ///     Gets or sets the supplementary information as JSON.
    /// </summary>
    public string? SupplementaryInformationJson { get; set; }

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
    ///     Gets or sets the flexible entry period start date time.
    /// </summary>
    [StringLength(256)]
    public string? FlexibleEntryPeriodStartDateTime { get; set; }

    /// <summary>
    ///     Gets or sets the flexible entry period end date time.
    /// </summary>
    [StringLength(256)]
    public string? FlexibleEntryPeriodEndDateTime { get; set; }

    /// <summary>
    ///     Gets or sets addresses as JSON.
    /// </summary>
    public string? AddressesJson { get; set; }

    /// <summary>
    ///     Gets or sets price information as JSON.
    /// </summary>
    public string? PriceInformationJson { get; set; }

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
    ///     Gets or sets the course foreign key.
    /// </summary>
    public Guid? CourseEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the test component foreign key.
    /// </summary>
    public Guid? TestComponentEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the organisation foreign key.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    /// <summary>
    ///     Gets or sets the academic session foreign key.
    /// </summary>
    public Guid? AcademicSessionEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the course this test component offering belongs to.
    /// </summary>
    [ForeignKey(nameof(CourseEntityId))]
    public virtual CourseEntity? Course { get; set; }

    /// <summary>
    ///     Gets or sets the test component.
    /// </summary>
    [ForeignKey(nameof(TestComponentEntityId))]
    public virtual TestComponentEntity? TestComponent { get; set; }

    /// <summary>
    ///     Gets or sets the organisation that provides this offering.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }

    /// <summary>
    ///     Gets or sets the academic session this offering takes place during.
    /// </summary>
    [ForeignKey(nameof(AcademicSessionEntityId))]
    public virtual AcademicSessionEntity? AcademicSession { get; set; }

    /// <summary>
    ///     Gets or sets the rooms used by this offering.
    /// </summary>
    public virtual ICollection<RoomEntity> Rooms { get; set; } = [];

    /// <summary>
    ///     Gets or sets the course offerings this offering is associated with.
    /// </summary>
    public virtual ICollection<CourseOfferingEntity> CourseOfferings { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of test component offering associations for this test component offering.
    /// </summary>
    public virtual ICollection<TestComponentOfferingAssociationEntity> TestComponentOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the groups associated with this test component offering.
    /// </summary>
    public virtual ICollection<GroupEntity> Groups { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
