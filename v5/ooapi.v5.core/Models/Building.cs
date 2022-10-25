using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// An object describing a building and the properties of a building.
    /// </summary>
    [DataContract]
    public partial class Building : ModelBase
    {
        /// <summary>
        /// Unique id of this building
        /// </summary>
        /// <value>Unique id of this building</value>
        [Required]

        [DataMember(Name = "buildingId")]
        public Guid? BuildingId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The abbreviation of the name of this building
        /// </summary>
        /// <value>The abbreviation of the name of this building</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The name of this building
        /// </summary>
        /// <value>The name of this building</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageTypedString> Name { get; set; }

        /// <summary>
        /// The description of this building.
        /// </summary>
        /// <value>The description of this building.</value>

        [DataMember(Name = "description")]
        public List<LanguageTypedString> Description { get; set; }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [Required]

        [DataMember(Name = "address")]
        public Address Address { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }



    }
}
