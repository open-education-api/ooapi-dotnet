using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The state of this result
/// </summary>
/// <value>The state of this result</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum Pass
{
    /// <summary>
    /// Enum UnknownEnum for unknown
    /// </summary>
    [EnumMember(Value = "unknown")]
    unknown = 0,
    /// <summary>
    /// Enum PassedEnum for passed
    /// </summary>
    [EnumMember(Value = "passed")]
    passed = 1,
    /// <summary>
    /// Enum FailedEnum for failed
    /// </summary>
    [EnumMember(Value = "failed")]
    failed = 2
}
