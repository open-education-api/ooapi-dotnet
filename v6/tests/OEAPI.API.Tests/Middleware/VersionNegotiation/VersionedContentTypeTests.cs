using OEAPI.API.Middleware.VersionNegotiation;
using Xunit;

namespace OEAPI.API.Tests.Middleware.VersionNegotiation;

public class VersionedContentTypeTests
{
    [Theory]
    [InlineData("application/json", true)]
    [InlineData("application/json; charset=utf-8", true)]
    [InlineData("APPLICATION/JSON", true)]
    [InlineData("application/problem+json", false)]
    [InlineData("application/octet-stream", false)]
    [InlineData("application/vnd.oeapi+json;version=6.0", false)]
    [InlineData(null, false)]
    public void IsPlainJson_ReturnsExpected(string? contentType, bool expected)
    {
        Assert.Equal(expected, VersionedContentType.IsPlainJson(contentType));
    }

    [Fact]
    public void Build_NoConsumer_ProducesVersionedMediaTypeOnly()
    {
        string result = VersionedContentType.Build("application/json", "6.0", null, null);

        Assert.Equal("application/vnd.oeapi+json; version=6.0", result);
    }

    [Fact]
    public void Build_WithConsumer_IncludesConsumerAndConsumerVersion()
    {
        string result = VersionedContentType.Build("application/json", "6.0", "rio", "1.0");

        Assert.Equal("application/vnd.oeapi+json; version=6.0; consumer=rio; consumer-version=1.0", result);
    }

    [Fact]
    public void Build_OriginalHadCharset_PreservesIt()
    {
        string result = VersionedContentType.Build("application/json; charset=utf-8", "6.0", null, null);

        Assert.Contains("charset=utf-8", result);
        Assert.StartsWith("application/vnd.oeapi+json", result);
    }

    [Fact]
    public void Build_OriginalHadNoCharset_DoesNotAddOne()
    {
        string result = VersionedContentType.Build("application/json", "6.0", null, null);

        Assert.DoesNotContain("charset", result);
    }
}
