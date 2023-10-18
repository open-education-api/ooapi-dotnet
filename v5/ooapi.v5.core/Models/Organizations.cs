using Newtonsoft.Json;

namespace ooapi.v5.Models;


public class Organizations : Pagination<Organization>
{
    /// <summary>
    /// Array of objects (Organization) 
    /// </summary>
    /// <value>Array of objects (Organization) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Organization> Items
    {
        get
        {
            return _items;
        }
    }
}
