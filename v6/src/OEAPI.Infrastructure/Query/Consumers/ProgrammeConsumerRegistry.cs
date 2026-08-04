using System.Text.Json;
using OEAPI.Core.Models.ApiModels.Consumers.EduXchange;
using OEAPI.Core.Models.ApiModels.Consumers.Rio;

namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Known consumers with a typed schema for the <c>Programme</c> entity shape. See
///     <see cref="CourseConsumerRegistry" /> for how to extend this.
/// </summary>
public static class ProgrammeConsumerRegistry
{
    public static readonly IReadOnlyDictionary<string, Func<JsonElement, object>> Deserializers =
        new Dictionary<string, Func<JsonElement, object>>(StringComparer.OrdinalIgnoreCase)
        {
            ["rio"] = element => element.Deserialize<RioProgrammeConsumer>(JsonSerializerOptions.Web)!,
            ["eduxchange"] = element => element.Deserialize<EduXchangeAllianceConsumer>(JsonSerializerOptions.Web)!
        };
}
