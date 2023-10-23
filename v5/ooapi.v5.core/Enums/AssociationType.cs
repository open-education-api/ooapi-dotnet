using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The type of this association
/// </summary>
/// <value>The type of this association</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AssociationType
{
    /// <summary>
    /// Enum ProgramOfferingAssociationEnum for programOfferingAssociation
    /// </summary>
    [EnumMember(Value = "programOfferingAssociation")]
    programOfferingAssociation = 0,
    /// <summary>
    /// Enum CourseOfferingAssociationEnum for courseOfferingAssociation
    /// </summary>
    [EnumMember(Value = "courseOfferingAssociation")]
    courseOfferingAssociation = 1,
    /// <summary>
    /// Enum ComponentOfferingAssociationEnum for componentOfferingAssociation
    /// </summary>
    [EnumMember(Value = "componentOfferingAssociation")]
    componentOfferingAssociation = 2
}