using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class CourseOfferingAssociationExpandable
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
