using Microsoft.AspNetCore.Mvc.Formatters;
using ooapi.v5.Attributes;
using System.Linq.Expressions;
using System.Reflection;

namespace ooapi.v5.core.Utility;

public static class OrderedQueryable
{
    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source) where TEntity : class
    {
        return OrderBy(source, string.Empty);
    }

    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByValues) where TEntity : class
    {
        IOrderedQueryable<TEntity>? returnValue = null;
        var sortingPropertiesList = new List<(PropertyInfo Property, bool Asc)>();
        var resultExpression = source.Expression;
        var strAsc = "OrderBy";
        var strDesc = "OrderByDescending";
        var type = typeof(TEntity);

        var orderByValuesArr = orderByValues.Trim().Split(',');
        string propertyName;
        var propertyFound = false;
        var asc = true;

        foreach (var orderByValue in orderByValuesArr)
        {
            var orderByValueTrimmed = orderByValue.Trim();

            if (string.IsNullOrWhiteSpace(orderByValueTrimmed))
            {
                continue;
            }

            if (orderByValueTrimmed[0] == '-' && orderByValueTrimmed.Length > 1)
            {
                asc = false;
                propertyName = orderByValueTrimmed[1..];
            }
            else
            {
                asc = true;
                propertyName = orderByValueTrimmed;
            }

            var typeProperties = type.GetProperties();
            propertyFound = false;
            foreach (var typeProperty in typeProperties)
            {
                if (propertyName.ToLower() == typeProperty.Name.ToLower() && typeProperty.GetCustomAttribute<SortAllowedAttribute>() != null)
                {
                    sortingPropertiesList.Add((typeProperty, asc));
                    propertyFound = true;
                    break;
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
            var typeProperty = Array.Find(type.GetProperties(), p => p.GetCustomAttribute<SortDefaultAttribute>() != null);
            if (typeProperty is not null)
            {
                var customattribute = typeProperty.GetCustomAttribute<SortDefaultAttribute>();
                if (customattribute is not null)
                {
                    asc = customattribute.Asc;
                    sortingPropertiesList.Add((typeProperty, asc));
                }
            }
        }

        foreach (var (Property, Asc) in sortingPropertiesList)
        {
            Expression propertyAccess;
            var parameter = Expression.Parameter(type, "p");

            var command = Asc ? strAsc : strDesc;

            propertyAccess = Expression.MakeMemberAccess(parameter, Property);

            if (Property.PropertyType == typeof(object))
            {
                propertyAccess = Expression.Call(propertyAccess, "ToString", null);
            }

            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, Property.PropertyType == typeof(object) ? typeof(string) : Property.PropertyType },
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
        var type = typeof(TEntity);
        var typeProperties = type.GetProperties();
        bool propertyFound;

        foreach (var filter in filters)
        {
            propertyFound = false;
            foreach (var typeProperty in typeProperties)
            {
                if (filter.Key.Trim().ToLower() == typeProperty.Name.ToLower())
                {
                    filteringPropertiesList.Add((typeProperty, (filter.Value is string) ? filter.Value.ToString()!.Trim().ToLower() : filter.Value));
                    propertyFound = true;
                    break;
                }
            }

            if (!propertyFound)
            {
                throw new FormatException($"'{filter.Key}' is not a filterable field. Remove it from the filter parameter and call the API again");
            }
        }

        var expTest = source;

        foreach (var (Property, Value) in filteringPropertiesList)
        {
            Expression propertyExpression;
            var parameter = Expression.Parameter(type, "p");

            propertyExpression = Expression.MakeMemberAccess(parameter, Property); // for example: {p.Type}

            if (Property.PropertyType == typeof(object))
            {
                propertyExpression = Expression.Call(propertyExpression, "ToString", null);
            }

            Expression targetExpression = Expression.Constant(Value); // for example: {"root"}
            if (Value is not string)
            {
                targetExpression = Expression.Convert(targetExpression, typeof(object));
            }
            var equalsMethod = type.GetMethod("Equals", new[] { type })!;
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
        var type = typeof(TEntity);
        var typeProperties = type.GetProperties();
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
            throw new InputFormatterException($"'There are no searchable fields defined for this endpoint. Clear the search parameter and call the API again");
        }

        var expTest = source;
        var parameter = Expression.Parameter(type, "p"); // for example: {p}
        Expression? orElseExpressionRight = null;
        var isFirstSearchProperty = true;

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
                var toLowerMethod = typeof(string).GetMethod("ToLower", Array.Empty<Type>());
                Expression toLowerExpressionRight = Expression.Call(propertyExpression, toLowerMethod!); // for example: {p.Name.ToLower()}
                var containsMethod = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                Expression containsExpressionRight = Expression.Call(toLowerExpressionRight, containsMethod!, targetExpression); // for example: {p.Name.ToLower().Contains("abc")}
                if (isFirstSearchProperty)
                {
                    orElseExpressionRight = containsExpressionRight;
                }
                else if (orElseExpressionRight is not null)
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