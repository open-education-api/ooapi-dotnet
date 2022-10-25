using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A system message including the error code and an explanation
    /// </summary>
    [DataContract]
    public partial class InlineResponse400 : IEquatable<InlineResponse400>
    {
        /// <summary>
        /// The HTTP status code
        /// </summary>
        /// <value>The HTTP status code</value>
        [JsonRequired]

        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// A short, human-readable summary of the problem type
        /// </summary>
        /// <value>A short, human-readable summary of the problem type</value>
        [JsonRequired]

        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem
        /// </summary>
        /// <value>A human-readable explanation specific to this occurrence of the problem</value>

        [DataMember(Name = "detail")]
        public string Detail { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class InlineResponse400 {\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  Detail: ").Append(Detail).Append("\n");
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
            return obj.GetType() == GetType() && Equals((InlineResponse400)obj);
        }

        /// <summary>
        /// Returns true if InlineResponse400 instances are equal
        /// </summary>
        /// <param name="other">Instance of InlineResponse400 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(InlineResponse400 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    Status == other.Status ||
                    Status != null &&
                    Status.Equals(other.Status)
                ) &&
                (
                    Title == other.Title ||
                    Title != null &&
                    Title.Equals(other.Title)
                ) &&
                (
                    Detail == other.Detail ||
                    Detail != null &&
                    Detail.Equals(other.Detail)
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
                if (Status != null)
                    hashCode = hashCode * 59 + Status.GetHashCode();
                if (Title != null)
                    hashCode = hashCode * 59 + Title.GetHashCode();
                if (Detail != null)
                    hashCode = hashCode * 59 + Detail.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(InlineResponse400 left, InlineResponse400 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(InlineResponse400 left, InlineResponse400 right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
