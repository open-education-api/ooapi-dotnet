using FluentAssertions;
using ooapi.v5.core.Utility;

namespace ooapi.v5.core.UnitTests.Utility;

public class FilterHelperTests
{
    [Test]
    public void ToFilterNodeCollection_ReturnsFilterNodes()
    {
        // Arrange
        string filter = "field1 eq 2 and field2 eq 'somestring' and noneFilterableField eq null";

        // Act
        var filterNodes = FilterHelper.ToFilterNodeCollection(filter);

        // Assert
        filterNodes.Should().NotBeEmpty();
        filterNodes.Count().Should().Be(3);
    }

    [Test]
    public void ToAllowedFilterExpression_ReturnsAllowedFilterExpression()
    {
        // Arrange
        string expression = "field1 eq 2 and field2 eq 'somestring' and noneFilterableField eq null";

        // Act
        var allowedExpression = FilterHelper.ToAllowedFilterExpression(expression, new[] { "noneFilterableField" });

        // Assert
        allowedExpression.Should().Be("field1 eq 2 and field2 eq 'somestring'");
    }
}
