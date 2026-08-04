using Microsoft.EntityFrameworkCore;
using OEAPI.Infrastructure.Query.Filtering;
using Xunit;

namespace OEAPI.Infrastructure.Tests.Query.Filtering;

/// <summary>
///     Covers every <see cref="FilterOperator" /> that translates to plain LINQ (testable against
///     <c>List&lt;T&gt;.AsQueryable()</c>) - <c>Like</c>/<c>NotLike</c> are deliberately excluded, since
///     they translate to <c>EF.Functions.Like</c>, which has no LINQ-to-Objects implementation and can
///     only run against a real EF Core provider (covered by the integration tests in
///     <c>OEAPI.API.Tests</c> instead).
/// </summary>
public class FilterQueryTranslatorTests
{
    private static IQueryable<TestEntity> Sample()
    {
        return new List<TestEntity>
        {
            new()
            {
                Name = "Alpha", Count = 1, Score = 1.5, Active = true, Tags = "[\"red\",\"blue\"]",
                EventDate = "2024-01-01T00:00:00Z", ConsumerKey = "tenant-a", IsActive = true,
                StartDateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Name = "Beta", Count = 5, Score = 5.5, Active = false, Tags = "[\"green\"]",
                EventDate = "2024-06-01T00:00:00Z", ConsumerKey = "tenant-b", IsActive = false,
                StartDateTime = new DateTime(2024, 6, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Name = null, Count = null, Score = null, Active = null, Tags = "[]", EventDate = "2024-01-15T00:00:00Z",
                Parent = new TestEntity { Name = "ParentOfNull", ConsumerKey = "tenant-a" }
            }
        }.AsQueryable();
    }

    private static ParsedFilterQuery And(params FilterClause[] clauses)
    {
        ParsedFilterQuery query = new();
        query.AndClauses.AddRange(clauses);
        return query;
    }

    [Fact]
    public void Apply_InOperator_ReturnsOnlyMatchingRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Name", FilterOperator.In, "Alpha,Missing")))];

        Assert.Single(result);
        Assert.Equal("Alpha", result[0].Name);
    }

    [Fact]
    public void Apply_NotInOperator_ExcludesMatchingRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Name", FilterOperator.NotIn, "Alpha")))];

