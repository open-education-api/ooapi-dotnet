﻿using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// Indicates whether the education is full-time, part-time, dual or self-paced.   - full-time: fulltime   - part-time: parttime   - dual training: duaal   - self-paced: eigen tempo 
/// </summary>
/// <value>Indicates whether the education is full-time, part-time, dual or self-paced.   - full-time: fulltime   - part-time: parttime   - dual training: duaal   - self-paced: eigen tempo </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ModeOfStudy
{
    /// <summary>
    /// Enum FullTimeEnum for full-time
    /// </summary>
    [EnumMember(Value = "full-time")]
    full_time = 0,
    /// <summary>
    /// Enum PartTimeEnum for part-time
    /// </summary>
    [EnumMember(Value = "part-time")]
    part_time = 1,
    /// <summary>
    /// Enum DualTrainingEnum for dual training
    /// </summary>
    [EnumMember(Value = "dual training")]
    dual_training = 2,
    /// <summary>
    /// Enum SelfPacedEnum for self-paced
    /// </summary>
    [EnumMember(Value = "self-paced")]
    self_paced = 3
}