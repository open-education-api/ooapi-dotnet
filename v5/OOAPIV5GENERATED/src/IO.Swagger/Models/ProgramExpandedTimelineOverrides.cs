using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A time-line override of the program.
    /// </summary>
    [DataContract]
    public partial class ProgramExpandedTimelineOverrides : IEquatable<ProgramExpandedTimelineOverrides>
    {
        /// <summary>
        /// The day on which this timelineOverride starts (inclusive), RFC3339 (date)
        /// </summary>
        /// <value>The day on which this timelineOverride starts (inclusive), RFC3339 (date)</value>
        [Required]

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The day on which this timelineOverride ends (exclusive), RFC3339 (date)
        /// </summary>
        /// <value>The day on which this timelineOverride ends (exclusive), RFC3339 (date)</value>

        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Gets or Sets Program
        /// </summary>
        [Required]

        [DataMember(Name = "program")]
        public Program Program { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProgramExpandedTimelineOverrides {\n");
            sb.Append("  ValidFrom: ").Append(ValidFrom).Append("\n");
            sb.Append("  ValidTo: ").Append(ValidTo).Append("\n");
            sb.Append("  Program: ").Append(Program).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProgramExpandedTimelineOverrides)obj);
        }

        /// <summary>
        /// Returns true if ProgramExpandedTimelineOverrides instances are equal
        /// </summary>
        /// <param name="other">Instance of ProgramExpandedTimelineOverrides to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProgramExpandedTimelineOverrides other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    ValidFrom == other.ValidFrom ||
                    ValidFrom != null &&
                    ValidFrom.Equals(other.ValidFrom)
                ) &&
                (
                    ValidTo == other.ValidTo ||
                    ValidTo != null &&
                    ValidTo.Equals(other.ValidTo)
                ) &&
                (
                    Program == other.Program ||
                    Program != null &&
                    Program.Equals(other.Program)
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
                if (ValidFrom != null)
                    hashCode = hashCode * 59 + ValidFrom.GetHashCode();
                if (ValidTo != null)
                    hashCode = hashCode * 59 + ValidTo.GetHashCode();
                if (Program != null)
                    hashCode = hashCode * 59 + Program.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ProgramExpandedTimelineOverrides left, ProgramExpandedTimelineOverrides right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProgramExpandedTimelineOverrides left, ProgramExpandedTimelineOverrides right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
