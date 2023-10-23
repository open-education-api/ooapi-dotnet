namespace ooapi.v5.core.Utility;

internal class FilterNode
{

    public Filter Filter { get; set; } = default!;
    public Operator NextOperator { get; set; }
}
