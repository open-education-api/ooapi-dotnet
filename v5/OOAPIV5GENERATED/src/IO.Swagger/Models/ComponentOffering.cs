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
    public partial class ComponentOffering : IEquatable<ComponentOffering>
    {
        /// <summary>
        /// The moment on which this offering starts, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this offering starts, RFC3339 (date-time)</value>
        [Required]

        [DataMember(Name = "startDateTime")]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// The moment on which this offering ends, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this offering ends, RFC3339 (date-time)</value>
        [Required]

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
        public Object Component { get; set; }

        /// <summary>
        /// The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. 
        /// </summary>
        /// <value>The courseoffering where this componentoffering is related to. [&#x60;expandable&#x60;](#tag/course_offering_model) By default only the &#x60;courseOfferingId&#x60; (a string) is returned. If the client requested an expansion of &#x60;courseOffering&#x60; the full courseOffering object should be returned. </value>

        [DataMember(Name = "courseOffering")]
        public Object CourseOffering { get; set; }

        /// <summary>
        /// The organization that manages this componentoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this componentoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public Object Organization { get; set; }

        /// <summary>
        /// The academicsession during which this componentoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. 
        /// </summary>
        /// <value>The academicsession during which this componentoffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. </value>

        [DataMember(Name = "academicSession")]
        public Object AcademicSession { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ComponentOffering {\n");
            sb.Append("  StartDateTime: ").Append(StartDateTime).Append("\n");
            sb.Append("  EndDateTime: ").Append(EndDateTime).Append("\n");
            sb.Append("  EnrollStartDate: ").Append(EnrollStartDate).Append("\n");
            sb.Append("  EnrollEndDate: ").Append(EnrollEndDate).Append("\n");
            sb.Append("  ResultWeight: ").Append(ResultWeight).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("  PriceInformation: ").Append(PriceInformation).Append("\n");
            sb.Append("  Room: ").Append(Room).Append("\n");
            sb.Append("  Component: ").Append(Component).Append("\n");
            sb.Append("  CourseOffering: ").Append(CourseOffering).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ComponentOffering)obj);
        }

        /// <summary>
        /// Returns true if ComponentOffering instances are equal
        /// </summary>
        /// <param name="other">Instance of ComponentOffering to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ComponentOffering other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    StartDateTime == other.StartDateTime ||
                    StartDateTime != null &&
                    StartDateTime.Equals(other.StartDateTime)
                ) &&
                (
                    EndDateTime == other.EndDateTime ||
                    EndDateTime != null &&
                    EndDateTime.Equals(other.EndDateTime)
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
                    ResultWeight == other.ResultWeight ||
                    ResultWeight != null &&
                    ResultWeight.Equals(other.ResultWeight)
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
                    Room == other.Room ||
                    Room != null &&
                    Room.Equals(other.Room)
                ) &&
                (
                    Component == other.Component ||
                    Component != null &&
                    Component.Equals(other.Component)
                ) &&
                (
                    CourseOffering == other.CourseOffering ||
                    CourseOffering != null &&
                    CourseOffering.Equals(other.CourseOffering)
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
                if (StartDateTime != null)
                    hashCode = hashCode * 59 + StartDateTime.GetHashCode();
                if (EndDateTime != null)
                    hashCode = hashCode * 59 + EndDateTime.GetHashCode();
                if (EnrollStartDate != null)
                    hashCode = hashCode * 59 + EnrollStartDate.GetHashCode();
                if (EnrollEndDate != null)
                    hashCode = hashCode * 59 + EnrollEndDate.GetHashCode();
                if (ResultWeight != null)
                    hashCode = hashCode * 59 + ResultWeight.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                if (PriceInformation != null)
                    hashCode = hashCode * 59 + PriceInformation.GetHashCode();
                if (Room != null)
                    hashCode = hashCode * 59 + Room.GetHashCode();
                if (Component != null)
                    hashCode = hashCode * 59 + Component.GetHashCode();
                if (CourseOffering != null)
                    hashCode = hashCode * 59 + CourseOffering.GetHashCode();
                if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                if (AcademicSession != null)
                    hashCode = hashCode * 59 + AcademicSession.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ComponentOffering left, ComponentOffering right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ComponentOffering left, ComponentOffering right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
