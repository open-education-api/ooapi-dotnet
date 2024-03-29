using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class CourseOfferings : Pagination<CourseOffering>
{
    /// <summary>
    /// Array of objects (CourseOffering) 
    /// </summary>
    /// <value>Array of objects (CourseOffering) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<CourseOffering> Items
    {
        get
        {
            return _items;
        }
    }
}