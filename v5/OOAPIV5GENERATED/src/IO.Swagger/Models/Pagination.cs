using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Pagination : IEquatable<Pagination>
    {
        /// <summary>
        /// The number of items per page
        /// </summary>
        /// <value>The number of items per page</value>
        [Required]

        [DataMember(Name = "pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// The current page number
        /// </summary>
        /// <value>The current page number</value>
        [Required]

        [DataMember(Name = "pageNumber")]
        public int? PageNumber { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [Required]

        [DataMember(Name = "hasPreviousPage")]
        public bool? HasPreviousPage { get; set; }

        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        /// <value>Whether there is a previous page</value>
        [Required]

        [DataMember(Name = "hasNextPage")]
        public bool? HasNextPage { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        /// <value>Total number of pages</value>

        [DataMember(Name = "totalPages")]
        public int? TotalPages { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Pagination {\n");
            sb.Append("  PageSize: ").Append(PageSize).Append("\n");
            sb.Append("  PageNumber: ").Append(PageNumber).Append("\n");
            sb.Append("  HasPreviousPage: ").Append(HasPreviousPage).Append("\n");
            sb.Append("  HasNextPage: ").Append(HasNextPage).Append("\n");
            sb.Append("  TotalPages: ").Append(TotalPages).Append("\n");
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
            return obj.GetType() == GetType() && Equals((Pagination)obj);
        }

        /// <summary>
        /// Returns true if Pagination instances are equal
        /// </summary>
        /// <param name="other">Instance of Pagination to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Pagination other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    PageSize == other.PageSize ||
                    PageSize != null &&
                    PageSize.Equals(other.PageSize)
                ) &&
                (
                    PageNumber == other.PageNumber ||
                    PageNumber != null &&
                    PageNumber.Equals(other.PageNumber)
                ) &&
                (
                    HasPreviousPage == other.HasPreviousPage ||
                    HasPreviousPage != null &&
                    HasPreviousPage.Equals(other.HasPreviousPage)
                ) &&
                (
                    HasNextPage == other.HasNextPage ||
                    HasNextPage != null &&
                    HasNextPage.Equals(other.HasNextPage)
                ) &&
                (
                    TotalPages == other.TotalPages ||
                    TotalPages != null &&
                    TotalPages.Equals(other.TotalPages)
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
                if (PageSize != null)
                    hashCode = hashCode * 59 + PageSize.GetHashCode();
                if (PageNumber != null)
                    hashCode = hashCode * 59 + PageNumber.GetHashCode();
                if (HasPreviousPage != null)
                    hashCode = hashCode * 59 + HasPreviousPage.GetHashCode();
                if (HasNextPage != null)
                    hashCode = hashCode * 59 + HasNextPage.GetHashCode();
                if (TotalPages != null)
                    hashCode = hashCode * 59 + TotalPages.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(Pagination left, Pagination right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Pagination left, Pagination right)
        {
            return !Equals(left, right);
        }

#pragma warning restore 1591
        #endregion Operators
    }
}
