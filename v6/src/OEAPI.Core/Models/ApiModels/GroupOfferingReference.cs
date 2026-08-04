using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     One entry of <see cref="Group.OfferingIds" />: a reference to exactly one offering, of exactly
///     one of the four offering types. Mirrors the spec's polymorphic <c>offeringIds</c> item schema
///     (<c>minProperties: 1, maxProperties: 1, additionalProperties: false</c> across
///     <c>courseOfferingId</c>/<c>programmeOfferingId</c>/<c>learningComponentOfferingId</c>/
///     <c>testComponentOfferingId</c>) - exactly one of these four properties is set per instance, the
///     others are omitted from JSON entirely (not serialized as <c>null</c>) via
///     <see cref="JsonIgnoreCondition.WhenWritingNull" />.
/// </summary>
public class GroupOfferingReference
{
    /// <summary>
    ///     The identifier of the course offering this group is associated with. Set only when this
    ///     reference points to a course offering.
    /// </summary>
    [JsonPropertyName("courseOfferingId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CourseOfferingId { get; set; }

    /// <summary>
    ///     The identifier of the programme offering this group is associated with. Set only when this
    ///     reference points to a programme offering.
    /// </summary>
    [JsonPropertyName("programmeOfferingId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProgrammeOfferingId { get; set; }

    /// <summary>
    ///     The identifier of the learning component offering this group is associated with. Set only
    ///     when this reference points to a learning component offering.
    /// </summary>
    [JsonPropertyName("learningComponentOfferingId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? LearningComponentOfferingId { get; set; }

    /// <summary>
    ///     The identifier of the test component offering this group is associated with. Set only when
    ///     this reference points to a test component offering.
    /// </summary>
    [JsonPropertyName("testComponentOfferingId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TestComponentOfferingId { get; set; }
}
