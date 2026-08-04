using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     An object describing the metadata of a course.
/// </summary>
public class CourseId
{
    /// <summary>
    ///     Unique id of this course.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("courseId")]
    public string CourseIdValue { get; set; } = string.Empty;
}
