using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A result as part of an association
    /// </summary>
    [DataContract]
    public partial class Result : IEquatable<Result>
    {

        /// <summary>
        /// The state of this result
        /// </summary>
        /// <value>The state of this result</value>
        [Required]

        [DataMember(Name = "state")]
        public ResultStateEnum? State { get; set; }


        /// <summary>
        /// The state of this result
        /// </summary>
        /// <value>The state of this result</value>

        [DataMember(Name = "pass")]
        public PassEnum? Pass { get; set; }

        /// <summary>
        /// The comment on this result
        /// </summary>
        /// <value>The comment on this result</value>

        [DataMember(Name = "comment")]
        public string Comment { get; set; }

        /// <summary>
        /// The score of this program/course/component association (based on resultValueType in offering)
        /// </summary>
        /// <value>The score of this program/course/component association (based on resultValueType in offering)</value>

        [DataMember(Name = "score")]
        public string Score { get; set; }

        /// <summary>
        /// The date this result has been published, RFC3339 (full-date)
        /// </summary>
        /// <value>The date this result has been published, RFC3339 (full-date)</value>
        [Required]

        [DataMember(Name = "resultDate")]
        public DateTime? ResultDate { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Result {\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Pass: ").Append(Pass).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  Score: ").Append(Score).Append("\n");
            sb.Append("  ResultDate: ").Append(ResultDate).Append("\n");
            sb.Append("  Ext: ").Append(Ext).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Result)obj);
        }

        /// <summary>
        /// Returns true if Result instances are equal
        /// </summary>
        /// <param name="other">Instance of Result to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Result other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    State == other.State ||
                    State != null &&
                    State.Equals(other.State)
                ) &&
                (
                    Pass == other.Pass ||
                    Pass != null &&
                    Pass.Equals(other.Pass)
                ) &&
                (
                    Comment == other.Comment ||
                    Comment != null &&
                    Comment.Equals(other.Comment)
                ) &&
                (
                    Score == other.Score ||
                    Score != null &&
                    Score.Equals(other.Score)
                ) &&
                (
                    ResultDate == other.ResultDate ||
                    ResultDate != null &&
                    ResultDate.Equals(other.ResultDate)
                ) &&
                (
                    Ext == other.Ext ||
                    Ext != null &&
                    Ext.Equals(other.Ext)
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
                if (State != null)
                    hashCode = hashCode * 59 + State.GetHashCode();
                if (Pass != null)
                    hashCode = hashCode * 59 + Pass.GetHashCode();
                if (Comment != null)
                    hashCode = hashCode * 59 + Comment.GetHashCode();
                if (Score != null)
                    hashCode = hashCode * 59 + Score.GetHashCode();
                if (ResultDate != null)
                    hashCode = hashCode * 59 + ResultDate.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Result left, Result right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Result left, Result right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
