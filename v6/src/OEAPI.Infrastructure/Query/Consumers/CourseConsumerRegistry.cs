using System.Text.Json;
using OEAPI.Core.Models.ApiModels.Consumers.EduXchange;
using OEAPI.Core.Models.ApiModels.Consumers.Rio;

namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Known consumers with a typed schema for the <c>Course</c> entity shape. Add an entry here to
///     give a new consumer's course data a real type instead of the generic passthrough object -
///     nothing else needs to change, every mapping call site already goes through this registry.
/// </summary>
public static class CourseConsumerRegistry
{
    public static readonly IReadOnlyDictionary<string, Func<JsonElement, object>> Deserializers =
        new Dictionary<string, Func<JsonElement, object>>(StringComparer.OrdinalIgnoreCase)
        {
            ["rio"] = element => element.Deserialize<RioCourseConsumer>(JsonSerializerOptions.Web)!,
            ["eduxchange"] = element => element.Deserialize<EduXchangeAllianceConsumer>(JsonSerializerOptions.Web)!
        };
}
