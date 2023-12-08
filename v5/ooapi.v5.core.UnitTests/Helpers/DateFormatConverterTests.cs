using AutoFixture;
using FluentAssertions;
using ooapi.v5.Helpers;

namespace ooapi.v5.core.UnitTests.Helpers;

public sealed class DateFormatConverterTests
{
    private readonly Fixture _fixture = new Fixture();

    [Test]
    public void DateFormatConverter_Constructor_ShouldHaveFormat()
    {
        // arrange
        var format = _fixture.Create<string>();

        // act
        var result = new DateFormatConverter(format);

        // assert
        result.DateTimeFormat.Should().Be(format);
    }
}