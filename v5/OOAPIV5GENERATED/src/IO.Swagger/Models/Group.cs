using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A group is simply a collection of persons. Groups can be used to accommodate various usecases.  Groups MAY optionally have a relation to an Offering, however the meaning of such relations is left unspecified and is left up to the implementer. 
    /// </summary>
    [DataContract]
    public partial class Group : IEquatable<Group>
    {
        /// <summary>
        /// Unique id for this group
        /// </summary>
        /// <value>Unique id for this group</value>
        [JsonRequired]

        [DataMember(Name = "groupId")]
        public Guid GroupId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }



        /// <summary>
        /// The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. 
        /// </summary>
        /// <value>The type of this group - learning group: A collection of participants carrying out common learning activities - class: A collection of participants carrying out jointly scheduled educational activities - team: A collection of members of a team, either students, employees or mixed. </value>
        [JsonRequired]

        [DataMember(Name = "groupType")]
        public GroupTypeEnum? GroupType { get; set; }

        /// <summary>
        /// The name of this group
        /// </summary>
        /// <value>The name of this group</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The description of this group
        /// </summary>
        /// <value>The description of this group</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The day on which this group starts being active, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this group starts being active, RFC3339 (full-date)</value>

        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The day on which this group ends being active, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this group ends being active, RFC3339 (full-date)</value>

        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The number of persons that are member of this group
        /// </summary>
        /// <value>The number of persons that are member of this group</value>

        [DataMember(Name = "personCount")]
        public decimal? PersonCount { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// The organization that manages this group. [&#x60;expandable&#x60;](.#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this group. [&#x60;expandable&#x60;](.#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Group {\n");
            sb.Append("  GroupId: ").Append(GroupId).Append("\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  GroupType: ").Append(GroupType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  StartDate: ").Append(StartDate).Append("\n");
            sb.Append("  EndDate: ").Append(EndDate).Append("\n");
            sb.Append("  PersonCount: ").Append(PersonCount).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  Consumers: ").Append(Consumers).Append("\n");
            sb.Append("  Organization: ").Append(Organization).Append("\n");
            sb.Append("  Ext: ").Append(Ext).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Group)obj);
        }

        /// <summary>
        /// Returns true if Group instances are equal
        /// </summary>
        /// <param name="other">Instance of Group to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    GroupId == other.GroupId ||
                    GroupId != null &&
                    GroupId.Equals(other.GroupId)
                ) &&
                (
                    PrimaryCode == other.PrimaryCode ||
                    PrimaryCode != null &&
                    PrimaryCode.Equals(other.PrimaryCode)
                ) &&
                (
                    GroupType == other.GroupType ||
                    GroupType != null &&
                    GroupType.Equals(other.GroupType)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.SequenceEqual(other.Name)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.SequenceEqual(other.Description)
                ) &&
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
                    PersonCount == other.PersonCount ||
                    PersonCount != null &&
                    PersonCount.Equals(other.PersonCount)
                ) &&
                (
                    OtherCodes == other.OtherCodes ||
                    OtherCodes != null &&
                    OtherCodes.SequenceEqual(other.OtherCodes)
                ) &&
                (
                    Consumers == other.Consumers ||
                    Consumers != null &&
                    Consumers.SequenceEqual(other.Consumers)
                ) &&
                (
                    Organization == other.Organization ||
                    Organization != null &&
                    Organization.Equals(other.Organization)
                ) &&
                (
                    Ext == other.Ext ||
                    Ext != null &&
                    Ext.Equals(other.Ext)
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
                if (GroupId != null)
                    hashCode = hashCode * 59 + GroupId.GetHashCode();
                if (PrimaryCode != null)
                    hashCode = hashCode * 59 + PrimaryCode.GetHashCode();
                if (GroupType != null)
                    hashCode = hashCode * 59 + GroupType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (StartDate != null)
                    hashCode = hashCode * 59 + StartDate.GetHashCode();
                if (EndDate != null)
                    hashCode = hashCode * 59 + EndDate.GetHashCode();
                if (PersonCount != null)
                    hashCode = hashCode * 59 + PersonCount.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Organization != null)
                    hashCode = hashCode * 59 + Organization.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Group left, Group right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Group left, Group right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
