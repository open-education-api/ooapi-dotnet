using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using ooapi.v5.Enums;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class PersonTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var model = _fixture.Build<Person>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = model.PrimaryCodeIdentifier;

        // Assert
        if (resultIsNotNull)
        {
            result.Should().BeOfType<IdentifierEntry>();
            result!.CodeType.Should().Be(primaryCodeType);
            result.Code.Should().Be(primaryCode);
        }
        else
        {
            result.Should().BeNull();
        }
    }

    [Test]
    public void SetPrimaryCodeIdentifier_SetsPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var model = _fixture.Build<Person>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        model.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        model.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        model.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var model = _fixture.Build<Person>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        model.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        model.PrimaryCodeType.Should().Be(codeType);
        model.PrimaryCode.Should().Be(code);
    }

    [Test]
    public void GetAffs_GivenNonExistingAffiliations_ReturnsEmptyList()
    {
        // arrange
        var person = new Person { Affiliations = string.Join(",", _fixture.Build<string>().CreateMany()), };

        // act
        var result = person.Affs;

        // assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetAffs_GivenAffiliations_ReturnsList()
    {
        // arrange
        var person = new Person { Affiliations = "student,employee,guest", };

        // act
        var result = person.Affs;

        // assert
        result.Should().NotBeEmpty();
        result.Should().Contain(Affiliations.student);
        result.Should().Contain(Affiliations.employee);
        result.Should().Contain(Affiliations.guest);
    }

    [Test]
    public void ConsumersList_ReturnsListOfJObjects()
    {
        // arrange
        var consumers = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.String).CreateMany().ToList();
        var model = _fixture.Build<Person>().With(x => x.Consumers, consumers).OmitAutoProperties().Create();
        model.Consumers.Should().NotBeEmpty();

        // act
        var result = model.ConsumersList;

        // assert
        result.Should().NotBeNullOrEmpty().And.HaveCount(model.Consumers.Count);
        result.Should().ContainItemsAssignableTo<JObject>();
    }

    [Test]
    public void ConsumersList_NoConsumers_ReturnsEmptyList()
    {
        // arrange
        var model = _fixture.Build<Person>().With(x => x.Consumers, new List<Consumer>()).OmitAutoProperties().Create();
        model.Consumers.Should().BeEmpty();

        // act
        var result = model.ConsumersList;

        // assert
        result.Should().BeEmpty();
    }
}