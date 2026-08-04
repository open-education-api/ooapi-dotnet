namespace OEAPI.Infrastructure.Query.Filtering;

/// <summary>
///     One parsed <c>filter_query[field][operation]=value</c> (or OR-block) entry.
/// </summary>
/// <param name="Field">
///     The field name as supplied by the client, possibly dotted for a nested/related field
///     (e.g. <c>organisation.primaryCode</c>).
/// </param>
/// <param name="Operator">The comparison operator.</param>
/// <param name="Value">The raw, unparsed value string from the query string.</param>
public sealed record FilterClause(string Field, FilterOperator Operator, string Value);
