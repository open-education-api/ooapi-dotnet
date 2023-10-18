using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace ooapi.v5.core.Utility;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class FilterToLinq<T> : IFilterToDataAccess<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterExpression"></param>
    public FilterToLinq(string filterExpression)
    {
        FilterExpression = filterExpression;
    }

    private string FilterExpression { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    /// <returns></returns>
    public IQueryable<T> Parse(IQueryable<T> collection)
    {
        var filterExpression = BuildExpression(BuildFilterCollection());
        if (filterExpression == null)
        {
            return collection;
        }
        return collection.Where(filterExpression);
    }

    private IEnumerable<FilterNode> BuildFilterCollection()
    {
        return FilterExpression.ToFilterNodeCollection();
    }

    private Expression<Func<T, bool>>? BuildExpression(IEnumerable<FilterNode> filterNodes)
    {
        if (filterNodes == null || !filterNodes.Any())
        {
            return null;
        }

        var param = Expression.Parameter(typeof(T), "t");
        Expression? exp = null;

        var logicalOperator = Operator.None;
        foreach (var node in filterNodes)
        {
            var expression = GetExpression(param, node.Filter);
            if (expression != null)
            {
                if (exp == null)
                {
                    exp = expression;
                }
                else if (logicalOperator == Operator.And)
                {
                    exp = Expression.AndAlso(exp, expression);
                }
                else if (logicalOperator == Operator.Or)
                {
                    exp = Expression.Or(exp, expression);
                }

                logicalOperator = node.NextOperator;

                if (logicalOperator == Operator.None)
                {
                    break;
                }
            }
        }

        if (exp is null)
        {
            return null;
        }

        return Expression.Lambda<Func<T, bool>>(exp, param);
    }

    private Expression? GetExpression(ParameterExpression param, Filter filter)
    {
        var member = Expression.Property(param, filter.PropertyName);

        if (member is null)
        {
            return null;
        }
        else if (member.Member.CustomAttributes.Any(x => x.AttributeType.Name == "NotFilterableAttribute"))
        {
            throw new FormatException($"'{filter.PropertyName}' is not filterable. Remove it from the filter parameter and call the API again");
        }

        var constant = GetExpressionConstant(filter.Value, member.Type);

        return filter.Operation switch
        {
            Operator.EqualTo => Expression.Equal(member, constant),
            Operator.NotEqualTo => Expression.NotEqual(member, constant),
            Operator.GreaterThan => Expression.GreaterThan(member, constant),
            Operator.GreaterThanOrEqualTo => Expression.GreaterThanOrEqual(member, constant),
            Operator.LessThan => Expression.LessThan(member, constant),
            Operator.LessThanOrEqualTo => Expression.LessThanOrEqual(member, constant),
            Operator.Contains => Expression.Call(member, containsMethod, constant),
            Operator.StartsWith => Expression.Call(member, startsWithMethod, constant),
            Operator.EndsWith => Expression.Call(member, endsWithMethod, constant),
            _ => null,
        };
    }

    private static ConstantExpression GetExpressionConstant(object filterValue, Type memberType)
    {
        if (!((string)filterValue).ToLower().Equals("null") && (IsNullable(memberType)))
        {
            if (memberType.Equals(typeof(DateTime?)))
            {
                return Expression.Constant(DateTime.Parse((string)filterValue, DateTimeFormatInfo.InvariantInfo), typeof(DateTime?));
            }
            else if (memberType.Equals(typeof(bool?)))
            {
                return Expression.Constant((bool?)Convert.ToBoolean(filterValue), typeof(bool?));
            }
            else if (memberType.Equals(typeof(int?)))
            {
                return Expression.Constant((int?)Convert.ToInt32(filterValue), typeof(int?));
            }
            else if (memberType.Equals(typeof(short?)))
            {
                return Expression.Constant((short?)Convert.ToInt16(filterValue), typeof(short?));
            }
            else if (memberType.Equals(typeof(long?)))
            {
                return Expression.Constant((long?)Convert.ToInt64(filterValue), typeof(long?));
            }
            else if (memberType.Equals(typeof(double?)))
            {
                return Expression.Constant((double?)Convert.ToDouble(filterValue), typeof(double?));
            }
            else if (memberType.Equals(typeof(float?)))
            {
                return Expression.Constant((float?)filterValue, typeof(float?));
            }
            else if (memberType.Equals(typeof(Guid?)))
            {
                return Expression.Constant((Guid?)(filterValue), typeof(Guid?));
            }
            return Expression.Constant(Filter.TryParseToType(filterValue, memberType));
        }
        else
        {
            return Expression.Constant(Filter.TryParseToType(filterValue, memberType));
        }
    }

    private static bool IsNullable<X>(X obj)
    {
        if (obj == null)
        {
            return true; // obvious
        }

        var type = typeof(X);
        if (!type.IsValueType)
        {
            return true; // ref-type
        }

        if (Nullable.GetUnderlyingType(type) != null)
        {
            return true;
        }

        return false; // value-type
    }

    private readonly MethodInfo containsMethod = typeof(string).GetMethod("Contains")!;
    private readonly MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) })!;
    private readonly MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) })!;
}
