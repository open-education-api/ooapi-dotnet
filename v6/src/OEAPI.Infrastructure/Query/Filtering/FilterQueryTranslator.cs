using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Query.Filtering;

/// <summary>
///     Translates a <see cref="ParsedFilterQuery" /> into <see cref="IQueryable{T}" /> predicates via
///     reflection/expression trees, purely generically - no per-entity field-mapping catalogue. Each
///     clause's dotted <c>Field</c> is resolved directly against the entity type's CLR property
///     names (case-insensitive), walking single-valued navigation properties for dotted paths (e.g.
///     <c>organisation.primaryCode</c> -&gt; <c>x.Organisation.PrimaryCode</c>), except for the internal
///     bookkeeping properties in <see cref="InternalOnlyPropertyNames" /> and raw JSON-blob storage
///     columns (<c>*Json</c>), which are never part of any API model's contract and are excluded so
///     <c>filter_query</c> can't be used as a side channel to probe their values. A clause whose field
///     can't be resolved, or whose operator doesn't make sense for the resolved property's type, is
///     silently dropped rather than erroring - consistent with this codebase's existing lenient-filter
///     convention (e.g. <c>ApplyPrimaryCodeFilter</c>) and with the spec's framing of <c>filter_query</c>
///     as an entirely optional, implementer-defined feature.
/// </summary>
public static class FilterQueryTranslator
{
    /// <summary>
    ///     Entity properties that exist purely for internal bookkeeping/partitioning - excluded from
    ///     <see cref="ResolvePath" /> resolution (including as an intermediate navigation hop), most
    ///     notably <c>ConsumerKey</c>, which is also the field the spec's <c>?consumer=</c> partitioning
    ///     matches against (see <see cref="OEAPI.Infrastructure.Query.Consumers.ConsumerKeyFilter" />) -
    ///     without this exclusion, filter_query could be used to enumerate valid ConsumerKey values.
    /// </summary>
    private static readonly HashSet<string> InternalOnlyPropertyNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "Id", "ConsumerKey", "CreatedAt", "ModifiedAt", "IsActive"
    };

    /// <summary>
    ///     Applies every clause in <paramref name="filter" /> to <paramref name="query" />. Top-level
    ///     clauses are AND-ed together (and with the query as it already stands); OR-block clauses are
    ///     combined with OR as one group, which is then AND-ed with everything else.
    /// </summary>
    public static IQueryable<T> Apply<T>(IQueryable<T> query, ParsedFilterQuery filter) where T : class
    {
        if (filter.IsEmpty) return query;

        foreach (FilterClause clause in filter.AndClauses)
        {
            Expression<Func<T, bool>>? predicate = BuildPredicate<T>(clause);
            if (predicate != null) query = query.Where(predicate);
        }

        if (filter.OrClauses.Count > 0)
        {
            Expression<Func<T, bool>>? combined = null;
            foreach (FilterClause clause in filter.OrClauses)
            {
                Expression<Func<T, bool>>? predicate = BuildPredicate<T>(clause);
                if (predicate == null) continue;

                combined = combined == null ? predicate : Or(combined, predicate);
            }

            if (combined != null) query = query.Where(combined);
        }

        return query;
    }

    private static Expression<Func<T, bool>>? BuildPredicate<T>(FilterClause clause)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
        (Expression Access, Type PropertyType, Expression? NullGuard)? path = ResolvePath(parameter, clause.Field);
        if (path == null) return null;

        (Expression access, Type propertyType, Expression? nullGuard) = path.Value;
        Expression? comparison = BuildOperatorExpression(access, propertyType, clause.Operator, clause.Value);
        if (comparison == null) return null;

        Expression body = nullGuard == null ? comparison : Expression.AndAlso(nullGuard, comparison);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    ///     Walks a dotted field path against successive CLR types, resolving each segment
    ///     case-insensitively. Intermediate reference-typed hops get a null guard (<c>x.Nav != null</c>)
    ///     so the final comparison never throws a <see cref="NullReferenceException" /> when translated -
    ///     EF Core turns this into ordinary SQL joins/null checks.
    /// </summary>
    /// <remarks>
    ///     A segment like <c>organisationId</c> has no matching CLR property on the entity itself - the
    ///     API's flattened relation-id field (the related resource's own external string id, e.g.
    ///     <c>entity.Organisation.OrganisationId</c>) doesn't correspond 1:1 to the entity's internal FK
    ///     property (<c>OrganisationEntityId</c>, an internal <c>Guid</c>, never exposed to API
    ///     consumers). When a segment ending in <c>Id</c> has no direct match, <see cref="ResolveIdFlattening" />
    ///     falls back to resolving it as &lt;nav-property-with-that-name-minus-"Id"&gt;.&lt;that
    ///     entity's own external id property&gt; - purely reflectively (via each entity's existing
    ///     <see cref="IndexAttribute" /> declaring its own unique external id property), so this stays
    ///     a no-per-entity-catalogue resolver like the rest of this class, not a hardcoded field map.
    /// </remarks>
    private static (Expression Access, Type PropertyType, Expression? NullGuard)? ResolvePath(
        ParameterExpression parameter,
        string dottedField)
    {
        string[] segments =
            dottedField.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (segments.Length == 0) return null;

        Expression current = parameter;
        Type currentType = parameter.Type;
        Expression? nullGuard = null;

        for (int i = 0; i < segments.Length; i++)
        {
            PropertyInfo? property = FindProperty(currentType, segments[i]);

            if (property == null && segments[i].EndsWith("Id", StringComparison.OrdinalIgnoreCase))
            {
                (PropertyInfo NavProperty, PropertyInfo IdProperty)? flattened =
                    ResolveIdFlattening(currentType, segments[i]);
                if (flattened == null) return null;

                (PropertyInfo navProperty, PropertyInfo idProperty) = flattened.Value;
                current = Expression.Property(current, navProperty);
                BinaryExpression navNotNull =
                    Expression.NotEqual(current, Expression.Constant(null, navProperty.PropertyType));
                nullGuard = nullGuard == null ? navNotNull : Expression.AndAlso(nullGuard, navNotNull);

                current = Expression.Property(current, idProperty);
                currentType = idProperty.PropertyType;
                continue;
            }

            if (property == null
                || InternalOnlyPropertyNames.Contains(property.Name)
                || property.Name.EndsWith("Json", StringComparison.OrdinalIgnoreCase))
                return null;

            current = Expression.Property(current, property);
            currentType = property.PropertyType;

            bool isLastSegment = i == segments.Length - 1;
            if (!isLastSegment && !currentType.IsValueType)
            {
                BinaryExpression notNull = Expression.NotEqual(current, Expression.Constant(null, currentType));
                nullGuard = nullGuard == null ? notNull : Expression.AndAlso(nullGuard, notNull);
            }
        }

        return (current, currentType, nullGuard);
    }

    private static PropertyInfo? FindProperty(Type type, string name)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    ///     Resolves a <c>&lt;name&gt;Id</c> segment (e.g. <c>organisationId</c>, <c>parentId</c>) to a
    ///     navigation property named <c>&lt;name&gt;</c> plus that navigation's target entity's own
    ///     external id property - found via the single-property, unique <see cref="IndexAttribute" />
    ///     every entity in this codebase already declares for its own external id (e.g.
    ///     <c>[Index(nameof(OrganisationId), IsUnique = true)]</c>). Returns <see langword="null" /> if
    ///     no matching navigation property exists, it isn't a reference type, or its target type
    ///     declares no such index.
    /// </summary>
    private static (PropertyInfo NavProperty, PropertyInfo IdProperty)? ResolveIdFlattening(
        Type currentType, string segment)
    {
        string navName = segment[..^"Id".Length];
        PropertyInfo? navProperty = FindProperty(currentType, navName);
        if (navProperty == null || navProperty.PropertyType.IsValueType || navProperty.PropertyType == typeof(string))
            return null;

        IndexAttribute? index = navProperty.PropertyType
            .GetCustomAttributes<IndexAttribute>()
            .FirstOrDefault(a => a.IsUnique
                                  && a.PropertyNames.Count == 1
                                  && a.PropertyNames[0].EndsWith("Id", StringComparison.OrdinalIgnoreCase));
        if (index == null) return null;

        PropertyInfo? idProperty = FindProperty(navProperty.PropertyType, index.PropertyNames[0]);
        return idProperty == null ? null : (navProperty, idProperty);
    }

    private static Expression? BuildOperatorExpression(Expression access, Type propertyType, FilterOperator op,
        string value)
    {
        return op switch
        {
            FilterOperator.Is => BuildIsExpression(access, propertyType, value),
            FilterOperator.In => BuildInExpression(access, propertyType, value, false),
            FilterOperator.NotIn => BuildInExpression(access, propertyType, value, true),
            FilterOperator.Like => BuildLikeExpression(access, propertyType, value, false),
            FilterOperator.NotLike => BuildLikeExpression(access, propertyType, value, true),
            FilterOperator.AnyInArray => BuildArrayExpression(access, propertyType, value, false),
            FilterOperator.AllInArray => BuildArrayExpression(access, propertyType, value, true),
            FilterOperator.GtInt => BuildNumericComparison(access, propertyType, value, true, false),
            FilterOperator.LtInt => BuildNumericComparison(access, propertyType, value, false, false),
            FilterOperator.GtFloat => BuildNumericComparison(access, propertyType, value, true, true),
            FilterOperator.LtFloat => BuildNumericComparison(access, propertyType, value, false, true),
            FilterOperator.GtDate => BuildDateComparison(access, propertyType, value, true),
            FilterOperator.LtDate => BuildDateComparison(access, propertyType, value, false),
            _ => null
        };
    }

    private static Expression? BuildIsExpression(Expression access, Type propertyType, string value)
    {
        bool isNullable = !propertyType.IsValueType || Nullable.GetUnderlyingType(propertyType) != null;

        switch (value.Trim().ToLowerInvariant())
        {
            case "null":
                return isNullable ? Expression.Equal(access, Expression.Constant(null, propertyType)) : null;

            case "not_null":
                return isNullable ? Expression.NotEqual(access, Expression.Constant(null, propertyType)) : null;

            case "empty":
                if (propertyType == typeof(string))
                    return Expression.Call(typeof(string), nameof(string.IsNullOrEmpty), null, access);
                return isNullable ? Expression.Equal(access, Expression.Constant(null, propertyType)) : null;

            case "not_empty":
                if (propertyType == typeof(string))
                    return Expression.Not(Expression.Call(typeof(string), nameof(string.IsNullOrEmpty), null, access));
                return isNullable ? Expression.NotEqual(access, Expression.Constant(null, propertyType)) : null;

            case "empty_array":
                // Array-shaped fields in this codebase are stored as a JSON-serialized string column.
                return propertyType == typeof(string)
                    ? Expression.OrElse(
                        Expression.Call(typeof(string), nameof(string.IsNullOrEmpty), null, access),
                        Expression.Equal(access, Expression.Constant("[]")))
                    : null;

            case "not_empty_array":
                return propertyType == typeof(string)
                    ? Expression.Not(Expression.OrElse(
                        Expression.Call(typeof(string), nameof(string.IsNullOrEmpty), null, access),
                        Expression.Equal(access, Expression.Constant("[]"))))
                    : null;

            case "true":
                return propertyType == typeof(bool) || propertyType == typeof(bool?)
                    ? Expression.Equal(access, Expression.Constant(true, propertyType))
                    : null;

            case "false":
                return propertyType == typeof(bool) || propertyType == typeof(bool?)
                    ? Expression.Equal(access, Expression.Constant(false, propertyType))
                    : null;

            default:
                return null;
        }
    }

    /// <summary>
    ///     Builds a <c>value IN (...)</c>-style predicate. The spec declares <c>in</c>/<c>not_in</c>'s
    ///     own value as <c>type: string</c> (a CSV-of-values query-parameter shape) regardless of the
    ///     target field's real type - that's a wire-format detail, not a restriction to string-typed
    ///     fields, so this supports every scalar property type this class's <c>gt</c>/<c>lt</c>
    ///     operators already support - numeric (matching <see cref="IsNumericType" />),
    ///     <see cref="bool" />, <see cref="DateTime" />, and <see cref="DateTimeOffset" /> - in addition
    ///     to <see cref="string" />.
    /// </summary>
    private static Expression? BuildInExpression(Expression access, Type propertyType, string value, bool negate)
    {
        string[] tokens = SplitCsv(value);
        if (tokens.Length == 0) return null;

        if (propertyType == typeof(string))
            return BuildContains(Expression.Constant(tokens), access, typeof(string), negate);

        Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        List<Expression> constants = new(tokens.Length);
        foreach (string token in tokens)
        {
            if (!TryParseInToken(token, underlyingType, out object? parsedValue)) return null;
            constants.Add(Expression.Constant(parsedValue, propertyType));
        }

        NewArrayExpression valuesArray = Expression.NewArrayInit(propertyType, constants);
        return BuildContains(valuesArray, access, propertyType, negate);
    }

    /// <summary>
    ///     Parses a single <c>in</c>/<c>not_in</c> CSV token against <paramref name="underlyingType" />
    ///     - numeric via <see cref="TryParseNumeric" />, <see cref="bool" /> via <see cref="bool.TryParse(string,out bool)" />,
    ///     <see cref="DateTime" />/<see cref="DateTimeOffset" /> via the same
    ///     <see cref="DateTimeStyles" /> <see cref="BuildDateComparison" /> already uses for
    ///     <c>gt_date</c>/<c>lt_date</c>, so <c>in</c>/<c>not_in</c> accepts exactly the same value
    ///     shapes those operators do. Returns <see langword="false" /> for an unsupported type or a
    ///     malformed token, matching this class's own "silently drop an unusable clause" convention.
    /// </summary>
    private static bool TryParseInToken(string token, Type underlyingType, out object? parsedValue)
    {
        if (IsNumericType(underlyingType)) return TryParseNumeric(token, underlyingType, out parsedValue);

        if (underlyingType == typeof(bool))
        {
            bool parsed = bool.TryParse(token, out bool boolValue);
            parsedValue = parsed ? boolValue : null;
            return parsed;
        }

        if (underlyingType == typeof(DateTime))
        {
            bool parsed = DateTime.TryParse(token, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal,
                out DateTime dateTimeValue);
            parsedValue = parsed ? dateTimeValue : null;
            return parsed;
        }

        if (underlyingType == typeof(DateTimeOffset))
        {
            bool parsed = DateTimeOffset.TryParse(token, CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal, out DateTimeOffset dateTimeOffsetValue);
            parsedValue = parsed ? dateTimeOffsetValue : null;
            return parsed;
        }

        parsedValue = null;
        return false;
    }

    private static Expression BuildContains(Expression valuesArray, Expression access, Type elementType, bool negate)
    {
        MethodInfo containsMethod = typeof(Enumerable).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .First(m => m.Name == nameof(Enumerable.Contains) && m.GetParameters().Length == 2)
            .MakeGenericMethod(elementType);

        MethodCallExpression contains = Expression.Call(containsMethod, valuesArray, access);
        return negate ? Expression.Not(contains) : contains;
    }

    private static bool TryParseNumeric(string token, Type underlyingType, out object? parsedValue)
    {
        // decimal/double/float all parse via double first (matching BuildNumericComparison's own
        // isFloat path); everything else (int/long/short) parses as an integer - a malformed token
        // (e.g. "abc") fails gracefully here rather than throwing, consistent with this class's
        // documented "silently drop an unusable clause" convention.
        bool isFloat = underlyingType == typeof(decimal) || underlyingType == typeof(double) ||
                       underlyingType == typeof(float);

        if (isFloat)
        {
            if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out double doubleValue))
            {
                parsedValue = null;
                return false;
            }

            parsedValue = Convert.ChangeType(doubleValue, underlyingType, CultureInfo.InvariantCulture);
            return true;
        }

        if (!long.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out long longValue))
        {
            parsedValue = null;
            return false;
        }

        parsedValue = Convert.ChangeType(longValue, underlyingType, CultureInfo.InvariantCulture);
        return true;
    }

    private static Expression? BuildLikeExpression(Expression access, Type propertyType, string value, bool negate)
    {
        if (propertyType != typeof(string)) return null;

        string sqlPattern = value.Contains('*') ? value.Replace('*', '%') : $"%{value}%";

        MemberExpression functionsProperty = Expression.Property(null, typeof(EF), nameof(EF.Functions));
        MethodInfo likeMethod = typeof(DbFunctionsExtensions).GetMethod(
            nameof(DbFunctionsExtensions.Like),
            [typeof(DbFunctions), typeof(string), typeof(string)])!;

        MethodCallExpression likeCall =
            Expression.Call(likeMethod, functionsProperty, access, Expression.Constant(sqlPattern));
        return negate ? Expression.Not(likeCall) : likeCall;
    }

    private static Expression? BuildArrayExpression(Expression access, Type propertyType, string value, bool requireAll)
    {
        if (propertyType != typeof(string)) return null;

        string[] values = SplitCsv(value);
        if (values.Length == 0) return null;

        MethodInfo containsMethod = typeof(string).GetMethod(nameof(string.Contains), [typeof(string)])!;

        Expression? combined = null;
        foreach (string token in values)
        {
            ConstantExpression quotedToken = Expression.Constant($"\"{token}\"");
            MethodCallExpression containsCall = Expression.Call(access, containsMethod, quotedToken);
            combined = combined == null
                ? containsCall
                : requireAll
                    ? Expression.AndAlso(combined, containsCall)
                    : Expression.OrElse(combined, containsCall);
        }

        return combined;
    }

    private static Expression? BuildNumericComparison(Expression access, Type propertyType, string value,
        bool greaterThan, bool isFloat)
    {
        Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;
        if (!IsNumericType(underlyingType)) return null;

        object parsedValue = isFloat
            ? Convert.ChangeType(double.Parse(value, CultureInfo.InvariantCulture), underlyingType,
                CultureInfo.InvariantCulture)
            : Convert.ChangeType(long.Parse(value, CultureInfo.InvariantCulture), underlyingType,
                CultureInfo.InvariantCulture);

        ConstantExpression constant = Expression.Constant(parsedValue, propertyType);
        return greaterThan ? Expression.GreaterThan(access, constant) : Expression.LessThan(access, constant);
    }

    private static Expression? BuildDateComparison(Expression access, Type propertyType, string value, bool greaterThan)
    {
        Type underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        if (underlyingType == typeof(string))
        {
            // Most date-like fields in this codebase are stored as RFC3339 strings rather than
            // DateTime/DateTimeOffset - compare lexicographically, matching the same pattern already
            // used by the `since`/`until` query filters on several controllers.
            MethodInfo compareToMethod = typeof(string).GetMethod(nameof(string.CompareTo), [typeof(string)])!;
            MethodCallExpression compareCall = Expression.Call(access, compareToMethod, Expression.Constant(value));
            ConstantExpression zero = Expression.Constant(0);
            return greaterThan ? Expression.GreaterThan(compareCall, zero) : Expression.LessThan(compareCall, zero);
        }

        if (underlyingType == typeof(DateTime))
        {
            if (!DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal,
                    out DateTime dateTimeValue)) return null;

            ConstantExpression constant = Expression.Constant(dateTimeValue, propertyType);
            return greaterThan ? Expression.GreaterThan(access, constant) : Expression.LessThan(access, constant);
        }

        if (underlyingType == typeof(DateTimeOffset))
        {
            if (!DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal,
                    out DateTimeOffset dateTimeOffsetValue)) return null;

            ConstantExpression constant = Expression.Constant(dateTimeOffsetValue, propertyType);
            return greaterThan ? Expression.GreaterThan(access, constant) : Expression.LessThan(access, constant);
        }

        return null;
    }

    private static bool IsNumericType(Type type)
    {
        return type == typeof(int) || type == typeof(long) || type == typeof(short) ||
               type == typeof(decimal) || type == typeof(double) || type == typeof(float);
    }

    private static string[] SplitCsv(string value)
    {
        return value.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private static Expression<Func<T, bool>> Or<T>(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        ParameterExpression parameter = left.Parameters[0];
        Expression rightBody = new ReplaceParameterVisitor(right.Parameters[0], parameter).Visit(right.Body);
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(left.Body, rightBody), parameter);
    }

    private sealed class ReplaceParameterVisitor(ParameterExpression from, ParameterExpression to) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == from ? to : base.VisitParameter(node);
        }
    }
}
