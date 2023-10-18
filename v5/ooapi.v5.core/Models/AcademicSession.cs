using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ooapi.v5.Attributes;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Enums;
using ooapi.v5.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace ooapi.v5.Models;

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
    public AcademicSessionType? AcademicSessionType { get; set; }

    /// <summary>
    /// Gets or Sets PrimaryCode
    /// </summary>
    [JsonProperty(PropertyName = "primaryCode")]
    [NotMapped]
    public IdentifierEntry? PrimaryCodeIdentifier
    {
        get
        {
            if (PrimaryCodeType is not null && PrimaryCode is not null)
            {
                return new IdentifierEntry() { CodeType = PrimaryCodeType, Code = PrimaryCode };
            }

            return null;
        }
        set
        {
            if (value is not null)
            {
                PrimaryCode = value.Code;
                PrimaryCodeType = value.CodeType;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? PrimaryCodeType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string? PrimaryCode { get; set; }

    /// <summary>
    /// The name of this academic session
    /// </summary>
    /// <value>The name of this academic session</value>
    [JsonRequired]
    [JsonProperty("name")]
    [NotMapped]
    public List<LanguageTypedString> Name
    {
        get
        {
            var result = new List<LanguageTypedString>();
            if (Attributes != null && Attributes.Any())
            {
                result = Attributes.Where(x => x.PropertyName.Equals("name")).Select(x => new LanguageTypedString() { Language = x.Language, Value = x.Value }).ToList();
            }
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [SortAllowed]
    public List<Attribute> Attributes { get; set; } = default!;

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
            if (ParentId == null)
            {
                return null;
            }

            return new OneOfAcademicSessionInstance(ParentId, Parent);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public AcademicSession? Parent { get; set; }

    /// <summary>
    /// The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
    /// </summary>
    /// <value>The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

    [JsonProperty("children")]
    [NotMapped]
    [JsonConverter(typeof(ListOneOfConverter))]
    public List<OneOfAcademicSession> ChildrenList
    {
        get
        {
            if (ChildrenIds == null || !ChildrenIds.Any())
            {
                return new List<OneOfAcademicSession>();
            }

            var result = new List<OneOfAcademicSession>();
            foreach (var ChildId in ChildrenIds)
            {
                result.Add(new OneOfAcademicSessionInstance(ChildId, Children?.Find(x => x.AcademicSessionId.Equals(ChildId))));
            }
            return result;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public List<Guid> ChildrenIds { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public List<AcademicSession> Children { get; set; } = default!;

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

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public Guid? YearId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    [NotMapped]
    public AcademicSession? Year { get; set; }

    /// <summary>
    /// An array of additional human readable codes/identifiers for the entity being described.
    /// </summary>
    /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

    [JsonProperty("otherCodes")]
    public List<OtherCodes> OtherCodes { get; set; } = default!;

    /// <summary>
    /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
    /// </summary>
    /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

    [JsonProperty(PropertyName = "consumers")]
    [NotMapped]
    public List<JObject> ConsumersList
    {
        get
        {
            if (Consumers != null && Consumers.Any())
            {
                return ConsumerConverter.GetDynamicConsumers(Consumers);
            }

            return new List<JObject>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public List<Consumer> Consumers { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<ProgramOffering> ProgramOfferings { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = default!;

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<ComponentOffering> ComponentOfferings { get; set; } = default!;
}
