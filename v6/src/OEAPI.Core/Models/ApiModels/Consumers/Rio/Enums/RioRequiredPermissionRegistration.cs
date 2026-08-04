using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     toestemmingVereistVoorAanmelding - whether a prospective student needs permission from the
///     education offerer to register for a given cohort of an offered programme.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioRequiredPermissionRegistration
{
    [JsonStringEnumMemberName("yes")] Yes,

    [JsonStringEnumMemberName("no")] No
}
