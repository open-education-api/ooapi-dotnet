using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.Many2Many;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class BuildingConsumer : Consumer
    {


        public Guid BuildingId { get; set; }

        public Building Building{ get; set; }



    }
}
