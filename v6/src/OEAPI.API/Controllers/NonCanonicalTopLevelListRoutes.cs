namespace OEAPI.API.Controllers;

/// <summary>
///     The 8 top-level "list all" <c>GET</c> endpoints with no canonical-spec counterpart - the spec
///     only ever exposes these 8 resource types nested under a parent, or by single-item <c>GET</c>,
///     never as a flat top-level list. Each is a free side effect of its controller sharing
///     <see cref="GenericEntityController{TEntity,TApiModel}" /> with every canonically top-level
///     resource (<c>Organisations</c>, <c>Persons</c>, <c>Groups</c>, etc.), whose own top-level
///     <c>GET</c> *is* spec-required and must always stay on regardless of this list.
/// </summary>
/// <remarks>
///     Single source of truth for both the runtime <c>404</c> in
///     <see cref="GenericEntityController{TEntity,TApiModel}.GetAll" /> and
///     <see cref="OEAPI.API.OpenApi.NonCanonicalListEndpointDocumentTransformer" />'s OpenAPI-document
///     pruning, keyed by each controller's own <c>RouteName</c> (the constructor argument every
///     controller already passes to the generic base, e.g. <c>"course-offerings"</c>). See
///     <c>docs/archive/DECISIONS-AND-ACTIONS.md</c> for the full rationale and
///     <see cref="OEAPI.API.Configuration.ServiceConfiguration.ExposeNonCanonicalListEndpoints" /> for the
///     per-deployment toggle.
/// </remarks>
public static class NonCanonicalTopLevelListRoutes
{
    public static readonly IReadOnlySet<string> Names = new HashSet<string>(StringComparer.Ordinal)
    {
        "course-offerings",
        "course-offering-associations",
        "learning-component-offerings",
        "learning-component-offering-associations",
        "programme-offerings",
        "programme-offering-associations",
        "test-component-offerings",
        "test-component-offering-associations"
    };
}
