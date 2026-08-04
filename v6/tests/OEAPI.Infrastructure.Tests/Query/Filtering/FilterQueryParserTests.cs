using OEAPI.Infrastructure.Query.Filtering;
using Xunit;

namespace OEAPI.Infrastructure.Tests.Query.Filtering;

public class FilterQueryParserTests
{
    [Fact]
    public void Parse_SingleAndClause_ParsesFieldOperatorAndValue()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[primaryCode][in]", "ABC,DEF")
        ]);

        FilterClause clause = Assert.Single(result.AndClauses);
        Assert.Equal("primaryCode", clause.Field);
        Assert.Equal(FilterOperator.In, clause.Operator);
        Assert.Equal("ABC,DEF", clause.Value);
        Assert.Empty(result.OrClauses);
    }

    [Fact]
    public void Parse_DottedNestedField_PreservesFullFieldPath()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[organisation.primaryCode][is]", "not_empty")
        ]);

        Assert.Equal("organisation.primaryCode", Assert.Single(result.AndClauses).Field);
    }

    [Fact]
    public void Parse_OrBlockClause_GoesToOrClausesNotAndClauses()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[__or][][state][is]", "not_empty")
        ]);

        Assert.Empty(result.AndClauses);
        FilterClause clause = Assert.Single(result.OrClauses);
        Assert.Equal("state", clause.Field);
        Assert.Equal(FilterOperator.Is, clause.Operator);
    }

    [Fact]
    public void Parse_UnrecognizedOperatorName_IsSilentlySkipped()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[primaryCode][not_a_real_operator]", "value")
        ]);

        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void Parse_MalformedKeyShape_IsSilentlySkipped()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[primaryCode]", "value"), // missing the [operation] segment
            new KeyValuePair<string, string?>("not_a_filter_query_key", "value")
        ]);

        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void Parse_NullValue_IsSkipped()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[primaryCode][in]", null)
        ]);

        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void Parse_MultipleKeys_CollectsAllAndClauses()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[primaryCode][in]", "ABC"),
            new KeyValuePair<string, string?>("filter_query[state][is]", "not_empty"),
            new KeyValuePair<string, string?>("unrelated_param", "ignored")
        ]);

        Assert.Equal(2, result.AndClauses.Count);
    }

    [Fact]
    public void Parse_OperatorNameIsCaseInsensitive()
    {
        ParsedFilterQuery result = FilterQueryParser.Parse([
            new KeyValuePair<string, string?>("filter_query[primaryCode][IN]", "ABC")
        ]);

        Assert.Equal(FilterOperator.In, Assert.Single(result.AndClauses).Operator);
    }
}
