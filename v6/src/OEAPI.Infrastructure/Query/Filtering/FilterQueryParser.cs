using System.Text.RegularExpressions;

namespace OEAPI.Infrastructure.Query.Filtering;

/// <summary>
///     Parses the spec's <c>deepObject</c>-style <c>filter_query[field][operation]=value</c> and
///     <c>filter_query[__or][][field][operation]=value</c> query-string keys. ASP.NET Core's default
///     model binding can't map this shape onto a single parameter, so controllers pass the raw query
///     string key/value pairs here directly instead of relying on <c>[FromQuery]</c> binding.
/// </summary>
public static class FilterQueryParser
{
    private static readonly Regex AndKeyPattern = new(
        @"^filter_query\[(?<field>[^\]]+)\]\[(?<op>[^\]]+)\]$",
        RegexOptions.Compiled);

    private static readonly Regex OrKeyPattern = new(
        @"^filter_query\[__or\]\[\]\[(?<field>[^\]]+)\]\[(?<op>[^\]]+)\]$",
        RegexOptions.Compiled);

    private static readonly Dictionary<string, FilterOperator> OperatorNames = new(StringComparer.OrdinalIgnoreCase)
    {
        ["is"] = FilterOperator.Is,
        ["in"] = FilterOperator.In,
        ["not_in"] = FilterOperator.NotIn,
        ["like"] = FilterOperator.Like,
        ["not_like"] = FilterOperator.NotLike,
        ["any_in_array"] = FilterOperator.AnyInArray,
        ["all_in_array"] = FilterOperator.AllInArray,
        ["gt_int"] = FilterOperator.GtInt,
        ["lt_int"] = FilterOperator.LtInt,
        ["gt_float"] = FilterOperator.GtFloat,
        ["lt_float"] = FilterOperator.LtFloat,
        ["gt_date"] = FilterOperator.GtDate,
        ["lt_date"] = FilterOperator.LtDate
    };

    /// <summary>
    ///     Parses every <c>filter_query[...]</c> key found in <paramref name="queryParameters" />.
    ///     Keys that don't match the expected shape, or whose operation name isn't recognized, are
    ///     silently skipped (consistent with this codebase's existing lenient-filter convention rather
    ///     than rejecting the whole request over one bad key).
    /// </summary>
    /// <param name="queryParameters">The raw request query-string key/value pairs.</param>
    public static ParsedFilterQuery Parse(IEnumerable<KeyValuePair<string, string?>> queryParameters)
    {
        ParsedFilterQuery result = new();

        foreach ((string key, string? value) in queryParameters)
        {
            if (value == null) continue;

            Match orMatch = OrKeyPattern.Match(key);
            if (orMatch.Success)
            {
                if (OperatorNames.TryGetValue(orMatch.Groups["op"].Value, out FilterOperator orOperator))
                    result.OrClauses.Add(new FilterClause(orMatch.Groups["field"].Value, orOperator, value));
                continue;
            }

            Match andMatch = AndKeyPattern.Match(key);
            if (andMatch.Success &&
                OperatorNames.TryGetValue(andMatch.Groups["op"].Value, out FilterOperator andOperator))
                result.AndClauses.Add(new FilterClause(andMatch.Groups["field"].Value, andOperator, value));
        }

        return result;
    }
}
