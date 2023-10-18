namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;


/// <summary>
/// Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place 
/// </summary>
/// <value>Address type - postal: post - visit: bezoek - deliveries: bezorg - billing: factuur - teaching: the address where education takes place </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AddressType
{
    /// <summary>
    /// Enum PostalEnum for postal
    /// </summary>
    [EnumMember(Value = "postal")]
    postal = 0,
    /// <summary>
    /// Enum VisitEnum for visit
    /// </summary>
    [EnumMember(Value = "visit")]
    visit = 1,
    /// <summary>
    /// Enum DeliveriesEnum for deliveries
    /// </summary>
    [EnumMember(Value = "deliveries")]
    deliveries = 2,
    /// <summary>
    /// Enum BillingEnum for billing
    /// </summary>
    [EnumMember(Value = "billing")]
    billing = 3,
    /// <summary>
    /// Enum TeachingEnum for teaching
    /// </summary>
    [EnumMember(Value = "teaching")]
    teaching = 4
}