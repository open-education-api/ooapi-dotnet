using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage]
public class Programs : Pagination<Program>
{
    /// <summary>
    /// Array of objects (Program) 
    /// </summary>
    /// <value>Array of objects (Program) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<Program> Items
    {
        get
        {
            return _items;
        }
    }
}
