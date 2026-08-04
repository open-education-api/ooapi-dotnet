using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio;

/// <summary>
///     RIO add-on attributes to the offering of either a collection of courses or a course that
///     leads to a certifiable learning outcome. Shared by CourseOffering and ProgrammeOffering.
///     Matches source/consumers/RIO/V1/Offering.yaml.
/// </summary>
public class RioOfferingConsumer
{
    [JsonPropertyName("consumerKey")] public string ConsumerKey { get; set; } = "rio";

    /// <summary>toelichtingVereisteToestemming - textual explanation of the required registration permission.</summary>
    [JsonPropertyName("explanationRequiredPermission")]
    public string? ExplanationRequiredPermission { get; set; }

    /// <summary>
    ///     toestemmingVereistVoorAanmelding - whether registration for this cohort requires the education offerer's
    ///     permission.
    /// </summary>
    [JsonPropertyName("requiredPermissionRegistration")]
    public RioRequiredPermissionRegistration RequiredPermissionRegistration { get; set; }

    /// <summary>cohortStatus - whether this cohort is open or closed for registration.</summary>
    [JsonPropertyName("registrationStatus")]
    public RioRegistrationStatus RegistrationStatus { get; set; }

    /// <summary>Optional override of the main modeOfDelivery value, mapped one-on-one to opleidingsvorm in RIO.</summary>
    [JsonPropertyName("modeOfDelivery")]
    public RioModeOfDelivery[]? ModeOfDelivery { get; set; }
}
