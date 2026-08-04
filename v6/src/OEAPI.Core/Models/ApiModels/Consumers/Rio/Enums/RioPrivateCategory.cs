using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     A classification for programmes in non-formal education, used when describing an
///     EducationSpecification that maps to a ParticuliereOpleiding in RIO.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioPrivateCategory
{
    [JsonStringEnumMemberName("business_and_project_support")]
    BusinessAndProjectSupport,

    [JsonStringEnumMemberName("economy")] Economy,

    [JsonStringEnumMemberName("behaviour_and_society")]
    BehaviourAndSociety,

    [JsonStringEnumMemberName("healthcare_and_sport")]
    HealthcareAndSport,

    [JsonStringEnumMemberName("hobby_and_leisure_time")]
    HobbyAndLeisureTime,

    [JsonStringEnumMemberName("agriculture_food_and_natural_environment")]
    AgricultureFoodAndNaturalEnvironment,

    [JsonStringEnumMemberName("management_and_project_management")]
    ManagementAndProjectManagement,

    [JsonStringEnumMemberName("nature")] Nature,

    [JsonStringEnumMemberName("education")]
    Education,

    [JsonStringEnumMemberName("law")] Law,

    [JsonStringEnumMemberName("cross_sectoral")]
    CrossSectoral,

    [JsonStringEnumMemberName("language_and_culture")]
    LanguageAndCulture,

    [JsonStringEnumMemberName("technology_and_ict")]
    TechnologyAndIct,

    [JsonStringEnumMemberName("tourism_hospitality_and_recreation")]
    TourismHospitalityAndRecreation,

    [JsonStringEnumMemberName("transport_and_logistics")]
    TransportAndLogistics,

    [JsonStringEnumMemberName("security_and_defense")]
    SecurityAndDefense
}
