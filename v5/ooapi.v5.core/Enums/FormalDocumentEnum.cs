namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES 
/// </summary>
/// <value>The type of formal document obtained after completion of this education   - diploma: DIPLOMA   - certificate: CERTIFICAAT   - no official document: GEEN OFFICIEEL DOCUMENT   - testimonial: GETUIGSCHRIFT   - school advice: SCHOOLADVIES </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum FormalDocumentEnum
{
    /// <summary>
    /// Enum DiplomaEnum for diploma
    /// </summary>
    [EnumMember(Value = "diploma")]
    diploma = 0,
    /// <summary>
    /// Enum CertificateEnum for certificate
    /// </summary>
    [EnumMember(Value = "certificate")]
    certificate = 1,
    /// <summary>
    /// Enum NoOfficialDocumentEnum for no official document
    /// </summary>
    [EnumMember(Value = "no official document")]
    no_official_document = 2,
    /// <summary>
    /// Enum TestimonialEnum for testimonial
    /// </summary>
    [EnumMember(Value = "testimonial")]
    testimonial = 3,
    /// <summary>
    /// Enum SchoolAdviceEnum for school advice
    /// </summary>
    [EnumMember(Value = "school advice")]
    school_advice = 4
}