using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ooapi.v5.Enums;

/// <summary>
/// The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining 
/// </summary>
/// <value>The component type - test: tentamen - lecture: college - practical: practicum - tutorial: werkcollege - consultation: consultatie - project: project - workshop: workshop - excursion: excursie - independent study: zelfstudie - external: extern - skills training: vaardighedentraining </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum ComponentType
{
    /// <summary>
    /// Enum TestEnum for test
    /// </summary>
    [EnumMember(Value = "test")]
    test = 0,
    /// <summary>
    /// Enum LectureEnum for lecture
    /// </summary>
    [EnumMember(Value = "lecture")]
    lecture = 1,
    /// <summary>
    /// Enum PracticalEnum for practical
    /// </summary>
    [EnumMember(Value = "practical")]
    practical = 2,
    /// <summary>
    /// Enum TutorialEnum for tutorial
    /// </summary>
    [EnumMember(Value = "tutorial")]
    tutorial = 3,
    /// <summary>
    /// Enum ConsultationEnum for consultation
    /// </summary>
    [EnumMember(Value = "consultation")]
    consultation = 4,
    /// <summary>
    /// Enum ProjectEnum for project
    /// </summary>
    [EnumMember(Value = "project")]
    project = 5,
    /// <summary>
    /// Enum WorkshopEnum for workshop
    /// </summary>
    [EnumMember(Value = "workshop")]
    workshop = 6,
    /// <summary>
    /// Enum ExcursionEnum for excursion
    /// </summary>
    [EnumMember(Value = "excursion")]
    excursion = 7,
    /// <summary>
    /// Enum IndependentStudyEnum for independent study
    /// </summary>
    [EnumMember(Value = "independent study")]
    independent_study = 8,
    /// <summary>
    /// Enum ExternalEnum for external
    /// </summary>
    [EnumMember(Value = "external")]
    external = 9,
    /// <summary>
    /// Enum SkillsTrainingEnum for skills training
    /// </summary>
    [EnumMember(Value = "skills training")]
    skills_training = 10
}