using ooapi.v5.Attributes;
using System.Linq.Expressions;
using System.Reflection;

namespace ooapi.v5.core.Utility
{
    public static class OrderedQueryable
    {
        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source) where TEntity : class
        {
            return OrderBy<TEntity>(source, String.Empty);
        }

        public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByValues) where TEntity : class
        {
            IOrderedQueryable<TEntity> returnValue = null;
            var sortingPropertiesList = new List<(PropertyInfo Property, bool Asc)>();
            Expression resultExpression = source.Expression;
            string strAsc = "OrderBy";
            string strDesc = "OrderByDescending";
            Type type = typeof(TEntity);

            string[] orderByValuesArr = orderByValues.Trim().Split(',');
            string orderByValueTrimmed, command, propertyName;
            bool propertyFound = false;
            bool asc = true;

            foreach (string orderByValue in orderByValuesArr)
            {
                orderByValueTrimmed = orderByValue.Trim();

                if (string.IsNullOrWhiteSpace(orderByValueTrimmed))
                    continue;

                if (orderByValueTrimmed[0] == '-' && orderByValueTrimmed.Length > 1)
                {
                    asc = false;
                    propertyName = orderByValueTrimmed.Substring(1);
                }
                else
                {
                    asc = true;
                    propertyName = orderByValueTrimmed;
                }

                PropertyInfo[] typeProperties = type.GetProperties();
                propertyFound = false;
                foreach (var typeProperty in typeProperties)
                {
                    if (propertyName.ToLower() == typeProperty.Name.ToLower())
                    {
                        if (typeProperty.GetCustomAttribute<SortAllowedAttribute>() != null)
                        {
                            sortingPropertiesList.Add((typeProperty, asc));
                            propertyFound = true;
                            break;
                        }
                    }
                }

                if (!propertyFound)
                {
                    sortingPropertiesList = new List<(PropertyInfo, bool)>();
                    break;
                }
            }

            if (!propertyFound)
            {
                var typeProperty = type.GetProperties()
                   .FirstOrDefault(p => p.GetCustomAttribute<SortDefaultAttribute>() != null);
                if (typeProperty != null)
                {
                    asc = typeProperty.GetCustomAttribute<SortDefaultAttribute>().Asc;
                    sortingPropertiesList.Add((typeProperty, asc));
                    propertyFound = true;
                }
            }

            foreach (var sortingProperty in sortingPropertiesList)
            {
                Expression propertyAccess;
                ParameterExpression parameter = Expression.Parameter(type, "p");

                command = sortingProperty.Asc ? strAsc : strDesc;

                propertyAccess = Expression.MakeMemberAccess(parameter, sortingProperty.Property);

                if (sortingProperty.Property.PropertyType == typeof(object))
                {
                    propertyAccess = Expression.Call(propertyAccess, "ToString", null);
                }

                LambdaExpression orderByExpression = Expression.Lambda(propertyAccess, parameter);

                resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, sortingProperty.Property.PropertyType == typeof(object) ? typeof(string) : sortingProperty.Property.PropertyType },
                    resultExpression, Expression.Quote(orderByExpression));

                strAsc = "ThenBy";
                strDesc = "ThenByDescending";
            }

            returnValue = (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);

            return returnValue;
        }

        public static IOrderedQueryable<TEntity> FilterBy<TEntity>(this IQueryable<TEntity> source, Dictionary<string, object> filters) where TEntity : class
        {
            IOrderedQueryable<TEntity> returnValue;
            var filteringPropertiesList = new List<(PropertyInfo Property, object Value)>();
            Type type = typeof(TEntity);
            PropertyInfo[] typeProperties = type.GetProperties();
            bool propertyFound;

            foreach (var filter in filters)
            {
                propertyFound = false;
                foreach (var typeProperty in typeProperties)
                {
                    if (filter.Key.Trim().ToLower() == typeProperty.Name.ToLower())
                    {
                        filteringPropertiesList.Add((typeProperty, filter.Value.GetType() == typeof(string) ? filter.Value.ToString().Trim().ToLower() : filter.Value));
                        propertyFound = true;
                        break;
                    }
                }

                if (!propertyFound)
                {
                    throw new Exception($"'{filter.Key}' is not a filterable field. Remove it from the filter parameter and call the API again");
                }
            }

            IQueryable<TEntity> expTest = source;

            foreach (var filteringProperty in filteringPropertiesList)
            {
                Expression propertyExpression;
                ParameterExpression parameter = Expression.Parameter(type, "p");

                propertyExpression = Expression.MakeMemberAccess(parameter, filteringProperty.Property); // for example: {p.Type}

                if (filteringProperty.Property.PropertyType == typeof(object))
                {
                    propertyExpression = Expression.Call(propertyExpression, "ToString", null);
                }

                Expression targetExpression = Expression.Constant(filteringProperty.Value); // for example: {"root"}
                if (filteringProperty.Value.GetType() != typeof(string))
                {
                    targetExpression = Expression.Convert(targetExpression, typeof(object));
                }
                MethodInfo equalsMethod = type.GetMethod("Equals", new[] { type });
                Expression equalsExpressionRight = Expression.Call(propertyExpression, equalsMethod, targetExpression); // for example: {p.Type.Equals("root")}
                var equalsLambda = Expression.Lambda<Func<TEntity, bool>>(equalsExpressionRight, parameter); // for example: {p => p.Type.Equals("root")}
                expTest = expTest.Where(equalsLambda); // for example: {[Microsoft.EntityFrameworkCore.Query.QueryRootExpression].OrderBy(p => p.Name).Where(p => p.Type.Equals("root"))}
            }

            returnValue = (IOrderedQueryable<TEntity>)expTest;

            return returnValue;
        }

        public static IOrderedQueryable<TEntity> SearchBy<TEntity>(this IQueryable<TEntity> source, string searchTerm) where TEntity : class
        {
            IOrderedQueryable<TEntity> returnValue;
            var searchingPropertiesList = new List<PropertyInfo>();
            Type type = typeof(TEntity);
            PropertyInfo[] typeProperties = type.GetProperties();
            searchTerm = searchTerm.Trim().ToLower();

            foreach (var typeProperty in typeProperties)
            {
                if (typeProperty.GetCustomAttribute<SearchableAttribute>() != null)
                {
                    searchingPropertiesList.Add(typeProperty);
                }
            }

            if (searchingPropertiesList.Count == 0 && searchTerm.Length > 0)
            {
                throw new Exception($"'There are no searchable fields defined for this endpoint. Clear the search parameter and call the API again");
            }

            IQueryable<TEntity> expTest = source;
            ParameterExpression parameter = Expression.Parameter(type, "p"); // for example: {p}
            Expression orElseExpressionRight = null;
            bool isFirstSearchProperty = true;

            foreach (var searchProperty in searchingPropertiesList)
            {
                try
                {
                    Expression propertyExpression;

                    propertyExpression = Expression.MakeMemberAccess(parameter, searchProperty); // for example: {p.Name}

                    if (searchProperty.PropertyType == typeof(object))
                    {
                        propertyExpression = Expression.Call(propertyExpression, "ToString", null);
                    }

                    Expression targetExpression = Expression.Constant(searchTerm); // for example: {"abc"}
                    MethodInfo toLowerMethod = typeof(string).GetMethod("ToLower", new Type[] { });
                    Expression toLowerExpressionRight = Expression.Call(propertyExpression, toLowerMethod); // for example: {p.Name.ToLower()}
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                    Expression containsExpressionRight = Expression.Call(toLowerExpressionRight, containsMethod, targetExpression); // for example: {p.Name.ToLower().Contains("abc")}
                    if (isFirstSearchProperty)
                    {
                        orElseExpressionRight = containsExpressionRight;
                    }
                    else
                    {
                        orElseExpressionRight = Expression.OrElse(orElseExpressionRight, containsExpressionRight); // for example: {p.Name.ToLower().Contains("abc") || p.Description.ToLower().Contains("abc")} 
                    }
                    isFirstSearchProperty = false;
                }
                catch (Exception)
                {
                    // The Searchable attribute is probably defined on a non-string property that doesn't have a ToLower and/or Contains method.
                }
            }

            if (orElseExpressionRight != null)
            {
                var equalsLambda = Expression.Lambda<Func<TEntity, bool>>(orElseExpressionRight, parameter); // for example: {p => p.Name.ToLower().Contains("abc") || p.Description.ToLower().Contains("abc")}
                expTest = expTest.Where(equalsLambda);
            }

            returnValue = (IOrderedQueryable<TEntity>)expTest;

            return returnValue;
        }
    }
}
