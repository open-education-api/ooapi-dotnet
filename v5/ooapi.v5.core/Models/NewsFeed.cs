using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

/// <summary>
/// A placeholder or collection of news items
/// </summary>
[DataContract]
public partial class NewsFeed : ModelBase
{
    /// <summary>
    /// Unique id for this news feed
    /// </summary>
    /// <value>Unique id for this news feed</value>
    [JsonRequired]

    [JsonProperty(PropertyName = "newsFeedId")]
    [SortAllowed]
    public Guid NewsFeedId { get; set; }

    /// <summary>
    /// The type of the object this news feed relates to
    /// </summary>
    /// <value>The type of the object this news feed relates to</value>
    [JsonRequired]

    [JsonProperty(PropertyName = "newsFeedType")]
    public NewsFeedType? NewsFeedType { get; set; }

    /// <summary>
    /// The name for this news feed
    /// </summary>
    /// <value>The name for this news feed</value>
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
    /// The description of this news feed.
    /// </summary>
    /// <value>The description of this news feed.</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "description")]
    [NotMapped]
    public List<LanguageTypedString> description
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("description")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

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
    public virtual ICollection<NewsItem> NewsItems { get; set; } = default!;
}
