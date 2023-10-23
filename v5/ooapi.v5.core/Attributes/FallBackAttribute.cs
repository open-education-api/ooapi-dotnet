using System.Diagnostics.CodeAnalysis;

namespace ooapi.v5.Attributes;

[ExcludeFromCodeCoverage(Justification = "Not used")]
[AttributeUsage(AttributeTargets.Property)]
public class FallBackAttribute : Attribute
{
    private readonly object _item;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public FallBackAttribute(object item)
    {
        _item = item;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual object Value
    {
        get { return _item; }
    }

}
