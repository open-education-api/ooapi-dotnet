using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A newsitem contains the message and metadata of that message
    /// </summary>
    [DataContract]
    public partial class NewsItem : ModelBase
    {
        /// <summary>
        /// Unique id for this news item
        /// </summary>
        /// <value>Unique id for this news item</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "newsItemId")]
        public Guid NewsItemId { get; set; }



        /// <summary>
        /// The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging 
        /// </summary>
        /// <value>The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging </value>

        [JsonProperty(PropertyName = "newsItemType")]
        public NewsItemTypeEnum? NewsItemType { get; set; }

        /// <summary>
        /// The name for this news item
        /// </summary>
        /// <value>The name for this news item</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "name")]
        [NotMapped]
        public List<LanguageTypedString> name
        {
            get
            {
                return Helpers.JsonConverter.GetLanguageTypesStringList(Name);
            }
            set
            {
                if (value != null)
                    Name = JsonConvert.SerializeObject(value);
            }
        }


        [JsonIgnore]
        public string Name { get; set; }



        [JsonIgnore]
        public string Authors { get; set; }

        /// <summary>
        /// The authors of the article with this news item
        /// </summary>
        /// <value>The authors of the article with this news item</value>

        [DataMember(Name = "authors")]
        [NotMapped]
        public List<string>? authors { get; set; }


        ////[JsonIgnore]
        ////public List<Author> Authors { get; set; }

        /////// <summary>
        /////// The authors of the article with this news item
        /////// </summary>
        /////// <value>The authors of the article with this news item</value>

        ////[JsonProperty(PropertyName = "authors")]
        ////[NotMapped]
        ////public List<string> authors { get; set; }


        /// <summary>
        /// The url containing the address of the image belonging to this news item
        /// </summary>
        /// <value>The url containing the address of the image belonging to this news item</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        /// <summary>
        /// The url containing the address of the article belonging to this news item
        /// </summary>
        /// <value>The url containing the address of the article belonging to this news item</value>

        [MaxLength(2048)]
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The content of this news item.
        /// </summary>
        /// <value>The content of this news item.</value>

        [JsonProperty(PropertyName = "content")]
        [NotMapped]
        public List<LanguageTypedString> content
        {
            get
            {
                return Helpers.JsonConverter.GetLanguageTypesStringList(Content);
            }
            set
            {
                if (value != null)
                    Content = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string Content { get; set; }


        /// <summary>
        /// The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)
        /// </summary>
        /// <value>The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)</value>

        [JsonProperty(PropertyName = "newsFeeds")]
        public List<OneOfNewsFeed> NewsFeeds { get; set; }

        /// <summary>
        /// The moment from which this news item is valid, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment from which this news item is valid, RFC3339 (date-time)</value>

        [JsonProperty(PropertyName = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The moment until which this news item is valid, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment until which this news item is valid, RFC3339 (date-time)</value>

        [JsonProperty(PropertyName = "validUntil")]
        public DateTime? ValidUntil { get; set; }

        /// <summary>
        /// The moment on which this news item was updated, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this news item was updated, RFC3339 (date-time)</value>

        [JsonProperty(PropertyName = "lastModified")]
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty(PropertyName = "consumers")]
        public List<Consumer> Consumers { get; set; }


        [JsonIgnore]
        public virtual ICollection<NewsFeed> NewsFeedsRef { get; set; }



    }
}
