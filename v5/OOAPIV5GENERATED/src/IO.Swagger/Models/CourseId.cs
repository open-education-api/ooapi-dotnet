using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// An object describing the metadata of a course
    /// </summary>
    [DataContract]
    public partial class CourseId : IEquatable<CourseId>
    {
        /// <summary>
        /// Unique id of this course
        /// </summary>
        /// <value>Unique id of this course</value>
        [Required]

        [DataMember(Name = "courseId")]
        public Guid? _CourseId { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CourseId {\n");
            sb.Append("  _CourseId: ").Append(_CourseId).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CourseId)obj);
        }

        /// <summary>
        /// Returns true if CourseId instances are equal
        /// </summary>
        /// <param name="other">Instance of CourseId to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CourseId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    _CourseId == other._CourseId ||
                    _CourseId != null &&
                    _CourseId.Equals(other._CourseId)
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
                if (_CourseId != null)
                    hashCode = hashCode * 59 + _CourseId.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CourseId left, CourseId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CourseId left, CourseId right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
