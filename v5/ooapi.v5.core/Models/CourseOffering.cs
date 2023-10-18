using Newtonsoft.Json;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class CourseOffering : OfferingShared
    {
        /// <summary>
        /// If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.
        /// </summary>
        /// <value>If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.</value>

        [JsonProperty(PropertyName = "flexibleEntryPeriodStart")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? FlexibleEntryPeriodStart { get; set; }

        /// <summary>
        /// If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.
        /// </summary>
        /// <value>If this is a program wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.</value>
        [JsonProperty(PropertyName = "flexibleEntryPeriodEnd")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime? FlexibleEntryPeriodEnd { get; set; }

        /// <summary>
        /// The course that is offered in this courseoffering. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
        /// </summary>
        /// <value>The course that is offered in this courseoffering. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>
        [JsonProperty(PropertyName = "course")]
        [NotMapped]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOfCourse OneOfCourse {
            get
            {
                if (CourseId == null) return null;
                return new OneOfCourseInstance(CourseId, Course);
            }
        }

        /// <summary>
        ///
        /// </summary>
        [JsonIgnore]
        public Guid? CourseId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Course Course { get; set; } = default!;

        /// <summary>
        /// The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. 
        /// </summary>
        /// <value>The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. </value>
        [JsonProperty(PropertyName = "programOffering")]
        public OneOfProgramOffering OneOfProgramOffering
        {
            get
            {
                if (CourseId == null) return null;
                return new OneOfProgramOfferingInstance(ProgramOfferingId, ProgramOffering);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Guid? ProgramOfferingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public ProgramOffering? ProgramOffering { get; set; }

        /// <summary>
        /// The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>
        [JsonProperty(PropertyName = "organization")]
        public OneOfOrganization? OneOfOrganization
        {
            get
            {
                if (OrganizationId == null)
                {
                    return null;
                }

                return new OneOfOrganizationInstance(OrganizationId, Organization);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public Organization? Organization { get; set; }
    }
}
