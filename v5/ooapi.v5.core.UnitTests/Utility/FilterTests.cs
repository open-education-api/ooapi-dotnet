using AutoFixture;
using FluentAssertions;
using ooapi.v5.core.Utility;

namespace ooapi.v5.core.UnitTests.Utility;

[TestFixture(typeof(string))]
[TestFixture(typeof(int))]
[TestFixture(typeof(int?))]
[TestFixture(typeof(short))]
[TestFixture(typeof(short?))]
[TestFixture(typeof(bool))]
[TestFixture(typeof(bool?))]
[TestFixture(typeof(long))]
[TestFixture(typeof(long?))]
[TestFixture(typeof(double))]
[TestFixture(typeof(double?))]
[TestFixture(typeof(float))]
[TestFixture(typeof(float?))]
[TestFixture(typeof(DateTime))]
[TestFixture(typeof(DateTime?))]
[TestFixture(typeof(Guid))]
[TestFixture(typeof(Guid?))]
public class FilterTests<T>
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void TryParseToType_ReturnsCorrectType()
    {
        // Arrange
        var _objectInput = _fixture.Create<T>();

        // Act
        var result = Filter.TryParseToType(_objectInput!, typeof(T));

        // Assert
        var expectedType = _objectInput!.GetType().UnderlyingSystemType;
        result.Should().BeOfType(expectedType);
    }

    [Test]
    public void TryParseToType_ReturnsStringForUnknownType()
    {
        // Arrange
        var _objectInput = _fixture.Create<T>();

        //Act
        var result = Filter.TryParseToType(_objectInput!, typeof(ISomeOtherType));

        // Assert
        result.Should().BeOfType<string>();
    }

    private interface ISomeOtherType
    {

    }
}