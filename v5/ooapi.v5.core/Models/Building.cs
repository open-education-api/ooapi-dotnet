using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.Many2Many;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [JsonRequired]

        [JsonProperty(PropertyName = "buildingId")]
        public Guid BuildingId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "primaryCode")]
        [NotMapped]
        public IdentifierEntry primaryCode
        {
            get
            {
                return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
            }
            set
            {
                PrimaryCode = value.Code;
                PrimaryCodeType = value.CodeType;
            }
        }


        [JsonIgnore]
        public string PrimaryCodeType { get; set; }

        [JsonIgnore]
        public string PrimaryCode { get; set; }

        /// <summary>
        /// The abbreviation of the name of this building
        /// </summary>
        /// <value>The abbreviation of the name of this building</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The name of this building
        /// </summary>
        /// <value>The name of this building</value>
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
        /// The description of this building.
        /// </summary>
        /// <value>The description of this building.</value>

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
        /// Gets or Sets Address
        /// </summary>
        [JsonRequired]

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonIgnore]
        public Guid? AddressId { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty("consumers")]
        [NotMapped]
        public List<dynamic>? Consumers { get; set; }



    }
}
