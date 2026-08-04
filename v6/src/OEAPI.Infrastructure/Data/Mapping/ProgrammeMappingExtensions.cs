using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class ProgrammeMappingExtensions
{
    public static Programme ToApiModel(this ProgrammeEntity entity, string? consumer)
    {
        return new Programme
        {
            ProgrammeIdValue = entity.ProgrammeId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            ValidFrom = entity.ValidFrom?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            ValidTo = entity.ValidTo?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Abbreviation = entity.Abbreviation,
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            TeachingLanguages = string.IsNullOrEmpty(entity.TeachingLanguagesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.TeachingLanguagesJson),
            ProgrammeType = entity.ProgrammeType,
            ModeOfStudy = entity.ModeOfStudy,
            ModesOfDelivery = string.IsNullOrEmpty(entity.ModesOfDeliveryJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ModesOfDeliveryJson),
            LevelOfQualification = entity.LevelOfQualification,
            FormalDocument = entity.FormalDocument,
            StudyLoad = string.IsNullOrEmpty(entity.StudyLoadJson)
                ? null
                : JsonSerializer.Deserialize<StudyLoadDescriptor[]>(entity.StudyLoadJson),
            QualificationAwarded = entity.QualificationAwarded,
            QualificationDesignations = string.IsNullOrEmpty(entity.QualificationDesignationsJson)
                ? []
                : JsonSerializer.Deserialize<string[]>(entity.QualificationDesignationsJson) ?? [],
            Duration = entity.Duration,
            FirstStartDateTime = entity.FirstStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            FirstPossibleOfferingStartDateTime =
                entity.FirstPossibleOfferingStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            LastPossibleOfferingStartDateTime =
                entity.LastPossibleOfferingStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            LastPossibleOfferingEndDateTime =
                entity.LastPossibleOfferingEndDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            Level = entity.Level,
            FieldsOfStudy = entity.FieldsOfStudy,
            Enrolment = string.IsNullOrEmpty(entity.EnrolmentJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.EnrolmentJson),
            Resources = string.IsNullOrEmpty(entity.ResourcesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ResourcesJson),
            LearningOutcomeIds = entity.LearningOutcomes.Count == 0
                ? null
                : [.. entity.LearningOutcomes.Select(o => new Identifier { Value = o.LearningOutcomeId })],
            Assessment = string.IsNullOrEmpty(entity.AssessmentJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AssessmentJson),
            AdmissionRequirements = string.IsNullOrEmpty(entity.AdmissionRequirementsJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AdmissionRequirementsJson),
            QualificationRequirements = string.IsNullOrEmpty(entity.QualificationRequirementsJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.QualificationRequirementsJson),
            Link = entity.Link,
            Addresses = string.IsNullOrEmpty(entity.AddressesJson)
                ? null
                : JsonSerializer.Deserialize<Address[]>(entity.AddressesJson),
            SupplementaryInformation = string.IsNullOrEmpty(entity.SupplementaryInformationJson)
                ? null
                : JsonSerializer.Deserialize<SupplementaryInformation[]>(entity.SupplementaryInformationJson),
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToProgrammeConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson),
            ParentId = entity.ParentEntityId.HasValue
                ? new Identifier { Value = entity.Parent?.ProgrammeId ?? string.Empty }
                : null,
            ChildIds = entity.Children.Count > 0
                ? [.. entity.Children.Select(c => new Identifier { Value = c.ProgrammeId })]
                : null,
            OrganisationId = entity.OrganisationEntityId.HasValue
                ? new Identifier { Value = entity.Organisation?.OrganisationId ?? string.Empty }
                : null,
            CoordinatorIds = entity.Coordinators.Count > 0
                ? [.. entity.Coordinators.Select(c => new Identifier { Value = c.PersonId })]
                : null,
            InstructorIds = entity.Instructors.Count > 0
                ? [.. entity.Instructors.Select(i => new Identifier { Value = i.PersonId })]
                : null
        };
    }

    /// <summary>
    ///     Maps a <see cref="Programme" /> API model to a new <see cref="ProgrammeEntity" />, for the create
    ///     half of an upsert. Parent/Organisation/coordinator/instructor FK resolution requires an async
    ///     DB lookup this synchronous method can't perform - the caller resolves and sets those itself
    ///     after calling this method.
    /// </summary>
    public static ProgrammeEntity ToEntity(this Programme model)
    {
        return new ProgrammeEntity
        {
            ProgrammeId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            Abbreviation = model.Abbreviation,
            NameJson = JsonSerializer.Serialize(model.Name),
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            QualificationAwarded = model.QualificationAwarded,
            StudyLoadJson = model.StudyLoad != null ? JsonSerializer.Serialize(model.StudyLoad) : null,
            TeachingLanguagesJson = model.TeachingLanguages != null
                ? JsonSerializer.Serialize(model.TeachingLanguages)
                : null,
            ProgrammeType = model.ProgrammeType,
            ModeOfStudy = model.ModeOfStudy,
            ModesOfDeliveryJson = model.ModesOfDelivery != null ? JsonSerializer.Serialize(model.ModesOfDelivery) : null,
            LevelOfQualification = model.LevelOfQualification,
            FormalDocument = model.FormalDocument,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            ValidFrom = model.ValidFrom != null ? DateTime.Parse(model.ValidFrom) : null,
            ValidTo = model.ValidTo != null ? DateTime.Parse(model.ValidTo) : null,
            Duration = model.Duration,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="ProgrammeEntity" /> from a <see cref="Programme" /> API model, for
    ///     the update half of an upsert. See <see cref="ToEntity" /> regarding FK resolution.
    /// </summary>
    public static void UpdateFrom(this ProgrammeEntity entity, Programme model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.QualificationAwarded = model.QualificationAwarded ?? entity.QualificationAwarded;
        entity.StudyLoadJson =
            model.StudyLoad != null ? JsonSerializer.Serialize(model.StudyLoad) : entity.StudyLoadJson;
        entity.TeachingLanguagesJson = model.TeachingLanguages != null
            ? JsonSerializer.Serialize(model.TeachingLanguages)
            : entity.TeachingLanguagesJson;
        entity.ProgrammeType = model.ProgrammeType ?? entity.ProgrammeType;
        entity.ModeOfStudy = model.ModeOfStudy ?? entity.ModeOfStudy;
        entity.ModesOfDeliveryJson = model.ModesOfDelivery != null
            ? JsonSerializer.Serialize(model.ModesOfDelivery)
            : entity.ModesOfDeliveryJson;
        entity.LevelOfQualification = model.LevelOfQualification ?? entity.LevelOfQualification;
        entity.FormalDocument = model.FormalDocument ?? entity.FormalDocument;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ValidFrom = model.ValidFrom != null ? DateTime.Parse(model.ValidFrom) : entity.ValidFrom;
        entity.ValidTo = model.ValidTo != null ? DateTime.Parse(model.ValidTo) : entity.ValidTo;
        entity.Duration = model.Duration ?? entity.Duration;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
