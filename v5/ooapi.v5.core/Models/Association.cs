using Newtonsoft.Json;
using ooapi.v5.Enums;
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
        public Guid AssociationId { get; set; }

        /// <summary>
        /// The type of this association
        /// </summary>
        /// <value>The type of this association</value>
        [JsonRequired]
        [DataMember(Name = "associationType")]
        public AssociationTypeEnum AssociationType { get; set; }



        /// <summary>
        /// The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast 
        /// </summary>
        /// <value>The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast </value>
        [JsonRequired]
        [DataMember(Name = "role")]
        public RoleEnum Role { get; set; }

        /// <summary>
        /// The state of this association
        /// </summary>
        /// <value>The state of this association</value>
        [JsonRequired]
        [DataMember(Name = "state")]
        public AssociationStateEnum State { get; set; }

        /// <summary>
        /// The state of this association for the institution performing the request.
        /// </summary>
        /// <value>The state of this association for the institution performing the request.</value>

        [DataMember(Name = "remoteState")]
        public RemoteStateEnum? RemoteState { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// A result as part of an association
        /// </summary>
        /// <value>A result as part of an association</value>

        [DataMember(Name = "result")]
        public OneOfResult? Result { get; set; }

        /// <summary>
        /// Identifier (string) or Person (object)
        /// </summary>
        /// <value>Identifier (string) or Person (object)</value>

        [DataMember(Name = "person")]
        public OneOfPerson? Person { get; set; }

        /// <summary>
        /// Identifier (string) or Offering (object) or Offering (object) or Offering (object))
        /// </summary>
        /// <value>Identifier (string) or Offering (object) or Offering (object) or Offering (object)</value>

        [DataMember(Name = "offering")]
        public OneOfOffering? Offering { get; set; }



    }



}
