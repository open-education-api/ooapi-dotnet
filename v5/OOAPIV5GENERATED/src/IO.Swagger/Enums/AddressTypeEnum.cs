namespace IO.Swagger.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


/// <summary>
/// Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place 
/// </summary>
/// <value>Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AddressTypeEnum
{
    /// <summary>
    /// Enum PostalEnum for postal
    /// </summary>
    [EnumMember(Value = "postal")]
    PostalEnum = 0,
    /// <summary>
    /// Enum VisitEnum for visit
    /// </summary>
    [EnumMember(Value = "visit")]
    VisitEnum = 1,
    /// <summary>
    /// Enum DeliveriesEnum for deliveries
    /// </summary>
    [EnumMember(Value = "deliveries")]
    DeliveriesEnum = 2,
    /// <summary>
    /// Enum BillingEnum for billing
    /// </summary>
    [EnumMember(Value = "billing")]
    BillingEnum = 3,
    /// <summary>
    /// Enum TeachingEnum for teaching
    /// </summary>
    [EnumMember(Value = "teaching")]
    TeachingEnum = 4
}