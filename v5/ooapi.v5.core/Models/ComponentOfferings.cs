using Newtonsoft.Json;

namespace ooapi.v5.Models;


public class ComponentOfferings : Pagination<ComponentOffering>
{
    /// <summary>
    /// Array of objects (ComponentOffering) 
    /// </summary>
    /// <value>Array of objects (ComponentOffering) </value>
    [JsonRequired]

    [JsonProperty(PropertyName = "items")]
    public override List<ComponentOffering> Items
    {
        get
        {
            return _items;
        }
    }
}
