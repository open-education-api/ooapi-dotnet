//using Newtonsoft.Json;
//using System;
//using System.Runtime.Serialization;
//using System.Text;

//namespace ooapi.v5.Models
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    [DataContract]
//    public partial class PersonsBody : IEquatable<PersonsBody>
//    {
//        /// <summary>
//        /// Returns the string presentation of the object
//        /// </summary>
//        /// <returns>String presentation of the object</returns>
//        public override string ToString()
//        {
//            var sb = new StringBuilder();
//            sb.Append("class PersonsBody {\n");
//            sb.Append("}\n");
//            return sb.ToString();
//        }

//        /// <summary>
//        /// Returns the JSON string presentation of the object
//        /// </summary>
//        /// <returns>JSON string presentation of the object</returns>
//        public string ToJson()
//        {
//            return JsonConvert.SerializeObject(this, Formatting.Indented);
//        }

//        /// <summary>
//        /// Returns true if objects are equal
//        /// </summary>
//        /// <param name="obj">Object to be compared</param>
//        /// <returns>Boolean</returns>
//        public override bool Equals(object obj)
//        {
//            if (ReferenceEquals(null, obj)) return false;
//            if (ReferenceEquals(this, obj)) return true;
//            return obj.GetType() == GetType() && Equals((PersonsBody)obj);
//        }

//        /// <summary>
//        /// Returns true if PersonsBody instances are equal
//        /// </summary>
//        /// <param name="other">Instance of PersonsBody to be compared</param>
//        /// <returns>Boolean</returns>
//        public bool Equals(PersonsBody other)
//        {
//            if (ReferenceEquals(null, other)) return false;
//            if (ReferenceEquals(this, other)) return true;

//            return false;
//        }

//        /// <summary>
//        /// Gets the hash code
//        /// </summary>
//        /// <returns>Hash code</returns>
//        public override int GetHashCode()
//        {
//            unchecked // Overflow is fine, just wrap
//            {
//                var hashCode = 41;
//                // Suitable nullity checks etc, of course :)
//                return hashCode;
//            }
//        }

//        #region Operators
//#pragma warning disable 1591

//        public static bool operator ==(PersonsBody left, PersonsBody right)
//        {
//            return Equals(left, right);
//        }

//        public static bool operator !=(PersonsBody left, PersonsBody right)
//        {
//            return !Equals(left, right);
//        }

//#pragma warning restore 1591
//        #endregion Operators
//    }
//}
