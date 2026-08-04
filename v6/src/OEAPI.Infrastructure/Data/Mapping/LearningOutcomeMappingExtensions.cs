using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class LearningOutcomeMappingExtensions
{
    public static LearningOutcome ToApiModel(this LearningOutcomeEntity entity, string? consumer)
    {
        return new LearningOutcome
        {
            LearningOutcomeIdValue = entity.LearningOutcomeId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            Abbreviation = entity.Abbreviation,
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            ParentIds = entity.Parents.Count > 0
                ? [.. entity.Parents.Select(p => new Identifier { Value = p.LearningOutcomeId })]
                : null,
            Parents = null,
            ChildIds = entity.Children.Count > 0
                ? [.. entity.Children.Select(c => new Identifier { Value = c.LearningOutcomeId })]
                : null,
            Children = null,
            FieldsOfStudy = entity.FieldsOfStudy,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            ComplexityLevel = entity.ComplexityLevel,
            ValidFrom = entity.ValidFrom,
            ValidTo = entity.ValidTo,
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson)
        };
    }

    /// <summary>
    ///     Maps a <see cref="LearningOutcome" /> API model to a new <see cref="LearningOutcomeEntity" />,
    ///     for the create half of an upsert. Parent/Child/Organisation FK resolution requires an async DB
    ///     lookup this synchronous method can't perform - the caller resolves and sets those itself after
    ///     calling this method.
    /// </summary>
    public static LearningOutcomeEntity ToEntity(this LearningOutcome model)
    {
        return new LearningOutcomeEntity
        {
            LearningOutcomeId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            NameJson = JsonSerializer.Serialize(model.Name),
            Abbreviation = model.Abbreviation,
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            FieldsOfStudy = model.FieldsOfStudy,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            ComplexityLevel = model.ComplexityLevel,
            ValidFrom = model.ValidFrom,
            ValidTo = model.ValidTo,
            ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="LearningOutcomeEntity" /> from a <see cref="LearningOutcome" /> API
    ///     model, for the update half of an upsert. See <see cref="ToEntity" /> regarding FK resolution.
    /// </summary>
    public static void UpdateFrom(this LearningOutcomeEntity entity, LearningOutcome model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.FieldsOfStudy = model.FieldsOfStudy ?? entity.FieldsOfStudy;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ComplexityLevel = model.ComplexityLevel ?? entity.ComplexityLevel;
        entity.ValidFrom = model.ValidFrom ?? entity.ValidFrom;
        entity.ValidTo = model.ValidTo ?? entity.ValidTo;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
