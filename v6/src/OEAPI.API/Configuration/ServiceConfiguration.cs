using OEAPI.Core.Models.ApiModels;

namespace OEAPI.API.Configuration;

/// <summary>
///     Deployment-level metadata for the spec's <c>GET /</c> Service resource - contact details, links
///     to the spec/documentation this deployment implements, and which consumers/expands it supports.
///     Configurable rather than database-backed: <c>GET /</c> is the spec's only operation for this
///     resource (no <c>POST</c>/<c>PUT</c>), so it's inherently singleton, read-only metadata about this
///     deployment, not a queryable/writable data resource - the same category as
///     <see cref="DatabaseConfiguration" /> and <see cref="AuthenticationConfiguration" />.
///     <see cref="Service.SupportedOperations" /> is deliberately not part of this configuration - see
///     <c>ServiceController</c>, which computes it live from the application's actual route table
///     instead, so it can never drift from what the API really serves.
/// </summary>
public class ServiceConfiguration
{
    /// <summary>Contact e-mail address of the service owner.</summary>
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>URL of the API specification this deployment implements.</summary>
    public string Specification { get; set; } = string.Empty;

    /// <summary>URL of the API documentation, including general terms and privacy statement.</summary>
    public string? Documentation { get; set; }

    /// <summary>Consumers (destinations) this deployment has consumer-specific data for.</summary>
    public SupportedConsumer[] SupportedConsumers { get; set; } = [];

    /// <summary>
    ///     OEAPI versions (<c>MAJOR.MINOR</c>, e.g. <c>"6.0"</c>) this deployment can serve, for
    ///     <c>Accept</c>-header version negotiation - see <c>VersionNegotiationMiddleware</c>. Not
    ///     derived from <see cref="Specification" /> (a URL, not a reliably parseable version number).
    /// </summary>
    public string[] SupportedOeapiVersions { get; set; } = [];

    /// <summary>Paths and the relationship fields each one supports <c>expand</c> for.</summary>
    public SupportedExpand[] SupportedExpands { get; set; } = [];

    /// <summary>
    ///     Whether the 8 top-level "list all" `GET` endpoints with no canonical-spec counterpart
    ///     (<c>/course-offerings</c>, <c>/course-offering-associations</c>, and their 3 sibling pairs -
    ///     see <see cref="OEAPI.API.Controllers.NonCanonicalTopLevelListRoutes" />) are exposed. A free
    ///     side effect of these resources' controllers sharing the same base class as every canonically
    ///     top-level resource, not spec-required - defaults to <c>false</c> so a deployment doesn't
    ///     advertise non-spec surface area unless it deliberately opts in. Read directly via
    ///     <c>HttpContext.RequestServices</c> in <c>GenericEntityController.GetAll</c> rather than
    ///     constructor-injected, so this toggle doesn't require touching every resource controller's
    ///     constructor.
    /// </summary>
    public bool ExposeNonCanonicalListEndpoints { get; set; }

    /// <summary>
    ///     Free-form extensions for the Service resource, as a JSON string - matches every other
    ///     free-form <c>Ext</c> field's existing <c>*Json</c>-string-plus-deserialize-on-read
    ///     convention throughout this codebase (see e.g. <c>OrganisationEntity.ExtJson</c>), rather
    ///     than binding directly to <c>object</c>, which the configuration binder doesn't support for
    ///     arbitrary nested JSON.
    /// </summary>
    public string? ExtJson { get; set; }
}
