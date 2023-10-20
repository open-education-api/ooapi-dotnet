using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
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