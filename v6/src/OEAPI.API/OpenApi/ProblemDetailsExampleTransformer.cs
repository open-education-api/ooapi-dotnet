using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace OEAPI.API.OpenApi;

/// <summary>
///     Gives the shared <see cref="ProblemDetails" /> schema a realistic example, instead of Scalar's
///     default of showing every field as literal <c>null</c> (the type has no non-null default for any
///     property, so without an explicit example there's nothing for the docs UI to show). The schema is
///     shared across every error response in the document, so this example is necessarily generic rather
///     than tailored to any one endpoint - it's here purely to show the shape of a real response.
/// </summary>
public sealed class ProblemDetailsExampleTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type == typeof(ProblemDetails))
            schema.Example = new JsonObject
            {
                ["type"] = "https://tools.ietf.org/html/rfc9110#section-15.5.5",
                ["title"] = "Not Found",
                ["status"] = 404,
                ["detail"] = "The requested resource could not be found.",
                ["instance"] = "/organisations/018f1e2a-1234-7abc-9def-0123456789ab"
            };

        return Task.CompletedTask;
    }
}
