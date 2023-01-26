using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ooapi.v5.Enums;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class Consumer
    {

        public string ConsumerKey { get; set; }

        public ConsumerPropertyTypeEnum PropertyType { get; set; } = 0;

        public string PropertyName { get; set; }

        public string PropertyValue{ get; set; }

    }
}
