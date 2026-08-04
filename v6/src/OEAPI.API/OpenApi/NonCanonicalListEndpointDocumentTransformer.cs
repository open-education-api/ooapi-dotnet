using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using OEAPI.API.Configuration;
using OEAPI.API.Controllers;

namespace OEAPI.API.OpenApi;

/// <summary>
///     Removes the 8 non-canonical top-level list endpoints
///     (<see cref="NonCanonicalTopLevelListRoutes" />) from the self-generated OpenAPI document
///     whenever <see cref="ServiceConfiguration.ExposeNonCanonicalListEndpoints" /> is off - an
///     endpoint that always <c>404</c>s has no business being advertised as documented API surface.
///     See <c>GenericEntityController.GetAll</c> for the matching runtime <c>404</c>.
/// </summary>
/// <param name="serviceConfig">Supplies the current deployment's toggle value.</param>
public sealed class NonCanonicalListEndpointDocumentTransformer(ServiceConfiguration serviceConfig)
    : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (serviceConfig.ExposeNonCanonicalListEndpoints)
            return Task.CompletedTask;

        foreach (string routeName in NonCanonicalTopLevelListRoutes.Names)
            document.Paths.Remove($"/{routeName}");

        return Task.CompletedTask;
    }
}
