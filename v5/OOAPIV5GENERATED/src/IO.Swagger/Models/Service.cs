using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A metadataset providing details on the provider of this OOAPI implementation
    /// </summary>
    [DataContract]
    public partial class Service
    {
        /// <summary>
        /// Contact e-mail address of the service owner
        /// </summary>
        /// <value>Contact e-mail address of the service owner</value>
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "contactEmail")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// URL of the API specification (YAML or JSON, compliant with [Open API Specification v3](https://github.com/OAI/OpenAPI-Specification/))
        /// </summary>
        /// <value>URL of the API specification (YAML or JSON, compliant with [Open API Specification v3](https://github.com/OAI/OpenAPI-Specification/))</value>
        [JsonRequired]

        [MaxLength(2048)]
        [DataMember(Name = "specification")]
        public string Specification { get; set; }

        /// <summary>
        /// URL of the API documentation, including general terms and privacy statement
        /// </summary>
        /// <value>URL of the API documentation, including general terms and privacy statement</value>
        [JsonRequired]

        [MaxLength(2048)]
        [DataMember(Name = "documentation")]
        public string Documentation { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }
    }
}
