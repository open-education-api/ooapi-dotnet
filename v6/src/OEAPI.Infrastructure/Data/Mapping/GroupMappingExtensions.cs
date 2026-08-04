using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class GroupMappingExtensions
{
    public static Group ToApiModel(this GroupEntity entity, string? consumer)
    {
        return new Group
        {
            GroupIdValue = entity.GroupId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            GroupType = string.IsNullOrEmpty(entity.GroupType) ? "group" : entity.GroupType,
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime,
            PersonCount = entity.PersonCount,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            OrganisationId = entity.Organisation != null
                ? new Identifier { Value = entity.Organisation.OrganisationId }
                : null,
            Organisation = null,
            AcademicSessionId = entity.AcademicSession != null
                ? new Identifier { Value = entity.AcademicSession.AcademicSessionId }
                : null,
            AcademicSession = null,
            OfferingIds = BuildOfferingIds(entity),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson)
        };
    }

    private static GroupOfferingReference[]? BuildOfferingIds(GroupEntity entity)
    {
        List<GroupOfferingReference> references =
        [
            .. entity.CourseOfferings.Select(co =>
                new GroupOfferingReference { CourseOfferingId = co.CourseOfferingId }),
            .. entity.ProgrammeOfferings.Select(po =>
                new GroupOfferingReference { ProgrammeOfferingId = po.ProgrammeOfferingIdValue }),
            .. entity.LearningComponentOfferings.Select(lco =>
                new GroupOfferingReference { LearningComponentOfferingId = lco.LearningComponentOfferingIdValue }),
            .. entity.TestComponentOfferings.Select(tco =>
                new GroupOfferingReference { TestComponentOfferingId = tco.TestComponentOfferingIdValue }),
        ];

        return references.Count > 0 ? [.. references] : null;
    }

    /// <summary>
    ///     Maps a <see cref="Group" /> API model to a new <see cref="GroupEntity" />, for the create half of
    ///     an upsert. Organisation/AcademicSession FK resolution (from <c>OrganisationId</c>/
    ///     <c>AcademicSessionId</c>, external string IDs) requires an async DB lookup this synchronous
    ///     method can't perform - the caller resolves and sets those FKs itself after calling this method.
    /// </summary>
    public static GroupEntity ToEntity(this Group model)
    {
        return new GroupEntity
        {
            GroupId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            GroupType = model.GroupType,
            NameJson = JsonSerializer.Serialize(model.Name),
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            PersonCount = model.PersonCount,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="GroupEntity" /> from a <see cref="Group" /> API model, for the
    ///     update half of an upsert. See <see cref="ToEntity" /> regarding Organisation/AcademicSession FK
    ///     resolution.
    /// </summary>
    public static void UpdateFrom(this GroupEntity entity, Group model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.GroupType = model.GroupType;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.StartDateTime = model.StartDateTime ?? entity.StartDateTime;
        entity.EndDateTime = model.EndDateTime ?? entity.EndDateTime;
        entity.PersonCount = model.PersonCount ?? entity.PersonCount;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
