namespace OEAPI.Infrastructure.Query;

/// <summary>
///     Base query parameters for filtering, pagination, and field selection.
/// </summary>
public class QueryParameters
{
    /// <summary>
    ///     Gets the maximum allowed page size (to prevent abuse).
    /// </summary>
    private const int MaxPageSize = 100;

    /// <summary>
    ///     Gets or sets the page number (1-based). Default is 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    ///     Gets or sets the number of items per page. Default is 20.
    /// </summary>
    public int PageSize { get; set; } = 20;

    /// <summary>
    ///     Gets or sets the comma-separated list of fields to include in the response.
    /// </summary>
    /// <example>id,name,description</example>
    public string? Fields { get; set; }

    /// <summary>
    ///     Gets or sets the field to order by.
    /// </summary>
    /// <example>name</example>
    public string? OrderBy { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to sort in descending order.
    /// </summary>
    public bool Descending { get; set; } = false;

    /// <summary>
    ///     Gets the validated page size.
    /// </summary>
    public int ValidatedPageSize => Math.Min(Math.Max(1, PageSize), MaxPageSize);

    /// <summary>
    ///     Gets the validated page number.
    /// </summary>
    public int ValidatedPage => Math.Max(1, Page);

    /// <summary>
    ///     Gets the parsed field list.
    /// </summary>
    public string[] ParsedFields => string.IsNullOrEmpty(Fields)
        ? []
        : Fields.Split([',', ';', ' '], StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    ///     Gets a value indicating whether field selection is requested.
    /// </summary>
    public bool HasFieldSelection => !string.IsNullOrEmpty(Fields);

    /// <summary>
    ///     Gets or sets the search term for text-based filtering.
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to include only active items.
    /// </summary>
    public bool OnlyActive { get; set; } = true;

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data filtering.
    /// </summary>
    public string? Consumer { get; set; }

    /// <summary>
    ///     Gets or sets the comma-separated list of related entities to expand.
    /// </summary>
    /// <example>addresses,courses</example>
    public string? Expand { get; set; }

    /// <summary>
    ///     Gets the parsed expand list.
    /// </summary>
    public string[] ParsedExpand => string.IsNullOrEmpty(Expand)
        ? []
        : Expand.Split([',', ';', ' '], StringSplitOptions.RemoveEmptyEntries);

    /// <summary>
    ///     Gets a value indicating whether expand is requested.
    /// </summary>
    public bool HasExpand => !string.IsNullOrEmpty(Expand);
}

/// <summary>
///     Filter parameters for OEAPI queries.
/// </summary>
public class FilterParameters
{
    /// <summary>
    ///     Gets or sets the search term for text-based filtering.
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to include only active items.
    /// </summary>
    public bool OnlyActive { get; set; } = true;

    /// <summary>
    ///     Gets or sets the start date for date range filtering (RFC3339 format).
    /// </summary>
    public string? StartDate { get; set; }

    /// <summary>
    ///     Gets or sets the end date for date range filtering (RFC3339 format).
    /// </summary>
    public string? EndDate { get; set; }

    /// <summary>
    ///     Gets a value indicating whether this filter is empty.
    /// </summary>
    public bool IsEmpty =>
        string.IsNullOrEmpty(Search) &&
        !OnlyActive &&
        string.IsNullOrEmpty(StartDate) &&
        string.IsNullOrEmpty(EndDate);
}

/// <summary>
///     Combined query parameters including filtering, pagination, and field selection.
/// </summary>
public class QueryParameters<TFilter> where TFilter : FilterParameters, new()
{
    /// <summary>
    ///     Gets or sets the pagination and field selection parameters.
    /// </summary>
    public QueryParameters Pagination { get; set; } = new();

    /// <summary>
    ///     Gets or sets the filter parameters.
    /// </summary>
    public TFilter Filter { get; set; } = new();

    /// <summary>
    ///     Gets a value indicating whether the query has any parameters.
    /// </summary>
    public bool HasParameters =>
        Pagination.Page != 1 ||
        Pagination.PageSize != 20 ||
        Pagination.HasFieldSelection ||
        Pagination.OrderBy != null ||
        !Filter.IsEmpty;
}
