using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A collection of courses that lead to a certifiable learning outcome.
/// </summary>
public class Programme : ProgrammeId
{
    /// <summary>
    ///     Unique id for this programme (API property name).
    /// </summary>
    [JsonIgnore]
    public string ProgrammeId => ProgrammeIdValue;

    /// <summary>
    ///     The first moment this programme is valid (inclusive).
    /// </summary>
    [JsonPropertyName("validFrom")]
    public string? ValidFrom { get; set; }

    /// <summary>
    ///     The moment this programme ceases to be valid (e.g. exclusive).
    /// </summary>
    [JsonPropertyName("validTo")]
    public string? ValidTo { get; set; }

    /// <summary>
    ///     The primary human readable identifier for this programme. This is often the source identifier as defined by the
    ///     institution.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     Programme type.
    /// </summary>
    [JsonPropertyName("programmeType")]
    [ExtensibleEnum("programmeType")]
    public string? ProgrammeType { get; set; }

    /// <summary>
    ///     The name of this programme.
    /// </summary>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The abbreviation of this programme.
    /// </summary>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The description of this programme.
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The languages in which this programme is given.
    /// </summary>
    [JsonPropertyName("teachingLanguages")]
    public string[]? TeachingLanguages { get; set; }

    /// <summary>
    ///     Study load descriptors.
    /// </summary>
    [JsonPropertyName("studyLoad")]
    public StudyLoadDescriptor[]? StudyLoad { get; set; }

    /// <summary>
    ///     Qualification awarded.
    /// </summary>
    [JsonPropertyName("qualificationAwarded")]
    [ExtensibleEnum("qualificationAwarded")]
    public string? QualificationAwarded { get; set; }

    /// <summary>
    ///     Academic field designations.
    /// </summary>
    [JsonPropertyName("qualificationDesignations")]
    public string[]? QualificationDesignations { get; set; }

    /// <summary>
    ///     Mode of study.
    /// </summary>
    [JsonPropertyName("modeOfStudy")]
    [ExtensibleEnum("modeOfStudy")]
    public string? ModeOfStudy { get; set; }

    /// <summary>
    ///     Modes of delivery.
    /// </summary>
    [JsonPropertyName("modesOfDelivery")]
    [ExtensibleEnum("modeOfDelivery")]
    public string[]? ModesOfDelivery { get; set; }

    /// <summary>
    ///     Level of qualification according to the European Qualifications Framework (EQF).
    /// </summary>
    [JsonPropertyName("levelOfQualification")]
    [ExtensibleEnum("levelOfQualification")]
    public string? LevelOfQualification { get; set; }

    /// <summary>
    ///     The type of formal document obtained upon completion of this programme.
    /// </summary>
    [JsonPropertyName("formalDocument")]
    [ExtensibleEnum("formalDocument")]
    public string? FormalDocument { get; set; }

    /// <summary>
    ///     Duration of this programme.
    /// </summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    /// <summary>
    ///     The moment when participants can follow this programme for the first time. Superseded by
    ///     the new DateTime attributes.
    /// </summary>
    [JsonPropertyName("firstStartDateTime")]
    public string? FirstStartDateTime { get; set; }

    /// <summary>
    ///     The first possible start datetime for an offering based on this programme. An offering may
    ///     not start before this moment.
    /// </summary>
    [JsonPropertyName("firstPossibleOfferingStartDateTime")]
    public string? FirstPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     The last possible start datetime for an offering based on this programme. An offering may
    ///     not start beyond this moment.
    /// </summary>
    [JsonPropertyName("lastPossibleOfferingStartDateTime")]
    public string? LastPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     The last possible end datetime for an offering based on this programme. An offering must
    ///     end on this moment.
    /// </summary>
    [JsonPropertyName("lastPossibleOfferingEndDateTime")]
    public string? LastPossibleOfferingEndDateTime { get; set; }

    /// <summary>
    ///     The level of this programme.
    /// </summary>
    [JsonPropertyName("level")]
    [ExtensibleEnum("level")]
    public string? Level { get; set; }

    /// <summary>
    ///     Field(s) of study (e.g. ISCED-F).
    /// </summary>
    [JsonPropertyName("fieldsOfStudy")]
    public string? FieldsOfStudy { get; set; }

    /// <summary>
    ///     Extra information that is provided for enrolment.
    /// </summary>
    [JsonPropertyName("enrolment")]
    public LanguageTypedString[]? Enrolment { get; set; }

