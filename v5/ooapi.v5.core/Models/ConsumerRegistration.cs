using System.Diagnostics.CodeAnalysis;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class ConsumerRegistration
{
    public string ConsumerKey { get; set; } = default!;

    public string Description { get; set; } = default!;
}