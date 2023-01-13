using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class ServiceConsumer : Consumer
    {


        public Guid ServiceId { get; set; }

        public Service Service { get; set; }



    }
}
