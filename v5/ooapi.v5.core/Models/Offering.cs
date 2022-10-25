using Newtonsoft.Json;
using ooapi.v5.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Offering : ModelBase
    {
        /// <summary>
        /// Unique id of this offering
        /// </summary>
        /// <value>Unique id of this offering</value>
        [JsonRequired]
        [JsonProperty("offeringId")]
        public Guid OfferingId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [Required]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The type of this offering
        /// </summary>
        /// <value>The type of this offering</value>
        [Required]

        [DataMember(Name = "offeringType")]
        public OfferingTypeEnum? OfferingType { get; set; }


        /// <summary>
        /// The academicsession during which this ffering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. 
        /// </summary>
        /// <value>The academicsession during which this offering takes place. [&#x60;expandable&#x60;](#tag/academic_session_model) By default only the &#x60;academicSessionId&#x60; (a string) is returned. If the client requested an expansion of &#x60;academicSession&#x60; the full academicsession object should be returned. </value>
        [DataMember(Name = "academicSession")]
        public OneOfAcadamicSession? AcademicSession { get; set; }


        /// <summary>
        /// The name of this offering
        /// </summary>
        /// <value>The name of this offering</value>
        [Required]

        [DataMember(Name = "name")]
        public List<LanguageTypedString> Name { get; set; }

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
        [Required]

        [DataMember(Name = "description")]
        public List<LanguageTypedString> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this offering is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [Required]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }


        [JsonIgnore]
        public string ModeOfDelivery { get; set; }


        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        [NotMapped]
        public List<ModeOfDeliveryEnum> ModeOfDel
        {
            get
            {
                if (!string.IsNullOrEmpty(ModeOfDelivery))
                {
                    List<ModeOfDeliveryEnum> list = new List<ModeOfDeliveryEnum>();
                    var affiliations = ModeOfDelivery.Split(',');
                    foreach (var affiliation in affiliations)
                    {
                        switch (affiliation)
                        {
                            case "distance-learning":
                                {
                                    list.Add(ModeOfDeliveryEnum.DistanceLearningEnum);
                                    break;
                                }
                            case "on campus":
                                {
                                    list.Add(ModeOfDeliveryEnum.OnCampusEnum);
                                    break;
                                }
                            case "online":
                                {
                                    list.Add(ModeOfDeliveryEnum.OnlineEnum);
                                    break;
                                }
                            case "hybrid":
                                {
                                    list.Add(ModeOfDeliveryEnum.HybridEnum);
                                    break;
                                }
                            case "situated":
                                {
                                    list.Add(ModeOfDeliveryEnum.SituatedEnum);
                                    break;
                                }
                            default:
                                break;
                        }
                    }
                    return list;
                }
                return new List<ModeOfDeliveryEnum>();
            }
        }

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
        /// resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain �grade� information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true 
        /// </summary>
        /// <value>resultExpected, previously knwon as isLineItem is used so the specific instance of the object is identified as being an element that CAN contain �grade� information. Offerings do not always have to result in a grade or an other type of result.  If there is a result expected from a programOffering/courseOffering/componentOffering the is resultExpected field should parse true </value>
        [Required]
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
        /// The moment on which this offering starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering starts, RFC3339 (full-date)</value>
        [Required]
        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The moment on which this offering ends, RFC3339 (full-date)
        /// </summary>
        /// <value>The moment on which this offering ends, RFC3339 (full-date)</value>
        [Required]
        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The first day on which a student can enroll for this course.
        /// </summary>
        /// <value>The first day on which a student can enroll for this course.</value>
        [DataMember(Name = "enrollStartDate")]
        public DateTime? EnrollStartDate { get; set; }

        /// <summary>
        /// The last day on which a student can enroll for this course.
        /// </summary>
        /// <value>The last day on which a student can enroll for this course.</value>
        [DataMember(Name = "enrollEndDate")]
        public DateTime? EnrollEndDate { get; set; }

        /// <summary>
        /// The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization that manages this courseoffering. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

    }
}