using OEAPI.API.Routing;
using Xunit;

namespace OEAPI.API.Tests.Routing;

public class KebabCaseParameterTransformerTests
{
    private readonly KebabCaseParameterTransformer _transformer = new();

    [Theory]
    [InlineData("CourseOfferingAssociations", "course-offering-associations")]
    [InlineData("Organisations", "organisations")]
    [InlineData("TestComponentOfferingAssociationAttempts", "test-component-offering-association-attempts")]
    public void TransformOutbound_PascalCaseControllerName_ReturnsKebabCase(string input, string expected)
    {
        string? result = _transformer.TransformOutbound(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TransformOutbound_Null_ReturnsNull()
    {
        string? result = _transformer.TransformOutbound(null);

        Assert.Null(result);
    }
}
