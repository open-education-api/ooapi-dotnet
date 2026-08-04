namespace OEAPI.Infrastructure.Query.Filtering;

/// <summary>
///     The operators the spec's <c>filter_query[field][operation]=value</c> DSL defines. See
///     <c>filter_query</c>/<c>filterQueryOr</c> parameter descriptions in <c>oeapi.json</c>.
/// </summary>
public enum FilterOperator
{
    /// <summary>
    ///     Presence check (<c>filterPresence</c>: empty, not_empty, empty_array, not_empty_array, true, false, null,
    ///     not_null).
    /// </summary>
    Is,

    /// <summary>Exact match; multiple values allowed as CSV.</summary>
    In,

    /// <summary>Negated inclusion; multiple values as CSV.</summary>
    NotIn,

    /// <summary>Partial match using <c>*</c> wildcards.</summary>
    Like,

    /// <summary>Negated partial match using <c>*</c> wildcards.</summary>
    NotLike,

    /// <summary>Match if any of the CSV values occur in an array-shaped field.</summary>
    AnyInArray,

    /// <summary>Match if all of the CSV values occur in an array-shaped field.</summary>
    AllInArray,

    /// <summary>Greater than (integer).</summary>
    GtInt,

    /// <summary>Less than (integer).</summary>
    LtInt,

    /// <summary>Greater than (float).</summary>
    GtFloat,

    /// <summary>Less than (float).</summary>
    LtFloat,

    /// <summary>Greater than (ISO 8601 date-time).</summary>
    GtDate,

    /// <summary>Less than (ISO 8601 date-time).</summary>
    LtDate
}
