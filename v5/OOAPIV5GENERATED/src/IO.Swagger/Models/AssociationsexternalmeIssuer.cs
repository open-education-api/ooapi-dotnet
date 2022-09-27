/*
 * Open Education API
 *
 * OpenAPI (fka Swagger) specification for the Open Education API.  <figure>  <a target=\"_blank\" href=\"OOAPIv5_model.png\">   <img src=\"OOAPIv5_model.png\" alt=\"OOAPI information model that feeds OOAPI specification\" width=\"70%\" class=\"img-responsive\">   </a>   <figcaption> OOAPI information model that feeds OOAPI specification (click to enlage)</figcaption> </figure>  The model provides an overview of how the objects on which the API is specified are related. The overarching concept educations is not found in the in the end points of the API. The smaller concepts of programOffering, courseOffering and conceptOffering are all found in the offering endpoint. The different types of association can all be found in the association endpoint.  The original file for this model can be found <a target=\"_blank\" href=\"OOAPIv5_model_v0.4.drawio\">here</a>  The program relations object is not found as a separate endpoint but relations between programs can be found within the program endpoint by expanding that endpoint.  Information about earlier meetings and presentations can be found <a target=\"_blank\" href=\"https://github.com/open-education-api/notulen\">here</a>  Information on the EDU-API model that was also used for this api is shown <a target=\"_blank\" href=\"eduapi.png\">here</a> 
 *
 * OpenAPI spec version: 5.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
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
    /// A description of a group of people working together to achieve a goal
    /// </summary>
    [DataContract]
    public partial class AssociationsexternalmeIssuer : IEquatable<AssociationsexternalmeIssuer>
    {
        /// <summary>
        /// Unique id of this organization
        /// </summary>
        /// <value>Unique id of this organization</value>
        [Required]

        [DataMember(Name = "organizationId")]
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The type of this organization. Each OOAPI endpoint should have a single organization with type `root`, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school 
        /// </summary>
        /// <value>The type of this organization. Each OOAPI endpoint should have a single organization with type `root`, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school </value>
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum OrganizationTypeEnum
        {
            /// <summary>
            /// Enum RootEnum for root
            /// </summary>
            [EnumMember(Value = "root")]
            RootEnum = 0,
            /// <summary>
            /// Enum InstituteEnum for institute
            /// </summary>
            [EnumMember(Value = "institute")]
            InstituteEnum = 1,
            /// <summary>
            /// Enum DepartmentEnum for department
            /// </summary>
            [EnumMember(Value = "department")]
            DepartmentEnum = 2,
            /// <summary>
            /// Enum FacultyEnum for faculty
            /// </summary>
            [EnumMember(Value = "faculty")]
            FacultyEnum = 3,
            /// <summary>
            /// Enum BranchEnum for branch
            /// </summary>
            [EnumMember(Value = "branch")]
            BranchEnum = 4,
            /// <summary>
            /// Enum AcademyEnum for academy
            /// </summary>
            [EnumMember(Value = "academy")]
            AcademyEnum = 5,
            /// <summary>
            /// Enum SchoolEnum for school
            /// </summary>
            [EnumMember(Value = "school")]
            SchoolEnum = 6
        }

        /// <summary>
        /// The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school 
        /// </summary>
        /// <value>The type of this organization. Each OOAPI endpoint should have a single organization with type &#x60;root&#x60;, describing the root organization. - root: the root of this organization, representing the Educational Institution itself - institute: instituut - department: departement - faculty: faculteit - branch: vestiging - academy: academie - school: school </value>
        [Required]

        [DataMember(Name = "organizationType")]
        public OrganizationTypeEnum? OrganizationType { get; set; }

        /// <summary>
        /// The name of the organization
        /// </summary>
        /// <value>The name of the organization</value>
        [Required]

        [DataMember(Name = "name")]
        public List<AddressAdditional> Name { get; set; }

        /// <summary>
        /// Short name of the organization
        /// </summary>
        /// <value>Short name of the organization</value>
        [Required]

        [MaxLength(256)]
        [DataMember(Name = "shortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).
        /// </summary>
        /// <value>Any general description of the organization should clearly mention the type of higher education organization, especially in the case of a binary system. In Dutch; universiteit (university) or hogeschool (university of applied sciences).</value>

        [DataMember(Name = "description")]
        public List<AddressAdditional> Description { get; set; }

        /// <summary>
        /// Addresses of this organization
        /// </summary>
        /// <value>Addresses of this organization</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// URL of the organization&#x27;s website
        /// </summary>
        /// <value>URL of the organization&#x27;s website</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Logo of this organization
        /// </summary>
        /// <value>Logo of this organization</value>

        [MaxLength(2048)]
        [DataMember(Name = "logo")]
        public string Logo { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organizational unit which is the parent of this organization. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "parent")]
        public OneOfassociationsexternalmeIssuerParent Parent { get; set; }

        /// <summary>
        /// All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>All the organizational units for which this organization is the parent. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "children")]
        public List<OneOfassociationsexternalmeIssuerChildrenItems> Children { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

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
            sb.Append("class AssociationsexternalmeIssuer {\n");
            sb.Append("  OrganizationId: ").Append(OrganizationId).Append("\n");
            sb.Append("  PrimaryCode: ").Append(PrimaryCode).Append("\n");
            sb.Append("  OrganizationType: ").Append(OrganizationType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  ShortName: ").Append(ShortName).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Addresses: ").Append(Addresses).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  Logo: ").Append(Logo).Append("\n");
            sb.Append("  OtherCodes: ").Append(OtherCodes).Append("\n");
            sb.Append("  Parent: ").Append(Parent).Append("\n");
            sb.Append("  Children: ").Append(Children).Append("\n");
            sb.Append("  Consumers: ").Append(Consumers).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AssociationsexternalmeIssuer)obj);
        }

        /// <summary>
        /// Returns true if AssociationsexternalmeIssuer instances are equal
        /// </summary>
        /// <param name="other">Instance of AssociationsexternalmeIssuer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AssociationsexternalmeIssuer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    OrganizationId == other.OrganizationId ||
                    OrganizationId != null &&
                    OrganizationId.Equals(other.OrganizationId)
                ) &&
                (
                    PrimaryCode == other.PrimaryCode ||
                    PrimaryCode != null &&
                    PrimaryCode.Equals(other.PrimaryCode)
                ) &&
                (
                    OrganizationType == other.OrganizationType ||
                    OrganizationType != null &&
                    OrganizationType.Equals(other.OrganizationType)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.SequenceEqual(other.Name)
                ) &&
                (
                    ShortName == other.ShortName ||
                    ShortName != null &&
                    ShortName.Equals(other.ShortName)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.SequenceEqual(other.Description)
                ) &&
                (
                    Addresses == other.Addresses ||
                    Addresses != null &&
                    Addresses.SequenceEqual(other.Addresses)
                ) &&
                (
                    Link == other.Link ||
                    Link != null &&
                    Link.Equals(other.Link)
                ) &&
                (
                    Logo == other.Logo ||
                    Logo != null &&
                    Logo.Equals(other.Logo)
                ) &&
                (
                    OtherCodes == other.OtherCodes ||
                    OtherCodes != null &&
                    OtherCodes.SequenceEqual(other.OtherCodes)
                ) &&
                (
                    Parent == other.Parent ||
                    Parent != null &&
                    Parent.Equals(other.Parent)
                ) &&
                (
                    Children == other.Children ||
                    Children != null &&
                    Children.SequenceEqual(other.Children)
                ) &&
                (
                    Consumers == other.Consumers ||
                    Consumers != null &&
                    Consumers.SequenceEqual(other.Consumers)
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
                if (OrganizationId != null)
                    hashCode = hashCode * 59 + OrganizationId.GetHashCode();
                if (PrimaryCode != null)
                    hashCode = hashCode * 59 + PrimaryCode.GetHashCode();
                if (OrganizationType != null)
                    hashCode = hashCode * 59 + OrganizationType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (ShortName != null)
                    hashCode = hashCode * 59 + ShortName.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (Addresses != null)
                    hashCode = hashCode * 59 + Addresses.GetHashCode();
                if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                if (Logo != null)
                    hashCode = hashCode * 59 + Logo.GetHashCode();
                if (OtherCodes != null)
                    hashCode = hashCode * 59 + OtherCodes.GetHashCode();
                if (Parent != null)
                    hashCode = hashCode * 59 + Parent.GetHashCode();
                if (Children != null)
                    hashCode = hashCode * 59 + Children.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(AssociationsexternalmeIssuer left, AssociationsexternalmeIssuer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssociationsexternalmeIssuer left, AssociationsexternalmeIssuer right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
