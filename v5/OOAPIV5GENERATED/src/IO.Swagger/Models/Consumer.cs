using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// Object for communicating data to a specific consumer (destination). This object has no relationship with the &#x60;consumer&#x60; query parameter.
    /// </summary>
    [DataContract(Name = "Consumer")]
    public partial class Consumer
    {


        /// <summary>
        /// The key of the consumer (destination) for which this information is intended. <br/>
        /// See the <![CDATA[<a href="https://open-education-api.github.io/specification/#/consumers" >consumer registry</a>]]> for more information.
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "consumerKey")]
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or Sets additional
        /// </summary>
        [DataMember(Name = "propertyName")]
        public string PropertyName { get; set; }



    }
}
