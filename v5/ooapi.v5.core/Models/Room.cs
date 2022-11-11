using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// An area within a building where education can take place
    /// </summary>
    [DataContract]
    public class Room : ModelBase
    {
        /// <summary>
        /// Unique id for this room
        /// </summary>
        /// <value>Unique id for this room</value>
        [JsonRequired]

        [JsonProperty(PropertyName = "roomId")]
        public Guid RoomId { get; set; }

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
        /// The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer 
        /// </summary>
        /// <value>The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "roomType")]
        public RoomTypeEnum? RoomType { get; set; }

        /// <summary>
        /// The abbreviation of the name of this room
        /// </summary>
        /// <value>The abbreviation of the name of this room</value>

        [MaxLength(256)]
        [JsonProperty(PropertyName = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The name of this room
        /// </summary>
        /// <value>The name of this room</value>
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
        /// The description of this room. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.
        /// </summary>
        /// <value>The description of this room. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.</value>
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
        /// The total number of seats located in the room
        /// </summary>
        /// <value>The total number of seats located in the room</value>

        [JsonProperty(PropertyName = "totalSeats")]
        public int? TotalSeats { get; set; }

        /// <summary>
        /// The total number of available (&#x3D;non-reserved) seats in the room
        /// </summary>
        /// <value>The total number of available (&#x3D;non-reserved) seats in the room</value>

        [JsonProperty(PropertyName = "availableSeats")]
        public int? AvailableSeats { get; set; }

        /// <summary>
        /// The floor on which this room is located
        /// </summary>
        /// <value>The floor on which this room is located</value>

        [JsonProperty(PropertyName = "floor")]
        public string Floor { get; set; }

        /// <summary>
        /// The wing in which this room is located
        /// </summary>
        /// <value>The wing in which this room is located</value>

        [JsonProperty(PropertyName = "wing")]
        public string Wing { get; set; }

        /// <summary>
        /// Geolocation of the entrance of this room (WGS84 coordinate reference system)
        /// </summary>
        /// <value>Geolocation of the entrance of this room (WGS84 coordinate reference system)</value>

        [JsonProperty(PropertyName = "geolocation")]
        [NotMapped]
        public Geolocation? Geolocation
        {
            get
            {
                if (Latitude != null && Longitude != null)
                {
                    return new Geolocation() { Latitude = (decimal)Latitude, Longitude = (decimal)Longitude };
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    Latitude = value.Latitude;
                    Longitude = value.Longitude;
                }
            }
        }


        [JsonIgnore]
        [Column(TypeName = "decimal(8, 6)")]
        public decimal? Latitude { get; set; }


        [JsonIgnore]
        [Column(TypeName = "decimal(8, 6)")]
        public decimal? Longitude { get; set; }



        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty(PropertyName = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The building in which the room is located. [&#x60;expandable&#x60;](#tag/building_model) By default only the &#x60;buildingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;building&#x60; the full building object should be returned. 
        /// </summary>
        /// <value>The building in which the room is located. [&#x60;expandable&#x60;](#tag/building_model) By default only the &#x60;buildingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;building&#x60; the full building object should be returned. </value>

        [JsonProperty(PropertyName = "building")]
        public OneOfBuilding Building { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty(PropertyName = "consumers")]
        public List<Consumer> Consumers { get; set; }


    }
}
