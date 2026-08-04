using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A component is a part of a course.
/// </summary>
public class LearningComponent : LearningComponentId
{
    /// <summary>
    ///     Unique id for this learning component (API property name).
    /// </summary>
    [JsonIgnore]
    public string ComponentId => ComponentIdValue;

    /// <summary>
    ///     Component type.
    /// </summary>
    [JsonPropertyName("componentType")]
    [ExtensibleEnum("learningComponentType")]
    public string? ComponentType { get; set; }

    /// <summary>
    ///     The primary human readable identifier for this component. This is often the source identifier as defined by the
    ///     institution.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The name of this learning component.
    /// </summary>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The description of this learning component.
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The abbreviation of this component.
    /// </summary>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     Modes of delivery.
    /// </summary>
    [JsonPropertyName("modesOfDelivery")]
    [ExtensibleEnum("modeOfDelivery")]
    public string[]? ModesOfDelivery { get; set; }

    /// <summary>
    ///     Duration of this component.
    /// </summary>
    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    /// <summary>
    ///     The first possible start datetime for an offering based on this learning component. An
    ///     offering may not start before this moment.
    /// </summary>
    [JsonPropertyName("firstPossibleOfferingStartDateTime")]
    public string? FirstPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     The last possible start datetime for an offering based on this learning component. An
    ///     offering may not start beyond this moment.
    /// </summary>
    [JsonPropertyName("lastPossibleOfferingStartDateTime")]
    public string? LastPossibleOfferingStartDateTime { get; set; }

    /// <summary>
    ///     The last possible end datetime for an offering based on this learning component. An offering
    ///     must end on this moment.
    /// </summary>
    [JsonPropertyName("lastPossibleOfferingEndDateTime")]
    public string? LastPossibleOfferingEndDateTime { get; set; }

    /// <summary>
    ///     The languages in which this component is given.
    /// </summary>
    [JsonPropertyName("teachingLanguages")]
    public string[]? TeachingLanguages { get; set; }

    /// <summary>
    ///     The identifiers of the learning outcomes related to this learning component.
    ///     When the client does not request expansion of `learningOutcomes`, only these identifiers
    ///     are returned.
    /// </summary>
    [JsonPropertyName("learningOutcomeIds")]
    public Identifier[]? LearningOutcomeIds { get; set; }

    /// <summary>
    ///     The expanded learning outcome objects related to this learning component.
    ///     When the client requests expansion of `learningOutcomes`, the full expanded learning
    ///     outcome objects MUST be returned here instead of only the identifiers.
    /// </summary>
    [JsonPropertyName("learningOutcomes")]
    public LearningOutcome[]? LearningOutcomes { get; set; }

    /// <summary>
    ///     The extra information that is provided for enrolment.
    /// </summary>
    [JsonPropertyName("enrolment")]
    public LanguageTypedString[]? Enrolment { get; set; }

    /// <summary>
    ///     An overview of the literature and other resources used in this component.
    /// </summary>
    [JsonPropertyName("resources")]
    public string[]? Resources { get; set; }

    /// <summary>
    ///     A description of the way exams for this component are taken.
    /// </summary>
    [JsonPropertyName("assessment")]
    public LanguageTypedString[]? Assessment { get; set; }

    /// <summary>
    ///     Addresses for this component.
    /// </summary>
    [JsonPropertyName("addresses")]
    public Address[]? Addresses { get; set; }

    /// <summary>
    ///     Other codes/identifiers for this component.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     The identifier of the parent learning component of which this learning component is a child.
    ///     When the client does not request expansion of `parent`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("parentId")]
    public Identifier? ParentId { get; set; }

    /// <summary>
    ///     The expanded parent learning component of which this learning component is a child.
    ///     When the client requests expansion of `parent`, the full expanded learning component object
    ///     MUST be returned here instead of only the identifier. If no parent is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("parent")]
    public LearningComponent? Parent { get; set; }

    /// <summary>
    ///     The identifiers of the learning components which are a part of this learning component
    ///     (e.g. combined tests). When the client does not request expansion of `children`, only these
    ///     identifiers are returned.
    /// </summary>
    [JsonPropertyName("childIds")]
    public Identifier[]? ChildIds { get; set; }

    /// <summary>
    ///     The expanded learning component objects which are a part of this learning component.
    ///     When the client requests expansion of `children`, the full expanded learning component
    ///     objects MUST be returned here instead of only the identifiers. If no children are defined,
    ///     this value is `null`.
    /// </summary>
    [JsonPropertyName("children")]
    public LearningComponent[]? Children { get; set; }

    /// <summary>
    ///     Consumer information.
    /// </summary>
    [JsonPropertyName("consumer")]
    public object? Consumer { get; set; }

    /// <summary>
    ///     The identifier of the course of which this component is a part.
    ///     When the client does not request expansion of `course`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("courseId")]
    public Identifier? CourseId { get; set; }

    /// <summary>
    ///     The expanded course object of which this component is a part.
    ///     When the client requests expansion of `course`, the full expanded course object
    ///     MUST be returned here instead of only the identifier. If no course is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("course")]
    public Course? Course { get; set; }

    /// <summary>
    ///     The identifier of the organisation that provides this component.
    ///     When the client does not request expansion of `organisation`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("organisationId")]
    public Identifier? OrganisationId { get; set; }

    /// <summary>
    ///     The expanded organisation object that provides this component.
    ///     When the client requests expansion of `organisation`, the full expanded organisation object
    ///     MUST be returned here instead of only the identifier. If no organisation is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("organisation")]
    public Organisation? Organisation { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}
