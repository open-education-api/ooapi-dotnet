using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// Object for communicating data to a specific consumer (destination). This object has no relationship with the &#x60;consumer&#x60; query parameter.
    /// </summary>
    [DataContract(Name = "Consumer")]
    public partial class Consumer : IEquatable<Consumer>
    {


        /// <summary>
        /// Gets or Sets ConsumerKey
        /// </summary>
        [Required]

        [DataMember(Name = "consumerKey")]
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or Sets additional
        /// </summary>
        [DataMember(Name = "additional")]
        public string Additional { get; set; }

        /// <summary>
        /// Gets or Sets attributes
        /// </summary>
        [DataMember(Name = "attributes")]
        public string Attributes { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Consumer {\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson()
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
            return obj.GetType() == GetType() && Equals((Consumer)obj);
        }

        /// <summary>
        /// Returns true if Consumer instances are equal
        /// </summary>
        /// <param name="other">Instance of Consumer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Consumer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return false;
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
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Consumer left, Consumer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Consumer left, Consumer right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
