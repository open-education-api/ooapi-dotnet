using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOfferingAssociationExpandablePerson
    {
        /// <summary>
        /// Unique id of this person
        /// </summary>
        /// <value>Unique id of this person</value>
        [JsonRequired]

        [DataMember(Name = "personId")]
        public Guid PersonId { get; set; }


    }

}
