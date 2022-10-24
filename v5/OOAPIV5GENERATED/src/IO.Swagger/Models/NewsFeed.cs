using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A placeholder or collection of news items
    /// </summary>
    [DataContract]
    public partial class NewsFeed : IEquatable<NewsFeed>
    {
        /// <summary>
        /// Unique id for this news feed
        /// </summary>
        /// <value>Unique id for this news feed</value>
        [JsonRequired]

        [DataMember(Name = "newsFeedId")]
        public Guid NewsFeedId { get; set; }



        /// <summary>
        /// The type of the object this news feed relates to
        /// </summary>
        /// <value>The type of the object this news feed relates to</value>
        [JsonRequired]

        [DataMember(Name = "newsFeedType")]
        public NewsFeedTypeEnum? NewsFeedType { get; set; }

        /// <summary>
        /// The name for this news feed
        /// </summary>
        /// <value>The name for this news feed</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The description of this news feed.
        /// </summary>
        /// <value>The description of this news feed.</value>
        [JsonRequired]

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

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
            sb.Append("class NewsFeed {\n");
            sb.Append("  NewsFeedId: ").Append(NewsFeedId).Append("\n");
            sb.Append("  NewsFeedType: ").Append(NewsFeedType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
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
            return obj.GetType() == GetType() && Equals((NewsFeed)obj);
        }

        /// <summary>
        /// Returns true if NewsFeed instances are equal
        /// </summary>
        /// <param name="other">Instance of NewsFeed to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NewsFeed other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    NewsFeedId == other.NewsFeedId ||
                    NewsFeedId != null &&
                    NewsFeedId.Equals(other.NewsFeedId)
                ) &&
                (
                    NewsFeedType == other.NewsFeedType ||
                    NewsFeedType != null &&
                    NewsFeedType.Equals(other.NewsFeedType)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.SequenceEqual(other.Name)
                ) &&
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.SequenceEqual(other.Description)
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
                if (NewsFeedId != null)
                    hashCode = hashCode * 59 + NewsFeedId.GetHashCode();
                if (NewsFeedType != null)
                    hashCode = hashCode * 59 + NewsFeedType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(NewsFeed left, NewsFeed right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NewsFeed left, NewsFeed right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
