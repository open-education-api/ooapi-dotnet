using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.core.Models.Many2Many;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{

    public class EducationSpecificationConsumer : Consumer
    {


        public Guid EducationSpecificationId { get; set; }

        public EducationSpecification EducationSpecification { get; set; }



    }
}
