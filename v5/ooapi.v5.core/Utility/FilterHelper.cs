using System.Text;
using System.Text.RegularExpressions;

namespace ooapi.v5.core.Utility;


public static class FilterHelper
{
    internal static IEnumerable<FilterNode> ToFilterNodeCollection(this string filter)
    {
        var result = new List<FilterNode>();
        var filterexpressions = Regex.Split(filter, OperatorConverter.LogicalOperatorPattern, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2));
        for (var i = 0; i < filterexpressions.Length; i += 2)
        {
            var keyValues = Regex.Split(filterexpressions[i], OperatorConverter.ValueOperatorPattern, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2));

            if (keyValues.Length == 3)
            {
                var operatorIndex = i + 1;
                result.Add(
                    new FilterNode
                    {
                        Filter = new Filter
                        {
                            PropertyName = keyValues[0].Trim(),
                            Operation = keyValues[1].Trim().ToValueOperator(),
                            Value = keyValues[2].Trim()
                        },
                        NextOperator = (operatorIndex >= filterexpressions.Length ? "" : filterexpressions[operatorIndex]).ToLogicalOperator()
                    });
            }
        }
        return result;
    }


    /// <param name="filter"></param>
    /// <param name="noneFilterablePropertyNames"></param>
    /// <returns></returns>
    public static string ToAllowedFilterExpression(this string filter, IEnumerable<string> noneFilterablePropertyNames)
    {
        var allowedFilterCollection = filter.ToFilterNodeCollection().Where(x => !noneFilterablePropertyNames.Contains(x.Filter.PropertyName));

        var result = new StringBuilder();
        var previousOperator = Operator.None;
        foreach (var allowedFilter in allowedFilterCollection)
        {
            var operatorExpression = previousOperator.ToExpressionLogicaOperator();
            result.Append($" {operatorExpression} {allowedFilter.Filter.PropertyName} {allowedFilter.Filter.Operation.ToExpressionValueOperator()} {allowedFilter.Filter.Value}");
            previousOperator = allowedFilter.NextOperator;
        }

        return result.ToString().Replace("  ", " ").Trim();
    }
}
