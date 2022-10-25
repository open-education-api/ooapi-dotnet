﻿namespace ooapi.v5.Enums;

using Newtonsoft.Json;
using System.Runtime.Serialization;

/// <summary>
/// The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer 
/// </summary>
/// <value>The type of this room - general purpose: algemeen - lecture room: collegezaal - computer room: computerruimte - laboratory: laboratorium - office: kantoor - workspace: werkruimte - exam location: tentamenruimte - study room: studieruimte - examination room: onderzoekskamer - conference room: vergaderkamer </value>
[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
public enum RoomTypeEnum
{
    /// <summary>
    /// Enum GeneralPurposeEnum for general purpose
    /// </summary>
    [EnumMember(Value = "general purpose")]
    GeneralPurposeEnum = 0,
    /// <summary>
    /// Enum LectureRoomEnum for lecture room
    /// </summary>
    [EnumMember(Value = "lecture room")]
    LectureRoomEnum = 1,
    /// <summary>
    /// Enum ComputerRoomEnum for computer room
    /// </summary>
    [EnumMember(Value = "computer room")]
    ComputerRoomEnum = 2,
    /// <summary>
    /// Enum LaboratoryEnum for laboratory
    /// </summary>
    [EnumMember(Value = "laboratory")]
    LaboratoryEnum = 3,
    /// <summary>
    /// Enum OfficeEnum for office
    /// </summary>
    [EnumMember(Value = "office")]
    OfficeEnum = 4,
    /// <summary>
    /// Enum WorkspaceEnum for workspace
    /// </summary>
    [EnumMember(Value = "workspace")]
    WorkspaceEnum = 5,
    /// <summary>
    /// Enum ExamLocationEnum for exam location
    /// </summary>
    [EnumMember(Value = "exam location")]
    ExamLocationEnum = 6,
    /// <summary>
    /// Enum StudyRoomEnum for study room
    /// </summary>
    [EnumMember(Value = "study room")]
    StudyRoomEnum = 7,
    /// <summary>
    /// Enum ExaminationRoomEnum for examination room
    /// </summary>
    [EnumMember(Value = "examination room")]
    ExaminationRoomEnum = 8,
    /// <summary>
    /// Enum ConferenceRoomEnum for conference room
    /// </summary>
    [EnumMember(Value = "conference room")]
    ConferenceRoomEnum = 9
}