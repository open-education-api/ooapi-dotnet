using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A named period of time that can be used to communicate the various schedules and time periods
///     an institution recognizes and uses to organise their education. AcademicSessions can be nested.
/// </summary>
public class AcademicSession
{
    /// <summary>
    ///     Unique id for this academic session.
    /// </summary>
    /// <example>123e4567-e89b-12d3-a456-426614174000</example>
    [JsonPropertyName("academicSessionId")]
    public string AcademicSessionId { get; set; } = string.Empty;

    /// <summary>
    ///     The type of this academic session.
    /// </summary>
    /// <example>semester</example>
    [JsonPropertyName("academicSessionType")]
    [ExtensibleEnum("academicSessionType")]
    public string AcademicSessionType { get; set; } = string.Empty;

    /// <summary>
    ///     The primary human readable identifier for this academic session. This is often the source identifier as defined by
    ///     the institution.
    /// </summary>
    /// <example>identifier: 2012-Q1</example>
    [JsonPropertyName("primaryCode")]
    public IdentifierEntry PrimaryCode { get; set; } = new();

    /// <summary>
    ///     The name of this academic session.
    /// </summary>
    /// <example>Autumn term 2020</example>
    [JsonPropertyName("name")]
    public LanguageTypedString[] Name { get; set; } = [];

    /// <summary>
    ///     The abbreviation or internal code used to identify this AcademicSession.
    /// </summary>
    /// <example>SPRING-2026</example>
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    ///     The moment on which this academic session starts, RFC3339 (full-date).
    /// </summary>
    /// <example>2025-09-28T08:30:00+01:00</example>
    [JsonPropertyName("startDateTime")]
    public string StartDateTime { get; set; } = string.Empty;

    /// <summary>
    ///     The moment on which this academic session ends, RFC3339 (full-date).
    /// </summary>
    /// <example>2025-09-28T08:30:00+01:00</example>
    [JsonPropertyName("endDateTime")]
    public string EndDateTime { get; set; } = string.Empty;

    /// <summary>
    ///     The identifier of the parent academicSession for this session.
    ///     When the client does not request expansion of `parent`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("parentId")]
    public Identifier? ParentId { get; set; }

    /// <summary>
    ///     The expanded parent academicSession object of this session.
    ///     When the client requests expansion of `parent`, the full expanded academicSession object
    ///     MUST be returned here instead of only the identifier. If no parent is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("parent")]
    public AcademicSession? Parent { get; set; }

    /// <summary>
    ///     The list of identifiers of child academicSessions of this session.
    ///     When the client does not request expansion of `children`, only these identifiers are returned.
    /// </summary>
    [JsonPropertyName("childIds")]
    public Identifier[]? ChildIds { get; set; }

    /// <summary>
    ///     The expanded child academicSession objects of this session.
    ///     When the client requests expansion of `children`, the full expanded academicSession objects
    ///     MUST be returned here instead of only the identifiers. If no child sessions are defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("children")]
    public AcademicSession[]? Children { get; set; }

    /// <summary>
    ///     The identifier of the top-level academicSession year for this session.
    ///     When the client does not request expansion of `year`, only this identifier is returned.
    /// </summary>
    [JsonPropertyName("yearId")]
    public Identifier? YearId { get; set; }

    /// <summary>
    ///     The expanded top-level academicSession year object for this session.
    ///     When the client requests expansion of `year`, the full expanded academicSession object
    ///     MUST be returned here instead of only the identifier. If no top-level year is defined, this value is `null`.
    /// </summary>
    [JsonPropertyName("year")]
    public AcademicSession? Year { get; set; }

    /// <summary>
    ///     An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    [JsonPropertyName("otherCodes")]
    public IdentifierEntry[]? OtherCodes { get; set; }

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
