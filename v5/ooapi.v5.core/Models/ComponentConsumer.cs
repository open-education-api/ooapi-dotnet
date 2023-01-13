using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class ComponentConsumer : Consumer
    {


        public Guid ComponentId { get; set; }

        public Component Component { get; set; }



    }
}
