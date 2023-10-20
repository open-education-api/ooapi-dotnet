using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using ooapi.v5.Helpers;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Helpers;

public sealed class LanguageTypedStringJsonConverterTests
{
    private readonly IFixture _fixture = new Fixture();

    [TestCase("")]
    [TestCase(null)]
    public void GetLanguageTypesStringList_EmptyList_ReturnsEmptyList(string? languages)
    {
        // act
        var result = LanguageTypedStringJsonConverter.GetLanguageTypesStringList(languages);

        // assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetLanguageTypesStringList_InvalidArray_ReturnsEmptyList()
    {
        // arrange
        var languages = new List<string> { "test" };
        var json = JsonConvert.SerializeObject(languages);

        // act
        var result = LanguageTypedStringJsonConverter.GetLanguageTypesStringList(json);

        // assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetLanguageTypesStringList_ValidLanguages_ReturnsList()
    {
        // arrange
        var languages = new List<LanguageTypedString>
                            {
                                new () { Language = "en-GB", Value = _fixture.Create<string>() },
                                new () { Language = "fr-BE", Value = _fixture.Create<string>() },
                                new () { Language = "nl-NL", Value = _fixture.Create<string>() },
                            };
        var json = JsonConvert.SerializeObject(languages);

        // act
        var result = LanguageTypedStringJsonConverter.GetLanguageTypesStringList(json);

        // assert
        result.Should().NotBeNullOrEmpty();
        result.Count.Should().Be(languages.Count);
        result.Should().BeEquivalentTo(languages);
    }
}