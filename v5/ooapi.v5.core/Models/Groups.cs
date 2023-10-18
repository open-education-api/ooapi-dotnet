using Newtonsoft.Json;

namespace ooapi.v5.Models;


public class Groups : Pagination<Group>
{
    /// <summary>
    /// Array of objects (Group) 
    /// </summary>
    /// <value>Array of objects (Group) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Group> Items
    {
        get
        {
            return _items;
        }
    }
}
