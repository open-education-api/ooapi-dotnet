using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// Type of relation between person and In Case of Emergency contact
/// </summary>
/// <value>Type of relation between person and In Case of Emergency contact</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
[SuppressMessage("Sonarcloud", "S2342", Justification = "Naming according to specification")]
public enum ICERelation
{
    /// <summary>
    /// Enum PartnerEnum for partner
    /// </summary>
    [EnumMember(Value = "partner")]
    partner = 0,
    /// <summary>
    /// Enum ParentEnum for parent
    /// </summary>
    [EnumMember(Value = "parent")]
    parent = 1,
    /// <summary>
    /// Enum OtherEnum for other
    /// </summary>
    [EnumMember(Value = "other")]
    other = 2
}