using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    [DataContract]
    public class Resource
    {
        public Guid ResourceId { get; set; }

        public string Value { get; set; }
    }
}
