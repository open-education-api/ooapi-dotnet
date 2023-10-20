using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
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