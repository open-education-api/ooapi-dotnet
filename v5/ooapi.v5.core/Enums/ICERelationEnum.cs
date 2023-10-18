﻿namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// Type of relation between person and In Case of Emergency contact
/// </summary>
/// <value>Type of relation between person and In Case of Emergency contact</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ICERelationEnum
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