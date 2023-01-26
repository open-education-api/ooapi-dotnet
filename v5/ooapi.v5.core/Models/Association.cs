using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    //[SwaggerDiscriminator("resultType")]
    //[SwaggerSubType(typeof(ProgramResult), DiscriminatorValue = "programResult")]
    //[SwaggerSubType(typeof(CourseResult), DiscriminatorValue = "courseResult")]
    //[SwaggerSubType(typeof(ComponentResult), DiscriminatorValue = "componentResult")]
    public partial class Association : ModelBase
    {

        /// <summary>
        /// Unique id for this association
        /// </summary>
        /// <value>Unique id for this association</value>
        [JsonRequired]
        [JsonProperty("associationId")]
        [SortAllowed]
        [SortDefault]
        public Guid AssociationId { get; set; }

        /// <summary>
        /// The type of this association
        /// </summary>
        /// <value>The type of this association</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "associationType")]
        public AssociationTypeEnum AssociationType { get; set; }



        /// <summary>
        /// The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast 
        /// </summary>
        /// <value>The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "role")]
        public RoleEnum Role { get; set; }

        /// <summary>
        /// The state of this association
        /// </summary>
        /// <value>The state of this association</value>
        [JsonRequired]
        [JsonProperty(PropertyName = "state")]
        public AssociationStateEnum State { get; set; }

        /// <summary>
        /// The state of this association for the institution performing the request.
        /// </summary>
        /// <value>The state of this association for the institution performing the request.</value>

        [JsonProperty(PropertyName = "remoteState")]
        public RemoteStateEnum? RemoteState { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty("consumers")]
        [NotMapped]
        public List<dynamic>? Consumers { get; set; }

        /// <summary>
        /// A result as part of an association
        /// </summary>
        /// <value>A result as part of an association</value>

        [JsonProperty(PropertyName = "result")]
        public OneOfResult? Result { get; set; }

        [JsonIgnore]
        public Guid? ProgramResultId { get; set; }

        [JsonIgnore]
        public Guid? CourseResultId { get; set; }

        [JsonIgnore]
        public Guid? ComponentResultId { get; set; }

        /// <summary>
        /// Identifier (string) or Person (object)
        /// </summary>
        /// <value>Identifier (string) or Person (object)</value>

        [JsonProperty(PropertyName = "person")]
        public OneOfPerson? Person { get; set; }

        [JsonIgnore]
        public Guid? PersonId { get; set; }


        /// <summary>
        /// Identifier (string) or Offering (object) or Offering (object) or Offering (object))
        /// </summary>
        /// <value>Identifier (string) or Offering (object) or Offering (object) or Offering (object)</value>

        [JsonProperty(PropertyName = "offering")]
        public OneOfOffering? Offering { get; set; }

        [JsonIgnore]
        public Guid? OfferingId { get; set; }

    }



}
