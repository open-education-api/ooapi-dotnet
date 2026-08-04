using System.Text.Json;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using OEAPI.Infrastructure.Query.Consumers;

namespace OEAPI.Infrastructure.Data.Mapping;

public static class MembershipMappingExtensions
{
    /// <summary>
    ///     Maps a MembershipEntity to a Membership API model. The group is passed explicitly rather than
    ///     read from <c>entity.Group</c> since callers typically already know the parent group (e.g. the
    ///     nested <c>/groups/{groupId}/memberships</c> endpoint) and don't eager-load that navigation.
    /// </summary>
    public static Membership ToApiModel(this MembershipEntity entity, string parentGroupId, string? consumer)
    {
        return new Membership
        {
            PersonId = entity.Person?.PersonId ?? string.Empty,
            GroupId = parentGroupId,
            Role = entity.Role ?? string.Empty,
            State = entity.State ?? string.Empty,
            StartDateTime = entity.StartDateTime,
            EndDateTime = entity.EndDateTime,
            Consumer = entity.ConsumerJson.ToGenericConsumer(consumer),
            Ext = entity.ExtJson != null ? JsonSerializer.Deserialize<object>(entity.ExtJson) : null
        };
    }
}