    /// <summary>
    ///     An overview of the literature and other resources used in this programme.
    /// </summary>
    [JsonPropertyName("resources")]
    public string[]? Resources { get; set; }

    /// <summary>
    ///     The identifiers of the learning outcomes related to this programme.
    ///     When the client does not request expansion of `learningOutcomes`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("learningOutcomeIds")]
    public Identifier[]? LearningOutcomeIds { get; set; }

    /// <summary>
    ///     The expanded learning outcome objects related to this programme.
    ///     When the client requests expansion of `learningOutcomes`, the full expanded learning outcome objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("learningOutcomes")]
    public LearningOutcome[]? LearningOutcomes { get; set; }

    /// <summary>
    ///     A description of the way exams for this programme are taken.
    /// </summary>
    [JsonPropertyName("assessment")]
    public LanguageTypedString[]? Assessment { get; set; }

    /// <summary>
    ///     Admission requirements for this programme.
    /// </summary>
    [JsonPropertyName("admissionRequirements")]
    public LanguageTypedString[]? AdmissionRequirements { get; set; }

    /// <summary>
    ///     Qualification requirements for this programme.
    /// </summary>
    [JsonPropertyName("qualificationRequirements")]
    public LanguageTypedString[]? QualificationRequirements { get; set; }

    /// <summary>
    ///     URL of the programme's website.
    /// </summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>
    ///     Addresses for this programme.
    /// </summary>
    [JsonPropertyName("addresses")]
    public Address[]? Addresses { get; set; }

    /// <summary>
    ///     Optional supplementary information associated with this programme.
    /// </summary>
    [JsonPropertyName("supplementaryInformation")]
    public SupplementaryInformation[]? SupplementaryInformation { get; set; }

    /// <summary>
    ///     Other codes/identifiers for this programme.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     The identifier of the parent programme of which this programme is a child.
    ///     When the client does not request expansion of `parent`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("parentId")]
    public Identifier? ParentId { get; set; }

    /// <summary>
    ///     The expanded programme object of which this programme is a child.
    ///     When the client requests expansion of `parent`, the full expanded programme object
    ///     MUST be returned here instead of only the identifier. If no parent is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("parent")]
    public Programme? Parent { get; set; }

    /// <summary>
    ///     The identifiers of the child programmes for which this programme is the parent.
    ///     When the client does not request expansion of `children`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("childIds")]
    public Identifier[]? ChildIds { get; set; }

    /// <summary>
    ///     The expanded programme objects for which this programme is the parent.
    ///     When the client requests expansion of `children`, the full expanded programme objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("children")]
    public Programme[]? Children { get; set; }

    /// <summary>
    ///     The identifiers of the persons that coordinate this programme.
    ///     When the client does not request expansion of `coordinators`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("coordinatorIds")]
    public Identifier[]? CoordinatorIds { get; set; }

    /// <summary>
    ///     The expanded person objects that coordinate this programme.
    ///     When the client requests expansion of `coordinators`, the full expanded person objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("coordinators")]
    public Person[]? Coordinators { get; set; }

    /// <summary>
    ///     The identifiers of the persons that instruct this programme.
    ///     When the client does not request expansion of `instructors`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("instructorIds")]
    public Identifier[]? InstructorIds { get; set; }

    /// <summary>
    ///     The expanded person objects that instruct this programme.
    ///     When the client requests expansion of `instructors`, the full expanded person objects
    ///     MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("instructors")]
    public Person[]? Instructors { get; set; }

    /// <summary>
    ///     The identifier of the organisation that provides this programme.
    ///     When the client does not request expansion of `organisation`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("organisationId")]
    public Identifier? OrganisationId { get; set; }

    /// <summary>
    ///     The expanded organisation object that provides this programme.
    ///     When the client requests expansion of `organisation`, the full expanded organisation object
    ///     MUST be returned here instead of only the identifier. If no organisation is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("organisation")]
    public Organisation? Organisation { get; set; }

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
    ///     Historical or future alternate snapshots of this programme, returned when the client requests
    ///     <c>returnTimelineOverrides=true</c>. Omitted when not requested or when none exist.
    /// </summary>
    [JsonPropertyName("timelineOverrides")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimelineOverrideProgramme[]? TimelineOverrides { get; set; }
}
