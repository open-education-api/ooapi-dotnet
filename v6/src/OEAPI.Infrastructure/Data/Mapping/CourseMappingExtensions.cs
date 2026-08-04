using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class CourseMappingExtensions
{
    public static Course ToApiModel(this CourseEntity entity, string? consumer)
    {
        return new Course
        {
            CourseIdValue = entity.CourseId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            Abbreviation = entity.Abbreviation,
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            ValidFrom = entity.ValidFrom?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ValidTo = entity.ValidTo?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Duration = entity.Duration,
            FirstStartDate = entity.FirstStartDate?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            FirstPossibleOfferingStartDateTime =
                entity.FirstPossibleOfferingStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            LastPossibleOfferingStartDateTime =
                entity.LastPossibleOfferingStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            LastPossibleOfferingEndDateTime =
                entity.LastPossibleOfferingEndDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            StudyLoad = string.IsNullOrEmpty(entity.StudyLoadJson)
                ? null
                : JsonSerializer.Deserialize<StudyLoadDescriptor[]>(entity.StudyLoadJson),
            ModesOfDelivery = string.IsNullOrEmpty(entity.ModesOfDeliveryJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ModesOfDeliveryJson),
            TeachingLanguages = string.IsNullOrEmpty(entity.TeachingLanguagesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.TeachingLanguagesJson),
            FieldsOfStudy = entity.FieldsOfStudy,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Link = entity.Link,
            Addresses = string.IsNullOrEmpty(entity.AddressesJson)
                ? null
                : JsonSerializer.Deserialize<Address[]>(entity.AddressesJson),
            Level = entity.Level,
            Resources = string.IsNullOrEmpty(entity.ResourcesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ResourcesJson),
            Assessment = string.IsNullOrEmpty(entity.AssessmentJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AssessmentJson),
            Enrolment = string.IsNullOrEmpty(entity.EnrolmentJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.EnrolmentJson),
            AdmissionRequirements = string.IsNullOrEmpty(entity.AdmissionRequirementsJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AdmissionRequirementsJson),
            QualificationRequirements = string.IsNullOrEmpty(entity.QualificationRequirementsJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.QualificationRequirementsJson),
            SupplementaryInformation = string.IsNullOrEmpty(entity.SupplementaryInformationJson)
                ? []
                : JsonSerializer.Deserialize<SupplementaryInformation[]>(entity.SupplementaryInformationJson),
            Consumer = entity.ConsumerJson.ToCourseConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson),
            OrganisationId = entity.Organisation == null
                ? null
                : new Identifier { Value = entity.Organisation.OrganisationId },
            CoordinatorIds = entity.CourseCoordinatorEntities.Count == 0
                ? null
                : [.. entity.CourseCoordinatorEntities.Select(p => new Identifier { Value = p.PersonId })],
            InstructorIds = entity.CourseInstructorEntities.Count == 0
                ? null
                : [.. entity.CourseInstructorEntities.Select(p => new Identifier { Value = p.PersonId })],
            ProgrammeIds = entity.ProgrammeEntities.Count == 0
                ? null
                : [.. entity.ProgrammeEntities.Select(p => new Identifier { Value = p.ProgrammeId })],
            LearningOutcomeIds = entity.LearningOutcomes.Count == 0
                ? null
                : [.. entity.LearningOutcomes.Select(o => new Identifier { Value = o.LearningOutcomeId })]
        };
    }

    /// <summary>
    ///     Maps a <see cref="Course" /> API model to a new <see cref="CourseEntity" />, for the create half
    ///     of an upsert. Organisation/coordinator/instructor/programme/learning-outcome FK resolution
    ///     requires an async DB lookup this synchronous method can't perform - the caller resolves and
    ///     sets those itself after calling this method.
    /// </summary>
    public static CourseEntity ToEntity(this Course model)
    {
        return new CourseEntity
        {
            CourseId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            Abbreviation = model.Abbreviation,
            NameJson = JsonSerializer.Serialize(model.Name),
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            Duration = model.Duration,
            FieldsOfStudy = model.FieldsOfStudy,
            FirstStartDate = ParseDateTime(model.FirstStartDate),
            FirstPossibleOfferingStartDateTime = ParseDateTime(model.FirstPossibleOfferingStartDateTime),
            LastPossibleOfferingStartDateTime = ParseDateTime(model.LastPossibleOfferingStartDateTime),
            LastPossibleOfferingEndDateTime = ParseDateTime(model.LastPossibleOfferingEndDateTime),
            ValidFrom = ParseDateTime(model.ValidFrom),
            ValidTo = ParseDateTime(model.ValidTo),
            StudyLoadJson = model.StudyLoad != null ? JsonSerializer.Serialize(model.StudyLoad) : null,
            ModesOfDeliveryJson =
                model.ModesOfDelivery != null ? JsonSerializer.Serialize(model.ModesOfDelivery) : null,
            TeachingLanguagesJson = model.TeachingLanguages != null
                ? JsonSerializer.Serialize(model.TeachingLanguages)
                : null,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            Link = model.Link,
            AddressesJson = model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : null,
            Level = model.Level,
            ResourcesJson = model.Resources != null ? JsonSerializer.Serialize(model.Resources) : null,
            AssessmentJson = model.Assessment != null ? JsonSerializer.Serialize(model.Assessment) : null,
            EnrolmentJson = model.Enrolment != null ? JsonSerializer.Serialize(model.Enrolment) : null,
            AdmissionRequirementsJson = model.AdmissionRequirements != null
                ? JsonSerializer.Serialize(model.AdmissionRequirements)
                : null,
            QualificationRequirementsJson = model.QualificationRequirements != null
                ? JsonSerializer.Serialize(model.QualificationRequirements)
                : null,
            SupplementaryInformationJson = model.SupplementaryInformation != null
                ? JsonSerializer.Serialize(model.SupplementaryInformation)
                : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="CourseEntity" /> from a <see cref="Course" /> API model, for the
    ///     update half of an upsert. See <see cref="ToEntity" /> regarding FK resolution.
    /// </summary>
    public static void UpdateFrom(this CourseEntity entity, Course model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.Duration = model.Duration ?? entity.Duration;
        entity.FieldsOfStudy = model.FieldsOfStudy ?? entity.FieldsOfStudy;
        entity.FirstStartDate = ParseDateTime(model.FirstStartDate) ?? entity.FirstStartDate;
        entity.FirstPossibleOfferingStartDateTime =
            ParseDateTime(model.FirstPossibleOfferingStartDateTime) ?? entity.FirstPossibleOfferingStartDateTime;
        entity.LastPossibleOfferingStartDateTime =
            ParseDateTime(model.LastPossibleOfferingStartDateTime) ?? entity.LastPossibleOfferingStartDateTime;
        entity.LastPossibleOfferingEndDateTime =
            ParseDateTime(model.LastPossibleOfferingEndDateTime) ?? entity.LastPossibleOfferingEndDateTime;
        entity.ValidFrom = ParseDateTime(model.ValidFrom) ?? entity.ValidFrom;
        entity.ValidTo = ParseDateTime(model.ValidTo) ?? entity.ValidTo;
        entity.StudyLoadJson =
            model.StudyLoad != null ? JsonSerializer.Serialize(model.StudyLoad) : entity.StudyLoadJson;
        entity.ModesOfDeliveryJson = model.ModesOfDelivery != null
            ? JsonSerializer.Serialize(model.ModesOfDelivery)
            : entity.ModesOfDeliveryJson;
        entity.TeachingLanguagesJson = model.TeachingLanguages != null
            ? JsonSerializer.Serialize(model.TeachingLanguages)
            : entity.TeachingLanguagesJson;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.Link = model.Link ?? entity.Link;
        entity.AddressesJson =
            model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : entity.AddressesJson;
        entity.Level = model.Level ?? entity.Level;
        entity.ResourcesJson =
            model.Resources != null ? JsonSerializer.Serialize(model.Resources) : entity.ResourcesJson;
        entity.AssessmentJson =
            model.Assessment != null ? JsonSerializer.Serialize(model.Assessment) : entity.AssessmentJson;
        entity.EnrolmentJson =
            model.Enrolment != null ? JsonSerializer.Serialize(model.Enrolment) : entity.EnrolmentJson;
        entity.AdmissionRequirementsJson = model.AdmissionRequirements != null
            ? JsonSerializer.Serialize(model.AdmissionRequirements)
            : entity.AdmissionRequirementsJson;
        entity.QualificationRequirementsJson = model.QualificationRequirements != null
            ? JsonSerializer.Serialize(model.QualificationRequirements)
            : entity.QualificationRequirementsJson;
        entity.SupplementaryInformationJson = model.SupplementaryInformation != null
            ? JsonSerializer.Serialize(model.SupplementaryInformation)
            : entity.SupplementaryInformationJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }

    private static DateTime? ParseDateTime(string? dateString)
    {
        if (string.IsNullOrEmpty(dateString)) return null;

        return DateTime.TryParse(dateString, out DateTime result) ? result : null;
    }
}
