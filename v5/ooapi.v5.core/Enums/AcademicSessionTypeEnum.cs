namespace ooapi.v5.Enums;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;



/// <summary>
/// The type of this Acaddemic Session
/// </summary>
/// <value>The type of this Acaddemic Session This is an extensible enumeration.</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AcademicSessionTypeEnum
{
    /// <summary>
    /// academic year
    /// </summary>
    [EnumMember(Value = "academic year")]
    academic_year = 0,
    /// <summary>
    /// semester, typically there are two semesters per academic year
    /// </summary>
    [EnumMember(Value = "semester")]
    semester = 1,
    /// <summary>
    /// trimester, typically there are three semesters per academic year
    /// </summary>
    [EnumMember(Value = "trimester")]
    trimester = 2,
    /// <summary>
    /// quarter, typically there are four quarters per academic year
    /// </summary>,
    [EnumMember(Value = "quarter")]
    quarter = 3,
    /// <summary>
    /// a period in which tests take place
    /// </summary>
    [EnumMember(Value = "testing period")]
    testing_period = 4,
    /// <summary>
    /// any other period in an academic year
    /// </summary>
    [EnumMember(Value = "period")]
    period = 5


}


