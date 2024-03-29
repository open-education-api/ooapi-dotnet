using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class Buildings : Pagination<Building>
{
    /// <summary>
    /// Array of objects (Building) 
    /// </summary>
    /// <value>Array of objects (Building) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Building> Items
    {
        get
        {
            return _items;
        }
    }
}