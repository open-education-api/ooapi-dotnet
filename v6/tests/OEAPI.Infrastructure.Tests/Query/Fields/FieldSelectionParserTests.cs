using OEAPI.Infrastructure.Query.Fields;
using Xunit;

namespace OEAPI.Infrastructure.Tests.Query.Fields;

public class FieldSelectionParserTests
{
    [Fact]
    public void Parse_NestedSelection_BuildsMatchingTree()
    {
        FieldSelection? selection = FieldSelectionParser.Parse("(id,title,programme(code),campus(city))");

        Assert.NotNull(selection);
        Assert.True(selection.Children.ContainsKey("id"));
        Assert.True(selection.Children.ContainsKey("title"));
        Assert.True(selection.Children["programme"].Children.ContainsKey("code"));
        Assert.True(selection.Children["campus"].Children.ContainsKey("city"));
    }

    [Fact]
    public void Parse_EmptyExpression_ReturnsNull()
    {
        FieldSelection? selection = FieldSelectionParser.Parse(string.Empty);

        Assert.Null(selection);
    }
}
