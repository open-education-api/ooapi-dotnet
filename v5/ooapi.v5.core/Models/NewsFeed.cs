using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
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
        public Guid NewsFeedId { get; set; }



        /// <summary>
        /// The type of the object this news feed relates to
        /// </summary>
        /// <value>The type of the object this news feed relates to</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "newsFeedType")]
        public NewsFeedTypeEnum? NewsFeedType { get; set; }

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
                return Helpers.JsonConverter.GetLanguageTypesStringList(Description);
            }
            set
            {
                if (value != null)
                    Description = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        public string Description { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty(PropertyName = "consumers")]
        public List<Consumer> Consumers { get; set; }


    }
}
