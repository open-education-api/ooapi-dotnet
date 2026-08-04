using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using OEAPI.API.Configuration;
using OEAPI.Core.Models.ApiModels;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for service metadata and API information.
/// </summary>
/// <remarks>
///     Initializes a new instance of the ServiceController class.
/// </remarks>
/// <param name="logger">The logger.</param>
/// <param name="endpointDataSource">
///     The application's real, live-registered endpoints - used to generate
///     <see cref="Service.SupportedOperations" /> directly from the actual route table instead of a
///     hand-maintained list, so it cannot silently drift from reality the way the previous
///     hardcoded version did (it still listed a removed <c>/addresses</c> resource and was missing
///     most real spec paths).
/// </param>
/// <param name="serviceConfiguration">
///     The rest of this deployment's Service metadata (contact/spec/documentation links, supported
///     consumers/expands) - see <see cref="ServiceConfiguration" /> for why this is configuration
///     rather than database-backed.
/// </param>
[ApiController]
[Route("")]
[Produces("application/json")]
[Tags("Service Metadata")]
public class ServiceController(
    ILogger<ServiceController> logger,
    EndpointDataSource endpointDataSource,
    ServiceConfiguration serviceConfiguration) : ControllerBase
{
    private readonly EndpointDataSource _endpointDataSource = endpointDataSource;
    private readonly ILogger<ServiceController> _logger = logger;
    private readonly ServiceConfiguration _serviceConfiguration = serviceConfiguration;

    /// <summary>
    ///     Retrieves metadata for the OEAPI service.
    /// </summary>
    /// <remarks>
    ///     This endpoint returns information about the service implementation,
    ///     including supported operations, endpoints, and consumer information.
    /// </remarks>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet]
    [ProducesResponseType(typeof(Service), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetServiceMetadata(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Service metadata requested");

            // Create and return service metadata
            Service service = CreateServiceMetadata();

            return Ok(service);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving service metadata");
            return Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error");
        }
    }

    /// <summary>
    ///     Creates the service metadata object.
    /// </summary>
    /// <returns>The service metadata.</returns>
    private Service CreateServiceMetadata()
    {
        return new Service
        {
            ContactEmail = _serviceConfiguration.ContactEmail,
            Specification = _serviceConfiguration.Specification,
            Documentation = _serviceConfiguration.Documentation,
            SupportedConsumers = _serviceConfiguration.SupportedConsumers,
            SupportedOperations = BuildSupportedOperations(),
            SupportedExpands = _serviceConfiguration.SupportedExpands,
            Ext = string.IsNullOrEmpty(_serviceConfiguration.ExtJson)
                ? null
                : JsonSerializer.Deserialize<object>(_serviceConfiguration.ExtJson)
        };
    }

    /// <summary>
    ///     Walks the application's actual, live-registered MVC controller-action endpoints and groups
    ///     them by path into <c>{verbs, path}</c> entries - generated, not hand-maintained, per the
    ///     reason this was rewritten (see the constructor's doc comment). Every real spec path this
    ///     implementation serves shows up here automatically, including nested/by-id routes and every
    ///     <c>PUT</c>/<c>POST</c>/<c>PATCH</c> operation, and a removed resource (like the old
    ///     <c>/addresses</c>) simply stops appearing on its own the moment its controller is deleted.
    /// </summary>
    private SupportedOperation[] BuildSupportedOperations()
    {
        // Keyed by the path's *shape* (segment count + which segments are parameters), not its raw
        // text - GenericEntityController's GET actions use the generic {id}, while a resource's own
        // hand-written PUT/PATCH actions typically name the same parameter after the resource (e.g.
        // {personId}). Grouping by raw text alone would list "/persons/{id}" (GET) and
        // "/persons/{personId}" (PUT) as two separate paths instead of one path with two verbs.
        Dictionary<string, (string DisplayPath, SortedSet<string> Verbs)> operationsByShape =
            new(StringComparer.OrdinalIgnoreCase);

        foreach (Endpoint endpoint in _endpointDataSource.Endpoints)
        {
            if (endpoint is not RouteEndpoint routeEndpoint) continue;

            // Only real MVC controller actions - excludes health checks and any other
            // non-controller endpoint that might be mapped in Program.cs.
            if (routeEndpoint.Metadata.GetMetadata<ControllerActionDescriptor>() == null) continue;

            IReadOnlyList<string>? httpMethods = routeEndpoint.Metadata.GetMetadata<HttpMethodMetadata>()?.HttpMethods;
            if (httpMethods == null || httpMethods.Count == 0) continue;

            string path = "/" + routeEndpoint.RoutePattern.RawText?.TrimStart('/');
            string shape = ToPathShape(path);

            if (!operationsByShape.TryGetValue(shape, out (string DisplayPath, SortedSet<string> Verbs) existing))
            {
                operationsByShape[shape] = (path, new SortedSet<string>(StringComparer.Ordinal));
            }
            else
            {
                // Prefer the semantically-named parameter (e.g. {personId}) over the generic {id}
                // for display, when both exist for the same path shape.
                bool preferNewPath = existing.DisplayPath.Contains("{id}", StringComparison.Ordinal)
                                     && !path.Contains("{id}", StringComparison.Ordinal);
                operationsByShape[shape] = (preferNewPath ? path : existing.DisplayPath, existing.Verbs);
            }

            foreach (string method in httpMethods) operationsByShape[shape].Verbs.Add(method);
        }

        return [.. operationsByShape.Values
            .OrderBy(entry => entry.DisplayPath, StringComparer.Ordinal)
            .Select(entry => new SupportedOperation { Verbs = [.. entry.Verbs], Path = entry.DisplayPath })];
    }

    /// <summary>
    ///     Reduces a route template to its shape - every <c>{param}</c> segment collapsed to a
    ///     single placeholder - so two templates that differ only in parameter *name* compare equal.
    /// </summary>
    private static string ToPathShape(string path)
    {
        string[] segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        IEnumerable<string> shaped =
            segments.Select(segment => segment.StartsWith('{') && segment.EndsWith('}') ? "{}" : segment);
        return "/" + string.Join('/', shaped);
    }
}
