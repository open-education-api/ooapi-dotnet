using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;


namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class NewsItemTests
{
    private readonly Fixture _fixture = new Fixture();
    
    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsItem.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetName_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsItem.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetContent_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "content", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsItem.content;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetContent_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsItem.content;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }
    
    [Test]
    public void GetConsumersList_ReturnsListJObject()
    {
        // Arrange
        var consumers = _fixture.Build<Consumer>()
            .CreateMany(1)
            .ToList();
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsItem.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var newsItem = _fixture.Build<NewsItem>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsItem.ConsumersList;

        // Assert
        result.Should().BeEmpty();
    }
}