using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage]
public class Persons : Pagination<Person>
{
    /// <summary>
    /// Array of objects (Person) 
    /// </summary>
    /// <value>Array of objects (Person) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Person> Items
    {
        get
        {
            return _items;
        }
    }
}
