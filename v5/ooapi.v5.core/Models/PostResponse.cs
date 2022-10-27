using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A system message as a response to a POST message
    /// </summary>
    [DataContract]
    public partial class PostResponse
    {
        /// <summary>
        /// information displayed to user
        /// </summary>
        /// <value>information displayed to user</value>
        [JsonRequired]

        [DataMember(Name = "message")]
        public List<LanguageTypedString> Message { get; set; }

        /// <summary>
        /// URL where additional information can be found e.g. by use of deeplink
        /// </summary>
        /// <value>URL where additional information can be found e.g. by use of deeplink</value>

        [DataMember(Name = "redirect")]
        public string Redirect { get; set; }


    }
}
