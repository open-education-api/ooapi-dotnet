using System.ComponentModel.DataAnnotations;
using System.Reflection;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Infrastructure.Data.Entities;
using Xunit;

namespace OEAPI.Infrastructure.Tests.Data.Entities;

/// <summary>
///     Guards against the drift risk the archived <c>write-body-string-validation</c> change's
///     <c>design.md</c> flagged but didn't fix: an API model's <c>[StringLength]</c> and the
///     entity-layer <c>[StringLength]</c> it was copied from are two independently-declared numbers on
///     two different classes - nothing stops a future migration changing one without the matching API
///     model attribute being updated too. If the entity is narrowed and the API model is left too wide,
///     the truncation crash that whole change existed to fix comes back for that field; if the entity is
///     widened and the API model is left too narrow, the API rejects values the database would now
///     happily accept.
/// </summary>
public class ApiModelStringLengthSyncTests
{
    /// <summary>
    ///     Every write-reachable API model class paired with the single entity class its write path
    ///     ultimately persists into, per <c>write-body-string-validation</c>'s tasks 3.1-3.13.
    ///     <see cref="IdentifierEntry" /> (shared by every entity's own <c>PrimaryCode</c> pair plus
    ///     <see cref="OtherCodeEntity" />) and <see cref="AssociationPatchRequest" /> (shared by 4
    ///     different <c>*AssociationEntity</c> classes' <c>RemoteState</c>) don't map to a single entity,
    ///     so they're checked separately below instead of listed here.
    /// </summary>
    private static readonly (Type Api, Type Entity)[] ApiToEntity =
    [
        (typeof(Organisation), typeof(OrganisationEntity)),
        (typeof(Person), typeof(PersonEntity)),
        (typeof(PersonProperties), typeof(PersonEntity)),
        (typeof(Group), typeof(GroupEntity)),
        (typeof(Membership), typeof(MembershipEntity)),
        (typeof(CourseOffering), typeof(CourseOfferingEntity)),
        (typeof(LearningComponentOffering), typeof(LearningComponentOfferingEntity)),
        (typeof(ProgrammeOffering), typeof(ProgrammeOfferingEntity)),
        (typeof(TestComponentOffering), typeof(TestComponentOfferingEntity)),
        (typeof(CourseOfferingAssociation), typeof(CourseOfferingAssociationEntity)),
        (typeof(CourseOfferingAssociationExternalMeRequest), typeof(CourseOfferingAssociationEntity)),
        (typeof(LearningComponentOfferingAssociation), typeof(LearningComponentOfferingAssociationEntity)),
        (typeof(ProgrammeOfferingAssociation), typeof(ProgrammeOfferingAssociationEntity)),
        (typeof(ProgrammeOfferingAssociationExternalMeRequest), typeof(ProgrammeOfferingAssociationEntity)),
        (typeof(TestComponentOfferingAssociation), typeof(TestComponentOfferingAssociationEntity)),
        (typeof(TestComponentOfferingAssociationAttempt), typeof(TestComponentOfferingAssociationAttemptEntity))
    ];

    /// <summary>
    ///     Discovers every <c>[StringLength]</c>-carrying property on each paired API model class -
    ///     deliberately not a hand-maintained field list, so a future field that adds
    ///     <c>[StringLength]</c> to one of these classes is automatically covered by this guard too,
    ///     not just the fields known when this test was written.
    /// </summary>
    public static TheoryData<Type, string, Type> StringLengthPropertyTheoryData()
    {
        TheoryData<Type, string, Type> data = [];
        foreach ((Type api, Type entity) in ApiToEntity)
        foreach (PropertyInfo apiProperty in api.GetProperties())
            if (apiProperty.GetCustomAttribute<StringLengthAttribute>() != null)
                data.Add(api, apiProperty.Name, entity);
        return data;
    }

    [Theory]
    [MemberData(nameof(StringLengthPropertyTheoryData))]
    public void ApiModelStringLength_MatchesEntityColumn(Type apiType, string apiPropertyName, Type entityType)
    {
        PropertyInfo? entityProperty = entityType.GetProperty(apiPropertyName);
        Assert.True(entityProperty != null,
            $"{entityType.Name} has no '{apiPropertyName}' property matching {apiType.Name}.{apiPropertyName}.");

        StringLengthAttribute? entityAttribute = entityProperty.GetCustomAttribute<StringLengthAttribute>();
        Assert.True(entityAttribute != null,
            $"{entityType.Name}.{apiPropertyName} has no [StringLength] to compare against {apiType.Name}.{apiPropertyName}.");

        StringLengthAttribute apiAttribute = apiType.GetProperty(apiPropertyName)!.GetCustomAttribute<StringLengthAttribute>()!;
        Assert.Equal(entityAttribute.MaximumLength, apiAttribute.MaximumLength);
    }

