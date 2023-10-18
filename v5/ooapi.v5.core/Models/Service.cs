using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// A metadataset providing details on the provider of this OOAPI implementation
/// </summary>
[DataContract]
public partial class Service : ModelBase
{
    /// <summary>
    /// Unique id
    /// </summary>
    /// <value>Unique id</value>
    [JsonIgnore]
    public Guid ServiceId { get; set; }

    /// <summary>
    /// Contact e-mail address of the service owner
    /// </summary>
    /// <value>Contact e-mail address of the service owner</value>
    [JsonRequired]

    [MaxLength(256)]
    [JsonProperty(PropertyName = "contactEmail")]
    public string ContactEmail { get; set; }

    /// <summary>
    /// URL of the API specification (YAML or JSON, compliant with [Open API Specification v3](https://github.com/OAI/OpenAPI-Specification/))
    /// </summary>
    /// <value>URL of the API specification (YAML or JSON, compliant with [Open API Specification v3](https://github.com/OAI/OpenAPI-Specification/))</value>
    [JsonRequired]

    [MaxLength(2048)]
    [JsonProperty(PropertyName = "specification")]
    public string Specification { get; set; }

    /// <summary>
    /// URL of the API documentation, including general terms and privacy statement
    /// </summary>
    /// <value>URL of the API documentation, including general terms and privacy statement</value>
    [JsonRequired]

    [MaxLength(2048)]
    [JsonProperty(PropertyName = "documentation")]
    public string Documentation { get; set; }

    /// <summary>
    /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
    /// </summary>
    /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

    [JsonProperty(PropertyName = "consumers")]
    [NotMapped]
    public List<JObject>? ConsumersList
    {
        get
        {
            if (Consumers != null && Consumers.Any())
            {
                return ConsumerConverter.GetDynamicConsumers(Consumers);
            }

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public List<Consumer> Consumers { get; set; } = default!;
}
