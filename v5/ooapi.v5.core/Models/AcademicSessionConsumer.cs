using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class AcademicSessionConsumer: Consumer
    {


        public Guid AcademicSessionId { get; set; }

        public AcademicSession AcademicSession { get; set; }



    }
}