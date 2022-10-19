using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class EducationSpecificationId : IEquatable<EducationSpecificationId>
    {
        /// <summary>
        /// Unique id for this education specification
        /// </summary>
        /// <value>Unique id for this education specification</value>
        [Required]

        [DataMember(Name = "educationSpecificationId")]
        public Guid? _EducationSpecificationId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EducationSpecificationId {\n");
            sb.Append("  _EducationSpecificationId: ").Append(_EducationSpecificationId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((EducationSpecificationId)obj);
        }

        /// <summary>
        /// Returns true if EducationSpecificationId instances are equal
        /// </summary>
        /// <param name="other">Instance of EducationSpecificationId to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EducationSpecificationId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    _EducationSpecificationId == other._EducationSpecificationId ||
                    _EducationSpecificationId != null &&
                    _EducationSpecificationId.Equals(other._EducationSpecificationId)
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
                if (_EducationSpecificationId != null)
                    hashCode = hashCode * 59 + _EducationSpecificationId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(EducationSpecificationId left, EducationSpecificationId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EducationSpecificationId left, EducationSpecificationId right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
