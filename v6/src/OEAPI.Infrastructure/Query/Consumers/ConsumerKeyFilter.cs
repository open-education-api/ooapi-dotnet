using System.Linq.Expressions;
using System.Reflection;

namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Applies the spec's list-level <c>?consumer=</c> filtering: "the implementation should only
///     return items required for that specific consumer." Entities with no <c>ConsumerKey</c> at all
///     are kept (they aren't tied to any particular consumer, so they're fine to return to anyone);
///     only entities explicitly tied to a <em>different</em> consumer than the one requested are
///     excluded. Resolves <c>ConsumerKey</c> purely via reflection against the entity type
///     (mirrors <c>GenericEntityController{TEntity,TApiModel}.IsConsumerMatchAsync</c>'s
///     approach for <c>GetById</c>/<c>Update</c>/<c>Delete</c>), so it works for every entity without
///     per-controller overrides, including entities that don't have a <c>ConsumerKey</c> property at
///     all (a no-op in that case).
/// </summary>
public static class ConsumerKeyFilter
{
    public static IQueryable<TEntity> Apply<TEntity>(IQueryable<TEntity> query, string? consumer) where TEntity : class
    {
        if (string.IsNullOrEmpty(consumer)) return query;

        PropertyInfo? consumerKeyProperty =
            typeof(TEntity).GetProperty("ConsumerKey", BindingFlags.Public | BindingFlags.Instance);
        if (consumerKeyProperty == null || consumerKeyProperty.PropertyType != typeof(string)) return query;

        ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
        MemberExpression property = Expression.Property(parameter, consumerKeyProperty);
        BinaryExpression isNull = Expression.Equal(property, Expression.Constant(null, typeof(string)));
        BinaryExpression matches = Expression.Equal(property, Expression.Constant(consumer, typeof(string)));
        Expression<Func<TEntity, bool>> predicate =
            Expression.Lambda<Func<TEntity, bool>>(Expression.OrElse(isNull, matches), parameter);

        return query.Where(predicate);
    }
}
