using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ooapi.v5.Enums;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class Consumer
    {
        public Guid Id { get; set; }

        public string ModelTypeName { get; set; }

        public string ConsumerKey { get; set; }

        public ConsumerPropertyTypeEnum PropertyType { get; set; } = 0;

        public string PropertyName { get; set; }

        public string PropertyValue{ get; set; }

    }
}
