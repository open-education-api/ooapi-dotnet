using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class CourseOffering : IEquatable<CourseOffering>
    {
        /// <summary>
        /// The moment on which this offering starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering starts, RFC3339 (full-date)</value>
        [JsonRequired]

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The moment on which this offering ends, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering ends, RFC3339 (full-date)</value>
        [JsonRequired]

        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }

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
        /// If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.
        /// </summary>
        /// <value>If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodEnd&#x60;.</value>

        [DataMember(Name = "flexibleEntryPeriodStart")]
        public DateTime? FlexibleEntryPeriodStart { get; set; }

        /// <summary>
        /// If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.
        /// </summary>
        /// <value>If this is a course wherein participants can start at various moments, without missing anything, use this attribute in combination with &#x60;flexibleEntryPeriodStart&#x60;.</value>

        [DataMember(Name = "flexibleEntryPeriodEnd")]
        public DateTime? FlexibleEntryPeriodEnd { get; set; }

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
        /// The course that is offered in this courseoffering. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
        /// </summary>
        /// <value>The course that is offered in this courseoffering. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>

        [DataMember(Name = "course")]
        public OneOfCourse Course { get; set; }

        /// <summary>
        /// The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. 
        /// </summary>
        /// <value>The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. </value>

        [DataMember(Name = "programOffering")]
        public OneOfOffering ProgramOffering { get; set; }

        /// <summary>
        /// The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// The academicsession during which this courseoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. 
        /// </summary>
        /// <value>The academicsession during which this courseoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. </value>
    }
}
