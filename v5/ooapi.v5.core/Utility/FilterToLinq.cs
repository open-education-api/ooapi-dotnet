using System.Linq.Expressions;
using System.Reflection;

namespace ooapi.v5.core.Utility

{
    public class FilterToLinq<T> : IFilterToDataAccess<T>
    {
        public FilterToLinq(string filterExpression)
        {
            FilterExpression = filterExpression;
        }
        string FilterExpression { get; }

        public IQueryable<T> Parse(IQueryable<T> collection)
        {

            var filterExpression = BuildExpression(BuildFilterCollection());
            if (filterExpression == null)
            {
                return collection;
            }
            return collection.Where(filterExpression);
        }

        IEnumerable<FilterNode> BuildFilterCollection()
        {
            return FilterExpression.ToFilterNodeCollection();
        }

        Expression<Func<T, bool>> BuildExpression(IEnumerable<FilterNode> filterNodes)
        {
            if (filterNodes == null || !filterNodes.Any()) { return null; }

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            var logicalOperator = Operator.None;
            foreach (var node in filterNodes)
            {
                var expression = GetExpression(param, node.Filter);
                if (expression != null)
                {
                    if (exp == null) { exp = expression; }
                    else if (logicalOperator == Operator.And)
                    {
                        exp = Expression.AndAlso(exp, expression);
                    }
                    else if (logicalOperator == Operator.Or)
                    {
                        exp = Expression.Or(exp, expression);
                    }
                    logicalOperator = node.NextOperator;

                    if (logicalOperator == Operator.None) { break; }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        Expression GetExpression(ParameterExpression param, Filter filter)
        {
            MemberExpression member = Expression.Property(param, filter.PropertyName);
            if (member != null)
            {
                if (member.Member.CustomAttributes.Any(x => x.AttributeType.Name == "NotFilterableAttribute"))
                {
                    throw new Exception($"'{filter.PropertyName}' is not filterable. Remove it from the filter parameter and call the API again");
                }
            }

            ConstantExpression constant = GetExpressionConstant(filter.Value, member.Type);

            switch (filter.Operation)
            {
                case Operator.EqualTo:
                    return Expression.Equal(member, constant);
                case Operator.NotEqualTo:
                    return Expression.NotEqual(member, constant);
                case Operator.GreaterThan:
                    return Expression.GreaterThan(member, constant);
                case Operator.GreaterThanOrEqualTo:
                    return Expression.GreaterThanOrEqual(member, constant);
                case Operator.LessThan:
                    return Expression.LessThan(member, constant);
                case Operator.LessThanOrEqualTo:
                    return Expression.LessThanOrEqual(member, constant);
                case Operator.Contains:
                    return Expression.Call(member, containsMethod, constant);
                case Operator.StartsWith:
                    return Expression.Call(member, startsWithMethod, constant);
                case Operator.EndsWith:
                    return Expression.Call(member, endsWithMethod, constant);
                default:
                    return null;
            }
        }

        private ConstantExpression GetExpressionConstant(object filterValue, Type memberType)
        {
            if (!((String)filterValue).ToLower().Equals("null") && (IsNullable(memberType)))
            {
                if (memberType.Equals(typeof(DateTime?)))
                {
                    return Expression.Constant(DateTime.Parse((String)filterValue), typeof(DateTime?));
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
                return Expression.Constant(Filter.TryParseToType(filterValue, memberType));
        }

        private static bool IsNullable<X>(X obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(X);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true;
            return false; // value-type
        }

        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
    }
}
