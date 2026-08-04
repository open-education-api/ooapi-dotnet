using System.ComponentModel.DataAnnotations;

namespace OEAPI.Core.Models.ApiModels.Validation;

/// <summary>
///     Validates that a string property (or every item of a string-array property) matches one of a
///     fixed set of known values, or carries the OOAPI spec's <c>x-</c> prefix for institution-defined
///     custom extensions - the actual contract behind the spec's "extensible enumeration" fields (see
///     e.g. <c>x-ooapi-extensible-enum</c> in the spec schema). Applied to plain
///     <see cref="string" />/<see cref="string" />[] properties rather than a C# <see langword="enum" />:
///     a real enum with <c>JsonStringEnumConverter</c> can only ever deserialize its fixed member set,
///     rejecting a genuinely spec-valid <c>x-</c> custom value outright - so an actually extensible
///     field has to stay string-typed, with this validation layered on top to enforce "one of these
///     values, or anything x-prefixed" instead of "any string at all." See the "Extensible enums"
///     section of <c>docs/archive/DECISIONS-AND-ACTIONS.md</c> for the full rationale and how to add a
///     new one.
/// </summary>
/// <param name="schemaName">
///     The OOAPI spec's schema name for this field's extensible enumeration (e.g.
///     <c>"offeringState"</c>), as registered in <see cref="ExtensibleEnumValues.Values" />. A
///     single schema is deliberately allowed to back a same-shaped property on more than one model
///     class (e.g. <c>offeringState</c> backs every offering type's <c>State</c>) - that's the
///     point of the indirection, so the value list only has to be updated in one place.
/// </param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ExtensibleEnumAttribute(string schemaName) : ValidationAttribute
{
    private const string CustomValuePrefix = "x-";
    private readonly string _schemaName = schemaName;

    /// <summary>
    ///     The spec schema name this value list was registered under in
    ///     <see cref="ExtensibleEnumValues.Values" /> - exposed so consumers other than
    ///     <see cref="IsValid" /> (e.g. an OpenAPI schema transformer) can look up the same known-value
    ///     list without reaching into a private field.
    /// </summary>
    public string SchemaName => _schemaName;

    private string[] KnownValues =>
        ExtensibleEnumValues.Values.TryGetValue(_schemaName, out string[]? values)
            ? values
            : throw new InvalidOperationException(
                $"No known-value list registered for extensible-enum schema '{_schemaName}' in " +
                $"{nameof(ExtensibleEnumValues)}.{nameof(ExtensibleEnumValues.Values)}.");

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        string[] knownValues = KnownValues;

        List<string> invalidValues = value switch
        {
            null => [],
            string single => IsKnownOrCustom(single, knownValues) ? [] : [single],
            IEnumerable<string> many => [.. many.Where(v => !IsKnownOrCustom(v, knownValues))],
            _ => throw new InvalidOperationException(
                $"{nameof(ExtensibleEnumAttribute)} only supports string or IEnumerable<string> properties; " +
                $"'{validationContext.MemberName}' is {value.GetType().Name}.")
        };

        if (invalidValues.Count == 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            $"'{string.Join("', '", invalidValues)}' is not a recognised value for " +
            $"{validationContext.DisplayName}. Expected one of: {string.Join(", ", knownValues)}, or a " +
            $"custom value prefixed with '{CustomValuePrefix}'.",
            new[] { validationContext.MemberName ?? validationContext.DisplayName });
    }

    private static bool IsKnownOrCustom(string value, string[] knownValues)
    {
        return value.StartsWith(CustomValuePrefix, StringComparison.OrdinalIgnoreCase)
               || knownValues.Contains(value, StringComparer.OrdinalIgnoreCase);
    }
}
