using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
    /// </summary>
    [DataContract]
    public partial class ModesOfDelivery : List<string>
    {
    }
}
