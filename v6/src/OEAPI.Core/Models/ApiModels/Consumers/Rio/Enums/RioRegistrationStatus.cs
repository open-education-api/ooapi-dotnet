using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels.Consumers.Rio.Enums;

/// <summary>
///     cohortStatus - whether a given cohort of an offered programme is open for registration or closed.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RioRegistrationStatus
{
    [JsonStringEnumMemberName("open")] Open,

    [JsonStringEnumMemberName("closed")] Closed
}
