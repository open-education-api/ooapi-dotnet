namespace ooapi.v5.Models.Params
{
    public class FilterParams
    {
        /// <summary>
        /// Request entities meant for a specific consumer. This query parameter is independent from the &#x60;consumers&#x60; attribute. See the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        public string? consumer { get; set; }

        /// <summary>
        /// Filter by items having a name, abbreviation or description containing the given search term (exact partial match, case insensitive)
        /// </summary>
        public string? q { get; set; }

        ///// <summary>
        ///// Sort by one or more attributes, the default is ascending. Prefixing the attribute with a minus sign - allows for descending sort. Examples: [ATTR | -ATTR | ATTR1,-ATTR2]
        ///// </summary>
        //[JsonProperty("sort")]
        //public string Sort { get; set; }


    }
}
