using System;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ComponentOfferingAssociation
    {
        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        [DataMember(Name = "result")]
        public Object Result { get; set; }
    }
}
