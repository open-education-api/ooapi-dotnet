using AutoFixture;
using FluentAssertions;
using ooapi.v5.Models;

namespace ooapi.v5.core.UnitTests.Models;

[TestFixture]
public class CostTests
{
    private readonly IFixture _fixture = new Fixture();

    [Test]
    public void GetDisplayAmount_ReturnsDisplayAmount()
    {
        // Arrange
        var language = "nl-NL";
        var value = "€ 1.000,00";
        string displayAmount = $"[{{\"language\": \"{language}\", \"value\": \"{value}\"}}]";
        var sut = _fixture.Build<Cost>()
            .With(x => x.DisplayAmount, displayAmount)
            .OmitAutoProperties()
            .Create();

        // Act
        var result = sut.displayAmount;

        // Assert
        result[0].Language.Should().Be(language);
        result[0].Value.Should().Be(value);
    }

    [Test]
    public void SetDisplayAmount_SetsDisplayAmount()
    {
        // Arrange
        var language = "nl-NL";
        var value = "€ 1.000,00";
        string ExpectedDisplayAmount = $"[{{\"language\":\"{language}\",\"value\":\"{value}\"}}]";
        var sut = _fixture.Build<Cost>()
            .OmitAutoProperties()
            .Create();

        var displayAmount = new List<LanguageTypedString>() { new LanguageTypedString() { Language = language, Value = value } };

        // Act
        sut.displayAmount = displayAmount;

        // Assert
        sut.DisplayAmount.Should().Be(ExpectedDisplayAmount);
    }
}