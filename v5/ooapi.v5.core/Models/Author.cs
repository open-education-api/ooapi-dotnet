using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    [DataContract]
    public class Author
    {
        public Guid AuthorId { get; set; }

        public string Value { get; set; }
    }
}
