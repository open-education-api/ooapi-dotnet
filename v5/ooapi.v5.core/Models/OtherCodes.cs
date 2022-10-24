using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class OtherCodes : IEquatable<OtherCodes>
    {
        /// <summary>
        /// The code/identifier type.   This is an *extensible enumeration*. Use &#x60;x-&#x60; to prefix custom values  The predefined values are:   - &#x60;brin&#x60;: The registration number for a Dutch educational institution that is issued by the Dutch Ministry of Education, Culture and Science   - &#x60;crohoCreboCode&#x60;: programs with a CREBO and CROHO number are accredited by the Dutch Ministry of Education, Culture and Science (OCW)   - &#x60;programCode&#x60;: Identifier for the program (collection of courses)   - &#x60;componentCode&#x60;: The code for a component (part of a course)   - &#x60;offeringCode&#x60;: The code to identify a specific offering (program, course or component offering)   - &#x60;organizationId&#x60;: The identifier for the organization   - &#x60;buildingId&#x60;: The number or code to identify a building   - &#x60;bagId&#x60;: The identification of a building as it is known in the Dutch Building Administration (BAG)   - &#x60;roomCode&#x60;: The code for a room   - &#x60;systemId&#x60;: Identifier assigned to an entity in context of a specific system   - &#x60;productId&#x60;: Identifier assigned to a specific product   - &#x60;nationalIdentityNumber&#x60;: Identifier assigned by the governement of the person. e.g. a social security number in the USA   - &#x60;studentNumber&#x60;: Identifier for the student   - &#x60;studielinkNumber&#x60;: Identifier for the person as determined by Studielink   - &#x60;esi&#x60;: European Student Identifier   - &#x60;userName&#x60;: The name of a user   - &#x60;accountId&#x60;: Identifier assigned to a specific account   - &#x60;emailAdress&#x60;: An email address   - &#x60;groupCode&#x60;: The identifier for a group (of persons)   - &#x60;isbn&#x60;: International Standard Book Number that serve as product identifiers for Books   - &#x60;issn&#x60;: International Standard Book Number that serve as product identifiers for periodicals   - &#x60;orcId&#x60;: Open Researcher and Contributor ID   - &#x60;uuid&#x60;: A universally unique identifier   - &#x60;schacHome&#x60;: Home organization using the domain name of the organization   - &#x60;identifier&#x60;: Generic Identifier 
        /// </summary>
        /// <value>The code/identifier type.   This is an *extensible enumeration*. Use &#x60;x-&#x60; to prefix custom values  The predefined values are:   - &#x60;brin&#x60;: The registration number for a Dutch educational institution that is issued by the Dutch Ministry of Education, Culture and Science   - &#x60;crohoCreboCode&#x60;: programs with a CREBO and CROHO number are accredited by the Dutch Ministry of Education, Culture and Science (OCW)   - &#x60;programCode&#x60;: Identifier for the program (collection of courses)   - &#x60;componentCode&#x60;: The code for a component (part of a course)   - &#x60;offeringCode&#x60;: The code to identify a specific offering (program, course or component offering)   - &#x60;organizationId&#x60;: The identifier for the organization   - &#x60;buildingId&#x60;: The number or code to identify a building   - &#x60;bagId&#x60;: The identification of a building as it is known in the Dutch Building Administration (BAG)   - &#x60;roomCode&#x60;: The code for a room   - &#x60;systemId&#x60;: Identifier assigned to an entity in context of a specific system   - &#x60;productId&#x60;: Identifier assigned to a specific product   - &#x60;nationalIdentityNumber&#x60;: Identifier assigned by the governement of the person. e.g. a social security number in the USA   - &#x60;studentNumber&#x60;: Identifier for the student   - &#x60;studielinkNumber&#x60;: Identifier for the person as determined by Studielink   - &#x60;esi&#x60;: European Student Identifier   - &#x60;userName&#x60;: The name of a user   - &#x60;accountId&#x60;: Identifier assigned to a specific account   - &#x60;emailAdress&#x60;: An email address   - &#x60;groupCode&#x60;: The identifier for a group (of persons)   - &#x60;isbn&#x60;: International Standard Book Number that serve as product identifiers for Books   - &#x60;issn&#x60;: International Standard Book Number that serve as product identifiers for periodicals   - &#x60;orcId&#x60;: Open Researcher and Contributor ID   - &#x60;uuid&#x60;: A universally unique identifier   - &#x60;schacHome&#x60;: Home organization using the domain name of the organization   - &#x60;identifier&#x60;: Generic Identifier </value>
        [Required]

        [DataMember(Name = "codeType")]
        public string CodeType { get; set; }

        /// <summary>
        /// Human readable value for the code/identifier
        /// </summary>
        /// <value>Human readable value for the code/identifier</value>
        [Required]

        [DataMember(Name = "code")]
        public string Code { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class OtherCodes {\n");
            sb.Append("  CodeType: ").Append(CodeType).Append("\n");
            sb.Append("  Code: ").Append(Code).Append("\n");
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
            return obj.GetType() == GetType() && Equals((OtherCodes)obj);
        }

        /// <summary>
        /// Returns true if OtherCodes instances are equal
        /// </summary>
        /// <param name="other">Instance of OtherCodes to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(OtherCodes other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    CodeType == other.CodeType ||
                    CodeType != null &&
                    CodeType.Equals(other.CodeType)
                ) &&
                (
                    Code == other.Code ||
                    Code != null &&
                    Code.Equals(other.Code)
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
                if (CodeType != null)
                    hashCode = hashCode * 59 + CodeType.GetHashCode();
                if (Code != null)
                    hashCode = hashCode * 59 + Code.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(OtherCodes left, OtherCodes right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OtherCodes left, OtherCodes right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
