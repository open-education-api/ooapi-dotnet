using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Query.Extensions;

/// <summary>
///     Extension methods for IQueryable for pagination, ordering, and filtering.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    ///     Applies pagination to the query.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="pageNumber">The page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>The paginated query.</returns>
    public static IQueryable<T> Paginate<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        int validatedPageNumber = Math.Max(1, pageNumber);
        int validatedPageSize = Math.Min(Math.Max(1, pageSize), 100); // Max 100 per page

        return query
            .Skip((validatedPageNumber - 1) * validatedPageSize)
            .Take(validatedPageSize);
    }

    /// <summary>
    ///     Applies pagination and returns a PagedResult.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to paginate.</param>
    /// <param name="pageNumber">The page number (1-based).</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the paged result.</returns>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        int validatedPageNumber = Math.Max(1, pageNumber);
        int validatedPageSize = Math.Min(Math.Max(1, pageSize), 100);

        int totalCount = await query.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalCount == 0) return new PagedResult<T>(Array.Empty<T>(), 0, validatedPageNumber, validatedPageSize);

        List<T> items = await query
            .Skip((validatedPageNumber - 1) * validatedPageSize)
            .Take(validatedPageSize)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedResult<T>(items, totalCount, validatedPageNumber, validatedPageSize);
    }

    /// <summary>
    ///     Orders the query by the specified field name.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to order.</param>
    /// <param name="orderBy">The field name to order by.</param>
    /// <param name="descending">Whether to order in descending direction.</param>
    /// <returns>The ordered query.</returns>
    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> query,
        string? orderBy,
        bool descending = false)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        if (string.IsNullOrEmpty(orderBy))
            return query;

        PropertyInfo? propertyInfo = typeof(T).GetProperty(orderBy);
        if (propertyInfo == null)
            return query;

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
        Expression<Func<T, object>> orderByExpression =
            Expression.Lambda<Func<T, object>>(Expression.Convert(propertyAccess, typeof(object)), parameter);

        return descending
            ? query.OrderByDescending(orderByExpression)
            : query.OrderBy(orderByExpression);
    }

    /// <summary>
    ///     Filters the query to include only active items.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to filter.</param>
    /// <param name="propertyName">The name of the IsActive property. Default is "IsActive".</param>
    /// <returns>The filtered query.</returns>
    public static IQueryable<T> WhereActive<T>(
        this IQueryable<T> query,
        string propertyName = "IsActive")
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyName);
        if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(propertyAccess, parameter);
            return query.Where(lambda);
        }

        return query;
    }

    /// <summary>
    ///     Applies a search filter using LIKE on string properties.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to filter.</param>
    /// <param name="searchTerm">The search term to match.</param>
    /// <param name="propertyNames">The property names to search in. If null, searches all string properties.</param>
    /// <returns>The filtered query.</returns>
    public static IQueryable<T> WhereContains<T>(
        this IQueryable<T> query,
        string? searchTerm,
        params string[]? propertyNames)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        if (string.IsNullOrEmpty(searchTerm))
            return query;

        if (propertyNames == null || propertyNames.Length == 0)
        {
            // Search all string properties
            PropertyInfo[] stringProperties = [.. typeof(T).GetProperties().Where(p => p.PropertyType == typeof(string))];

            if (stringProperties.Length == 0)
                return query;

            propertyNames = [.. stringProperties.Select(p => p.Name)];
        }

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        Expression? combinedExpression = null;

        foreach (string propertyName in propertyNames)
        {
            PropertyInfo? propertyInfo = typeof(T).GetProperty(propertyName);
            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
                continue;

            MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);

            // Create null check: x.Property != null
            BinaryExpression nullCheck = Expression.NotEqual(propertyAccess, Expression.Constant(null));

            // Create Contains call: x.Property.Contains(searchTerm)
            MethodInfo? containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
            MethodCallExpression containsCall = Expression.Call(
                propertyAccess,
                containsMethod!,
                Expression.Constant(searchTerm));

            // Combine null check and contains: x.Property != null && x.Property.Contains(searchTerm)
            BinaryExpression propertyExpression = Expression.AndAlso(nullCheck, containsCall);

            combinedExpression = combinedExpression == null
                ? propertyExpression
                : Expression.OrElse(combinedExpression, propertyExpression);
        }

        if (combinedExpression != null)
        {
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
            return query.Where(lambda);
        }

        return query;
    }

    /// <summary>
    ///     Applies a filter expression to the query using the FilterParser.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to filter.</param>
    /// <param name="filterExpression">The filter expression string.</param>
    /// <returns>The filtered query.</returns>
    public static IQueryable<T> ApplyFilter<T>(
        this IQueryable<T> query,
        string? filterExpression)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        if (string.IsNullOrEmpty(filterExpression))
            return query;

        // For now, implement basic filtering - this would be replaced with the FilterParser logic
        // Simple implementation for basic equality filtering
        try
        {
            // Split by common filter operators
            if (filterExpression.Contains("="))
            {
                string[] parts = filterExpression.Split('=');
                if (parts.Length == 2)
                {
                    string fieldName = parts[0].Trim();
                    string value = parts[1].Trim();

                    ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
                    PropertyInfo? propertyInfo = typeof(T).GetProperty(fieldName);

                    if (propertyInfo != null)
                    {
                        MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                        ConstantExpression constant = Expression.Constant(value, propertyInfo.PropertyType);
                        BinaryExpression equality = Expression.Equal(propertyAccess, constant);
                        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
                        return query.Where(lambda);
                    }
                }
            }
        }
        catch
        {
            // If parsing fails, return the original query
        }

        return query;
    }

    /// <summary>
    ///     Selects only the specified fields from the query.
    /// </summary>
    /// <typeparam name="T">The type of elements in the query.</typeparam>
    /// <param name="query">The query to select from.</param>
    /// <param name="fields">The field names to select.</param>
    /// <returns>The query with selected fields.</returns>
    public static IQueryable<T> SelectFields<T>(
        this IQueryable<T> query,
        params string[] fields) where T : class, new()
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));

        if (fields.Length == 0)
            return query;

        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        List<MemberBinding> bindings = [];

        foreach (string fieldName in fields)
        {
            PropertyInfo? propertyInfo = typeof(T).GetProperty(fieldName);
            if (propertyInfo != null && propertyInfo.CanRead)
            {
                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, propertyInfo);
                bindings.Add(Expression.Bind(propertyInfo, propertyAccess));
            }
        }

        if (bindings.Count == 0)
            return query;

        MemberInitExpression body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
        Expression<Func<T, T>> lambda = Expression.Lambda<Func<T, T>>(body, parameter);

        return query.Select(lambda);
    }
}
