using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class TestComponentMappingExtensions
{
    public static TestComponent ToApiModel(this TestComponentEntity entity, string? consumer)
    {
        return new TestComponent
        {
            ComponentIdValue = entity.ComponentId,
            ComponentType = entity.ComponentType,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            ModesOfDelivery = string.IsNullOrEmpty(entity.ModesOfDeliveryJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ModesOfDeliveryJson),
            Duration = entity.Duration,
            FirstPossibleOfferingStartDateTime =
                entity.FirstPossibleOfferingStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            LastPossibleOfferingStartDateTime =
                entity.LastPossibleOfferingStartDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            LastPossibleOfferingEndDateTime =
                entity.LastPossibleOfferingEndDateTime?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
            TeachingLanguages = string.IsNullOrEmpty(entity.TeachingLanguagesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.TeachingLanguagesJson),
            Abbreviation = entity.Abbreviation,
            ExtraDuration = entity.ExtraDuration,
            ResultValueType = entity.ResultValueType,
            Attempts = entity.Attempts,
            PassFrom = entity.PassFrom,
            State = entity.State,
            Enrolment = string.IsNullOrEmpty(entity.EnrolmentJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.EnrolmentJson),
            Resources = string.IsNullOrEmpty(entity.ResourcesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ResourcesJson),
            Assessment = string.IsNullOrEmpty(entity.AssessmentJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.AssessmentJson),
            Addresses = string.IsNullOrEmpty(entity.AddressesJson)
                ? null
                : JsonSerializer.Deserialize<Address[]>(entity.AddressesJson),
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson),
            CourseId = entity.CourseEntityId.HasValue
                ? new Identifier { Value = entity.Course?.CourseId ?? string.Empty }
                : null,
            OrganisationId = entity.OrganisationEntityId.HasValue
                ? new Identifier { Value = entity.Organisation?.OrganisationId ?? string.Empty }
                : null,
            ParentId = entity.Parent != null ? new Identifier { Value = entity.Parent.ComponentId } : null,
            Parent = null,
            // childIds is a plain array in the spec (no "null" alternative, unlike the equivalent
            // field on Organisation/AcademicSession/Programme) - always an array, even when empty.
            // children stays null-until-expanded, matching every sibling resource's expand
            // convention, even though TestComponent.children's own spec type (also array-only, no
            // null) would technically allow always returning an array here too.
            ChildIds = [.. entity.Children.Select(c => new Identifier { Value = c.ComponentId })],
            Children = null,
            LearningOutcomeIds = entity.LearningOutcomes.Count > 0
                ? [.. entity.LearningOutcomes.Select(lo => new Identifier { Value = lo.LearningOutcomeId })]
                : null,
            LearningOutcomes = null
        };
    }

    /// <summary>
    ///     Maps a <see cref="TestComponent" /> API model to a new <see cref="TestComponentEntity" />, for
    ///     the create half of an upsert. Course/Organisation/Parent/LearningOutcome FK resolution requires
    ///     an async DB lookup this synchronous method can't perform - the caller resolves and sets those
    ///     itself after calling this method.
    /// </summary>
    public static TestComponentEntity ToEntity(this TestComponent model)
    {
        return new TestComponentEntity
        {
            ComponentId = Guid.NewGuid().ToString(),
            ComponentType = model.ComponentType,
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            NameJson = JsonSerializer.Serialize(model.Name),
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            ModesOfDeliveryJson =
                model.ModesOfDelivery != null ? JsonSerializer.Serialize(model.ModesOfDelivery) : null,
            Duration = model.Duration,
            FirstPossibleOfferingStartDateTime = ParseDateTime(model.FirstPossibleOfferingStartDateTime),
            LastPossibleOfferingStartDateTime = ParseDateTime(model.LastPossibleOfferingStartDateTime),
            LastPossibleOfferingEndDateTime = ParseDateTime(model.LastPossibleOfferingEndDateTime),
            TeachingLanguagesJson = model.TeachingLanguages != null
                ? JsonSerializer.Serialize(model.TeachingLanguages)
                : null,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            Abbreviation = model.Abbreviation,
            ExtraDuration = model.ExtraDuration,
            ResultValueType = model.ResultValueType,
            Attempts = model.Attempts,
            PassFrom = model.PassFrom,
            State = model.State,
            EnrolmentJson = model.Enrolment != null ? JsonSerializer.Serialize(model.Enrolment) : null,
            ResourcesJson = model.Resources != null ? JsonSerializer.Serialize(model.Resources) : null,
            AssessmentJson = model.Assessment != null ? JsonSerializer.Serialize(model.Assessment) : null,
            AddressesJson = model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="TestComponentEntity" /> from a <see cref="TestComponent" /> API
    ///     model, for the update half of an upsert. See <see cref="ToEntity" /> regarding FK resolution.
    /// </summary>
    public static void UpdateFrom(this TestComponentEntity entity, TestComponent model)
    {
        entity.ComponentType = model.ComponentType ?? entity.ComponentType;
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.ModesOfDeliveryJson = model.ModesOfDelivery != null
            ? JsonSerializer.Serialize(model.ModesOfDelivery)
            : entity.ModesOfDeliveryJson;
        entity.Duration = model.Duration ?? entity.Duration;
        entity.FirstPossibleOfferingStartDateTime =
            ParseDateTime(model.FirstPossibleOfferingStartDateTime) ?? entity.FirstPossibleOfferingStartDateTime;
        entity.LastPossibleOfferingStartDateTime =
            ParseDateTime(model.LastPossibleOfferingStartDateTime) ?? entity.LastPossibleOfferingStartDateTime;
        entity.LastPossibleOfferingEndDateTime =
            ParseDateTime(model.LastPossibleOfferingEndDateTime) ?? entity.LastPossibleOfferingEndDateTime;
        entity.TeachingLanguagesJson = model.TeachingLanguages != null
            ? JsonSerializer.Serialize(model.TeachingLanguages)
            : entity.TeachingLanguagesJson;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.ExtraDuration = model.ExtraDuration ?? entity.ExtraDuration;
        entity.ResultValueType = model.ResultValueType ?? entity.ResultValueType;
        entity.Attempts = model.Attempts ?? entity.Attempts;
        entity.PassFrom = model.PassFrom ?? entity.PassFrom;
        entity.State = model.State ?? entity.State;
        entity.EnrolmentJson =
            model.Enrolment != null ? JsonSerializer.Serialize(model.Enrolment) : entity.EnrolmentJson;
        entity.ResourcesJson =
            model.Resources != null ? JsonSerializer.Serialize(model.Resources) : entity.ResourcesJson;
        entity.AssessmentJson =
            model.Assessment != null ? JsonSerializer.Serialize(model.Assessment) : entity.AssessmentJson;
        entity.AddressesJson =
            model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : entity.AddressesJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }

    private static DateTime? ParseDateTime(string? dateString)
    {
        if (string.IsNullOrEmpty(dateString)) return null;

        return DateTime.TryParse(dateString, out DateTime result) ? result : null;
    }
}
