namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ConsumerPropertyType
{

    [EnumMember(Value = "String")]
    String = 0,


    [EnumMember(Value = "JArray")]
    JArray = 1,


    [EnumMember(Value = "JObject")]
    JObject = 2,


    [EnumMember(Value = "Int")]
    Int = 3,


    [EnumMember(Value = "Bool")]
    Bool = 4

}