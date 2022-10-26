using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ComponentOffering
    {
        /// <summary>
        /// The moment on which this offering starts, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this offering starts, RFC3339 (date-time)</value>
        [JsonRequired]

        [DataMember(Name = "startDateTime")]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// The moment on which this offering ends, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this offering ends, RFC3339 (date-time)</value>
        [JsonRequired]

        [DataMember(Name = "endDateTime")]
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// The first day on which a student can enroll for this course.
        /// </summary>
        /// <value>The first day on which a student can enroll for this course.</value>

        [DataMember(Name = "enrollStartDate")]
        public DateTime? EnrollStartDate { get; set; }

        /// <summary>
        /// The last day on which a student can enroll for this course.
        /// </summary>
        /// <value>The last day on which a student can enroll for this course.</value>

        [DataMember(Name = "enrollEndDate")]
        public DateTime? EnrollEndDate { get; set; }

        /// <summary>
        /// The result weight of this offering
        /// </summary>
        /// <value>The result weight of this offering</value>

        [Range(0, 100)]
        [DataMember(Name = "resultWeight")]
        public int? ResultWeight { get; set; }

        /// <summary>
        /// Addresses for this offering
        /// </summary>
        /// <value>Addresses for this offering</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// Price information for this offering.
        /// </summary>
        /// <value>Price information for this offering.</value>

        [DataMember(Name = "priceInformation")]
        public List<ProgramOfferingPriceInformation> PriceInformation { get; set; }

        /// <summary>
        /// Gets or Sets Room
        /// </summary>

        [DataMember(Name = "room")]
        public Room Room { get; set; }

        /// <summary>
        /// The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. 
        /// </summary>
        /// <value>The component that is offered in this componentoffering. [&#x60;expandable&#x60;](#tag/component_model) By default only the &#x60;componentId&#x60; (a string) is returned. If the client requested an expansion of &#x60;component&#x60; the full component object should be returned. </value>

        [DataMember(Name = "component")]
        public OneOfComponent Component { get; set; }

        /// <summary>
        /// The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. 
        /// </summary>
        /// <value>The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. </value>

        [DataMember(Name = "courseOffering")]
        public OneOfOffering CourseOffering { get; set; }

        /// <summary>
        /// The organization that manages this componentoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this componentoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// The academicsession during which this componentoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. 
        /// </summary>
        /// <value>The academicsession during which this componentoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. </value>
    }
}