        Assert.DoesNotContain(result, e => e.Name == "Alpha");
    }

    [Fact]
    public void Apply_InOperator_OnNullableIntField_ReturnsOnlyMatchingRows()
    {
        // Regression test: filter_query[field][in] previously silently dropped the whole clause for
        // any non-string field (e.g. attempts, an int?) - returning every row unfiltered.
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Count", FilterOperator.In, "1,99")))];

        Assert.Single(result);
        Assert.Equal("Alpha", result[0].Name);
    }

    [Fact]
    public void Apply_NotInOperator_OnNullableIntField_ExcludesMatchingRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Count", FilterOperator.NotIn, "1")))];

        Assert.DoesNotContain(result, e => e.Count == 1);
    }

    [Fact]
    public void Apply_InOperator_OnNullableDoubleField_ReturnsOnlyMatchingRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Score", FilterOperator.In, "5.5")))];

        Assert.Single(result);
        Assert.Equal("Beta", result[0].Name);
    }

    [Fact]
    public void Apply_InOperator_OnNumericField_WithUnparseableToken_IsSilentlyIgnored()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Count", FilterOperator.In, "not-a-number")))];

        Assert.Equal(3, result.Count); // no filtering applied - the whole clause was dropped
    }

    [Fact]
    public void Apply_InOperator_OnNullableBoolField_ReturnsOnlyMatchingRows()
    {
        // Regression test: filter_query[field][in] previously silently dropped the whole clause for
        // bool fields (e.g. Person.activeEnrolment) - returning every row unfiltered.
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Active", FilterOperator.In, "true")))];

        Assert.Single(result);
        Assert.Equal("Alpha", result[0].Name);
    }

    [Fact]
    public void Apply_NotInOperator_OnNullableBoolField_ExcludesMatchingRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Active", FilterOperator.NotIn, "true")))];

        Assert.DoesNotContain(result, e => e.Active == true);
    }

    [Fact]
    public void Apply_InOperator_OnBoolField_WithUnparseableToken_IsSilentlyIgnored()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Active", FilterOperator.In, "not-a-bool")))];

        Assert.Equal(3, result.Count); // no filtering applied - the whole clause was dropped
    }

    [Fact]
    public void Apply_InOperator_OnNullableDateTimeField_ReturnsOnlyMatchingRows()
    {
        // Regression test: filter_query[field][in] previously silently dropped the whole clause for
        // DateTime fields (e.g. Course.validFrom) - returning every row unfiltered.
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(),
            And(new FilterClause("StartDateTime", FilterOperator.In, "2024-01-01T00:00:00Z,1999-01-01T00:00:00Z")))];

        Assert.Single(result);
        Assert.Equal("Alpha", result[0].Name);
    }

    [Fact]
    public void Apply_NotInOperator_OnNullableDateTimeField_ExcludesMatchingRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(),
            And(new FilterClause("StartDateTime", FilterOperator.NotIn, "2024-01-01T00:00:00Z")))];

        Assert.DoesNotContain(result, e => e.StartDateTime == new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }

    [Fact]
    public void Apply_InOperator_OnDateTimeField_WithUnparseableToken_IsSilentlyIgnored()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(),
            And(new FilterClause("StartDateTime", FilterOperator.In, "not-a-date")))];

        Assert.Equal(3, result.Count); // no filtering applied - the whole clause was dropped
    }

    [Fact]
    public void Apply_InOperator_OnNullableIntField_NullRowsNeverMatch()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Count", FilterOperator.In, "1,5")))];

        Assert.Equal(2, result.Count);
        Assert.DoesNotContain(result, e => e.Count == null);
    }

    [Fact]
    public void Apply_IsNotNull_ExcludesNullRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Name", FilterOperator.Is, "not_null")))];

        Assert.Equal(2, result.Count);
        Assert.All(result, e => Assert.NotNull(e.Name));
    }

    [Fact]
    public void Apply_IsTrue_MatchesOnlyBooleanTrueRows()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Active", FilterOperator.Is, "true")))];

        Assert.Single(result);
        Assert.Equal("Alpha", result[0].Name);
    }

    [Fact]
    public void Apply_GtInt_ReturnsRowsAboveThreshold()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Count", FilterOperator.GtInt, "2")))];

        Assert.Single(result);
        Assert.Equal("Beta", result[0].Name);
    }

    [Fact]
    public void Apply_LtFloat_ReturnsRowsBelowThreshold()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Score", FilterOperator.LtFloat, "2.0")))];

        Assert.Single(result);
        Assert.Equal("Alpha", result[0].Name);
    }

    [Fact]
    public void Apply_GtDate_OnStringField_ComparesLexicographically()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(),
            And(new FilterClause("EventDate", FilterOperator.GtDate, "2024-03-01T00:00:00Z")))];

        Assert.Single(result);
        Assert.Equal("Beta", result[0].Name);
    }

    [Fact]
    public void Apply_AnyInArray_MatchesIfAnyTokenPresent()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Tags", FilterOperator.AnyInArray, "green,purple")))];

        Assert.Single(result);
        Assert.Equal("Beta", result[0].Name);
    }

    [Fact]
    public void Apply_AllInArray_RequiresEveryToken()
    {
        List<TestEntity> matchesBoth = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Tags", FilterOperator.AllInArray, "red,blue")))];
        List<TestEntity> missingOne = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Tags", FilterOperator.AllInArray, "red,green")))];

        Assert.Single(matchesBoth);
        Assert.Empty(missingOne);
    }

    [Fact]
    public void Apply_DottedNestedField_ResolvesThroughNavigationProperty()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Parent.Name", FilterOperator.In, "ParentOfNull")))];

        Assert.Single(result);
        Assert.Null(result[0].Name);
    }

    private static IQueryable<RelatedEntity> RelatedSample()
    {
        RelatedTarget matchTarget = new() { TargetId = "target-1" };
        RelatedTarget otherTarget = new() { TargetId = "target-2" };

        return new List<RelatedEntity>
        {
            new() { Name = "Matches", Target = matchTarget },
            new() { Name = "DoesNotMatch", Target = otherTarget },
            new() { Name = "NoTarget", Target = null }
        }.AsQueryable();
    }

    [Fact]
    public void Apply_FlattenedRelationIdField_ResolvesThroughNavigationToTargetsOwnIdProperty()
    {
        // Regression test: "targetId" has no direct CLR property on RelatedEntity (the internal FK is
        // "TargetEntityId", never exposed to API consumers) - this must resolve via the "Target"
        // navigation property to RelatedTarget's own [Index(..., IsUnique = true)] id property
        // ("TargetId"), not silently drop the whole clause (which would return every row unfiltered).
        List<RelatedEntity> result = [.. FilterQueryTranslator.Apply(RelatedSample(), And(new FilterClause("targetId", FilterOperator.In, "target-1")))];

        Assert.Single(result);
        Assert.Equal("Matches", result[0].Name);
    }

    [Fact]
    public void Apply_FlattenedRelationIdField_ExcludesRowsWithNoRelatedEntity()
    {
        List<RelatedEntity> result = [.. FilterQueryTranslator.Apply(RelatedSample(), And(new FilterClause("targetId", FilterOperator.In, "target-1,target-2")))];

        Assert.Equal(2, result.Count);
        Assert.DoesNotContain(result, e => e.Name == "NoTarget");
    }

    [Fact]
    public void Apply_UnresolvableField_IsSilentlyIgnoredNotThrown()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("NoSuchField", FilterOperator.In, "x")))];

        Assert.Equal(3, result.Count); // no filtering applied - the whole clause was dropped
    }

    [Theory]
    [InlineData("ConsumerKey", "In", "tenant-a")]
    [InlineData("consumerkey", "In", "tenant-a")] // resolution is case-insensitive, so the exclusion must be too
    [InlineData("TagsJson", "In", "[\"red\",\"blue\"]")]
    [InlineData("IsActive", "Is", "true")]
    [InlineData("CreatedAt", "GtDate", "2000-01-01T00:00:00Z")]
    [InlineData("ModifiedAt", "GtDate", "2000-01-01T00:00:00Z")]
    [InlineData("Parent.ConsumerKey", "In", "tenant-a")] // excluded as an intermediate/leaf hop too
    public void Apply_InternalOnlyOrJsonBlobField_CannotBeFilteredOn(string field, string op, string value)
    {
        // filter_query must not be usable as a side channel to probe internal bookkeeping fields
        // (see InternalOnlyPropertyNames) - the clause should be silently dropped, same as an
        // unresolvable field, not translated into a real predicate. Each field is paired with an
        // operator its real type would otherwise support, so the drop is provably due to the
        // exclusion rather than an incidental operator/type mismatch.
        FilterOperator filterOperator = Enum.Parse<FilterOperator>(op);
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause(field, filterOperator, value)))];

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Apply_Id_CannotBeFilteredOn()
    {
        // Id is excluded like the other internal-only fields, though no operator in this DSL
        // supports non-nullable Guid anyway - kept for documentation of intent.
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), And(new FilterClause("Id", FilterOperator.In, "x")))];

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void Apply_OrBlock_CombinesWithOrThenAndsWithTopLevelClauses()
    {
        ParsedFilterQuery query = new();
        query.AndClauses.Add(new FilterClause("Active", FilterOperator.Is, "not_null")); // excludes the null row
        query.OrClauses.Add(new FilterClause("Name", FilterOperator.In, "Alpha"));
        query.OrClauses.Add(new FilterClause("Name", FilterOperator.In, "Beta"));

        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), query)];

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void Apply_EmptyFilter_ReturnsQueryUnchanged()
    {
        List<TestEntity> result = [.. FilterQueryTranslator.Apply(Sample(), new ParsedFilterQuery())];

        Assert.Equal(3, result.Count);
    }

    // ReSharper disable UnusedAutoPropertyAccessor.Local, UnusedMember.Local - every property below is
    // reached only via FilterQueryTranslator's own string-based reflection, never a direct C# accessor.
    private sealed class TestEntity
    {
        public string? Name { get; set; }
        public int? Count { get; set; }
        public double? Score { get; set; }
        public bool? Active { get; set; }
        public string? Tags { get; set; } // JSON-array-as-string, per this codebase's convention
        public string? EventDate { get; set; } // RFC3339 string, per this codebase's convention
        public DateTime? StartDateTime { get; set; } // real DateTime column, e.g. CourseEntity.ValidFrom
        public TestEntity? Parent { get; set; }

        // Mirrors the real entities' internal bookkeeping properties, to prove ResolvePath excludes
        // them - see InternalOnlyPropertyNames.
        public Guid Id { get; set; }
        public string? ConsumerKey { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool IsActive { get; set; }
        public string? TagsJson { get; set; }
    }

    // Mirrors the real entities' FK-plus-navigation shape (e.g. CourseEntity.OrganisationEntityId +
    // .Organisation) used to reproduce/regression-guard the relation-id flattening fix.
    private sealed class RelatedEntity
    {
        public string? Name { get; set; }
        public Guid? TargetEntityId { get; set; }
        public RelatedTarget? Target { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Local, UnusedMember.Local

    [Index(nameof(TargetId), IsUnique = true)]
    private sealed class RelatedTarget
    {
        public string TargetId { get; set; } = string.Empty;
    }
}
