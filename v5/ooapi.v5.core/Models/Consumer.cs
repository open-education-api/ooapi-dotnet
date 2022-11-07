using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// Object for communicating data to a specific consumer (destination). This object has no relationship with the &#x60;consumer&#x60; query parameter.
    /// </summary>
    [DataContract(Name = "Consumer")]
    public class Consumer
    {

        /// <summary>
        /// Unique id of this consumer
        /// </summary>
        /// <value>Unique id of this consumer</value>
        [JsonIgnore]
        [JsonProperty("consumerId")]
        public Guid ConsumerId { get; set; }


        /// <summary>
        /// The key of the consumer (destination) for which this information is intended. <br/>
        /// See the <![CDATA[<a href="https://open-education-api.github.io/specification/#/consumers" >consumer registry</a>]]> for more information.
        /// </summary>
        [JsonRequired]

        [JsonProperty(PropertyName = "consumerKey")]
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or Sets additional
        /// </summary>
        [JsonProperty(PropertyName = "propertyName")]
        public string PropertyName { get; set; }



    }
}
