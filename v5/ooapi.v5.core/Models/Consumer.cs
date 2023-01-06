using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.Many2Many;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class Consumer
    {

        public string ConsumerKey { get; set; }

        public string PropertyName { get; set; }

        public string PropertyValue{ get; set; }

    }
}
