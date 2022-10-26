using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Offering
    {
        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }



        /// <summary>
        /// The type of this offering
        /// </summary>
        /// <value>The type of this offering</value>
        [JsonRequired]

        [DataMember(Name = "offeringType")]
        public OfferingTypeEnum? OfferingType { get; set; }

        /// <summary>
        /// Gets or Sets AcademicSession
        /// </summary>

        [DataMember(Name = "academicSession")]
        public OneOfAcademicSession AcademicSession { get; set; }

        /// <summary>
        /// The name of this offering
        /// </summary>
        /// <value>The name of this offering</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation or internal code used to identify this offering
        /// </summary>
        /// <value>The abbreviation or internal code used to identify this offering</value>

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The description of this offering.
        /// </summary>
        /// <value>The description of this offering.</value>
        [JsonRequired]

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }


        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        public List<ModeOfDeliveryEnum> ModeOfDelivery { get; set; }

        /// <summary>
        /// The maximum number of students allowed to enroll for this offering
        /// </summary>
        /// <value>The maximum number of students allowed to enroll for this offering</value>

        [DataMember(Name = "maxNumberStudents")]
        public decimal? MaxNumberStudents { get; set; }

        /// <summary>
        /// The number of students that have already enrolled for this offering
        /// </summary>
        /// <value>The number of students that have already enrolled for this offering</value>

        [DataMember(Name = "enrolledNumberStudents")]
        public decimal? EnrolledNumberStudents { get; set; }

        /// <summary>
        /// The number of students that have a pending enrollment request for this offering
        /// </summary>
        /// <value>The number of students that have a pending enrollment request for this offering</value>

        [DataMember(Name = "pendingNumberStudents")]
        public decimal? PendingNumberStudents { get; set; }

        /// <summary>
        /// The minimum number of students needed for this offering to proceed
        /// </summary>
        /// <value>The minimum number of students needed for this offering to proceed</value>

        [DataMember(Name = "minNumberStudents")]
        public decimal? MinNumberStudents { get; set; }

        /// <summary>
        /// resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain “grade” information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true 
        /// </summary>
        /// <value>resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain “grade” information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true </value>
        [JsonRequired]

        [DataMember(Name = "resultExpected")]
        public bool? ResultExpected { get; set; }



        /// <summary>
        /// The result value type for this offering
        /// </summary>
        /// <value>The result value type for this offering</value>

        [DataMember(Name = "resultValueType")]
        public ResultValueTypeEnum? ResultValueType { get; set; }

        /// <summary>
        /// URL of this offering&#x27;s webpage.
        /// </summary>
        /// <value>URL of this offering&#x27;s webpage.</value>

        [MaxLength(2048)]
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [DataMember(Name = "consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [DataMember(Name = "ext")]
        public Object Ext { get; set; }
    }
}
