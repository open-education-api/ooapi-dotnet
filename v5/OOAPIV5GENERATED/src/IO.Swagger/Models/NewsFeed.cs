using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A placeholder or collection of news items
    /// </summary>
    [DataContract]
    public partial class NewsFeed
    {
        /// <summary>
        /// Unique id for this news feed
        /// </summary>
        /// <value>Unique id for this news feed</value>
        [JsonRequired]

        [DataMember(Name = "newsFeedId")]
        public Guid NewsFeedId { get; set; }



        /// <summary>
        /// The type of the object this news feed relates to
        /// </summary>
        /// <value>The type of the object this news feed relates to</value>
        [JsonRequired]

        [DataMember(Name = "newsFeedType")]
        public NewsFeedTypeEnum? NewsFeedType { get; set; }

        /// <summary>
        /// The name for this news feed
        /// </summary>
        /// <value>The name for this news feed</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The description of this news feed.
        /// </summary>
        /// <value>The description of this news feed.</value>
        [JsonRequired]

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

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
