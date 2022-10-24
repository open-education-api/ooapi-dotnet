namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The state of this result
/// </summary>
/// <value>The state of this result</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ResultStateEnum
{
    /// <summary>
    /// Enum InProgressEnum for in progress
    /// </summary>
    [EnumMember(Value = "in progress")]
    InProgressEnum = 0,
    /// <summary>
    /// Enum PostponedEnum for postponed
    /// </summary>
    [EnumMember(Value = "postponed")]
    PostponedEnum = 1,
    /// <summary>
    /// Enum CompletedEnum for completed
    /// </summary>
    [EnumMember(Value = "completed")]
    CompletedEnum = 2,
    /// <summary>
    /// Enum QueuedEnum for queued
    /// </summary>
    [EnumMember(Value = "queued")]
    QueuedEnum = 3
}