using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using ooapi.v5.Attributes;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A named period of time that can be used to communicate the various schedules and time periods an institution recognizes and uses to organize their education. AcademicSessions can be nested. Offerings MAY be be linked to a specific AcademicSession to indicate that the specified Offering takes place during the AcademicSession, however this is not mandatory. 
    /// </summary>

    public class AcademicSession : ModelBase, IValidatableObject
    {
        /// <summary>
        /// Unique id for this academic session
        /// </summary>
        /// <value>Unique id for this academic session</value>
        [JsonRequired]
        [JsonProperty("academicSessionId")]
        [SortAllowed]
        public Guid AcademicSessionId { get; set; }

        /// <summary>
        /// The type of this Academic Session This is an *extensible enumeration*. <br/>
        /// - academic year: academic year <br/>
        /// - semester: semester, typically there are two semesters per academic year <br/>
        /// - trimester: trimester, typically there are three semesters per academic year <br/>
        /// - quarter: quarter, typically there are four quarters per academic year <br/>
        /// - testing period: a period in which tests take place <br/>
        /// - period: any other period in an academic year  <br/>
        /// Implementations are allowed to add additional values to those above, as long as they do not overlap in definition to existing values. 
        /// </summary>

        [JsonProperty("academicSessionType")]
        public AcademicSessionTypeEnum? AcademicSessionType { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>
        [JsonRequired]
        [JsonProperty(PropertyName = "primaryCode")]
        [NotMapped]
        public IdentifierEntry primaryCode
        {
            get
            {
                return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
            }
            set
            {
                PrimaryCode = value.Code;
                PrimaryCodeType = value.CodeType;
            }
        }


        [JsonIgnore]
        public string? PrimaryCodeType { get; set; }

        [JsonIgnore]
        public string? PrimaryCode { get; set; }

        /// <summary>
        /// The name of this academic session
        /// </summary>
        /// <value>The name of this academic session</value>
        [JsonRequired]
        [JsonProperty("name")]
        [NotMapped]
        public List<LanguageTypedString> name
        {
            get
            {
                return Helpers.JsonConverter.GetLanguageTypesStringList(Name);
            }
            set
            {
                if (value != null)
                    Name = JsonConvert.SerializeObject(value);
            }
        }

        [JsonIgnore]
        [SortAllowed]
        public string? Name { get; set; }


        /// <summary>
        /// The day on which this academic session starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this academic session starts, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty("startDate")]
        [JsonConverter(typeof(MyDateFormatConverter))]
        [SortAllowed]
        [SortDefault]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The day on which this academic session ends, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this academic session ends, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty("endDate")]
        [JsonConverter(typeof(MyDateFormatConverter))]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The parent Academicsession of this session (e.g. fall semester 20xx where the current session is a week 40). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The parent Academicsession of this session (e.g. fall semester 20xx where the current session is a week 40). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("parent")]
        public OneOfAcademicSession? Parent { get; set; }

        [JsonIgnore]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("children")]
        [NotMapped]
        public List<OneOfAcademicSession>? Children { get; set; }

        /// <summary>
        /// The top level year of this session (e.g. 20xx where the current session is a week 40 of a semester). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The top level year of this session (e.g. 20xx where the current session is a week 40 of a semester). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("year")]
        public OneOfAcademicSession? Year { get; set; }

        [JsonIgnore]
        public Guid? YearId { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty("otherCodes")]
        public List<OtherCodes>? OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty("consumers")]
        [NotMapped]
        //        public List<Consumer>? Consumers { get; set; }
        public List<dynamic>? Consumers { get; set; }


        [JsonIgnore]
        public virtual ICollection<ProgramOffering> ProgramOfferings { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseOffering> CourseOfferings { get; set; }

        [JsonIgnore]
        public virtual ICollection<ComponentOffering> ComponentOfferings { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var vali = validationContext;
            return null;
            //Type = result.ToString(); //normalize Type
        }
    }
}
