using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
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
        [Required]

        [DataMember(Name = "roomId")]
        public Guid RoomId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }


        /// <summary>
        /// The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer 
        /// </summary>
        /// <value>The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer </value>
        [Required]

        [DataMember(Name = "roomType")]
        public RoomTypeEnum? RoomType { get; set; }

        /// <summary>
        /// The abbreviation of the name of this room
        /// </summary>
        /// <value>The abbreviation of the name of this room</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The name of this room
        /// </summary>
        /// <value>The name of this room</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageTypedString> Name { get; set; }

        /// <summary>
        /// The description of this room. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.
        /// </summary>
        /// <value>The description of this room. [The limited implementation of Git Hub Markdown syntax](#tag/formatting-and-displaying-results-from-API) MAY be used for rich text representation.</value>

        [DataMember(Name = "description")]
        public List<LanguageTypedString> Description { get; set; }

        /// <summary>
        /// The total number of seats located in the room
        /// </summary>
        /// <value>The total number of seats located in the room</value>

        [DataMember(Name = "totalSeats")]
        public int? TotalSeats { get; set; }

        /// <summary>
        /// The total number of available (&#x3D;non-reserved) seats in the room
        /// </summary>
        /// <value>The total number of available (&#x3D;non-reserved) seats in the room</value>

        [DataMember(Name = "availableSeats")]
        public int? AvailableSeats { get; set; }

        /// <summary>
        /// The floor on which this room is located
        /// </summary>
        /// <value>The floor on which this room is located</value>

        [DataMember(Name = "floor")]
        public string Floor { get; set; }

        /// <summary>
        /// The wing in which this room is located
        /// </summary>
        /// <value>The wing in which this room is located</value>

        [DataMember(Name = "wing")]
        public string Wing { get; set; }

        /// <summary>
        /// Gets or Sets Geolocation
        /// </summary>

        [DataMember(Name = "geolocation")]
        public RoomGeolocation Geolocation { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The building in which the room is located. [&#x60;expandable&#x60;](#tag/building_model) By default only the &#x60;buildingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;building&#x60; the full building object should be returned. 
        /// </summary>
        /// <value>The building in which the room is located. [&#x60;expandable&#x60;](#tag/building_model) By default only the &#x60;buildingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;building&#x60; the full building object should be returned. </value>

        [DataMember(Name = "building")]
        public Guid Building { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }


    }
}