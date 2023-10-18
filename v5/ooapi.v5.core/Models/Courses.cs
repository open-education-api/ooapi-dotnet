using Newtonsoft.Json;

namespace ooapi.v5.Models;


public class Courses : Pagination<Course>
{
    /// <summary>
    /// Array of objects (Course) 
    /// </summary>
    /// <value>Array of objects (Course) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Course> Items
    {
        get
        {
            return _items;
        }
    }
}
