//using IO.Swagger.Enums;
//using Newtonsoft.Json;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Runtime.Serialization;

//namespace IO.Swagger.Models
//{
//    /// <summary>
//    /// A result as part of an association
//    /// </summary>
//    [DataContract]
//    public partial class Result
//    {

//        /// <summary>
//        /// The state of this result
//        /// </summary>
//        /// <value>The state of this result</value>
//        [JsonRequired]

//        [DataMember(Name = "state")]
//        public ResultStateEnum State { get; set; }


//        /// <summary>
//        /// The state of this result
//        /// </summary>
//        /// <value>The state of this result</value>

//        [DataMember(Name = "pass")]
//        public PassEnum? Pass { get; set; }

//        /// <summary>
//        /// The comment on this result
//        /// </summary>
//        /// <value>The comment on this result</value>

//        [DataMember(Name = "comment")]
//        public string Comment { get; set; }

//        /// <summary>
//        /// The score of this program/course/component association (based on resultValueType in offering)
//        /// </summary>
//        /// <value>The score of this program/course/component association (based on resultValueType in offering)</value>

//        [DataMember(Name = "score")]
//        public string Score { get; set; }

//        /// <summary>
//        /// The date this result has been published, RFC3339 (full-date)
//        /// </summary>
//        /// <value>The date this result has been published, RFC3339 (full-date)</value>
//        [JsonRequired]

//        [DataMember(Name = "resultDate")]
//        public DateTime ResultDate { get; set; }

//        /// <summary>
//        /// Object for additional non-standard attributes
//        /// </summary>
//        /// <value>Object for additional non-standard attributes</value>

//        [DataMember(Name = "ext")]
//        public Object Ext { get; set; }


//        protected ResultTypeEnum resultType { get; set; }


//        // igv ComponentResult
//        [DataMember(Name = "weight")]
//        [MaxLength(100)]
//        public int? Weight { get; set; }

//        // igv ProgramResult and CourseResult
//        [DataMember(Name = "studyLoad")]
//        public StudyLoadDescriptor? StudyLoad { get; set; }


//    }
//}
