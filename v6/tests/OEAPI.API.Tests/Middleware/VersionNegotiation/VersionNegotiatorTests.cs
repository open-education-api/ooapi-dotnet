using OEAPI.API.Middleware.VersionNegotiation;
using OEAPI.Core.Models.ApiModels;
using Xunit;

namespace OEAPI.API.Tests.Middleware.VersionNegotiation;

public class VersionNegotiatorTests
{
    private readonly VersionNegotiator _negotiator = new();
    private readonly string[] _supportedOeapiVersions = ["6.0"];
    private readonly SupportedConsumer[] _supportedConsumers = [new() { ConsumerKey = "rio", Version = "1.0" }];

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("application/json")]
    [InlineData("*/*")]
    [InlineData("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8")]
    public void Negotiate_NoOeapiVersionRequested_AcceptsWithDefault(string? acceptHeader)
    {
        VersionNegotiationResult result =
            _negotiator.Negotiate(acceptHeader, _supportedOeapiVersions, _supportedConsumers);

        Assert.True(result.IsAccepted);
        Assert.Equal("6.0", result.ResolvedOeapiVersion);
        Assert.Null(result.ResolvedConsumer);
        Assert.False(result.WasExplicitlyRequested);
    }

    [Fact]
    public void Negotiate_ExactVersionMatch_Accepts()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=6.0", _supportedOeapiVersions, _supportedConsumers);

        Assert.True(result.IsAccepted);
        Assert.Equal("6.0", result.ResolvedOeapiVersion);
        Assert.True(result.WasExplicitlyRequested);
    }

    [Theory]
    [InlineData("6.1")]
    [InlineData("6.5")]
    public void Negotiate_CompatibleMinorFallback_Accepts(string requestedVersion)
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            $"application/vnd.oeapi+json;version={requestedVersion}", _supportedOeapiVersions, _supportedConsumers);

        Assert.True(result.IsAccepted);
        Assert.Equal("6.0", result.ResolvedOeapiVersion);
    }

    [Fact]
    public void Negotiate_IncompatibleMajorVersion_Rejects()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=7.0", _supportedOeapiVersions, _supportedConsumers);

        Assert.False(result.IsAccepted);
        Assert.Equal("7.0", result.RequestedVersion);
        Assert.Equal(["6.0"], result.SupportedVersions);
        Assert.Null(result.Consumer);
    }

    [Theory]
    [InlineData("not-a-version")]
    [InlineData("6")]
    [InlineData("6.0.1")]
    public void Negotiate_MalformedVersionParameter_Rejects(string malformedVersion)
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            $"application/vnd.oeapi+json;version={malformedVersion}", _supportedOeapiVersions, _supportedConsumers);

        Assert.False(result.IsAccepted);
        Assert.Equal(malformedVersion, result.RequestedVersion);
    }

    [Fact]
    public void Negotiate_KnownConsumerNoConsumerVersion_AcceptsWithConsumerResolved()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=6.0;consumer=rio", _supportedOeapiVersions, _supportedConsumers);

        Assert.True(result.IsAccepted);
        Assert.Equal("rio", result.ResolvedConsumer);
        Assert.Equal("1.0", result.ResolvedConsumerVersion);
        Assert.True(result.WasExplicitlyRequested);
    }

    [Fact]
    public void Negotiate_KnownConsumerCompatibleConsumerVersion_Accepts()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=6.0;consumer=rio;consumer-version=1.5",
            _supportedOeapiVersions, _supportedConsumers);

        Assert.True(result.IsAccepted);
        Assert.Equal("rio", result.ResolvedConsumer);
        Assert.Equal("1.0", result.ResolvedConsumerVersion);
        Assert.True(result.WasExplicitlyRequested);
    }

    [Fact]
    public void Negotiate_KnownConsumerIncompatibleConsumerVersion_RejectsWithConsumerPopulated()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=6.0;consumer=rio;consumer-version=2.0",
            _supportedOeapiVersions, _supportedConsumers);

        Assert.False(result.IsAccepted);
        Assert.Equal("2.0", result.RequestedVersion);
        Assert.Equal(["1.0"], result.SupportedVersions);
        Assert.NotNull(result.Consumer);
        Assert.Equal("rio", result.Consumer.ConsumerKey);
    }

    [Fact]
    public void Negotiate_UnknownConsumerKey_RejectsWithEmptySupportedVersions()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=6.0;consumer=unknown-consumer",
            _supportedOeapiVersions, _supportedConsumers);

        Assert.False(result.IsAccepted);
        Assert.NotNull(result.Consumer);
        Assert.Equal("unknown-consumer", result.Consumer.ConsumerKey);
        Assert.Empty(result.SupportedVersions);
    }

    [Fact]
    public void Negotiate_IncompatibleOeapiVersion_NeverEvaluatesConsumer()
    {
        VersionNegotiationResult result = _negotiator.Negotiate(
            "application/vnd.oeapi+json;version=7.0;consumer=unknown-consumer",
            _supportedOeapiVersions, _supportedConsumers);

        Assert.False(result.IsAccepted);
        Assert.Equal("7.0", result.RequestedVersion);
        Assert.Null(result.Consumer);
    }
}
