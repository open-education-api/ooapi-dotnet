using IO.Swagger.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A component is a part of a course
    /// </summary>
    [DataContract]
    public partial class Component
    {
        /// <summary>
        /// Unique id of this component
        /// </summary>
        /// <value>Unique id of this component</value>
        [JsonRequired]

        [DataMember(Name = "componentId")]
        public Guid ComponentId { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]

        [DataMember(Name = "primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }


        /// <summary>
        /// The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining 
        /// </summary>
        /// <value>The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining </value>
        [JsonRequired]

        [DataMember(Name = "componentType")]
        public ComponentTypeEnum? ComponentType { get; set; }

        /// <summary>
        /// The name of this component
        /// </summary>
        /// <value>The name of this component</value>
        [JsonRequired]

        [DataMember(Name = "name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The abbreviation of this component
        /// </summary>
        /// <value>The abbreviation of this component</value>
        [JsonRequired]

        [MaxLength(256)]
        [DataMember(Name = "abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie 
        /// </summary>
        /// <value>The mode of delivery of the component (ECTS-mode of delivery) - distance-learning: afstandsleren - on campus: op de campus - online: online - hybrid: hybride - situated: op locatie </value>

        [DataMember(Name = "modeOfDelivery")]
        public List<ModeOfDeliveryEnum> ModeOfDelivery { get; set; }

        /// <summary>
        /// The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.
        /// </summary>
        /// <value>The duration of this component. The duration format is from the ISO 8601 ABNF as given in Appendix A of RFC 3339.</value>
        [RegularExpression("/^(-?)P(?=\\d|T\\d)(?:(\\d+)Y)?(?:(\\d+)M)?(?:(\\d+)([DW]))?(?:T(?:(\\d+)H)?(?:(\\d+)M)?(?:(\\d+(?:\\.\\d+)?)S)?)?$/")]
        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        /// <summary>
        /// The description of this component.
        /// </summary>
        /// <value>The description of this component.</value>

        [DataMember(Name = "description")]
        public List<LanguageValueItem> Description { get; set; }

        /// <summary>
        /// The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.
        /// </summary>
        /// <value>The (primary) teaching language in which this component is given, should be a three-letter language code as specified by ISO 639-2.</value>
        [JsonRequired]
        [RegularExpression("/^[a-z]{3}$/")]
        [StringLength(3, MinimumLength = 3)]
        [DataMember(Name = "teachingLanguage")]
        public string TeachingLanguage { get; set; }

        /// <summary>
        /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
        /// </summary>
        /// <value>Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).</value>

        [DataMember(Name = "learningOutcomes")]
        public List<List<Object>> LearningOutcomes { get; set; }

        /// <summary>
        /// The extra information that is provided for enrollment
        /// </summary>
        /// <value>The extra information that is provided for enrollment</value>

        [DataMember(Name = "enrollment")]
        public List<LanguageValueItem> Enrollment { get; set; }

        /// <summary>
        /// An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)
        /// </summary>
        /// <value>An overview of the literature and other resources that is used in this course (ECTS-recommended reading and other sources)</value>

        [DataMember(Name = "resources")]
        public List<string> Resources { get; set; }

        /// <summary>
        /// A description of the way exams for this course are taken (ECTS-assessment method and criteria).
        /// </summary>
        /// <value>A description of the way exams for this course are taken (ECTS-assessment method and criteria).</value>

        [DataMember(Name = "assessment")]
        public List<LanguageValueItem> Assessment { get; set; }

        /// <summary>
        /// Addresses for this component
        /// </summary>
        /// <value>Addresses for this component</value>

        [DataMember(Name = "addresses")]
        public List<Address> Addresses { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [DataMember(Name = "otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. 
        /// </summary>
        /// <value>The course of which this component is a part. [&#x60;expandable&#x60;](#tag/course_model) By default only the &#x60;courseId&#x60; (a string) is returned. If the client requested an expansion of &#x60;course&#x60; the full course object should be returned. </value>

        [DataMember(Name = "course")]
        public OneOfCourse Course { get; set; }

        /// <summary>
        /// The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. 
        /// </summary>
        /// <value>The organization which provides this component. [&#x60;expandable&#x60;](#tag/organization_model) By default only the &#x60;organizationId&#x60; (a string) is returned. If the client requested an expansion of &#x60;organization&#x60; the full organization object should be returned. </value>

        [DataMember(Name = "organization")]
        public OneOfOrganization Organization { get; set; }

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
