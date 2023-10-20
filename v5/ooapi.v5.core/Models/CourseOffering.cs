using Newtonsoft.Json;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models;

[DataContract]
public class CourseOffering : OfferingShared
{
    /// <summary>
    /// The course that is offered in this courseoffering. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
    /// </summary>
    /// <value>The course that is offered in this courseoffering. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>
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

    /// <summary>
    ///
    /// </summary>
    [JsonIgnore]
    public Guid? CourseId { get; set; }

    [JsonIgnore]
    public Course? Course { get; set; } = default!;

    /// <summary>
    /// The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. 
    /// </summary>
    /// <value>The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. </value>
    [JsonProperty(PropertyName = "programOffering")]
    public OneOfProgramOffering? OneOfProgramOffering
    {
        get
        {
            if (ProgramOfferingId == null)
            {
                return null;
            }

            return new OneOfProgramOfferingInstance(ProgramOfferingId, ProgramOffering);
        }
    }

    [JsonIgnore]
    public Guid? ProgramOfferingId { get; set; }

    [JsonIgnore]
    public ProgramOffering? ProgramOffering { get; set; }
}