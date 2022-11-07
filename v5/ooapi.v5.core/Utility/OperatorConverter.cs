namespace ooapi.v5.core.Utility;

public static class OperatorConverter
{
    internal static Operator ToLogicalOperator(this string input)
    {
        switch (input.ToLower())
        {
            case "and":
                return Operator.And;
            case "or":
                return Operator.Or;
            default:
                return Operator.None;
        }
    }
    internal static string ToExpressionLogicaOperator(this Operator @operator)
    {
        switch (@operator)
        {
            case Operator.And:
                return "and";
            case Operator.Or:
                return "or";
            default:
                return "";
        }
    }
    internal static Operator ToValueOperator(this string input)
    {
        switch (input.ToLower())
        {
            case "eq":
                return Operator.EqualTo;
            case "ne":
                return Operator.NotEqualTo;
            case "gt":
                return Operator.GreaterThan;
            case "ge":
                return Operator.GreaterThanOrEqualTo;
            case "lt":
                return Operator.LessThan;
            case "le":
                return Operator.LessThanOrEqualTo;
            case "startswith":
                return Operator.StartsWith;
            case "endswith":
                return Operator.EndsWith;
            case "contains":
                return Operator.Contains;
            default:
                return Operator.None;
        }
    }

    internal static string ToExpressionValueOperator(this Operator @operator)
    {
        switch (@operator)
        {
            case Operator.EqualTo:
                return "eq";
            case Operator.NotEqualTo:
                return "ne";
            case Operator.GreaterThan:
                return "gt";
            case Operator.GreaterThanOrEqualTo:
                return "ge";
            case Operator.LessThan:
                return "lt";
            case Operator.LessThanOrEqualTo:
                return "le";
            case Operator.StartsWith:
                return "startswith";
            case Operator.EndsWith:
                return "endswith";
            case Operator.Contains:
                return "contains";
            default:
                return "";
        }
    }
    internal const string ValueOperatorPattern = @"(\beq\b)|(\bne\b)|(\bgt\b)|(\bge\b)|(\blt\b)|(\ble\b)|(\bendsWith\b)|(\bstartsWith\b)|(\bcontains\b)|(\bexists\b)";
    internal const string LogicalOperatorPattern = @"(\band\b)|(\bor\b)";
}
