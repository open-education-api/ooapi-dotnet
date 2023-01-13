using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class OrganizationConsumer : Consumer
    {


        public Guid OrganizationId { get; set; }

        public Organization Organization { get; set; }



    }
}
