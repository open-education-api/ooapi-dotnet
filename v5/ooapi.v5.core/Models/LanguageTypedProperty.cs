using System.Diagnostics.CodeAnalysis;

namespace ooapi.v5.Models;

[ExcludeFromCodeCoverage(Justification = "Get/Set")]
public class LanguageTypedProperty
{

    public string PropertyName { get; set; } = default!;


    public string Language { get; set; } = default!;


    public string Value { get; set; } = default!;
}