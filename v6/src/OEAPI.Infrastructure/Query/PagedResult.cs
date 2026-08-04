using System.Text.Json.Serialization;

namespace OEAPI.Infrastructure.Query;

/// <summary>
///     Result of a paged query operation.
/// </summary>
/// <typeparam name="T">The type of items in the result.</typeparam>
/// <remarks>
///     Initializes a new instance of the PagedResult class.
/// </remarks>
/// <param name="items">The items on the current page.</param>
/// <param name="totalCount">The total number of items across all pages.</param>
/// <param name="pageNumber">The current page number (1-based).</param>
/// <param name="pageSize">The number of items per page.</param>
/// <param name="ext">
///     Free-form, spec-defined extension data for the list response as a whole (distinct from each
///     item's own <c>ext</c> field). Optional since no controller currently has a source of
///     list-level extension data to populate it with - defaults to <see langword="null" /> so the
///     field is spec-shaped without fabricating a value, matching how per-item <c>ext</c> fields
///     already behave throughout this codebase when no data backs them.
/// </param>
public class PagedResult<T>(
    IReadOnlyList<T> items,
    int totalCount,
    int pageNumber,
    int pageSize,
    object? ext = null)
{

    /// <summary>
    ///     Gets the items on the current page.
    /// </summary>
    public IReadOnlyList<T> Items { get; } = items;

    /// <summary>
    ///     Gets the total number of items across all pages. Not part of the spec's <c>Pagination</c>
    ///     schema - excluded from JSON serialization. Only used internally to compute
    ///     <see cref="TotalPages" />; not exposed to clients anywhere (no replacement header - see
    ///     the <c>remove-undeclared-pagination-headers</c> change).
    /// </summary>
    [JsonIgnore]
    public int TotalCount { get; } = totalCount;

    /// <summary>
    ///     Gets the current page number (1-based).
    /// </summary>
    public int PageNumber { get; } = pageNumber;

    /// <summary>
    ///     Gets the number of items per page.
    /// </summary>
    public int PageSize { get; } = pageSize;

    /// <summary>
    ///     Gets the free-form extension data for this list response (spec's <c>ext</c> field on the
    ///     paginated response shape). See the constructor parameter doc for why this is usually
    ///     <see langword="null" />.
    /// </summary>
    public object? Ext { get; } = ext;

    /// <summary>
    ///     Gets the total number of pages.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    ///     Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    ///     Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;
}
