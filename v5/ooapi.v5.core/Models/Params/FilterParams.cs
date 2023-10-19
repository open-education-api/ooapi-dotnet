namespace ooapi.v5.Models.Params;

public class FilterParams
{
    /// <summary>
    /// Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism. <br/>
    /// When this query parameter is specified, the implementation will only return items needed for this consumer
    /// </summary>
    public string? consumer { get; set; }

    /// <summary>
    /// Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)
    /// </summary>
    public string? q { get; set; }

}
