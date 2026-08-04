using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class CourseOfferingMappingExtensions
{
    public static CourseOffering ToApiModel(this CourseOfferingEntity entity, string? consumer)
    {
        Identifier[]? programmeOfferingIds = null;
        if (entity.ProgrammeOfferings.Count > 0)
        {
            programmeOfferingIds = [.. entity.ProgrammeOfferings
                .Where(po => !string.IsNullOrEmpty(po.ProgrammeOfferingIdValue))
                .Select(po => new Identifier { Value = po.ProgrammeOfferingIdValue })];
            if (programmeOfferingIds.Length == 0) programmeOfferingIds = null;
        }

        return new CourseOffering
        {
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            CourseOfferingIdValue = entity.CourseOfferingId,
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            State = entity.State,
            RosteringState = entity.RosteringState,
            Abbreviation = entity.Abbreviation,
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            TeachingLanguages = string.IsNullOrEmpty(entity.TeachingLanguagesJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.TeachingLanguagesJson),
            ModesOfDelivery = string.IsNullOrEmpty(entity.ModesOfDeliveryJson)
                ? null
                : JsonSerializer.Deserialize<string[]>(entity.ModesOfDeliveryJson),
            MaxNumberStudents = entity.MaxNumberStudents,
            EnrolledNumberStudents = entity.EnrolledNumberStudents,
            PendingNumberStudents = entity.PendingNumberStudents,
            MinNumberStudents = entity.MinNumberStudents,
            ResultValueType = entity.ResultValueType,
            Link = entity.Link,
            EnrolmentPeriods = string.IsNullOrEmpty(entity.EnrolmentPeriodsJson)
                ? null
                : JsonSerializer.Deserialize<EnrolmentPeriods[]>(entity.EnrolmentPeriodsJson),
            SupplementaryInformation = string.IsNullOrEmpty(entity.SupplementaryInformationJson)
                ? []
                : JsonSerializer.Deserialize<SupplementaryInformation[]>(entity.SupplementaryInformationJson),
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime,
            FlexibleEntryPeriodStartDateTime = entity.FlexibleEntryPeriodStartDateTime,
            FlexibleEntryPeriodEndDateTime = entity.FlexibleEntryPeriodEndDateTime,
            Addresses = string.IsNullOrEmpty(entity.AddressesJson)
                ? null
                : JsonSerializer.Deserialize<Address[]>(entity.AddressesJson),
            PriceInformation = string.IsNullOrEmpty(entity.PriceInformationJson)
                ? null
                : JsonSerializer.Deserialize<Cost[]>(entity.PriceInformationJson),
            CourseId = entity.Course != null ? new Identifier { Value = entity.Course.CourseId } : null,
            Course = null,
            ProgrammeOfferingIds = programmeOfferingIds,
            ProgrammeOfferings = null,
            AcademicSessionId = entity.AcademicSession != null
                ? new Identifier { Value = entity.AcademicSession.AcademicSessionId }
                : null,
            AcademicSession = null,
            OrganisationId = entity.Organisation != null
                ? new Identifier { Value = entity.Organisation.OrganisationId }
                : null,
            Organisation = null,
            GroupIds = entity.Groups.Count > 0
                ? [.. entity.Groups.Select(g => new Identifier { Value = g.GroupId })]
                : null,
            OtherCodes = entity.OtherCodes.ToApiModel(),
            ResultExpected = entity.ResultExpected,
            Consumer = entity.ConsumerJson.ToOfferingConsumer(consumer),
            Ext = entity.ExtJson != null ? JsonSerializer.Deserialize<object>(entity.ExtJson) : null
        };
    }

    /// <summary>
    ///     Maps a <see cref="CourseOffering" /> API model to a new <see cref="CourseOfferingEntity" />, for
    ///     the create half of an upsert. Course/Organisation/AcademicSession FK resolution requires an
    ///     async DB lookup this synchronous method can't perform - the caller resolves and sets those FKs
    ///     itself after calling this method.
    /// </summary>
    public static CourseOfferingEntity ToEntity(this CourseOffering model)
    {
        return new CourseOfferingEntity
        {
            CourseOfferingId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            NameJson = model.Name.Length > 0 ? JsonSerializer.Serialize(model.Name) : string.Empty,
            State = model.State,
            RosteringState = model.RosteringState,
            Abbreviation = model.Abbreviation,
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            TeachingLanguagesJson = model.TeachingLanguages != null
                ? JsonSerializer.Serialize(model.TeachingLanguages)
                : null,
            ModesOfDeliveryJson =
                model.ModesOfDelivery != null ? JsonSerializer.Serialize(model.ModesOfDelivery) : null,
            MaxNumberStudents = model.MaxNumberStudents,
            EnrolledNumberStudents = model.EnrolledNumberStudents,
            PendingNumberStudents = model.PendingNumberStudents,
            MinNumberStudents = model.MinNumberStudents,
            ResultValueType = model.ResultValueType,
            Link = model.Link,
            EnrolmentPeriodsJson =
                model.EnrolmentPeriods != null ? JsonSerializer.Serialize(model.EnrolmentPeriods) : null,
            SupplementaryInformationJson = model.SupplementaryInformation != null
                ? JsonSerializer.Serialize(model.SupplementaryInformation)
                : null,
            StartDateTime = model.StartDateTime,
            EndDateTime = model.EndDateTime,
            FlexibleEntryPeriodStartDateTime = model.FlexibleEntryPeriodStartDateTime,
            FlexibleEntryPeriodEndDateTime = model.FlexibleEntryPeriodEndDateTime,
            AddressesJson = model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : null,
            PriceInformationJson =
                model.PriceInformation != null ? JsonSerializer.Serialize(model.PriceInformation) : null,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            ResultExpected = model.ResultExpected,
            ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : null,
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="CourseOfferingEntity" /> from a <see cref="CourseOffering" /> API
    ///     model, for the update half of an upsert. See <see cref="ToEntity" /> regarding
    ///     Course/Organisation/AcademicSession FK resolution.
    /// </summary>
    public static void UpdateFrom(this CourseOfferingEntity entity, CourseOffering model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.NameJson = model.Name.Length > 0
            ? JsonSerializer.Serialize(model.Name)
            : entity.NameJson;
        entity.State = model.State ?? entity.State;
        entity.RosteringState = model.RosteringState ?? entity.RosteringState;
        entity.Abbreviation = model.Abbreviation ?? entity.Abbreviation;
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.TeachingLanguagesJson = model.TeachingLanguages != null
            ? JsonSerializer.Serialize(model.TeachingLanguages)
            : entity.TeachingLanguagesJson;
        entity.ModesOfDeliveryJson = model.ModesOfDelivery != null
            ? JsonSerializer.Serialize(model.ModesOfDelivery)
            : entity.ModesOfDeliveryJson;
        entity.MaxNumberStudents = model.MaxNumberStudents ?? entity.MaxNumberStudents;
        entity.EnrolledNumberStudents = model.EnrolledNumberStudents ?? entity.EnrolledNumberStudents;
        entity.PendingNumberStudents = model.PendingNumberStudents ?? entity.PendingNumberStudents;
        entity.MinNumberStudents = model.MinNumberStudents ?? entity.MinNumberStudents;
        entity.ResultValueType = model.ResultValueType ?? entity.ResultValueType;
        entity.Link = model.Link ?? entity.Link;
        entity.EnrolmentPeriodsJson = model.EnrolmentPeriods != null
            ? JsonSerializer.Serialize(model.EnrolmentPeriods)
            : entity.EnrolmentPeriodsJson;
        entity.SupplementaryInformationJson = model.SupplementaryInformation != null
            ? JsonSerializer.Serialize(model.SupplementaryInformation)
            : entity.SupplementaryInformationJson;
        entity.StartDateTime = model.StartDateTime ?? entity.StartDateTime;
        entity.EndDateTime = model.EndDateTime ?? entity.EndDateTime;
        entity.FlexibleEntryPeriodStartDateTime =
            model.FlexibleEntryPeriodStartDateTime ?? entity.FlexibleEntryPeriodStartDateTime;
        entity.FlexibleEntryPeriodEndDateTime =
            model.FlexibleEntryPeriodEndDateTime ?? entity.FlexibleEntryPeriodEndDateTime;
        entity.AddressesJson =
            model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : entity.AddressesJson;
        entity.PriceInformationJson = model.PriceInformation != null
            ? JsonSerializer.Serialize(model.PriceInformation)
            : entity.PriceInformationJson;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ResultExpected = model.ResultExpected ?? entity.ResultExpected;
        entity.ConsumerJson = model.Consumer != null ? JsonSerializer.Serialize(model.Consumer) : entity.ConsumerJson;
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
