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
    /// 
    /// </summary>
    [DataContract]
    public partial class CourseOfferingAssociation : IEquatable<CourseOfferingAssociation>
    { 
        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        [DataMember(Name="result")]
        public Object Result { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class CourseOfferingAssociation {\n");
            sb.Append("  Result: ").Append(Result).Append("\n");
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
            return obj.GetType() == GetType() && Equals((CourseOfferingAssociation)obj);
        }

        /// <summary>
        /// Returns true if CourseOfferingAssociation instances are equal
        /// </summary>
        /// <param name="other">Instance of CourseOfferingAssociation to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(CourseOfferingAssociation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Result == other.Result ||
                    Result != null &&
                    Result.Equals(other.Result)
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
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(CourseOfferingAssociation left, CourseOfferingAssociation right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CourseOfferingAssociation left, CourseOfferingAssociation right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
