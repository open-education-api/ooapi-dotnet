using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
        public Object Course { get; set; }

        /// <summary>
        /// The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. 
        /// </summary>
        /// <value>The programoffering where this courseoffering is related to. [&#x60;expandable&#x60;](#tag/program_offering_model) By default only the &#x60;programOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;programOffering&#x60; the full programOffering object should be returned. </value>

        [DataMember(Name = "programOffering")]
        public Object ProgramOffering { get; set; }

        /// <summary>
        /// The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public Object Organization { get; set; }

        /// <summary>
        /// The academicsession during which this courseoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. 
        /// </summary>
        /// <value>The academicsession during which this courseoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. </value>

        [DataMember(Name = "academicSession")]
        public Object AcademicSession { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CourseOffering {\n");
            sb.Append("  StartDate: ").Append(StartDate).Append("\n");
            sb.Append("  EndDate: ").Append(EndDate).Append("\n");
            sb.Append("  EnrollStartDate: ").Append(EnrollStartDate).Append("\n");
            sb.Append("  EnrollEndDate: ").Append(EnrollEndDate).Append("\n");
            sb.Append("  FlexibleEntryPeriodStart: ").Append(FlexibleEntryPeriodStart).Append("\n");
            sb.Append("  FlexibleEntryPeriodEnd: ").Append(FlexibleEntryPeriodEnd).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("  PriceInformation: ").Append(PriceInformation).Append("\n");
            sb.Append("  Course: ").Append(Course).Append("\n");
            sb.Append("  ProgramOffering: ").Append(ProgramOffering).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  AcademicSession: ").Append(AcademicSession).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CourseOffering)obj);
        }

        /// <summary>
        /// Returns true if CourseOffering instances are equal
        /// </summary>
        /// <param name="other">Instance of CourseOffering to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CourseOffering other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    StartDate == other.StartDate ||
                    StartDate != null &&
                    StartDate.Equals(other.StartDate)
                ) &&
                (
                    EndDate == other.EndDate ||
                    EndDate != null &&
                    EndDate.Equals(other.EndDate)
                ) &&
                (
                    EnrollStartDate == other.EnrollStartDate ||
                    EnrollStartDate != null &&
                    EnrollStartDate.Equals(other.EnrollStartDate)
                ) &&
                (
                    EnrollEndDate == other.EnrollEndDate ||
                    EnrollEndDate != null &&
                    EnrollEndDate.Equals(other.EnrollEndDate)
                ) &&
                (
                    FlexibleEntryPeriodStart == other.FlexibleEntryPeriodStart ||
                    FlexibleEntryPeriodStart != null &&
                    FlexibleEntryPeriodStart.Equals(other.FlexibleEntryPeriodStart)
                ) &&
                (
                    FlexibleEntryPeriodEnd == other.FlexibleEntryPeriodEnd ||
                    FlexibleEntryPeriodEnd != null &&
                    FlexibleEntryPeriodEnd.Equals(other.FlexibleEntryPeriodEnd)
                ) &&
                (
                    Addresses == other.Addresses ||
                    Addresses != null &&
                    Addresses.SequenceEqual(other.Addresses)
                ) &&
                (
                    PriceInformation == other.PriceInformation ||
                    PriceInformation != null &&
                    PriceInformation.SequenceEqual(other.PriceInformation)
                ) &&
                (
                    Course == other.Course ||
                    Course != null &&
                    Course.Equals(other.Course)
                ) &&
                (
                    ProgramOffering == other.ProgramOffering ||
                    ProgramOffering != null &&
                    ProgramOffering.Equals(other.ProgramOffering)
                ) &&
                (
                    Organization == other.Organization ||
                    Organization != null &&
                    Organization.Equals(other.Organization)
                ) &&
                (
                    AcademicSession == other.AcademicSession ||
                    AcademicSession != null &&
                    AcademicSession.Equals(other.AcademicSession)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (StartDate != null)
                    hashCode = hashCode * 59 + StartDate.GetHashCode();
                if (EndDate != null)
                    hashCode = hashCode * 59 + EndDate.GetHashCode();
                if (EnrollStartDate != null)
                    hashCode = hashCode * 59 + EnrollStartDate.GetHashCode();
                if (EnrollEndDate != null)
                    hashCode = hashCode * 59 + EnrollEndDate.GetHashCode();
                if (FlexibleEntryPeriodStart != null)
                    hashCode = hashCode * 59 + FlexibleEntryPeriodStart.GetHashCode();
                if (FlexibleEntryPeriodEnd != null)
                    hashCode = hashCode * 59 + FlexibleEntryPeriodEnd.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                if (PriceInformation != null)
                    hashCode = hashCode * 59 + PriceInformation.GetHashCode();
                if (Course != null)
                    hashCode = hashCode * 59 + Course.GetHashCode();
                if (ProgramOffering != null)
                    hashCode = hashCode * 59 + ProgramOffering.GetHashCode();
                if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                if (AcademicSession != null)
                    hashCode = hashCode * 59 + AcademicSession.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CourseOffering left, CourseOffering right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CourseOffering left, CourseOffering right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
