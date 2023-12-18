using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;
using Attribute = ooapi.v5.Models.Attribute;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class AddressTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void GetAddition_ReturnsListLanguageTypedString()
    {
        // Arrange
        var attributes = _fixture.Build<LanguageTypedProperty>()
            .With(x => x.PropertyName, "additional")
            .CreateMany(3)
            .ToList();
        var academicSession = _fixture.Build<Address>()
            .With(x => x.Attributes, attributes)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.addition;

        // Assert
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().HaveCount(3);
        result?[0].Language.Should().Be(attributes[0].Language);
        result?[0].Value.Should().Be(attributes[0].Value);
    }

    [Test]
    public void GetAddition_WhenAttributesEmpty_ReturnsEmptyListLanguageTypedString()
    {
        // Arrange
        var academicSession = _fixture.Build<Address>()
            .With(x => x.Attributes, new List<LanguageTypedProperty>())
            .OmitAutoProperties()
            .Create();

        // Act
        var result = academicSession.addition;

        // Assert
        result.Should().BeOfType<List<LanguageTypedString>>();
        result.Should().BeEmpty();
    }

    [TestCase(1234, 1234, true)]
    [TestCase(null, 1234, false)]
    [TestCase(1234, null, false)]
    [TestCase(null, null, false)]
    public void GetGeolocation_ReturnsGeolocation(decimal? latitude, decimal? longitude, bool isNotNull)
    {
        // Arrange
        var address = _fixture.Build<Address>()
            .With(x => x.Latitude, latitude)
            .With(x => x.Longitude, longitude)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = address.Geolocation;

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

        var address = _fixture.Build<Address>()
            .OmitAutoProperties()
            .Create();

        // Act
        address.Geolocation = geolocation;

        // Assert
        if (isNotNull)
        {
            address.Latitude.Should().Be(geolocation!.Latitude);
            address.Longitude.Should().Be(geolocation.Longitude);
        }
        else
        {
            address.Latitude.Should().BeNull();
            address.Latitude.Should().BeNull();
        }
    }
}