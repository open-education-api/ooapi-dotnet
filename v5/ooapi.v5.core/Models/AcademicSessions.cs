using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AcademicSessions : Pagination<AcademicSession>
    {
        /// <summary>
        /// Array of objects (AcademicSession) 
        /// </summary>
        /// <value>Array of objects (AcademicSessions) </value>
        [JsonRequired]
        [JsonProperty(PropertyName = "items")]
        public List<AcademicSession> Items
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
