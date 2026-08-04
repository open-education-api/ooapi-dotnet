using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OEAPI.Core.Models.ApiModels.Validation;

/// <summary>
///     Validates that a string property (or every item of a string-array property) matches a
///     spec-declared regex `pattern` (looked up from <see cref="RegexPatterns.Values" /> by schema
///     name, the same indirection <see cref="ExtensibleEnumAttribute" /> uses for known-value lists).
///     The built-in <see cref="RegularExpressionAttribute" /> only validates a single string - given a
///     <see cref="string" />[] value it falls back to <c>ToString()</c> on the array itself, which can
///     never match a real pattern - so any pattern-constrained field that's actually a string array
///     (e.g. <c>PersonProperties.LanguageOfChoice</c>, every offering's <c>teachingLanguages</c>) needs
///     this instead.
/// </summary>
/// <param name="schemaName">
///     The OOAPI spec's schema name for this field's pattern (e.g. <c>"language"</c>), as registered
///     in <see cref="RegexPatterns.Values" />.
/// </param>
[AttributeUsage(AttributeTargets.Property)]
public sealed class RegexPatternAttribute(string schemaName) : ValidationAttribute
{
    private readonly string _schemaName = schemaName;

    /// <summary>
    ///     The spec schema name this pattern was registered under in <see cref="RegexPatterns.Values" />
    ///     - exposed so consumers other than <see cref="IsValid" /> (e.g. an OpenAPI schema transformer)
    ///     can look up the same pattern without reaching into a private field.
    /// </summary>
    public string SchemaName => _schemaName;

    // Computed lazily, inside IsValid() - not eagerly in a field initializer - for the same reason
    // ExtensibleEnumAttribute.KnownValues is a lazily-evaluated property rather than a constructor-
    // time field: a custom attribute's constructor (including primary-constructor field
    // initializers) can run via reflection machinery (e.g. PropertyDescriptor.Attributes, which
    // Validator.TryValidateObject uses internally) that silently swallows a thrown exception instead
    // of propagating it - confirmed directly: an eager version of this same lookup never actually
    // threw during Validator.TryValidateObject, even though the unregistered schema name was real.
    private Regex Regex =>
        RegexPatterns.Values.TryGetValue(_schemaName, out string? pattern)
            ? new Regex(pattern, RegexOptions.Compiled)
            : throw new InvalidOperationException(
                $"No pattern registered for schema '{_schemaName}' in " +
                $"{nameof(RegexPatterns)}.{nameof(RegexPatterns.Values)}.");

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        Regex regex = Regex;

        List<string> invalidValues = value switch
        {
            null => [],
            string single => regex.IsMatch(single) ? [] : [single],
            IEnumerable<string> many => [.. many.Where(v => !regex.IsMatch(v))],
            _ => throw new InvalidOperationException(
                $"{nameof(RegexPatternAttribute)} only supports string or IEnumerable<string> properties; " +
                $"'{validationContext.MemberName}' is {value.GetType().Name}.")
        };

        if (invalidValues.Count == 0)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            $"'{string.Join("', '", invalidValues)}' does not match the required pattern for " +
            $"{validationContext.DisplayName}.",
            new[] { validationContext.MemberName ?? validationContext.DisplayName });
    }
}
