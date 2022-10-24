using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class EducationSpecificationExpanded : IEquatable<EducationSpecificationExpanded>
    {
        /// <summary>
        /// Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.
        /// </summary>
        /// <value>Timeline overrides allow an implementation to provide versions of entities that will be valid in the future or have been in the past.</value>

        [DataMember(Name = "timelineOverrides")]
        public List<EducationSpecificationExpandedTimelineOverrides> TimelineOverrides { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class EducationSpecificationExpanded {\n");
            sb.Append("  TimelineOverrides: ").Append(TimelineOverrides).Append("\n");
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
            return obj.GetType() == GetType() && Equals((EducationSpecificationExpanded)obj);
        }

        /// <summary>
        /// Returns true if EducationSpecificationExpanded instances are equal
        /// </summary>
        /// <param name="other">Instance of EducationSpecificationExpanded to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(EducationSpecificationExpanded other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    TimelineOverrides == other.TimelineOverrides ||
                    TimelineOverrides != null &&
                    TimelineOverrides.SequenceEqual(other.TimelineOverrides)
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
                if (TimelineOverrides != null)
                    hashCode = hashCode * 59 + TimelineOverrides.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(EducationSpecificationExpanded left, EducationSpecificationExpanded right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EducationSpecificationExpanded left, EducationSpecificationExpanded right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
