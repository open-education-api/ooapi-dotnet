using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class AcademicSessionMappingExtensions
{
    public static AcademicSession ToApiModel(this AcademicSessionEntity entity, string? consumer)
    {
        string academicSessionType = string.IsNullOrEmpty(entity.AcademicSessionType)
            ? "academic_year"
            : entity.AcademicSessionType;

        return new AcademicSession
        {
            AcademicSessionId = entity.AcademicSessionId,
            AcademicSessionType = academicSessionType,
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
            StartDateTime = entity.StartDateTime ?? string.Empty,
            EndDateTime = entity.EndDateTime ?? string.Empty,
            ParentId = entity.Parent != null ? new Identifier { Value = entity.Parent.AcademicSessionId } : null,
            Parent = null,
            ChildIds = entity.Children.Count > 0
                ? [.. entity.Children.Select(c => new Identifier { Value = c.AcademicSessionId })]
                : null,
            Children = null,
            YearId = entity.Year != null ? new Identifier { Value = entity.Year.AcademicSessionId } : null,
            Year = null,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = entity.ExtJson != null ? JsonSerializer.Deserialize<object>(entity.ExtJson) : null
        };
    }

    /// <summary>
    ///     Maps an <see cref="AcademicSession" /> API model to a new <see cref="AcademicSessionEntity" />,
    ///     for the create half of an upsert. Parent/Year FK resolution (from <c>ParentId</c>/<c>YearId</c>,
    ///     external string IDs) requires an async DB lookup this synchronous method can't perform - the
    ///     caller resolves and sets those FKs itself after calling this method.
    /// </summary>
    public static AcademicSessionEntity ToEntity(this AcademicSession model)
    {
        return new AcademicSessionEntity
        {
            AcademicSessionId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            NameJson = model.Name.Length > 0 ? JsonSerializer.Serialize(model.Name) : string.Empty,
            DescriptionJson = model.Name.Length > 0 ? JsonSerializer.Serialize(model.Name) : null,
            AcademicSessionType = model.AcademicSessionType,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            Abbreviation = model.Abbreviation,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="AcademicSessionEntity" /> from an <see cref="AcademicSession" />
    ///     API model, for the update half of an upsert. See <see cref="ToEntity" /> regarding Parent/Year FK
    ///     resolution.
    /// </summary>
    public static void UpdateFrom(this AcademicSessionEntity entity, AcademicSession model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.NameJson = model.Name.Length > 0
            ? JsonSerializer.Serialize(model.Name)
            : entity.NameJson;
        entity.DescriptionJson = model.Name.Length > 0
            ? JsonSerializer.Serialize(model.Name)
            : entity.DescriptionJson;
        entity.AcademicSessionType = model.AcademicSessionType;
        entity.StartDateTime = model.StartDateTime;
        entity.EndDateTime = model.EndDateTime;
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
