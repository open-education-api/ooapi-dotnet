using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class ProgramConsumer : Consumer
    {


        public Guid ProgramId { get; set; }

        public Program Program { get; set; }



    }
}
