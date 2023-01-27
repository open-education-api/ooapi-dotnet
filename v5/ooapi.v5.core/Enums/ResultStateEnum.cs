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
    in_progress = 0,
    /// <summary>
    /// Enum PostponedEnum for postponed
    /// </summary>
    [EnumMember(Value = "postponed")]
    postponed = 1,
    /// <summary>
    /// Enum CompletedEnum for completed
    /// </summary>
    [EnumMember(Value = "completed")]
    completed = 2,
    /// <summary>
    /// Enum QueuedEnum for queued
    /// </summary>
    [EnumMember(Value = "queued")]
    queued = 3
}