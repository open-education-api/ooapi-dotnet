using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A result as part of an association or attempt.
/// </summary>
public class Result
{
    /// <summary>
    ///     The state of this result.
    /// </summary>
    [JsonPropertyName("state")]
    [ExtensibleEnum("resultState")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    ///     Whether this result is a pass, a fail, or unknown.
    /// </summary>
    [JsonPropertyName("pass")]
    [ExtensibleEnum("passState")]
    public string? Pass { get; set; }

    /// <summary>
    ///     The comment on this result.
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    /// <summary>
    ///     The score of this programme/course/component association (based on <c>resultValueType</c>
    ///     in the offering).
    /// </summary>
    [JsonPropertyName("score")]
    public string? Score { get; set; }

    /// <summary>
    ///     The type of the result value (e.g. grade, percentage, pass/fail, etc.), when it differs
    ///     from the <c>resultValueType</c> defined in the offering.
    /// </summary>
    [JsonPropertyName("resultvaluetype")]
    [ExtensibleEnum("resultValueType")]
    public string? ResultValueType { get; set; }

    /// <summary>
    ///     The number of points scored by a person from which the result could be calculated.
    /// </summary>
    [JsonPropertyName("rawScore")]
    public int? RawScore { get; set; }

    /// <summary>
    ///     The maximum number of points a person could achieve on the test or assessment form.
    /// </summary>
    [JsonPropertyName("maxRawScore")]
    public int? MaxRawScore { get; set; }

    /// <summary>
    ///     Indicates that the result has been finalised by the exam committee.
    /// </summary>
    [JsonPropertyName("final")]
    public bool? Final { get; set; }

    /// <summary>
    ///     The identifier of the assessor responsible for evaluating the result.
    ///     When the client does not request expansion of `assessor`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("assessorId")]
    public Identifier? AssessorId { get; set; }

    /// <summary>
    ///     The expanded assessor (person) responsible for evaluating the result.
    ///     When the client requests expansion of `assessor`, the full person object MUST be returned
    ///     here instead of only the identifier. If no assessor is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("assessor")]
    public Person? Assessor { get; set; }

    /// <summary>
    ///     The date this result has been published, RFC3339 (full-date).
    /// </summary>
    [JsonPropertyName("resultDateTime")]
    public string ResultDateTime { get; set; } = string.Empty;

    /// <summary>
    ///     Documents that are related to the result (e.g. assessment form, assessment model, etc.).
    /// </summary>
    [JsonPropertyName("documents")]
    public Document[]? Documents { get; set; }

    /// <summary>
    ///     Only relevant on a <c>CourseOfferingAssociation</c>/<c>ProgrammeOfferingAssociation</c>
    ///     result: the study load for the individual student/enrolment, when different from the
    ///     offering's own study load. Stays absent everywhere else.
    /// </summary>
    [JsonPropertyName("studyLoad")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StudyLoadDescriptor? StudyLoad { get; set; }

    /// <summary>
    ///     Only relevant on a <c>LearningComponentOfferingAssociation</c>/
    ///     <c>TestComponentOfferingAssociation</c> result: the weight of this result out of 100 for
    ///     the offering. Stays absent everywhere else.
    /// </summary>
    [JsonPropertyName("weight")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Weight { get; set; }

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
