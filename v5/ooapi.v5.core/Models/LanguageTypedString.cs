using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A String with an associated language code.
    /// </summary>
    [DataContract]
    public partial class LanguageTypedString : IEquatable<LanguageTypedString>
    {
        /// <summary>
        /// The language used in the described entity. A string formatted according to RFC3066.
        /// </summary>
        /// <value>The language used in the described entity. A string formatted according to RFC3066.</value>
        [RegularExpression("/^[a-z]{2,4}(-[A-Z][a-z]{3})?(-([A-Z]{2}|[0-9]{3}))?$/")]
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// String to describe the entity.
        /// </summary>
        /// <value>String to describe the entity.</value>

        [DataMember(Name = "value")]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class LanguageTypedString {\n");
            sb.Append("  Language: ").Append(Language).Append("\n");
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
            return obj.GetType() == GetType() && Equals((LanguageTypedString)obj);
        }

        /// <summary>
        /// Returns true if LanguageTypedString instances are equal
        /// </summary>
        /// <param name="other">Instance of LanguageTypedString to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(LanguageTypedString other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Language == other.Language ||
                    Language != null &&
                    Language.Equals(other.Language)
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
                if (Language != null)
                    hashCode = hashCode * 59 + Language.GetHashCode();
                if (Value != null)
                    hashCode = hashCode * 59 + Value.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(LanguageTypedString left, LanguageTypedString right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LanguageTypedString left, LanguageTypedString right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
