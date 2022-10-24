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
    public partial class PersonId : IEquatable<PersonId>
    {
        /// <summary>
        /// Unique id of this person
        /// </summary>
        /// <value>Unique id of this person</value>
        [Required]

        [DataMember(Name = "personId")]
        public Guid? _PersonId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PersonId {\n");
            sb.Append("  _PersonId: ").Append(_PersonId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PersonId)obj);
        }

        /// <summary>
        /// Returns true if PersonId instances are equal
        /// </summary>
        /// <param name="other">Instance of PersonId to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PersonId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    _PersonId == other._PersonId ||
                    _PersonId != null &&
                    _PersonId.Equals(other._PersonId)
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
                if (_PersonId != null)
                    hashCode = hashCode * 59 + _PersonId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(PersonId left, PersonId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonId left, PersonId right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
