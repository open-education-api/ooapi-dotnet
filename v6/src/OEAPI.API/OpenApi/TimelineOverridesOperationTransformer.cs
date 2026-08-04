using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace OEAPI.API.OpenApi;

/// <summary>
///     Documents the <c>returnTimelineOverrides</c> query parameter on <c>GET /courses/{courseId}</c>
///     and <c>GET /programmes/{programmeId}</c> - the only two endpoints the spec declares it on. It
///     can never be picked up by reflection-based OpenAPI generation like an action's other parameters:
///     it's deliberately read via <c>BaseApiController.GetQueryValue(string)</c> rather than a formal
///     method parameter, since a formal parameter would have to be added to every one of the 19
///     controllers overriding <c>GetById</c> and would incorrectly advertise the parameter on
///     resources that don't support it.
/// </summary>
public sealed class TimelineOverridesOperationTransformer : IOpenApiOperationTransformer
{
    private static readonly HashSet<string> ApplicableControllers = ["Courses", "Programmes"];

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        if (context.Description.ActionDescriptor is not ControllerActionDescriptor
                { ActionName: "GetById" } descriptor ||
            !ApplicableControllers.Contains(descriptor.ControllerName))
            return Task.CompletedTask;

        operation.Parameters ??= [];
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "returnTimelineOverrides",
            In = ParameterLocation.Query,
            Required = false,
            Description =
                "When true, includes the resource's historical/future timelineOverrides entries in the response. Defaults to false.",
            Schema = new OpenApiSchema { Type = JsonSchemaType.Boolean, Default = JsonValue.Create(false) }
        });

        return Task.CompletedTask;
    }
}
