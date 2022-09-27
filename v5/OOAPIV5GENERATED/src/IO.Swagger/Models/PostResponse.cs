/*
 * Open Education API
 *
 * OpenAPI (fka Swagger) specification for the Open Education API.  <figure>  <a target=\"_blank\" href=\"OOAPIv5_model.png\">   <img src=\"OOAPIv5_model.png\" alt=\"OOAPI information model that feeds OOAPI specification\" width=\"70%\" class=\"img-responsive\">   </a>   <figcaption> OOAPI information model that feeds OOAPI specification (click to enlage)</figcaption> </figure>  The model provides an overview of how the objects on which the API is specified are related. The overarching concept educations is not found in the in the end points of the API. The smaller concepts of programOffering, courseOffering and conceptOffering are all found in the offering endpoint. The different types of association can all be found in the association endpoint.  The original file for this model can be found <a target=\"_blank\" href=\"OOAPIv5_model_v0.4.drawio\">here</a>  The program relations object is not found as a separate endpoint but relations between programs can be found within the program endpoint by expanding that endpoint.  Information about earlier meetings and presentations can be found <a target=\"_blank\" href=\"https://github.com/open-education-api/notulen\">here</a>  Information on the EDU-API model that was also used for this api is shown <a target=\"_blank\" href=\"eduapi.png\">here</a> 
 *
 * OpenAPI spec version: 5.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{ 
    /// <summary>
    /// A system message as a response to a POST message
    /// </summary>
    [DataContract]
    public partial class PostResponse : IEquatable<PostResponse>
    { 
        /// <summary>
        /// information displayed to user
        /// </summary>
        /// <value>information displayed to user</value>
        [Required]

        [DataMember(Name="message")]
        public List<AddressAdditional> Message { get; set; }

        /// <summary>
        /// URL where additional information can be found e.g. by use of deeplink
        /// </summary>
        /// <value>URL where additional information can be found e.g. by use of deeplink</value>

        [DataMember(Name="redirect")]
        public string Redirect { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PostResponse {\n");
            sb.Append("  Message: ").Append(Message).Append("\n");
            sb.Append("  Redirect: ").Append(Redirect).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PostResponse)obj);
        }

        /// <summary>
        /// Returns true if PostResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of PostResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PostResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Message == other.Message ||
                    Message != null &&
                    Message.SequenceEqual(other.Message)
                ) && 
                (
                    Redirect == other.Redirect ||
                    Redirect != null &&
                    Redirect.Equals(other.Redirect)
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
                    if (Message != null)
                    hashCode = hashCode * 59 + Message.GetHashCode();
                    if (Redirect != null)
                    hashCode = hashCode * 59 + Redirect.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PostResponse left, PostResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PostResponse left, PostResponse right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
