using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;
using OEAPI.API.Configuration;

namespace OEAPI.API.OpenApi;

/// <summary>
///     Populates the self-generated OpenAPI document's <c>info</c>/<c>externalDocs</c> from the same
///     <see cref="ServiceConfiguration" /> that backs the spec's own <c>GET /</c> Service resource, so
///     the two can never drift apart on which OEAPI version this deployment implements or where the
///     public specification/documentation live. Without this, the document falls back to
///     <c>AddOpenApi()</c>'s defaults (title <c>"OEAPI.API | v1"</c>, version <c>"1.0.0"</c>, no
///     <c>externalDocs</c>) - "v1"/"1.0.0" there are just the framework's internal document-name
///     default, unrelated to the OEAPI spec version actually served.
/// </summary>
/// <param name="serviceConfig">Supplies the OEAPI version, contact e-mail, and documentation link.</param>
public sealed class ServiceInfoDocumentTransformer(ServiceConfiguration serviceConfig) : IOpenApiDocumentTransformer
{
    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        string oeapiVersion = serviceConfig.SupportedOeapiVersions.Length > 0
            ? serviceConfig.SupportedOeapiVersions[0]
            : "unknown";

        document.Info.Title = "OOAPI v6 .NET Reference Implementation";
        document.Info.Version = oeapiVersion;
        document.Info.Description =
            $"A .NET / ASP.NET Core implementation of the Open Education API (OOAPI) version {oeapiVersion} " +
            "specification.";

        if (!string.IsNullOrEmpty(serviceConfig.ContactEmail))
            document.Info.Contact = new OpenApiContact { Email = serviceConfig.ContactEmail };

        // The spec itself (not this implementation's own source, which carries no separate licence)
        // is EUPL-1.2 - see LICENSE.md in the specification repository, on the release branch
        // matching the version this deployment serves.
        document.Info.License = new OpenApiLicense
        {
            Name = "EUPL-1.2",
            Url = new Uri(
                $"https://github.com/open-education-api/specification/blob/release/{oeapiVersion}/LICENSE.md")
        };

        if (!string.IsNullOrEmpty(serviceConfig.Documentation))
            document.ExternalDocs = new OpenApiExternalDocs
            {
                Description = "OEAPI specification and documentation", Url = new Uri(serviceConfig.Documentation)
            };

        return Task.CompletedTask;
    }
}
