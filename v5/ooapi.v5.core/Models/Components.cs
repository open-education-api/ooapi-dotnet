using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class Components : Pagination<Component>
{
    /// <summary>
    /// Array of objects (Component) 
    /// </summary>
    /// <value>Array of objects (Component) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Component> Items
    {
        get
        {
            return _items;
        }
    }
}