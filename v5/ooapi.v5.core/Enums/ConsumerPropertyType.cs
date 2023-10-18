namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ConsumerPropertyType
{
    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "String")]
    String = 0,

    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "JArray")]
    JArray = 1,

    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "JObject")]
    JObject = 2,

    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "Int")]
    Int = 3,

    /// <summary>
    /// 
    /// </summary>
    [EnumMember(Value = "Bool")]
    Bool = 4

}