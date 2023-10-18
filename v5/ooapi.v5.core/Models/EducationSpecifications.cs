using Newtonsoft.Json;

namespace ooapi.v5.Models;

public class EducationSpecifications : Pagination<EducationSpecification>
{
    /// <summary>
    /// Array of objects (EducationSpecification) 
    /// </summary>
    /// <value>Array of objects (EducationSpecification) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<EducationSpecification> Items
    {
        get
        {
            return _items;
        }
    }
}
