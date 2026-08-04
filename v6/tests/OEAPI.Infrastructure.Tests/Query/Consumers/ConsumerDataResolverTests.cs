using System.Text.Json;
using OEAPI.Infrastructure.Query.Consumers;
using Xunit;

namespace OEAPI.Infrastructure.Tests.Query.Consumers;

public class ConsumerDataResolverTests
{
    private const string RioBlob = """{"consumerKey":"rio","educationOffererCode":"12345"}""";

    [Fact]
    public void Resolve_NoRequestedKey_ReturnsNull()
    {
        Assert.Null(ConsumerDataResolver.Resolve(RioBlob, null));
    }

    [Fact]
    public void Resolve_NoStoredData_ReturnsNull()
    {
        Assert.Null(ConsumerDataResolver.Resolve(null, "rio"));
    }

    [Fact]
    public void Resolve_StoredKeyDoesNotMatchRequestedKey_ReturnsNull()
    {
        Assert.Null(ConsumerDataResolver.Resolve(RioBlob, "eduxchange"));
    }

    [Fact]
    public void Resolve_StoredKeyMatchesRequestedKey_ReturnsGenericPassthroughWhenNoTypedRegistry()
    {
        object? result = ConsumerDataResolver.Resolve(RioBlob, "rio");

        Assert.NotNull(result);
    }

    [Fact]
    public void Resolve_KeyMatchIsCaseInsensitive()
    {
        Assert.NotNull(ConsumerDataResolver.Resolve(RioBlob, "RIO"));
    }

    [Fact]
    public void Resolve_MalformedJson_ReturnsNullNotThrows()
    {
        Assert.Null(ConsumerDataResolver.Resolve("{not valid json", "rio"));
    }

    [Fact]
    public void Resolve_MissingConsumerKeyProperty_ReturnsNull()
    {
        Assert.Null(ConsumerDataResolver.Resolve("""{"educationOffererCode":"12345"}""", "rio"));
    }

    [Fact]
    public void Resolve_TypedRegistryHasMatchingDeserializer_ReturnsTypedResult()
    {
        Dictionary<string, Func<JsonElement, object>> registry = new()
        {
            ["rio"] = element => new { Code = element.GetProperty("educationOffererCode").GetString() }
        };

        object? result = ConsumerDataResolver.Resolve(RioBlob, "rio", registry);

        Assert.NotNull(result);
        Assert.Equal("12345", result.GetType().GetProperty("Code")!.GetValue(result));
    }

    [Fact]
    public void Resolve_TypedDeserializerThrows_FallsBackToGenericPassthrough()
    {
        Dictionary<string, Func<JsonElement, object>> registry = new()
        {
            ["rio"] = _ => throw new JsonException("simulated schema mismatch")
        };

        object? result = ConsumerDataResolver.Resolve(RioBlob, "rio", registry);

        Assert.NotNull(result); // still resolved via the generic passthrough, not null/thrown
    }
}
