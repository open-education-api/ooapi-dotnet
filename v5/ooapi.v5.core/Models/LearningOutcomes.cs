using System.Runtime.Serialization;

namespace ooapi.v5.Models
{
    /// <summary>
    /// Statements that describe the knowledge or skills students should acquire by the end of a particular course (ECTS-learningoutcome).
    /// </summary>
    [DataContract]
    public partial class LearningOutcomes
    {

        public List<LanguageTypedString> languageTypedStrings { get; set; }


    }
}
