using IO.Swagger.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IO.Swagger.Models
{
    /// <summary>
    /// A named period of time that can be used to communicate the various schedules and time periods an institution recognizes and uses to organize their education. AcademicSessions can be nested. Offerings MAY be be linked to a specific AcademicSession to indicate that the specified Offering takes place during the AcademicSession, however this is not mandatory. 
    /// </summary>

    public partial class AcademicSession
    {
        /// <summary>
        /// Unique id for this academic session
        /// </summary>
        /// <value>Unique id for this academic session</value>
        [JsonRequired]
        [JsonProperty("academicSessionId")]
        public Guid? AcademicSessionId { get; set; }

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
        public string AcademicSessionType { get; set; }

        /// <summary>
        /// Gets or Sets PrimaryCode
        /// </summary>

        [JsonProperty("primaryCode")]
        public PrimaryCode PrimaryCode { get; set; }

        /// <summary>
        /// The name of this academic session
        /// </summary>
        /// <value>The name of this academic session</value>
        [JsonRequired]

        [JsonProperty("name")]
        public List<LanguageValueItem> Name { get; set; }

        /// <summary>
        /// The day on which this academic session starts, RFC3339 (full-date)
        /// </summary>
        /// <value>The day on which this academic session starts, RFC3339 (full-date)</value>
        [JsonRequired]
        [JsonProperty("startDate")]
        [JsonConverter(typeof(MyDateFormatConverter))]
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
        public Guid Parent { get; set; }

        /// <summary>
        /// The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The list of Academicsession children of this Session (e.g. all academic sessions in fall semester 20xx). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("children")]
        public List<Guid> Children { get; set; }

        /// <summary>
        /// The top level year of this session (e.g. 20xx where the current session is a week 40 of a semester). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)
        /// </summary>
        /// <value>The top level year of this session (e.g. 20xx where the current session is a week 40 of a semester). This object is [&#x60;expandable&#x60;](#tag/academic_sessions_model)</value>

        [JsonProperty("year")]
        public Guid Year { get; set; }

        /// <summary>
        /// An array of additional human readable codes/identifiers for the entity being described.
        /// </summary>
        /// <value>An array of additional human readable codes/identifiers for the entity being described.</value>

        [JsonProperty("otherCodes")]
        public List<OtherCodes> OtherCodes { get; set; }

        /// <summary>
        /// The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.
        /// </summary>
        /// <value>The additional consumer elements that can be provided, see the [documentation on support for specific consumers](https://open-education-api.github.io/specification/#/consumers) for more information about this mechanism.</value>

        [JsonProperty("consumers")]
        public List<Consumer> Consumers { get; set; }

        /// <summary>
        /// Object for additional non-standard attributes
        /// </summary>
        /// <value>Object for additional non-standard attributes</value>

        [JsonProperty("ext")]
        public Object Ext { get; set; }


    }
}
