namespace IO.Swagger.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The state of this association
/// </summary>
/// <value>The state of this association</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum StateEnum
{
    /// <summary>
    /// Enum PendingEnum for pending
    /// </summary>
    [EnumMember(Value = "pending")]
    PendingEnum = 0,
    /// <summary>
    /// Enum CanceledEnum for canceled
    /// </summary>
    [EnumMember(Value = "canceled")]
    CanceledEnum = 1,
    /// <summary>
    /// Enum DeniedEnum for denied
    /// </summary>
    [EnumMember(Value = "denied")]
    DeniedEnum = 2,
    /// <summary>
    /// Enum AssociatedEnum for associated
    /// </summary>
    [EnumMember(Value = "associated")]
    AssociatedEnum = 3,
    /// <summary>
    /// Enum QueuedEnum for queued
    /// </summary>
    [EnumMember(Value = "queued")]
    QueuedEnum = 4
}