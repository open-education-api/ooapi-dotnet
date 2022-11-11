using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Buildings : Pagination<Building>
    {
        public Buildings()
        {

        }

        public Buildings(Pagination<Building> pagination)
        {
            Buildings result = new Buildings();
            result.TotalPages = pagination.TotalPages;
            result.HasPreviousPage = pagination.HasPreviousPage;
            result.HasNextPage = pagination.HasNextPage;
            result.PageNumber = pagination.PageNumber;
            result.PageSize = pagination.PageSize;
            AddItems((IEnumerable<Building>)pagination.PaginationItems);
        }

        /// <summary>
        /// Array of objects (Building) 
        /// </summary>
        /// <value>Array of objects (Building) </value>
        [JsonRequired]

        [JsonProperty(PropertyName = "items")]
        public List<Building> Items
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

        private void AddItems(IEnumerable<Building> buildings)
        {
            if (Items == null)
                Items = new List<Building>();
            Items.AddRange(buildings);
        }
    }
}
