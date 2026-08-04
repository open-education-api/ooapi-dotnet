using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

/// <summary>
///     The single mapping from <see cref="OrganisationEntity" /> to the API's <see cref="Organisation" />
///     model - used both by <c>OrganisationsController</c>'s own <c>GetAll</c>/<c>GetById</c> and by
///     every other controller that embeds a full <c>Organisation</c> object via <c>expand=organisation</c>.
///     See the "Scope and architecture" section of <c>docs/archive/DECISIONS-AND-ACTIONS.md</c> for why
///     this mapping lives here rather than per-controller.
/// </summary>
public static class OrganisationMappingExtensions
{
    /// <summary>
    ///     Maps an <see cref="OrganisationEntity" /> to an <see cref="Organisation" /> API model.
    ///     <c>Parent</c>/<c>Root</c>/<c>Children</c> (the full nested objects, as opposed to their
    ///     <c>*Id</c> counterparts) are always left <see langword="null" /> here - callers that want them
    ///     populated do so themselves after calling this method, consistent with every other resource's
    ///     "full objects are only ever one <c>expand</c> hop deep" convention.
    /// </summary>
    /// <param name="entity">
    ///     The organisation entity. <see cref="OrganisationEntity.Parent" />/
    ///     <see cref="OrganisationEntity.Root" />/<see cref="OrganisationEntity.Children" /> must already be
    ///     eager-loaded by the caller's query for <c>ParentId</c>/<c>RootId</c>/<c>ChildIds</c> to be
    ///     populated - if a caller doesn't need those fields, it doesn't need to eager-load them either.
    /// </param>
    /// <param name="consumer">The requested consumer, for consumer-specific data.</param>
    public static Organisation ToApiModel(this OrganisationEntity entity, string? consumer)
    {
        return new Organisation
        {
            OrganisationId = entity.OrganisationId,
            PrimaryCode = new IdentifierEntry
            {
                CodeType = entity.PrimaryCodeType,
                Code = entity.PrimaryCode
            },
            OrganisationType = string.IsNullOrEmpty(entity.OrganisationType) ? "root" : entity.OrganisationType,
            Name = string.IsNullOrEmpty(entity.NameJson)
                ? []
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.NameJson) ??
                  [],
            ShortName = entity.ShortName,
            Description = string.IsNullOrEmpty(entity.DescriptionJson)
                ? null
                : JsonSerializer.Deserialize<LanguageTypedString[]>(entity.DescriptionJson),
            Link = entity.Link,
            Logo = entity.Logo,
            Addresses = string.IsNullOrEmpty(entity.AddressesJson)
                ? null
                : JsonSerializer.Deserialize<Address[]>(entity.AddressesJson),
            OtherCodes = entity.OtherCodes.ToApiModel(),
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = string.IsNullOrEmpty(entity.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(entity.ExtJson),
            ParentId = entity.Parent != null ? new Identifier { Value = entity.Parent.OrganisationId } : null,
            Parent = null,
            RootId = entity.Root != null ? new Identifier { Value = entity.Root.OrganisationId } : null,
            Root = null,
            ChildIds = entity.Children.Count > 0
                ? [.. entity.Children.Select(c => new Identifier { Value = c.OrganisationId })]
                : null,
            Children = null
        };
    }

    /// <summary>
    ///     Maps an <see cref="Organisation" /> API model to a new <see cref="OrganisationEntity" />, for the
    ///     create half of an upsert. <c>Parent</c>/<c>Root</c> FK resolution (from <c>ParentId</c>/
    ///     <c>RootId</c>, external string IDs) requires an async DB lookup this synchronous method can't
    ///     perform - the caller resolves and sets those FKs itself after calling this method.
    /// </summary>
    public static OrganisationEntity ToEntity(this Organisation model)
    {
        return new OrganisationEntity
        {
            OrganisationId = Guid.NewGuid().ToString(),
            PrimaryCodeType = model.PrimaryCode.CodeType,
            PrimaryCode = model.PrimaryCode.Code,
            ShortName = model.ShortName,
            NameJson = JsonSerializer.Serialize(model.Name),
            DescriptionJson = model.Description != null ? JsonSerializer.Serialize(model.Description) : null,
            Logo = model.Logo,
            Link = model.Link,
            AddressesJson = model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : null,
            OrganisationType = model.OrganisationType,
            OtherCodes =
                model.OtherCodes?.Select(oc => new OtherCodeEntity { CodeType = oc.CodeType, Code = oc.Code })
                    .ToList() ?? [],
            ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : null,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
    }

    /// <summary>
    ///     Updates an existing <see cref="OrganisationEntity" /> from an <see cref="Organisation" /> API
    ///     model, for the update half of an upsert. See <see cref="ToEntity" /> regarding
    ///     Parent/Root FK resolution.
    /// </summary>
    public static void UpdateFrom(this OrganisationEntity entity, Organisation model)
    {
        entity.PrimaryCodeType = model.PrimaryCode.CodeType;
        entity.PrimaryCode = model.PrimaryCode.Code;
        entity.ShortName = model.ShortName ?? entity.ShortName;
        entity.NameJson = JsonSerializer.Serialize(model.Name);
        entity.DescriptionJson = model.Description != null
            ? JsonSerializer.Serialize(model.Description)
            : entity.DescriptionJson;
        entity.Logo = model.Logo ?? entity.Logo;
        entity.Link = model.Link ?? entity.Link;
        entity.AddressesJson =
            model.Addresses != null ? JsonSerializer.Serialize(model.Addresses) : entity.AddressesJson;
        entity.OrganisationType = model.OrganisationType;
        entity.OtherCodes.SyncFrom(model.OtherCodes);
        entity.ExtJson = model.Ext != null ? JsonSerializer.Serialize(model.Ext) : entity.ExtJson;
        entity.ModifiedAt = DateTime.UtcNow;
    }
}
