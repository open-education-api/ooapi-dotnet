using Microsoft.EntityFrameworkCore;
using OEAPI.Infrastructure.Data.Entities;

namespace OEAPI.Infrastructure.Query.Extensions;

/// <summary>
///     Shared <c>.Include()</c> chains for entities whose canonical mapping (<c>ToApiModel</c>) reads
///     self-referencing/related navigations that every caller needs eager-loaded the same way - used
///     by both the canonical controller's own <c>ApplyIncludes</c> and every ad-hoc <c>expand=</c>
///     lookup query elsewhere that needs the same navigation set, for
///     <see cref="OrganisationEntity" />/<see cref="AcademicSessionEntity" />/
///     <see cref="LearningOutcomeEntity" />.
/// </summary>
public static class EntityIncludeExtensions
{
    /// <summary>
    ///     Eager-loads <see cref="OrganisationEntity.Parent" />/<see cref="OrganisationEntity.Root" />/
    ///     <see cref="OrganisationEntity.Children" />, needed by
    ///     <c>OrganisationMappingExtensions.ToApiModel</c> to populate <c>ParentId</c>/<c>RootId</c>/
    ///     <c>ChildIds</c>.
    /// </summary>
    public static IQueryable<OrganisationEntity> IncludeHierarchy(this IQueryable<OrganisationEntity> query)
    {
        return query
            .Include(o => o.Parent)
            .Include(o => o.Root)
            .Include(o => o.Children);
    }

    /// <summary>
    ///     Eager-loads <see cref="AcademicSessionEntity.Parent" />/<see cref="AcademicSessionEntity.Children" />/
    ///     <see cref="AcademicSessionEntity.Year" />, needed by
    ///     <c>AcademicSessionMappingExtensions.ToApiModel</c> to populate <c>ParentId</c>/<c>ChildIds</c>/
    ///     <c>YearId</c>.
    /// </summary>
    public static IQueryable<AcademicSessionEntity> IncludeHierarchy(this IQueryable<AcademicSessionEntity> query)
    {
        return query
            .Include(a => a.Parent)
            .Include(a => a.Children)
            .Include(a => a.Year);
    }

    /// <summary>
    ///     Eager-loads <see cref="LearningOutcomeEntity.Organisation" />/<see cref="LearningOutcomeEntity.Parents" />/
    ///     <see cref="LearningOutcomeEntity.Children" />, needed by
    ///     <c>LearningOutcomeMappingExtensions.ToApiModel</c> to populate <c>OrganisationId</c>/
    ///     <c>ParentIds</c>/<c>ChildIds</c>.
    /// </summary>
    public static IQueryable<LearningOutcomeEntity> IncludeHierarchy(this IQueryable<LearningOutcomeEntity> query)
    {
        return query
            .Include(lo => lo.Organisation)
            .Include(lo => lo.Parents)
            .Include(lo => lo.Children);
    }
}
