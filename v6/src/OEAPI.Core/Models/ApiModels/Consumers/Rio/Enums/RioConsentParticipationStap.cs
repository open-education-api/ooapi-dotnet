using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     toestemmingDeelnameSTAP - whether an offered programme is available under the STAP scheme
///     and appears in the Scholingsregister.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioConsentParticipationStap
{
    [JsonStringEnumMemberName("permission_granted")]
    PermissionGranted,

    [JsonStringEnumMemberName("permission_not_granted")]
    PermissionNotGranted
}
