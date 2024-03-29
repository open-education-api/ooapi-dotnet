using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class Associations : Pagination<Association>
{
    /// <summary>
    /// Array of objects (Association) 
    /// </summary>
    /// <value>Array of objects (Association) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Association> Items
    {
        get
        {
            return _items;
        }
    }
}