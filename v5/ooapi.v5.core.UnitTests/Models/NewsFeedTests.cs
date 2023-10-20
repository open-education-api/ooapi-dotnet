using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;


namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class NewsFeedTests
{
    private readonly IFixture _fixture = new Fixture();
    
    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "name", Language = "en", Value = "TestName" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsFeed.name;

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
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsFeed.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }
    
    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.Attributes, new List<Attribute>() { new() { PropertyName = "description", Language = "en", Value = "TestDescription" } })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsFeed.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestDescription");
    }

    [Test]
    public void GetDescription_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.Attributes, new List<Attribute>() { })
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsFeed.description;

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
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.Consumers, consumers)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsFeed.ConsumersList;

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
    }

    [Test]
    public void GetConsumersList_WhenConsumersEmpty_ReturnsEmptyListJObject()
    {
        // Arrange
        var newsFeed = _fixture.Build<NewsFeed>()
            .With(x => x.Consumers, new List<Consumer>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = newsFeed.ConsumersList;

        // Assert
        result.Should().BeEmpty();
    }
}