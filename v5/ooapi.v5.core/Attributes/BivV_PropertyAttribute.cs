using System.Diagnostics.CodeAnalysis;

namespace ooapi.v5.Attributes;

[ExcludeFromCodeCoverage(Justification = "Not used")]
[AttributeUsage(AttributeTargets.Property)]
public class BivVAttribute : Attribute
{
    private readonly bool _laag = false;
    private readonly bool _middel = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="middel"></param>
    /// <param name="laag"></param>
    public BivVAttribute(bool middel = false, bool laag = false)
    {
        _middel = middel;
        _laag = laag;
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual bool Middel
    {
        get { return _middel; }
    }

    /// <summary>
    /// 
    /// </summary>
    public virtual bool Laag
    {
        get { return _laag; }
    }
}