    /// <summary>
    ///     Every write-reachable entity with its own <c>PrimaryCodeType</c>/<c>PrimaryCode</c> pair -
    ///     all confirmed <c>[StringLength(256)]</c> for both properties, on every entity, during
    ///     <c>write-body-string-validation</c> task 2.1. <see cref="TestComponentOfferingAssociationAttemptEntity" />
    ///     is the one write-reachable entity with no <c>PrimaryCode</c> pair at all (the spec defines
    ///     none for it) - deliberately not in this list.
    /// </summary>
    private static readonly Type[] PrimaryCodeEntities =
    [
        typeof(OrganisationEntity), typeof(PersonEntity), typeof(GroupEntity), typeof(MembershipEntity),
        typeof(CourseOfferingEntity), typeof(LearningComponentOfferingEntity), typeof(ProgrammeOfferingEntity),
        typeof(TestComponentOfferingEntity), typeof(CourseOfferingAssociationEntity),
        typeof(LearningComponentOfferingAssociationEntity), typeof(ProgrammeOfferingAssociationEntity),
        typeof(TestComponentOfferingAssociationEntity)
    ];

    public static TheoryData<Type, string, string> IdentifierEntryBackedTheoryData()
    {
        TheoryData<Type, string, string> data = [];
        foreach (Type entity in PrimaryCodeEntities)
        {
            data.Add(entity, "PrimaryCodeType", nameof(IdentifierEntry.CodeType));
            data.Add(entity, "PrimaryCode", nameof(IdentifierEntry.Code));
        }

        data.Add(typeof(OtherCodeEntity), nameof(OtherCodeEntity.CodeType), nameof(IdentifierEntry.CodeType));
        data.Add(typeof(OtherCodeEntity), nameof(OtherCodeEntity.Code), nameof(IdentifierEntry.Code));
        return data;
    }

    [Theory]
    [MemberData(nameof(IdentifierEntryBackedTheoryData))]
    public void EntityPrimaryOrOtherCode_MatchesIdentifierEntry(
        Type entityType, string entityPropertyName, string identifierEntryPropertyName)
    {
        StringLengthAttribute entityAttribute = entityType.GetProperty(entityPropertyName)!
            .GetCustomAttribute<StringLengthAttribute>()!;
        StringLengthAttribute identifierEntryAttribute = typeof(IdentifierEntry).GetProperty(identifierEntryPropertyName)!
            .GetCustomAttribute<StringLengthAttribute>()!;

        Assert.Equal(entityAttribute.MaximumLength, identifierEntryAttribute.MaximumLength);
    }

    /// <summary>
    ///     <see cref="AssociationPatchRequest.RemoteState" /> is the literal same property bound by all
    ///     4 offering-association <c>PATCH</c> endpoints (<c>write-body-string-validation</c> task 5.4)
    ///     - checked against each of the 4 entities' own <c>RemoteState</c> separately, since it doesn't
    ///     pair with a single entity the way every other class in <see cref="ApiToEntity" /> does.
    /// </summary>
    public static TheoryData<Type> AssociationPatchRequestRemoteStateEntities()
    {
        TheoryData<Type> data =
        [
            typeof(CourseOfferingAssociationEntity), typeof(LearningComponentOfferingAssociationEntity),
            typeof(ProgrammeOfferingAssociationEntity), typeof(TestComponentOfferingAssociationEntity)
        ];
        return data;
    }

    [Theory]
    [MemberData(nameof(AssociationPatchRequestRemoteStateEntities))]
    public void AssociationPatchRequestRemoteState_MatchesEntity(Type entityType)
    {
        StringLengthAttribute apiAttribute = typeof(AssociationPatchRequest)
            .GetProperty(nameof(AssociationPatchRequest.RemoteState))!.GetCustomAttribute<StringLengthAttribute>()!;
        StringLengthAttribute entityAttribute = entityType.GetProperty(nameof(CourseOfferingAssociationEntity.RemoteState))!
            .GetCustomAttribute<StringLengthAttribute>()!;

        Assert.Equal(entityAttribute.MaximumLength, apiAttribute.MaximumLength);
    }
}
