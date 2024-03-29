﻿using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The gender of this person
/// </summary>
/// <value>The gender of this person</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum Gender
{
    /// <summary>
    /// Enum MEnum for M
    /// </summary>
    [EnumMember(Value = "M")]
    M = 0,
    /// <summary>
    /// Enum FEnum for F
    /// </summary>
    [EnumMember(Value = "F")]
    F = 1,
    /// <summary>
    /// Enum UEnum for U
    /// </summary>
    [EnumMember(Value = "U")]
    U = 2,
    /// <summary>
    /// Enum XEnum for X
    /// </summary>
    [EnumMember(Value = "X")]
    X = 3
}