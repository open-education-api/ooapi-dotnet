using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EducationSpecifications : Pagination<EducationSpecification>
    {
        public EducationSpecifications()
        {

        }

        public EducationSpecifications(Pagination<EducationSpecification> pagination)
        {
            EducationSpecifications result = new EducationSpecifications();
            result.TotalPages = pagination.TotalPages;
            result.HasPreviousPage = pagination.HasPreviousPage;
            result.HasNextPage = pagination.HasNextPage;
            result.PageNumber = pagination.PageNumber;
            result.PageSize = pagination.PageSize;
            AddItems((IEnumerable<EducationSpecification>)pagination.PaginationItems);
        }

        private void AddItems(IEnumerable<EducationSpecification> educationSpecifications)
        {
            if (Items == null)
                Items = new List<EducationSpecification>();
            Items.AddRange(educationSpecifications);
        }
        /// <summary>
        /// Array of objects (EducationSpecification) 
        /// </summary>
        /// <value>Array of objects (EducationSpecification) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<EducationSpecification> Items
        {
            get
            {
                return PaginationItems;
            }
            set
            {
                PaginationItems = value;
            }
        }
    }
}
