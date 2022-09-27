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
    /// A metadataset providing details on the provider of this OOAPI implementation
    /// </summary>
    [DataContract]
    public partial class Service : IEquatable<Service>
    { 
        /// <summary>
        /// Contact e-mail address of the service owner
        /// </summary>
        /// <value>Contact e-mail address of the service owner</value>
        [Required]

        [DataMember(Name="contactEmail")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// URL of the API specification (YAML or JSON, compliant with [Open API Specification v3](https://github.com/OAI/OpenAPI-Specification/))
        /// </summary>
        /// <value>URL of the API specification (YAML or JSON, compliant with [Open API Specification v3](https://github.com/OAI/OpenAPI-Specification/))</value>
        [Required]

        [MaxLength(2048)]
        [DataMember(Name="specification")]
        public string Specification { get; set; }

        /// <summary>
        /// URL of the API documentation, including general terms and privacy statement
        /// </summary>
        /// <value>URL of the API documentation, including general terms and privacy statement</value>
        [Required]

        [MaxLength(2048)]
        [DataMember(Name="documentation")]
        public string Documentation { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name="consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name="ext")]
        public Object Ext { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Service {\n");
            sb.Append("  ContactEmail: ").Append(ContactEmail).Append("\n");
            sb.Append("  Specification: ").Append(Specification).Append("\n");
            sb.Append("  Documentation: ").Append(Documentation).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Service)obj);
        }

        /// <summary>
        /// Returns true if Service instances are equal
        /// </summary>
        /// <param name="other">Instance of Service to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Service other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    ContactEmail == other.ContactEmail ||
                    ContactEmail != null &&
                    ContactEmail.Equals(other.ContactEmail)
                ) && 
                (
                    Specification == other.Specification ||
                    Specification != null &&
                    Specification.Equals(other.Specification)
                ) && 
                (
                    Documentation == other.Documentation ||
                    Documentation != null &&
                    Documentation.Equals(other.Documentation)
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
                    if (ContactEmail != null)
                    hashCode = hashCode * 59 + ContactEmail.GetHashCode();
                    if (Specification != null)
                    hashCode = hashCode * 59 + Specification.GetHashCode();
                    if (Documentation != null)
                    hashCode = hashCode * 59 + Documentation.GetHashCode();
                    if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                    if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Service left, Service right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Service left, Service right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
