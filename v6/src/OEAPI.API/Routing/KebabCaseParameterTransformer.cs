using System.Text.RegularExpressions;

namespace OEAPI.API.Routing;

/// <summary>
///     Transforms the <c>[controller]</c>/<c>[action]</c> route tokens from PascalCase to kebab-case
///     (e.g. <c>CourseOfferingAssociations</c> -&gt; <c>course-offering-associations</c>), so controller
///     route attributes can stay <c>[Route("[controller]")]</c> instead of a hand-written literal per
///     controller matching the spec's kebab-case paths. Registered globally via
///     <see cref="Microsoft.AspNetCore.Mvc.ApplicationModels.RouteTokenTransformerConvention" /> in
///     <c>Program.cs</c> - does not affect any hand-written literal route segments (e.g. nested endpoint
///     paths like <c>{groupId}/memberships</c>), only the <c>[controller]</c>/<c>[action]</c> tokens
///     themselves.
/// </summary>
public class KebabCaseParameterTransformer : IOutboundParameterTransformer
{
    /// <inheritdoc />
    public string? TransformOutbound(object? value)
    {
        if (value is null) return null;

        return Regex.Replace(value.ToString() ?? string.Empty, "([a-z])([A-Z])", "$1-$2").ToLowerInvariant();
    }
}
