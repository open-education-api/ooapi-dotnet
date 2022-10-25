using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
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
        [Required]

        [DataMember(Name = "newsFeedId")]
        public Guid NewsFeedId { get; set; }



        /// <summary>
        /// The type of the object this news feed relates to
        /// </summary>
        /// <value>The type of the object this news feed relates to</value>
        [Required]

        [DataMember(Name = "newsFeedType")]
        public NewsFeedTypeEnum? NewsFeedType { get; set; }

        /// <summary>
        /// The name for this news feed
        /// </summary>
        /// <value>The name for this news feed</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageTypedString> Name { get; set; }

        /// <summary>
        /// The description of this news feed.
        /// </summary>
        /// <value>The description of this news feed.</value>
        [Required]

        [DataMember(Name = "description")]
        public List<LanguageTypedString> Description { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }


    }
}
