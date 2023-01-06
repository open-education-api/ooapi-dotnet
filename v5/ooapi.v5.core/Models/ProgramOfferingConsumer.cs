using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.Many2Many;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class ProgramOfferingConsumer : Consumer
    {


        public Guid ProgramOfferingId { get; set; }

        public ProgramOffering ProgramOffering { get; set; }



    }
}
