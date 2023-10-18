using Newtonsoft.Json;

namespace ooapi.v5.Models;

/// <summary>
/// 
/// </summary>
public class AcademicSessions : Pagination<AcademicSession>
{
    /// <summary>
    /// Array of objects (AcademicSession) 
    /// </summary>
    /// <value>Array of objects (AcademicSessions) </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "items")]
    public override List<AcademicSession> Items
    {
        get
        {
            return _items;
        }
    }
}
