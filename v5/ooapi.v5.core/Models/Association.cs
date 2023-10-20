using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

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
    [JsonProperty(PropertyName = "associationType")]
    public AssociationType AssociationType { get; set; }

    /// <summary>
    /// The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: co�rdinator   - guest: gast 
    /// </summary>
    /// <value>The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: co�rdinator   - guest: gast </value>
    [JsonRequired]
    [JsonProperty(PropertyName = "role")]
    public AssociationRole Role { get; set; }

    /// <summary>
    /// The state of this association
    /// </summary>
    /// <value>The state of this association</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "state")]
    public AssociationState State { get; set; }

    /// <summary>
    /// The state of this association for the institution performing the request.
    /// </summary>
    /// <value>The state of this association for the institution performing the request.</value>
    [JsonProperty(PropertyName = "remoteState")]
    public RemoteState? RemoteState { get; set; }

    /// <summary>
    /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
    /// </summary>
    /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>
    [JsonProperty(PropertyName = "consumers")]
    [NotMapped]
    public List<JObject> ConsumersList
    {
        get
        {
            if (Consumers != null && Consumers.Any())
            {
                return ConsumerConverter.GetDynamicConsumers(Consumers);
            }

            return new List<JObject>();
        }
    }

    [JsonIgnore]
    public List<Consumer> Consumers { get; set; } = default!;

    /// <summary>
    /// A result as part of an association
    /// </summary>
    /// <value>A result as part of an association</value>
    [JsonProperty(PropertyName = "result")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfResult? OneOfResult
    {
        get
        {
            if (ResultId == null)
            {
                return null;
            }

            if (ProgramResult != null)
            {
                return new OneOfResultInstance(ResultId, ProgramResult);
            }
            else if (CourseResult != null)
            {
                return new OneOfResultInstance(ResultId, CourseResult);
            }
            else if (ComponentResult != null)
            {
                return new OneOfResultInstance(ResultId, ComponentResult);
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Id of the result
    /// </summary>
    [JsonIgnore]
    public Guid? ResultId { get; set; }

    /// <summary>
    /// The result of the program
    /// </summary>
    [JsonIgnore]
    public ProgramResult? ProgramResult { get; set; }

    /// <summary>
    /// The result of the course
    /// </summary>
    [JsonIgnore]
    public CourseResult? CourseResult { get; set; }

    /// <summary>
    /// The result of component
    /// </summary>
    [JsonIgnore]
    public ComponentResult? ComponentResult { get; set; }

    /// <summary>
    /// Identifier (string) or Person (object)
    /// </summary>
    /// <value>Identifier (string) or Person (object)</value>

    [JsonProperty(PropertyName = "person")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfPerson? OneOfPerson
    {
        get
        {
            if (PersonId == null)
            {
                return null;
            }

            return new OneOfPersonInstance(PersonId, Person);
        }
    }

    /// <summary>
    /// The id of the person
    /// </summary>
    [JsonIgnore]
    public Guid? PersonId { get; set; }

    /// <summary>
    /// The person
    /// </summary>
    [JsonIgnore]
    public Person? Person { get; set; }

    /// <summary>
    /// Identifier (string) or Offering (object) or Offering (object) or Offering (object))
    /// </summary>
    /// <value>Identifier (string) or Offering (object) or Offering (object) or Offering (object)</value>
    [JsonProperty(PropertyName = "offering")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfOffering? OneOfOffering
    {
        get
        {
            if (OfferingId == null)
            {
                return null;
            }

            if (ProgramOffering != null)
            {
                return new OneOfOfferingInstance(OfferingId, ProgramOffering);
            }
            else if (CourseOffering != null)
            {
                return new OneOfOfferingInstance(OfferingId, CourseOffering);
            }
            else if (ComponentOffering != null)
            {
                return new OneOfOfferingInstance(OfferingId, ComponentOffering);
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Id of the offering
    /// </summary>
    [JsonIgnore]
    public Guid? OfferingId { get; set; }

    /// <summary>
    /// The program offering
    /// </summary>
    [JsonIgnore]
    public ProgramOffering? ProgramOffering { get; set; }

    /// <summary>
    /// The course offering
    /// </summary>
    [JsonIgnore]
    public CourseOffering? CourseOffering { get; set; }

    /// <summary>
    /// The component offering
    /// </summary>
    [JsonIgnore]
    public ComponentOffering? ComponentOffering { get; set; }
}