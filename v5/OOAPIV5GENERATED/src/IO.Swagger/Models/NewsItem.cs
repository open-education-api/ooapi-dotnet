using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A newsitem contains the message and metadata of that message
    /// </summary>
    [DataContract]
    public partial class NewsItem : IEquatable<NewsItem>
    {
        /// <summary>
        /// Unique id for this news item
        /// </summary>
        /// <value>Unique id for this news item</value>
        [JsonRequired]

        [DataMember(Name = "newsItemId")]
        public Guid NewsItemId { get; set; }



        /// <summary>
        /// The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging 
        /// </summary>
        /// <value>The type of this news item - calamity: calamiteit - general: algemeen - schedule-change: roosterwijziging - announcement: aankondiging </value>

        [DataMember(Name = "newsItemType")]
        public NewsItemTypeEnum? NewsItemType { get; set; }

        /// <summary>
        /// The name for this news item
        /// </summary>
        /// <value>The name for this news item</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The authors of the article with this news item
        /// </summary>
        /// <value>The authors of the article with this news item</value>

        [DataMember(Name = "authors")]
        public List<string> Authors { get; set; }

        /// <summary>
        /// The url containing the address of the image belonging to this news item
        /// </summary>
        /// <value>The url containing the address of the image belonging to this news item</value>

        [MaxLength(2048)]
        [DataMember(Name = "image")]
        public string Image { get; set; }

        /// <summary>
        /// The url containing the address of the article belonging to this news item
        /// </summary>
        /// <value>The url containing the address of the article belonging to this news item</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// The content of this news item.
        /// </summary>
        /// <value>The content of this news item.</value>

        [DataMember(Name = "content")]
        public List<LanguageValueItem> Content { get; set; }

        /// <summary>
        /// The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)
        /// </summary>
        /// <value>The newsFeeds where this item can be found. [&#x60;expandable&#x60;](#tag/news_feed_model)</value>

        [DataMember(Name = "newsFeeds")]
        public List<Guid> NewsFeeds { get; set; }

        /// <summary>
        /// The moment from which this news item is valid, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment from which this news item is valid, RFC3339 (date-time)</value>

        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// The moment until which this news item is valid, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment until which this news item is valid, RFC3339 (date-time)</value>

        [DataMember(Name = "validUntil")]
        public DateTime? ValidUntil { get; set; }

        /// <summary>
        /// The moment on which this news item was updated, RFC3339 (date-time)
        /// </summary>
        /// <value>The moment on which this news item was updated, RFC3339 (date-time)</value>

        [DataMember(Name = "lastModified")]
        public DateTime? LastModified { get; set; }

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
            sb.Append("class NewsItem {\n");
            sb.Append("  NewsItemId: ").Append(NewsItemId).Append("\n");
            sb.Append("  NewsItemType: ").Append(NewsItemType).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Authors: ").Append(Authors).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  NewsFeeds: ").Append(NewsFeeds).Append("\n");
            sb.Append("  ValidFrom: ").Append(ValidFrom).Append("\n");
            sb.Append("  ValidUntil: ").Append(ValidUntil).Append("\n");
            sb.Append("  LastModified: ").Append(LastModified).Append("\n");
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
            return obj.GetType() == GetType() && Equals((NewsItem)obj);
        }

        /// <summary>
        /// Returns true if NewsItem instances are equal
        /// </summary>
        /// <param name="other">Instance of NewsItem to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(NewsItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    NewsItemId == other.NewsItemId ||
                    NewsItemId != null &&
                    NewsItemId.Equals(other.NewsItemId)
                ) &&
                (
                    NewsItemType == other.NewsItemType ||
                    NewsItemType != null &&
                    NewsItemType.Equals(other.NewsItemType)
                ) &&
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.SequenceEqual(other.Name)
                ) &&
                (
                    Authors == other.Authors ||
                    Authors != null &&
                    Authors.SequenceEqual(other.Authors)
                ) &&
                (
                    Image == other.Image ||
                    Image != null &&
                    Image.Equals(other.Image)
                ) &&
                (
                    Link == other.Link ||
                    Link != null &&
                    Link.Equals(other.Link)
                ) &&
                (
                    Content == other.Content ||
                    Content != null &&
                    Content.SequenceEqual(other.Content)
                ) &&
                (
                    NewsFeeds == other.NewsFeeds ||
                    NewsFeeds != null &&
                    NewsFeeds.SequenceEqual(other.NewsFeeds)
                ) &&
                (
                    ValidFrom == other.ValidFrom ||
                    ValidFrom != null &&
                    ValidFrom.Equals(other.ValidFrom)
                ) &&
                (
                    ValidUntil == other.ValidUntil ||
                    ValidUntil != null &&
                    ValidUntil.Equals(other.ValidUntil)
                ) &&
                (
                    LastModified == other.LastModified ||
                    LastModified != null &&
                    LastModified.Equals(other.LastModified)
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
                if (NewsItemId != null)
                    hashCode = hashCode * 59 + NewsItemId.GetHashCode();
                if (NewsItemType != null)
                    hashCode = hashCode * 59 + NewsItemType.GetHashCode();
                if (Name != null)
                    hashCode = hashCode * 59 + Name.GetHashCode();
                if (Authors != null)
                    hashCode = hashCode * 59 + Authors.GetHashCode();
                if (Image != null)
                    hashCode = hashCode * 59 + Image.GetHashCode();
                if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                if (Content != null)
                    hashCode = hashCode * 59 + Content.GetHashCode();
                if (NewsFeeds != null)
                    hashCode = hashCode * 59 + NewsFeeds.GetHashCode();
                if (ValidFrom != null)
                    hashCode = hashCode * 59 + ValidFrom.GetHashCode();
                if (ValidUntil != null)
                    hashCode = hashCode * 59 + ValidUntil.GetHashCode();
                if (LastModified != null)
                    hashCode = hashCode * 59 + LastModified.GetHashCode();
                if (Consumers != null)
                    hashCode = hashCode * 59 + Consumers.GetHashCode();
                if (Ext != null)
                    hashCode = hashCode * 59 + Ext.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(NewsItem left, NewsItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NewsItem left, NewsItem right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
