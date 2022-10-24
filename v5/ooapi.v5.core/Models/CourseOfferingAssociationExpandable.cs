using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class CourseOfferingAssociationExpandable : IEquatable<CourseOfferingAssociationExpandable>
    {
        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        [DataMember(Name = "result")]
        public Object Result { get; set; }

        /// <summary>
        /// Gets or Sets Person
        /// </summary>

        [DataMember(Name = "person")]
        public ProgramOfferingAssociationExpandablePerson Person { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CourseOfferingAssociationExpandable {\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
            sb.Append("  Person: ").Append(Person).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CourseOfferingAssociationExpandable)obj);
        }

        /// <summary>
        /// Returns true if CourseOfferingAssociationExpandable instances are equal
        /// </summary>
        /// <param name="other">Instance of CourseOfferingAssociationExpandable to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CourseOfferingAssociationExpandable other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Result == other.Result ||
                    Result != null &&
                    Result.Equals(other.Result)
                ) &&
                (
                    Person == other.Person ||
                    Person != null &&
                    Person.Equals(other.Person)
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
                if (Result != null)
                    hashCode = hashCode * 59 + Result.GetHashCode();
                if (Person != null)
                    hashCode = hashCode * 59 + Person.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(CourseOfferingAssociationExpandable left, CourseOfferingAssociationExpandable right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CourseOfferingAssociationExpandable left, CourseOfferingAssociationExpandable right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
