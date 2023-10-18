using Newtonsoft.Json;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
public class ProgramOfferings : Pagination<ProgramOffering>
{
    /// <summary>
    /// Array of objects (ProgramOffering) 
    /// </summary>
    /// <value>Array of objects (ProgramOffering) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<ProgramOffering> Items
    {
        get
        {
            return _items;
        }
    }
}
