using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class ProgrammeOfferingAssociationMappingExtensions
{
    public static ProgrammeOfferingAssociation ToApiModel(this ProgrammeOfferingAssociationEntity entity,
        string? consumer)
    {
        return new ProgrammeOfferingAssociation
        {
            AssociationIdValue = entity.ProgrammeOfferingAssociationIdValue,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            Role = entity.Role,
            StartDateTime = entity.StartDateTime,
            ExpectedEndDateTime = entity.ExpectedEndDateTime,
            ActualEndDateTime = entity.ActualEndDateTime,
            State = entity.State,
            RemoteState = entity.RemoteState,
            Result = string.IsNullOrEmpty(entity.ResultJson)
                ? null
                : JsonSerializer.Deserialize<Result>(entity.ResultJson),
            ProgrammeOfferingId = entity.ProgrammeOffering != null
                ? new Identifier { Value = entity.ProgrammeOffering.ProgrammeOfferingIdValue }
                : null,
            PersonId = entity.Person != null ? new Identifier { Value = entity.Person.PersonId } : null,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson)
        };
    }

    /// <summary>
    ///     Maps a <see cref="ProgrammeOfferingAssociation" /> API model to a new
    ///     <see cref="ProgrammeOfferingAssociationEntity" />, for the create half of an upsert.
    ///     ProgrammeOffering/Person/Organisation FK resolution (external string IDs) requires an async DB
    ///     lookup this synchronous method can't perform - the caller resolves and sets those FKs itself
    ///     after calling this method.
    /// </summary>
    public static ProgrammeOfferingAssociationEntity ToEntity(this ProgrammeOfferingAssociation model)
    {
        return new ProgrammeOfferingAssociationEntity
        {
            ProgrammeOfferingAssociationIdValue = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode?.CodeType ?? string.Empty,
            PrimaryCode = model.PrimaryCode?.Code ?? string.Empty,
            Role = model.Role,
            StartDateTime = model.StartDateTime,
            ExpectedEndDateTime = model.ExpectedEndDateTime,
            ActualEndDateTime = model.ActualEndDateTime,
            State = model.State,
            RemoteState = model.RemoteState,
            ResultJson = model.Result != null ? JsonSerializer.Serialize(model.Result) : null,
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
    ///     Updates an existing <see cref="ProgrammeOfferingAssociationEntity" /> from a
    ///     <see cref="ProgrammeOfferingAssociation" /> API model, for the update half of an upsert. See
    ///     <see cref="ToEntity" /> regarding ProgrammeOffering/Person/Organisation FK resolution.
    /// </summary>
    public static void UpdateFrom(this ProgrammeOfferingAssociationEntity entity, ProgrammeOfferingAssociation model)
    {
        entity.PrimaryCodeType = model.PrimaryCode?.CodeType ?? entity.PrimaryCodeType;
        entity.PrimaryCode = model.PrimaryCode?.Code ?? entity.PrimaryCode;
        entity.Role = model.Role ?? entity.Role;
        entity.StartDateTime = model.StartDateTime ?? entity.StartDateTime;
        entity.ExpectedEndDateTime = model.ExpectedEndDateTime ?? entity.ExpectedEndDateTime;
        entity.ActualEndDateTime = model.ActualEndDateTime ?? entity.ActualEndDateTime;
        entity.State = model.State ?? entity.State;
        entity.RemoteState = model.RemoteState ?? entity.RemoteState;
        entity.ResultJson = model.Result != null ? JsonSerializer.Serialize(model.Result) : entity.ResultJson;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
