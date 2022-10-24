using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A collection of courses that lead to a certifiable learning outcome
    /// </summary>
    [DataContract]
    public partial class ProgramId : IEquatable<ProgramId>
    {
        /// <summary>
        /// Unique id for this program
        /// </summary>
        /// <value>Unique id for this program</value>
        [Required]

        [DataMember(Name = "programId")]
        public Guid? _ProgramId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProgramId {\n");
            sb.Append("  _ProgramId: ").Append(_ProgramId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProgramId)obj);
        }

        /// <summary>
        /// Returns true if ProgramId instances are equal
        /// </summary>
        /// <param name="other">Instance of ProgramId to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProgramId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    _ProgramId == other._ProgramId ||
                    _ProgramId != null &&
                    _ProgramId.Equals(other._ProgramId)
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
                if (_ProgramId != null)
                    hashCode = hashCode * 59 + _ProgramId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ProgramId left, ProgramId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProgramId left, ProgramId right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
