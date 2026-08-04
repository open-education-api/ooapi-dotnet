namespace OEAPI.Infrastructure.Query.Filtering;

/// <summary>
///     A fully parsed <c>filter_query</c>/<c>filter_query[__or][]</c> request. Top-level
///     <see cref="AndClauses" /> are combined with AND; <see cref="OrClauses" /> (each a single-field
///     filter) are combined with OR as one group, which is then AND-ed with the top-level clauses -
///     matching the spec's documented semantics.
/// </summary>
public sealed class ParsedFilterQuery
{
    /// <summary>Gets the top-level filter clauses, combined with AND.</summary>
    public List<FilterClause> AndClauses { get; } = [];

    /// <summary>Gets the <c>__or</c> block's clauses, combined with OR as one group.</summary>
    public List<FilterClause> OrClauses { get; } = [];

    /// <summary>Gets a value indicating whether no filter clauses were parsed.</summary>
    public bool IsEmpty => AndClauses.Count == 0 && OrClauses.Count == 0;
}
