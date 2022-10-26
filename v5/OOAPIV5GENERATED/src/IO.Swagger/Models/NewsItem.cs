using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A newsitem contains the message and metadata of that message
    /// </summary>
    [DataContract]
    public partial class NewsItem
    {
        /// <summary>
        /// Unique id for this news item
        /// </summary>
        /// <value>Unique id for this news item</value>
        [JsonRequired]

        [DataMember(Name = "newsItemId")]
        public Guid NewsItemId { get; set; }



        /// <summary>
        /// The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging 
        /// </summary>
        /// <value>The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging </value>

        [DataMember(Name = "newsItemType")]
        public NewsItemTypeEnum? NewsItemType { get; set; }

        /// <summary>
        /// The name for this news item
        /// </summary>
        /// <value>The name for this news item</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The authors of the article with this news item
        /// </summary>
        /// <value>The authors of the article with this news item</value>

        [DataMember(Name = "authors")]
        public List<string> Authors { get; set; }

        /// <summary>
        /// The url containing the address of the image belonging to this news item
        /// </summary>
        /// <value>The url containing the address of the image belonging to this news item</value>

        [MaxLength(2048)]
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// The url containing the address of the article belonging to this news item
        /// </summary>
        /// <value>The url containing the address of the article belonging to this news item</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The content of this news item.
        /// </summary>
        /// <value>The content of this news item.</value>

        [DataMember(Name = "content")]
        public List<LanguageValueItem> Content { get; set; }

        /// <summary>
        /// The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)
        /// </summary>
        /// <value>The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)</value>

        [DataMember(Name = "newsFeeds")]
        public List<OneOfNewsFeed> NewsFeeds { get; set; }

        /// <summary>
        /// The moment from which this news item is valid, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment from which this news item is valid, RFC3339 (date-time)</value>

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The moment until which this news item is valid, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment until which this news item is valid, RFC3339 (date-time)</value>

        [DataMember(Name = "validUntil")]
        public DateTime? ValidUntil { get; set; }

        /// <summary>
        /// The moment on which this news item was updated, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this news item was updated, RFC3339 (date-time)</value>

        [DataMember(Name = "lastModified")]
        public DateTime? LastModified { get; set; }

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
