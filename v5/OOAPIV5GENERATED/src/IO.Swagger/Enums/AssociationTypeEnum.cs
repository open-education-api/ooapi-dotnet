namespace IO.Swagger.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this association
/// </summary>
/// <value>The type of this association</value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum AssociationTypeEnum
{
    /// <summary>
    /// Enum ProgramOfferingAssociationEnum for programOfferingAssociation
    /// </summary>
    [EnumMember(Value = "programOfferingAssociation")]
    ProgramOfferingAssociationEnum = 0,
    /// <summary>
    /// Enum CourseOfferingAssociationEnum for courseOfferingAssociation
    /// </summary>
    [EnumMember(Value = "courseOfferingAssociation")]
    CourseOfferingAssociationEnum = 1,
    /// <summary>
    /// Enum ComponentOfferingAssociationEnum for componentOfferingAssociation
    /// </summary>
    [EnumMember(Value = "componentOfferingAssociation")]
    ComponentOfferingAssociationEnum = 2
}