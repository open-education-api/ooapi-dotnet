using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class TestComponentOfferingAssociationMappingExtensions
{
    public static TestComponentOfferingAssociation ToApiModel(this TestComponentOfferingAssociationEntity entity,
        string? consumer)
    {
        return new TestComponentOfferingAssociation
        {
            AssociationIdValue = entity.TestComponentOfferingAssociationIdValue,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            Role = entity.Role,
            Attendance = entity.Attendance,
            RequiredPersonalNeeds = string.IsNullOrEmpty(entity.RequiredPersonalNeedsJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.RequiredPersonalNeedsJson),
            ExtraDuration = entity.ExtraDuration,
            InitialAttemptOnAssociation = entity.InitialAttemptOnAssociation,
            MaximumNumberOfAttemptsOnAssociation = entity.MaximumNumberOfAttemptsOnAssociation,
            Irregularities = string.IsNullOrEmpty(entity.IrregularitiesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.IrregularitiesJson),
            Documents = string.IsNullOrEmpty(entity.DocumentsJson)
                ? null
                : JsonSerializer.Deserialize<Document[]>(entity.DocumentsJson),
            AttemptIds = entity.Attempts.Count == 0
                ? null
                : [.. entity.Attempts.Select(a => new Identifier { Value = a.AttemptIdValue })],
            StartDateTime = entity.StartDateTime,
            ExpectedEndDateTime = entity.ExpectedEndDateTime,
            ActualEndDateTime = entity.ActualEndDateTime,
            State = entity.State,
            RemoteState = entity.RemoteState,
            Result = string.IsNullOrEmpty(entity.ResultJson)
                ? null
                : JsonSerializer.Deserialize<Result>(entity.ResultJson),
            TestComponentOfferingId = entity.TestComponentOffering != null
                ? new Identifier { Value = entity.TestComponentOffering.TestComponentOfferingIdValue }
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
    ///     Maps a <see cref="TestComponentOfferingAssociation" /> API model to a new
    ///     <see cref="TestComponentOfferingAssociationEntity" />, for the create half of an upsert.
    ///     TestComponentOffering/Person/Organisation FK resolution (external string IDs) requires an async
    ///     DB lookup this synchronous method can't perform - the caller resolves and sets those FKs itself
    ///     after calling this method.
    /// </summary>
    public static TestComponentOfferingAssociationEntity ToEntity(this TestComponentOfferingAssociation model)
    {
        return new TestComponentOfferingAssociationEntity
        {
            TestComponentOfferingAssociationIdValue = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode?.CodeType ?? string.Empty,
            PrimaryCode = model.PrimaryCode?.Code ?? string.Empty,
            Role = model.Role,
            Attendance = model.Attendance,
            RequiredPersonalNeedsJson =
                model.RequiredPersonalNeeds != null ? JsonSerializer.Serialize(model.RequiredPersonalNeeds) : null,
            ExtraDuration = model.ExtraDuration,
            InitialAttemptOnAssociation = model.InitialAttemptOnAssociation,
            MaximumNumberOfAttemptsOnAssociation = model.MaximumNumberOfAttemptsOnAssociation,
            IrregularitiesJson =
                model.Irregularities != null ? JsonSerializer.Serialize(model.Irregularities) : null,
            DocumentsJson = model.Documents != null ? JsonSerializer.Serialize(model.Documents) : null,
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
    ///     Updates an existing <see cref="TestComponentOfferingAssociationEntity" /> from a
    ///     <see cref="TestComponentOfferingAssociation" /> API model, for the update half of an upsert. See
    ///     <see cref="ToEntity" /> regarding TestComponentOffering/Person/Organisation FK resolution.
    /// </summary>
    public static void UpdateFrom(this TestComponentOfferingAssociationEntity entity,
        TestComponentOfferingAssociation model)
    {
        entity.PrimaryCodeType = model.PrimaryCode?.CodeType ?? entity.PrimaryCodeType;
        entity.PrimaryCode = model.PrimaryCode?.Code ?? entity.PrimaryCode;
        entity.Role = model.Role ?? entity.Role;
        entity.Attendance = model.Attendance ?? entity.Attendance;
        entity.RequiredPersonalNeedsJson = model.RequiredPersonalNeeds != null
            ? JsonSerializer.Serialize(model.RequiredPersonalNeeds)
            : entity.RequiredPersonalNeedsJson;
        entity.ExtraDuration = model.ExtraDuration ?? entity.ExtraDuration;
        entity.InitialAttemptOnAssociation =
            model.InitialAttemptOnAssociation ?? entity.InitialAttemptOnAssociation;
        entity.MaximumNumberOfAttemptsOnAssociation =
            model.MaximumNumberOfAttemptsOnAssociation ?? entity.MaximumNumberOfAttemptsOnAssociation;
        entity.IrregularitiesJson = model.Irregularities != null
            ? JsonSerializer.Serialize(model.Irregularities)
            : entity.IrregularitiesJson;
        entity.DocumentsJson =
            model.Documents != null ? JsonSerializer.Serialize(model.Documents) : entity.DocumentsJson;
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
