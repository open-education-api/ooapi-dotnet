using System.Text.Json;
using OEAPI.Core.Models.ApiModels.Consumers.Rio;

namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Known consumers with a typed schema for the offering entity shape (shared by
///     <c>CourseOffering</c> and <c>ProgrammeOffering</c> - RIO's <c>Offering.yaml</c> explicitly
///     covers both). See <see cref="CourseConsumerRegistry" /> for how to extend this.
/// </summary>
public static class OfferingConsumerRegistry
{
    public static readonly IReadOnlyDictionary<string, Func<JsonElement, object>> Deserializers =
        new Dictionary<string, Func<JsonElement, object>>(StringComparer.OrdinalIgnoreCase)
        {
            ["rio"] = element => element.Deserialize<RioOfferingConsumer>(JsonSerializerOptions.Web)!
        };
}
