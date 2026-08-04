using System.Text.Json;
using OEAPI.Core.Models.ApiModels.Consumers.EduXchange;

namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Known consumers with a typed schema for the <c>Person</c> entity shape. See
///     <see cref="CourseConsumerRegistry" /> for how to extend this.
/// </summary>
public static class PersonConsumerRegistry
{
    public static readonly IReadOnlyDictionary<string, Func<JsonElement, object>> Deserializers =
        new Dictionary<string, Func<JsonElement, object>>(StringComparer.OrdinalIgnoreCase)
        {
            ["eduxchange"] = element => element.Deserialize<EduXchangePersonConsumer>(JsonSerializerOptions.Web)!
        };
}
