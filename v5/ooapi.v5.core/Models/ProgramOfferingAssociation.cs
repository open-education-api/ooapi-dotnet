using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOfferingAssociation
    {
        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        [JsonProperty(PropertyName = "result")]
        public Object Result { get; set; }
    }

}
