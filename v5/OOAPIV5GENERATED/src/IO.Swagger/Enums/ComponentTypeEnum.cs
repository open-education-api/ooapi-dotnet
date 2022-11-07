namespace IO.Swagger.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining 
/// </summary>
/// <value>The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ComponentTypeEnum
{
    /// <summary>
    /// Enum TestEnum for test
    /// </summary>
    [EnumMember(Value = "test")]
    TestEnum = 0,
    /// <summary>
    /// Enum LectureEnum for lecture
    /// </summary>
    [EnumMember(Value = "lecture")]
    LectureEnum = 1,
    /// <summary>
    /// Enum PracticalEnum for practical
    /// </summary>
    [EnumMember(Value = "practical")]
    PracticalEnum = 2,
    /// <summary>
    /// Enum TutorialEnum for tutorial
    /// </summary>
    [EnumMember(Value = "tutorial")]
    TutorialEnum = 3,
    /// <summary>
    /// Enum ConsultationEnum for consultation
    /// </summary>
    [EnumMember(Value = "consultation")]
    ConsultationEnum = 4,
    /// <summary>
    /// Enum ProjectEnum for project
    /// </summary>
    [EnumMember(Value = "project")]
    ProjectEnum = 5,
    /// <summary>
    /// Enum WorkshopEnum for workshop
    /// </summary>
    [EnumMember(Value = "workshop")]
    WorkshopEnum = 6,
    /// <summary>
    /// Enum ExcursionEnum for excursion
    /// </summary>
    [EnumMember(Value = "excursion")]
    ExcursionEnum = 7,
    /// <summary>
    /// Enum IndependentStudyEnum for independent study
    /// </summary>
    [EnumMember(Value = "independent study")]
    IndependentStudyEnum = 8,
    /// <summary>
    /// Enum ExternalEnum for external
    /// </summary>
    [EnumMember(Value = "external")]
    ExternalEnum = 9,
    /// <summary>
    /// Enum SkillsTrainingEnum for skills training
    /// </summary>
    [EnumMember(Value = "skills training")]
    SkillsTrainingEnum = 10
}