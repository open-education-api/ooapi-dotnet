using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The state of this association
/// </summary>
/// <value>The state of this association</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AssociationState
{
    /// <summary>
    /// Enum PendingEnum for pending
    /// </summary>
    [EnumMember(Value = "pending")]
    pending = 0,
    /// <summary>
    /// Enum CanceledEnum for canceled
    /// </summary>
    [EnumMember(Value = "canceled")]
    canceled = 1,
    /// <summary>
    /// Enum DeniedEnum for denied
    /// </summary>
    [EnumMember(Value = "denied")]
    denied = 2,
    /// <summary>
    /// Enum AssociatedEnum for associated
    /// </summary>
    [EnumMember(Value = "associated")]
    associated = 3,
    /// <summary>
    /// Enum QueuedEnum for queued
    /// </summary>
    [EnumMember(Value = "queued")]
    queued = 4
}