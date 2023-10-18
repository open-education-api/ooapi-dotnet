namespace ooapi.v5.core.Utility;

internal class FilterNode
{
    /// <summary>
    /// 
    /// </summary>
    public Filter Filter { get; set; } = default!;
    public Operator NextOperator { get; set; }
}
