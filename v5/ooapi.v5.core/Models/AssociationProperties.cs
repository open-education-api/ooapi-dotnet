using ooapi.v5.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A relationship between a person object and an offering
    /// </summary>
    [DataContract]
    public partial class AssociationProperties : IEquatable<AssociationProperties>
    {


        /// <summary>
        /// The type of this association
        /// </summary>
        /// <value>The type of this association</value>
        [Required]

        [DataMember(Name = "associationType")]
        public AssociationTypeEnum? AssociationType { get; set; }



        /// <summary>
        /// The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast 
        /// </summary>
        /// <value>The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast </value>
        [Required]

        [DataMember(Name = "role")]
        public RoleEnum? Role { get; set; }



        /// <summary>
        /// The state of this association
        /// </summary>
        /// <value>The state of this association</value>
        [Required]

        [DataMember(Name = "state")]
        public StateEnum? State { get; set; }


        /// <summary>
        /// The state of this association for the institution performing the request.
        /// </summary>
        /// <value>The state of this association for the institution performing the request.</value>

        [DataMember(Name = "remoteState")]
        public RemoteStateEnum? RemoteState { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

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
            sb.Append("class AssociationProperties {\n");
            sb.Append("  AssociationType: ").Append(AssociationType).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  RemoteState: ").Append(RemoteState).Append("\n");
            sb.Append("  Consumers: ").Append(Consumers).Append("\n");
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
            return obj.GetType() == GetType() && Equals((AssociationProperties)obj);
        }

        /// <summary>
        /// Returns true if AssociationProperties instances are equal
        /// </summary>
        /// <param name="other">Instance of AssociationProperties to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(AssociationProperties other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    AssociationType == other.AssociationType ||
                    AssociationType != null &&
                    AssociationType.Equals(other.AssociationType)
                ) &&
                (
                    Role == other.Role ||
                    Role != null &&
                    Role.Equals(other.Role)
                ) &&
                (
                    State == other.State ||
                    State != null &&
                    State.Equals(other.State)
                ) &&
                (
                    RemoteState == other.RemoteState ||
                    RemoteState != null &&
                    RemoteState.Equals(other.RemoteState)
                ) &&
                (
                    Consumers == other.Consumers ||
                    Consumers != null &&
                    Consumers.SequenceEqual(other.Consumers)
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
                if (AssociationType != null)
                    hashCode = hashCode * 59 + AssociationType.GetHashCode();
                if (Role != null)
                    hashCode = hashCode * 59 + Role.GetHashCode();
                if (State != null)
                    hashCode = hashCode * 59 + State.GetHashCode();
                if (RemoteState != null)
                    hashCode = hashCode * 59 + RemoteState.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(AssociationProperties left, AssociationProperties right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AssociationProperties left, AssociationProperties right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
