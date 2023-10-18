using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

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
    [SortAllowed]
    [SortDefault]
    public Guid NewsItemId { get; set; }

    /// <summary>
    /// The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging 
    /// </summary>
    /// <value>The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging </value>
    [JsonProperty(PropertyName = "newsItemType")]
    public NewsItemType? NewsItemType { get; set; }

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
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("name")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [SortAllowed]
    [SortDefault]
    public List<Attribute> Attributes { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string Authors { get; set; } = default!;

    /// <summary>
    /// The authors of the article with this news item
    /// </summary>
    /// <value>The authors of the article with this news item</value>

    [DataMember(Name = "authors")]
    [NotMapped]
    public List<string> authors { get; set; } = default!;

    /// <summary>
    /// The url containing the address of the image belonging to this news item
    /// </summary>
    /// <value>The url containing the address of the image belonging to this news item</value>
    [MaxLength(2048)]
    [JsonProperty(PropertyName = "image")]
    public string Image { get; set; } = default!;

    /// <summary>
    /// The url containing the address of the article belonging to this news item
    /// </summary>
    /// <value>The url containing the address of the article belonging to this news item</value>

    [MaxLength(2048)]
    [JsonProperty(PropertyName = "link")]
    public string? Link { get; set; }

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
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("content")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)
    /// </summary>
    /// <value>The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)</value>
    [JsonProperty(PropertyName = "newsFeeds")]
    [NotMapped]
    public List<OneOfNewsFeed> OneOfNewsFeeds { get; set; } = default!;

    /// <summary>
    /// The moment from which this news item is valid, RFC3339 (date-time)
    /// </summary>
    /// <value>The moment from which this news item is valid, RFC3339 (date-time)</value>
    [JsonProperty(PropertyName = "validFrom")]
    [SortAllowed]
    public DateTime? ValidFrom { get; set; }

    /// <summary>
    /// The moment until which this news item is valid, RFC3339 (date-time)
    /// </summary>
    /// <value>The moment until which this news item is valid, RFC3339 (date-time)</value>
    [JsonProperty(PropertyName = "validUntil")]
    [SortAllowed]
    public DateTime? ValidUntil { get; set; }

    /// <summary>
    /// The moment on which this news item was updated, RFC3339 (date-time)
    /// </summary>
    /// <value>The moment on which this news item was updated, RFC3339 (date-time)</value>
    [JsonProperty(PropertyName = "lastModified")]
    [SortAllowed]
    public DateTime? LastModified { get; set; }

    /// <summary>
    /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
    /// </summary>
    /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>
    [JsonProperty(PropertyName = "consumers")]
    [NotMapped]
    public List<JObject> ConsumersList
    {
        get
        {
            if (Consumers != null && Consumers.Any())
            {
                return ConsumerConverter.GetDynamicConsumers(Consumers);
            }

            return new List<JObject>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public List<Consumer> Consumers { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<NewsFeed> NewsFeeds { get; set; } = default!;
}
