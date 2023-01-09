namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ConsumerPropertyTypeEnum
{

    [EnumMember(Value = "string")]
    String = 0,

    [EnumMember(Value = "JArray")]
    JArray = 1,

    [EnumMember(Value = "JObject")]
    JObject = 2,

    [EnumMember(Value = "int")]
    Int = 3,

    [EnumMember(Value = "bool")]
    Bool = 4

}