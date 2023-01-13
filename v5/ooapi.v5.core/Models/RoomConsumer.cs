using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class RoomConsumer : Consumer
    {


        public Guid RoomId { get; set; }

        public Room Room { get; set; }



    }
}
