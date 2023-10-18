using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;


[DataContract]
public partial class ComponentOffering : Offering
{

    [Range(0, 100)]
    [JsonProperty(PropertyName = "resultWeight")]
    public int? ResultWeight { get; set; }

    /// <summary>
    /// Gets or Sets Room
    /// </summary>
    [JsonProperty(PropertyName = "room")]
    public Room? Room { get; set; }

    /// <summary>
    /// The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. 
    /// </summary>
    /// <value>The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. </value>
    [JsonProperty(PropertyName = "component")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfComponent? OneOfComponent
    {
        get
        {
            if (ComponentId == null)
            {
                return null;
            }

            return new OneOfComponentInstance(ComponentId, Component);
        }
    }


    [JsonIgnore]
    public Guid? ComponentId { get; set; }


    [JsonIgnore]
    public Component? Component { get; set; }

    /// <summary>
    /// The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. 
    /// </summary>
    /// <value>The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. </value>
    [JsonProperty(PropertyName = "course")]
    [NotMapped]
    [JsonConverter(typeof(OneOfConverter))]
    public OneOfCourse? OneOfCourse
    {
        get
        {
            if (CourseId == null)
            {
                return null;
            }
            return new OneOfCourseInstance(CourseId, Course);
        }
    }


    [JsonIgnore]
    public Guid? CourseId { get; set; }


    [JsonIgnore]
    public Course? Course { get; set; }

    /// <summary>
    /// The moment on which this offering starts, RFC3339 (full-date)
    /// </summary>
    /// <value>The moment on which this offering starts, RFC3339 (full-date)</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "startDateTime")]
    [SortAllowed]
    [SortDefault]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// The moment on which this offering ends, RFC3339 (full-date)
    /// </summary>
    /// <value>The moment on which this offering ends, RFC3339 (full-date)</value>
    [JsonRequired]
    [JsonProperty(PropertyName = "endDateTime")]
    [SortAllowed]
    public DateTime? EndDate { get; set; }
}
