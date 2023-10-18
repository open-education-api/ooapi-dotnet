namespace ooapi.v5.core.Utility;

/// <summary>
/// 
/// </summary>
public static class OperatorConverter
{
    internal static Operator ToLogicalOperator(this string input)
    {
        return input.ToLower() switch
        {
            "and" => Operator.And,
            "or" => Operator.Or,
            _ => Operator.None,
        };
    }

    internal static string ToExpressionLogicaOperator(this Operator @operator)
    {
        return @operator switch
        {
            Operator.And => "and",
            Operator.Or => "or",
            _ => "",
        };
    }

    internal static Operator ToValueOperator(this string input)
    {
        return input.ToLower() switch
        {
            "eq" => Operator.EqualTo,
            "ne" => Operator.NotEqualTo,
            "gt" => Operator.GreaterThan,
            "ge" => Operator.GreaterThanOrEqualTo,
            "lt" => Operator.LessThan,
            "le" => Operator.LessThanOrEqualTo,
            "startswith" => Operator.StartsWith,
            "endswith" => Operator.EndsWith,
            "contains" => Operator.Contains,
            _ => Operator.None,
        };
    }

    internal static string ToExpressionValueOperator(this Operator @operator)
    {
        return @operator switch
        {
            Operator.EqualTo => "eq",
            Operator.NotEqualTo => "ne",
            Operator.GreaterThan => "gt",
            Operator.GreaterThanOrEqualTo => "ge",
            Operator.LessThan => "lt",
            Operator.LessThanOrEqualTo => "le",
            Operator.StartsWith => "startswith",
            Operator.EndsWith => "endswith",
            Operator.Contains => "contains",
            _ => "",
        };
    }

    internal const string ValueOperatorPattern = @"(\beq\b)|(\bne\b)|(\bgt\b)|(\bge\b)|(\blt\b)|(\ble\b)|(\bendsWith\b)|(\bstartsWith\b)|(\bcontains\b)|(\bexists\b)";
    internal const string LogicalOperatorPattern = @"(\band\b)|(\bor\b)";
}
