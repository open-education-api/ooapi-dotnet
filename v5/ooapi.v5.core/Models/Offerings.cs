using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using ooapi.v5.core.Models.OneOfModels;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class Offerings : Pagination<OneOfOfferingNoIdentifier>
{
    /// <summary>
    /// Array of objects (Offering) 
    /// </summary>
    /// <value>Array of objects (Offering) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<OneOfOfferingNoIdentifier> Items
    {
        get
        {
            return _items;
        }
    }
}