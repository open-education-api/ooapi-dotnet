using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A learning outcome - statements regarding what a learner knows, understands and is able to do on completion of a
///     learning process.
/// </summary>
public class LearningOutcome : LearningOutcomeId
{
    /// <summary>
    ///     Unique id for this learning outcome (API property name).
    /// </summary>
    [JsonIgnore]
    public string LearningOutcomeId => LearningOutcomeIdValue;

    /// <summary>
    ///     The primary human readable identifier for this learning outcome. This is often the source identifier as defined by
    ///     the institution.
    /// </summary>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The name of this learning outcome.
    /// </summary>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The abbreviation or internal code used to identify this LearningOutcome.
    /// </summary>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The description of this learning outcome.
    /// </summary>
    [JsonPropertyName("description")]
    public LanguageTypedString[]? Description { get; set; }

    /// <summary>
    ///     The identifiers of the learning outcomes which are the parents of this learning outcome.
    ///     When the client does not request expansion of parents, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("parentIds")]
    public Identifier[]? ParentIds { get; set; }

    /// <summary>
    ///     The expanded learning outcome objects which are the parents of this learning outcome.
    ///     When the client requests expansion of parents, the full expanded learning outcome objects MUST be returned here.
    /// </summary>
    [JsonPropertyName("parents")]
    public LearningOutcome[]? Parents { get; set; }

    /// <summary>
    ///     The identifiers of all learning outcomes for which this learning outcome is the parent.
    ///     When the client does not request expansion of children, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("childIds")]
    public Identifier[]? ChildIds { get; set; }

    /// <summary>
    ///     The expanded learning outcome objects for which this learning outcome is the parent.
    ///     When the client requests expansion of children, the full expanded learning outcome objects MUST be returned here.
    /// </summary>
    [JsonPropertyName("children")]
    public LearningOutcome[]? Children { get; set; }

    /// <summary>
    ///     Field(s) of study (e.g. ISCED-F). Preferably contains at least 4 digits.
    /// </summary>
    [JsonPropertyName("fieldsOfStudy")]
    public string? FieldsOfStudy { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

    /// <summary>
    ///     The level of the learning outcome using various frameworks.
    /// </summary>
    [JsonPropertyName("complexityLevel")]
    [ExtensibleEnum("learningOutcomeLevel")]
    public string? ComplexityLevel { get; set; }

    /// <summary>
    ///     The date and time for when this learning outcome will be active.
    /// </summary>
    [JsonPropertyName("validFrom")]
    public string? ValidFrom { get; set; }

    /// <summary>
    ///     The date and time when this learning outcome will no longer be valid.
    /// </summary>
    [JsonPropertyName("validTo")]
    public string? ValidTo { get; set; }

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
}
