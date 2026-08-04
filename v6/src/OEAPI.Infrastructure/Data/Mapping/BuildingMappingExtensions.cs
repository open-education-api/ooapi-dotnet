using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class BuildingMappingExtensions
{
    /// <summary>
    ///     Maps a <see cref="BuildingEntity" /> to a <see cref="Building" /> API model. Unlike a real
    ///     relationship (e.g. <c>Room.building</c>, which has a <c>buildingId</c> sibling and is only
    ///     populated on request via <c>expand=</c>), the spec declares <c>Building.address</c> as a plain
    ///     <c>oneOf[Address, null]</c> field with no id-based indirection and no <c>expand</c> parameter
    ///     at all on either <c>GET /buildings</c> or <c>GET /buildings/{buildingId}</c> - so it's always
    ///     populated directly here, same as e.g. <c>Organisation.addresses</c>. Requires the caller's
    ///     query to include <see cref="BuildingEntity.Address" /> (see
    ///     <c>BuildingsController.ApplyIncludes</c>).
    /// </summary>
    public static Building ToApiModel(this BuildingEntity entity, string? consumer)
    {
        return new Building
        {
            BuildingId = entity.BuildingId,
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
            Address = entity.Address?.ToApiModel(),
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = entity.ExtJson != null ? JsonSerializer.Deserialize<object>(entity.ExtJson) : null
        };
    }

    /// <summary>
    ///     Maps a <see cref="Building" /> API model to a new <see cref="BuildingEntity" />, for the create
    ///     half of an upsert.
    /// </summary>
    public static BuildingEntity ToEntity(this Building model)
    {
        return new BuildingEntity
        {
            BuildingId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            NameJson = model.Name.Length > 0 ? JsonSerializer.Serialize(model.Name) : string.Empty,
            Abbreviation = model.Abbreviation,
            DescriptionJson = model.Description != null && model.Description.Length > 0
                ? JsonSerializer.Serialize(model.Description)
                : null,
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
    ///     Updates an existing <see cref="BuildingEntity" /> from a <see cref="Building" /> API model, for
    ///     the update half of an upsert.
    /// </summary>
    public static void UpdateFrom(this BuildingEntity entity, Building model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.NameJson = model.Name.Length > 0
            ? JsonSerializer.Serialize(model.Name)
            : entity.NameJson;
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.DescriptionJson = model.Description != null && model.Description.Length > 0
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
