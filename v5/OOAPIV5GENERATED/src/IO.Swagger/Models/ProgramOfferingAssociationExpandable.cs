using System;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class ProgramOfferingAssociationExpandable
    {
        /// <summary>
        /// Gets or Sets Result
        /// </summary>

        [DataMember(Name = "result")]
        public Object Result { get; set; }

        /// <summary>
        /// Gets or Sets Person
        /// </summary>

        [DataMember(Name = "person")]
        public ProgramOfferingAssociationExpandablePerson Person { get; set; }
    }
}
