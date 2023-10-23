using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class PostResponseTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetMessage_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var postResponse = _fixture.Build<PostResponse>()
            .With(x => x.Message, JsonConvert.SerializeObject(new List<object> { new { language = "en", value = "TestName" } }))
            .OmitAutoProperties()
            .Create();

        // Act
        var result = postResponse.message;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetMessage_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var postResponse = _fixture.Build<PostResponse>()
            .With(x => x.Message, JsonConvert.SerializeObject(new List<object>()))
            .OmitAutoProperties()
            .Create();

        // Act
        var result = postResponse.message;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }
}