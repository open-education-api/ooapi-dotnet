using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// The amount of effort to complete this education in the specified unit.
    /// </summary>
    [DataContract]
    public partial class ProgramResultStudyLoad : IEquatable<ProgramResultStudyLoad>
    {


        /// <summary>
        /// The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours 
        /// </summary>
        /// <value>The unit in which the studyload is specfied - contacttime: CONTACTUUR amount of time spent in classes - ects: ECTS_PUNT European Credit Transfer System - sbu: SBU studentloadhours - sp: STUDIEPUNT studentpoints - hour: UUR hours </value>

        [DataMember(Name = "studyLoadUnit")]
        public StudyLoadUnitEnum? StudyLoadUnit { get; set; }

        /// <summary>
        /// The amount of load depicted in numbers
        /// </summary>
        /// <value>The amount of load depicted in numbers</value>

        [DataMember(Name = "value")]
        public decimal? Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ProgramResultStudyLoad {\n");
            sb.Append("  StudyLoadUnit: ").Append(StudyLoadUnit).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ProgramResultStudyLoad)obj);
        }

        /// <summary>
        /// Returns true if ProgramResultStudyLoad instances are equal
        /// </summary>
        /// <param name="other">Instance of ProgramResultStudyLoad to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ProgramResultStudyLoad other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    StudyLoadUnit == other.StudyLoadUnit ||
                    StudyLoadUnit != null &&
                    StudyLoadUnit.Equals(other.StudyLoadUnit)
                ) &&
                (
                    Value == other.Value ||
                    Value != null &&
                    Value.Equals(other.Value)
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
                if (StudyLoadUnit != null)
                    hashCode = hashCode * 59 + StudyLoadUnit.GetHashCode();
                if (Value != null)
                    hashCode = hashCode * 59 + Value.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(ProgramResultStudyLoad left, ProgramResultStudyLoad right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ProgramResultStudyLoad left, ProgramResultStudyLoad right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
