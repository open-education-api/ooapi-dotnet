using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A self-contained and formally structured learning experience, aimed at delivering defined learning outcomes to
///     students.
/// </summary>
public class Course : CourseId
{
    /// <summary>
    ///     Unique id of this course (API property name).
    /// </summary>
    [JsonIgnore]
    public string CourseId => CourseIdValue;

    /// <summary>
    ///     The primary human readable identifier for this course.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The name of this course (ECTS-title).
    /// </summary>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The abbreviation or internal code used to identify this course (ECTS-code).
    /// </summary>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The first moment this course is valid (inclusive).
    /// </summary>
    [JsonPropertyName("validFrom")]
    public string? ValidFrom { get; set; }

    /// <summary>
    ///     The moment this course ceases to be valid (e.g. exclusive).
    /// </summary>
    [JsonPropertyName("validTo")]
    public string? ValidTo { get; set; }

    /// <summary>
    ///     Study load descriptors.
    /// </summary>
    [JsonPropertyName("studyLoad")]
    public StudyLoadDescriptor[]? StudyLoad { get; set; }

    /// <summary>
    ///     Modes of delivery.
    /// </summary>
    [JsonPropertyName("modesOfDelivery")]
    [ExtensibleEnum("modeOfDelivery")]
    public string[]? ModesOfDelivery { get; set; }

    /// <summary>
    ///     The duration of this course.
    /// </summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    /// <summary>
    ///     The date and time when participants can follow this course for the first time.
    /// </summary>
    [JsonPropertyName("firstStartDate")]
    public string? FirstStartDate { get; set; }

    /// <summary>
    ///     The first possible start datetime for an offering based on this course. An offering may not
    ///     start before this moment.
    /// </summary>
    [JsonPropertyName("firstPossibleOfferingStartDateTime")]
    public string? FirstPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     The last possible start datetime for an offering based on this course. An offering may not
    ///     start beyond this moment.
    /// </summary>
    [JsonPropertyName("lastPossibleOfferingStartDateTime")]
    public string? LastPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     The last possible end datetime for an offering based on this course. An offering must end on
    ///     this moment.
    /// </summary>
    [JsonPropertyName("lastPossibleOfferingEndDateTime")]
    public string? LastPossibleOfferingEndDateTime { get; set; }

    /// <summary>
    ///     The description of this course (ECTS-description).
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The languages in which this course is given.
    /// </summary>
    [JsonPropertyName("teachingLanguages")]
    public string[]? TeachingLanguages { get; set; }

    /// <summary>
    ///     Field(s) of study (e.g. ISCED-F).
    /// </summary>
    [JsonPropertyName("fieldsOfStudy")]
    public string? FieldsOfStudy { get; set; }

    /// <summary>
    ///     The identifiers of the learning outcomes related to this course.
    ///     When the client does not request expansion of `learningOutcomes`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("learningOutcomeIds")]
    public Identifier[]? LearningOutcomeIds { get; set; }

    /// <summary>
    ///     The expanded learning outcome objects related to this course.
    ///     When the client requests expansion of `learningOutcomes`, the full expanded learning outcome objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("learningOutcomes")]
    public LearningOutcome[]? LearningOutcomes { get; set; }

    /// <summary>
    ///     The identifiers of the programmes of which this course is a part.
    ///     When the client does not request expansion of `programmes`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("programmeIds")]
    public Identifier[]? ProgrammeIds { get; set; }

    /// <summary>
    ///     The expanded programme objects of which this course is a part.
    ///     When the client requests expansion of `programmes`, the full expanded programme objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("programmes")]
    public Programme[]? Programmes { get; set; }

    /// <summary>
    ///     The identifiers of the organisations that coordinate this course.
    ///     When the client does not request expansion of `coordinators`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("coordinatorIds")]
    public Identifier[]? CoordinatorIds { get; set; }

    /// <summary>
    ///     The expanded person objects that coordinate this course.
    ///     When the client requests expansion of `coordinators`, the full expanded person objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("coordinators")]
    public Person[]? Coordinators { get; set; }

    /// <summary>
    ///     The identifiers of the organisations that instruct this course.
    ///     When the client does not request expansion of `instructors`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("instructorIds")]
    public Identifier[]? InstructorIds { get; set; }

    /// <summary>
    ///     The expanded person objects that instruct this course.
    ///     When the client requests expansion of `instructors`, the full expanded person objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("instructors")]
    public Person[]? Instructors { get; set; }

    /// <summary>
    ///     The identifier of the organisation that provides this course.
    ///     When the client does not request expansion of `organisation`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("organisationId")]
    public Identifier? OrganisationId { get; set; }

    /// <summary>
    ///     The expanded organisation object that provides this course.
    ///     When the client requests expansion of `organisation`, the full expanded organisation object
    ///     MUST be returned here instead of only the identifier. If no organisation is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("organisation")]
    public Organisation? Organisation { get; set; }

    /// <summary>
    ///     Other codes/identifiers for this course.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     URL of the course's website.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    ///     Addresses for this course.
    /// </summary>
    [JsonPropertyName("addresses")]
    public Address[]? Addresses { get; set; }

    /// <summary>
    ///     The level of this course (ECTS year of study if applicable).
    /// </summary>
    [JsonPropertyName("level")]
    [ExtensibleEnum("level")]
    public string? Level { get; set; }

    /// <summary>
    ///     An overview of the literature and other resources used in this course (ECTS-recommended
    ///     reading and other sources).
    /// </summary>
    [JsonPropertyName("resources")]
    public string[]? Resources { get; set; }

    /// <summary>
    ///     A description of the way exams for this course are taken (ECTS-assessment method and criteria).
    /// </summary>
    [JsonPropertyName("assessment")]
    public LanguageTypedString[]? Assessment { get; set; }

    /// <summary>
    ///     Extra information that is provided for enrolment.
    /// </summary>
    [JsonPropertyName("enrolment")]
    public LanguageTypedString[]? Enrolment { get; set; }

    /// <summary>
    ///     Admission requirements for this course.
    /// </summary>
    [JsonPropertyName("admissionRequirements")]
    public LanguageTypedString[]? AdmissionRequirements { get; set; }

    /// <summary>
    ///     Qualification requirements for this course.
    /// </summary>
    [JsonPropertyName("qualificationRequirements")]
    public LanguageTypedString[]? QualificationRequirements { get; set; }

    /// <summary>
    ///     Optional supplementary information associated with this course.
    /// </summary>
    [JsonPropertyName("supplementaryInformation")]
    public SupplementaryInformation[]? SupplementaryInformation { get; set; }

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }

    /// <summary>
    ///     Historical or future alternate snapshots of this course, returned when the client requests
    ///     <c>returnTimelineOverrides=true</c>. Omitted when not requested or when none exist.
    /// </summary>
    [JsonPropertyName("timelineOverrides")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimelineOverrideCourse[]? TimelineOverrides { get; set; }
}
