using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.Models
{
    /// <summary>
    /// A named period of time that can be used to communicate the various schedules and time periods an institution recognizes and uses to organize their education. AcademicSessions can be nested. Offerings MAY be be linked to a specific AcademicSession to indicate that the specified Offering takes place during the AcademicSession, however this is not mandatory. 
    /// </summary>

    public class AcademicSession : ModelBase
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
        public IdentifierEntry primaryCodeIdentifier
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
                List<LanguageTypedString> result = new List<LanguageTypedString>();
                if (Attributes != null && Attributes.Any())
                {
                    result = Attributes.Where(x => x.PropertyName.Equals("name")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
                }
                return result;
            }
        }

        [JsonIgnore]
        [SortAllowed]
        public List<Attribute> Attributes { get; set; }


        /// <summary>
        /// The day on which this academic session starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this academic session starts, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty("startDate")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        [SortAllowed]
        [SortDefault]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The day on which this academic session ends, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this academic session ends, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty("endDate")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The parent Academicsession of this session (e.g. fall semester 20xx where the current session is a week 40). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The parent Academicsession of this session (e.g. fall semester 20xx where the current session is a week 40). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("parent")]
        [NotMapped]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOfAcademicSession? OneOfParent
        {
            get
            {
                if (ParentId == null) return null;
                return new OneOfAcademicSessionInstance(ParentId, Parent);
            }
        }

        [JsonIgnore]
        public Guid? ParentId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public AcademicSession? Parent { get; set; }


        //??public virtual ICollection<AcademicSession> ChildAcademicSessions { get; set; }

        /// <summary>
        /// The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("children")]
        [NotMapped]
        [JsonConverter(typeof(ListOneOfConverter))]
        public List<OneOfAcademicSession>? ChildrenList
        {
            get
            {
                if (ChildrenIds == null || !ChildrenIds.Any()) return null;

                List<OneOfAcademicSession>? result = new List<OneOfAcademicSession>();
                foreach (var ChildId in ChildrenIds)
                {
                    result.Add(new OneOfAcademicSessionInstance(ChildId, (Children != null) ? Children.FirstOrDefault(x => x.AcademicSessionId.Equals(ChildId)) : null));
                }
                return result;
            }
        }


        [JsonIgnore]
        [NotMapped]
        public List<Guid>? ChildrenIds { get; set; }


        [JsonIgnore]
        [NotMapped]
        public List<AcademicSession>? Children { get; set; }


        /// <summary>
        /// The top level year of this session (e.g. 20xx where the current session is a week 40 of a semester). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The top level year of this session (e.g. 20xx where the current session is a week 40 of a semester). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("year")]
        [NotMapped]
        [JsonConverter(typeof(OneOfConverter))]
        public OneOfAcademicSession? OneOfYear
        {
            get
            {
                if (YearId == null) return null;
                if (Year != null && Year.AcademicSessionId.Equals(AcademicSessionId))
                    return null;
                return new OneOfAcademicSessionInstance(YearId, Year);
            }
        }

        [JsonIgnore]
        public Guid? YearId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public AcademicSession? Year { get; set; }

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

        [JsonProperty(PropertyName = "consumers")]
        [NotMapped]
        public List<JObject>? ConsumersList
        {
            get
            {
                if (Consumers != null && Consumers.Any())
                    return ConsumerConverter.GetDynamicConsumers(Consumers);
                return null;
            }
        }

        [JsonIgnore]
        public List<Consumer>? Consumers { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProgramOffering> ProgramOfferings { get; set; }

        [JsonIgnore]
        public virtual ICollection<CourseOffering> CourseOfferings { get; set; }

        [JsonIgnore]
        public virtual ICollection<ComponentOffering> ComponentOfferings { get; set; }

    }
}
