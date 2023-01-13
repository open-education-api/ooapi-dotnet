using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class EducationSpecificationConsumer : Consumer
    {


        public Guid EducationSpecificationId { get; set; }

        public EducationSpecification EducationSpecification { get; set; }



    }
}
