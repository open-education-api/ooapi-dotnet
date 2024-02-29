using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

public sealed class RoomTests
{
    private readonly Fixture _fixture = new Fixture();

    [TestCase("TestType", "TestCode", true)]
    [TestCase("TestType", null, false)]
    [TestCase(null, "TestCode", false)]
    [TestCase(null, null, false)]
    public void GetPrimaryCodeIdentifier_ReturnsIdentifierEntryOrNull(string? primaryCodeType, string? primaryCode, bool resultIsNotNull)
    {
        // Arrange
        var room = _fixture.Build<Room>()
            .With(x => x.PrimaryCodeType, primaryCodeType)
            .With(x => x.PrimaryCode, primaryCode)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.PrimaryCodeIdentifier;

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
        var room = _fixture.Build<Room>()
            .OmitAutoProperties()
            .Create();

        var primaryCodeIdentifier = new IdentifierEntry() { CodeType = "TestType", Code = "TestCode" };

        // Act
        room.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        room.PrimaryCodeType.Should().Be(primaryCodeIdentifier.CodeType);
        room.PrimaryCode.Should().Be(primaryCodeIdentifier.Code);
    }

    [Test]
    public void SetPrimaryCodeIdentifier_WhenIdentifierEntryIsNull_DoesNotSetPrimaryCodeAndPrimaryCodeType()
    {
        // Arrange
        var codeType = "TestType";
        var code = "TestCode";
        var room = _fixture.Build<Room>()
            .With(x => x.PrimaryCodeType, codeType)
            .With(x => x.PrimaryCode, code)
            .OmitAutoProperties()
            .Create();

        IdentifierEntry? primaryCodeIdentifier = null;

        // Act
        room.PrimaryCodeIdentifier = primaryCodeIdentifier;

        // Assert
        room.PrimaryCodeType.Should().Be(codeType);
        room.PrimaryCode.Should().Be(code);
    }

    [Test]
    public void GetName_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var room = _fixture.Build<Room>()
            .With(x => x.Name, JsonConvert.SerializeObject(new List<object> { new { language = "en", value = "TestName" } }))
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.name;

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
        var room = _fixture.Build<Room>()
            .With(x => x.Name, JsonConvert.SerializeObject(new List<object>()))
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.name;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [Test]
    public void GetDescription_WhenAttributesExist_ReturnsListLanguageTypedString()
    {
        // Arrange
        var room = _fixture.Build<Room>()
            .With(x => x.Description, JsonConvert.SerializeObject(new List<object> { new { language = "en", value = "TestName" } }))
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(1);
        result[0].Language.Should().Be("en");
        result[0].Value.Should().Be("TestName");
    }

    [Test]
    public void GetDescription_WhenAttributesAreEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var room = _fixture.Build<Room>()
            .With(x => x.Description, JsonConvert.SerializeObject(new List<object>()))
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.description;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(0);
    }

    [TestCase(1234, 1234, true)]
    [TestCase(null, 1234, false)]
    [TestCase(1234, null, false)]
    [TestCase(null, null, false)]
    public void GetGeolocation_ReturnsGeolocation(decimal? latitude, decimal? longitude, bool isNotNull)
    {
        // Arrange
        var room = _fixture.Build<Room>()
            .With(x => x.Latitude, latitude)
            .With(x => x.Longitude, longitude)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.Geolocation;

        // Assert
        if (isNotNull)
        {
            result.Should().NotBeNull();
            result!.Latitude.Should().Be(latitude);
            result.Longitude.Should().Be(longitude);
        }
        else
        {
            result.Should().BeNull();
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public void SetGeolocation_SetsGeolocation(bool isNotNull)
    {
        // Arrange
        Geolocation? geolocation = null;
        if (isNotNull)
        {
            geolocation = _fixture.Create<Geolocation>();
        }

        var room = _fixture.Build<Room>()
            .OmitAutoProperties()
            .Create();

        // Act
        room.Geolocation = geolocation;

        // Assert
        if (isNotNull)
        {
            room.Latitude.Should().Be(geolocation!.Latitude);
            room.Longitude.Should().Be(geolocation.Longitude);
        }
        else
        {
            room.Latitude.Should().BeNull();
            room.Latitude.Should().BeNull();
        }
    }

    [Test]
    public void ConsumersList_ReturnsListOfJObjects()
    {
        // arrange
        var consumers = _fixture.Build<Consumer>().With(x => x.PropertyType, ConsumerPropertyType.String).CreateMany().ToList();
        var service = _fixture.Build<Room>().With(x => x.Consumers, consumers).OmitAutoProperties().Create();
        service.Consumers.Should().NotBeEmpty();

        // act
        var result = service.ConsumersList;

        // assert
        result.Should().NotBeNullOrEmpty().And.HaveCount(service.Consumers.Count);
        result.Should().ContainItemsAssignableTo<JObject>();
    }

    [Test]
    public void ConsumersList_NoConsumers_ReturnsEmptyList()
    {
        // arrange
        var service = _fixture.Build<Room>().With(x => x.Consumers, new List<Consumer>()).OmitAutoProperties().Create();
        service.Consumers.Should().BeEmpty();

        // act
        var result = service.ConsumersList;

        // assert
        result.Should().BeEmpty();
    }

    [Test]
    public void GetOneOfBuilding_ReturnsOneOfBuilding()
    {
        // Arrange
        var buildingId = _fixture.Create<Guid>();
        var building = _fixture.Build<Building>()
            .With(x => x.BuildingId, buildingId)
            .OmitAutoProperties()
            .Create();
        var room = _fixture.Build<Room>()
            .With(x => x.BuildingId, buildingId)
            .With(x => x.Building, building)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.OneOfBuilding;

        // Assert
        result.Should().NotBeNull();
        ((OneOfBuildingInstance?)result)?.Building?.BuildingId.Should().Be(buildingId);
        ((OneOfBuildingInstance?)result)?.Id.Should().Be(buildingId);
    }

    [Test]
    public void GetOneOfBuilding_WhenBuildingIdIsNull_ReturnsNull()
    {
        // Arrange
        Guid? buildingId = null;
        var building = _fixture.Build<Building>()
            .With(x => x.BuildingId, buildingId)
            .OmitAutoProperties()
            .Create();
        var room = _fixture.Build<Room>()
            .With(x => x.BuildingId, buildingId)
            .With(x => x.Building, building)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = room.OneOfBuilding;

        // Assert
        result.Should().BeNull();
    }
}