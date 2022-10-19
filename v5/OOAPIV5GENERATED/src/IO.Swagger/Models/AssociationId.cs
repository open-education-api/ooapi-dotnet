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
    public partial class AssociationId : IEquatable<AssociationId>
    {
        /// <summary>
        /// Unique id of this association
        /// </summary>
        /// <value>Unique id of this association</value>
        [Required]

        [DataMember(Name = "associationId")]
        public Guid? _AssociationId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class AssociationId {\n");
            sb.Append("  _AssociationId: ").Append(_AssociationId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AssociationId)obj);
        }

        /// <summary>
        /// Returns true if AssociationId instances are equal
        /// </summary>
        /// <param name="other">Instance of AssociationId to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AssociationId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    _AssociationId == other._AssociationId ||
                    _AssociationId != null &&
                    _AssociationId.Equals(other._AssociationId)
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
                if (_AssociationId != null)
                    hashCode = hashCode * 59 + _AssociationId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(AssociationId left, AssociationId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssociationId left, AssociationId right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
