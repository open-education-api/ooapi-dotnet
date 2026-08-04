using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class RoomMappingExtensions
{
    public static Room ToApiModel(this RoomEntity entity, string? consumer)
    {
        Geolocation? geolocation = null;
        if (entity.Latitude.HasValue && entity.Longitude.HasValue)
            geolocation = new Geolocation
            {
                Latitude = entity.Latitude.Value,
                Longitude = entity.Longitude.Value
            };

        return new Room
        {
            RoomId = entity.RoomId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            RoomType = string.IsNullOrEmpty(entity.RoomType) ? "general_purpose" : entity.RoomType,
            Abbreviation = entity.Abbreviation,
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            TotalSeats = entity.TotalSeats,
            AvailableSeats = entity.AvailableSeats,
            Floor = entity.Floor,
            Wing = entity.Wing,
            Geolocation = geolocation,
            BuildingId = entity.Building != null ? new Identifier { Value = entity.Building.BuildingId } : null,
            Building = null,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson)
        };
    }

    /// <summary>
    ///     Maps a <see cref="Room" /> API model to a new <see cref="RoomEntity" />, for the create half of
    ///     an upsert. Building FK resolution (from <c>BuildingId</c>, an external string ID) requires an
    ///     async DB lookup this synchronous method can't perform - the caller resolves and sets that FK
    ///     itself after calling this method.
    /// </summary>
    public static RoomEntity ToEntity(this Room model)
    {
        return new RoomEntity
        {
            RoomId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            RoomType = model.RoomType,
            Abbreviation = model.Abbreviation,
            Name = model.Name.Length > 0 ? model.Name[0].Value : null,
            NameJson = JsonSerializer.Serialize(model.Name),
            Description = model.Description?.Length > 0 ? model.Description[0].Value : null,
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            TotalSeats = model.TotalSeats,
            AvailableSeats = model.AvailableSeats,
            Floor = model.Floor,
            Wing = model.Wing,
            Latitude = model.Geolocation?.Latitude,
            Longitude = model.Geolocation?.Longitude,
            Capacity = model.TotalSeats,
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
    ///     Updates an existing <see cref="RoomEntity" /> from a <see cref="Room" /> API model, for the
    ///     update half of an upsert. See <see cref="ToEntity" /> regarding Building FK resolution.
    /// </summary>
    public static void UpdateFrom(this RoomEntity entity, Room model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.RoomType = model.RoomType;
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.Name = model.Name.Length > 0 ? model.Name[0].Value : entity.Name;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.Description = model.Description?.Length > 0 ? model.Description[0].Value : entity.Description;
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.TotalSeats = model.TotalSeats ?? entity.TotalSeats;
        entity.AvailableSeats = model.AvailableSeats ?? entity.AvailableSeats;
        entity.Floor = model.Floor ?? entity.Floor;
        entity.Wing = model.Wing ?? entity.Wing;
        entity.Latitude = model.Geolocation?.Latitude ?? entity.Latitude;
        entity.Longitude = model.Geolocation?.Longitude ?? entity.Longitude;
        entity.Capacity = model.TotalSeats ?? entity.Capacity;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
