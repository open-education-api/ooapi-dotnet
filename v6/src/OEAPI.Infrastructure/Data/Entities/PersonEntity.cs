using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Person.
/// </summary>
[Table("Persons")]
[Index(nameof(PersonId), IsUnique = true)]
[Index(nameof(PrimaryCodeType), nameof(PrimaryCode), IsUnique = true)]
public class PersonEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the person's unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string PersonId { get; set; } = string.Empty;

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
    ///     Gets or sets the given name (first name).
    /// </summary>
    [StringLength(256)]
    public string? GivenName { get; set; }

    /// <summary>
    ///     Gets or sets the alternate name.
    /// </summary>
    [StringLength(256)]
    public string? AlternateName { get; set; }

    /// <summary>
    ///     Gets or sets the preferred name.
    /// </summary>
    [StringLength(256)]
    public string? PreferredName { get; set; }

    /// <summary>
    ///     Gets or sets the surname prefix.
    /// </summary>
    [StringLength(256)]
    public string? SurnamePrefix { get; set; }

    /// <summary>
    ///     Gets or sets the surname (family name).
    /// </summary>
    [StringLength(256)]
    public string? Surname { get; set; }

    /// <summary>
    ///     Gets or sets the display name.
    /// </summary>
    [StringLength(256)]
    public string? DisplayName { get; set; }

    /// <summary>
    ///     Gets or sets the initials.
    /// </summary>
    [StringLength(256)]
    public string? Initials { get; set; }

    /// <summary>
    ///     Gets or sets the ID check name.
    /// </summary>
    [StringLength(2048)]
    public string? IdCheckName { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this person has an active enrolment.
    /// </summary>
    public bool ActiveEnrolment { get; set; }

    /// <summary>
    ///     Gets or sets the date of birth.
    /// </summary>
    [StringLength(256)]
    public string? DateOfBirth { get; set; }

    /// <summary>
    ///     Gets or sets the city of birth.
    /// </summary>
    [StringLength(256)]
    public string? CityOfBirth { get; set; }

    /// <summary>
    ///     Gets or sets the country of birth as JSON.
    /// </summary>
    public string? CountryOfBirthJson { get; set; }

    /// <summary>
    ///     Gets or sets the nationality as JSON.
    /// </summary>
    public string? NationalityJson { get; set; }

    /// <summary>
    ///     Gets or sets the date of nationality.
    /// </summary>
    [StringLength(256)]
    public string? DateOfNationality { get; set; }

    /// <summary>
    ///     Gets or sets the gender (extensible enum). Sized 256, matching every other extensible-enum
    ///     field in this codebase - the spec's own <c>gender.yaml</c> has no <c>maxLength</c>.
    /// </summary>
    [StringLength(256)]
    public string? Gender { get; set; }

    /// <summary>
    ///     Gets or sets the title prefix.
    /// </summary>
    [StringLength(256)]
    public string? TitlePrefix { get; set; }

    /// <summary>
    ///     Gets or sets the title suffix.
    /// </summary>
    [StringLength(256)]
    public string? TitleSuffix { get; set; }

    /// <summary>
    ///     Gets or sets the office.
    /// </summary>
    [StringLength(256)]
    public string? Office { get; set; }

    /// <summary>
    ///     Gets or sets the email.
    /// </summary>
    [StringLength(256)]
    public string? Email { get; set; }

    /// <summary>
    ///     Gets or sets the secondary email.
    /// </summary>
    [StringLength(256)]
    public string? SecondaryEmail { get; set; }

    /// <summary>
    ///     Gets or sets the telephone number.
    /// </summary>
    [StringLength(256)]
    public string? TelephoneNumber { get; set; }

    /// <summary>
    ///     Gets or sets the mobile number.
    /// </summary>
    [StringLength(256)]
    public string? MobileNumber { get; set; }

    /// <summary>
    ///     Gets or sets the photo social URL.
    /// </summary>
    [StringLength(2048)]
    public string? PhotoSocial { get; set; }

    /// <summary>
    ///     Gets or sets the photo official URL.
    /// </summary>
    [StringLength(2048)]
    public string? PhotoOfficial { get; set; }

    /// <summary>
    ///     Gets or sets the ICE name.
    /// </summary>
    [StringLength(256)]
    public string? IceName { get; set; }

    /// <summary>
    ///     Gets or sets the ICE phone number.
    /// </summary>
    [StringLength(256)]
    public string? IcePhoneNumber { get; set; }

    /// <summary>
    ///     Gets or sets the affiliations as JSON.
    /// </summary>
    public string? AffiliationsJson { get; set; }

    /// <summary>
    ///     Gets or sets the language of choice as JSON.
    /// </summary>
    public string? LanguageOfChoiceJson { get; set; }

    /// <summary>
    ///     Gets or sets the assigned needs as JSON.
    /// </summary>
    public string? AssignedNeedsJson { get; set; }

    /// <summary>
    ///     Gets or sets the address as JSON.
    /// </summary>
    public string? AddressJson { get; set; }

    /// <summary>
    ///     Gets or sets the ICE relation as JSON.
    /// </summary>
    public string? IceRelationJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets the extension data as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    // Navigation properties and collections for relationships
    /// <summary>
    ///     Gets or sets the collection of memberships for this person.
    /// </summary>
    public virtual ICollection<MembershipEntity> Memberships { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of course offering associations for this person.
    /// </summary>
    public virtual ICollection<CourseOfferingAssociationEntity> CourseOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the collection of learning component offering associations for this person.
    /// </summary>
    public virtual ICollection<LearningComponentOfferingAssociationEntity> LearningComponentOfferingAssociations
    {
        get;
        set;
    } = [];

    /// <summary>
    ///     Gets or sets the collection of programme offering associations for this person.
    /// </summary>
    public virtual ICollection<ProgrammeOfferingAssociationEntity> ProgrammeOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the collection of test component offering associations for this person.
    /// </summary>
    public virtual ICollection<TestComponentOfferingAssociationEntity> TestComponentOfferingAssociations { get; set; } =
        [];

    /// <summary>
    ///     Gets or sets the programmes for which this person is a coordinator.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> CoordinatedProgrammes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the programmes for which this person is an instructor.
    /// </summary>
    public virtual ICollection<ProgrammeEntity> InstructedProgrammes { get; set; } = [];

    /// <summary>
    ///     Gets or sets the courses for which this person is a coordinator.
    /// </summary>
    public virtual ICollection<CourseEntity> CoordinatedCourses { get; set; } = [];

    /// <summary>
    ///     Gets or sets the courses for which this person is an instructor.
    /// </summary>
    public virtual ICollection<CourseEntity> InstructedCourses { get; set; } = [];

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];
}
