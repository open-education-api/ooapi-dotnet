//using ooapi.v5.Enums;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;

//namespace ooapi.v5.Models
//{
//    /// <summary>
//    /// A relationship between a person object and an offering
//    /// </summary>
//    [DataContract]
//    public partial class AssociationProperties 
//    {


//        /// <summary>
//        /// The type of this association
//        /// </summary>
//        /// <value>The type of this association</value>
//        [Required]

//        [DataMember(Name = "associationType")]
//        public AssociationTypeEnum? AssociationType { get; set; }



//        /// <summary>
//        /// The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast 
//        /// </summary>
//        /// <value>The role of this person associated with the offering   - student: student   - lecturer: docent   - teaching assistant: studentassistent   - coordinator: coördinator   - guest: gast </value>
//        [Required]

//        [DataMember(Name = "role")]
//        public RoleEnum? Role { get; set; }



//        /// <summary>
//        /// The state of this association
//        /// </summary>
//        /// <value>The state of this association</value>
//        [Required]

//        [DataMember(Name = "state")]
//        public StateEnum? State { get; set; }


//        /// <summary>
//        /// The state of this association for the institution performing the request.
//        /// </summary>
//        /// <value>The state of this association for the institution performing the request.</value>

//        [DataMember(Name = "remoteState")]
//        public RemoteStateEnum? RemoteState { get; set; }

//        /// <summary>
//        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
//        /// </summary>
//        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

//        [DataMember(Name = "consumers")]
//        public List<Consumer> Consumers { get; set; }


//    }
//}
