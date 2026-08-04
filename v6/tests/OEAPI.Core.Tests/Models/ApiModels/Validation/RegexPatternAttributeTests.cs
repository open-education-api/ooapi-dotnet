using System.ComponentModel.DataAnnotations;
using OEAPI.Core.Models.ApiModels.Validation;
using Xunit;

namespace OEAPI.Core.Tests.Models.ApiModels.Validation;

public class RegexPatternAttributeTests
{
    private static List<ValidationResult> Validate(object model)
    {
        List<ValidationResult> results = [];
        Validator.TryValidateObject(model, new ValidationContext(model), results, true);
        return results;
    }

    [Theory]
    [InlineData("en")]
    [InlineData("en-GB")]
    // The spec's own Language.yaml `description` explicitly gives these as valid examples, but its
    // literal `pattern` (wrong subtag order, wrong case on script, wrong variant/extension grammar -
    // see RegexPatterns.Values' "language" entry) rejects all three - filed upstream as
    // open-education-api/specification#698. RegexPatterns.Values uses a corrected pattern built
    // directly from RFC 5646 §2.1's ABNF instead of the spec's literal one, so these are accepted
    // here.
    [InlineData("zh-Hant-TW")]
    [InlineData("nl-sgn-NL")]
    [InlineData("nl-s-NL")]
    // A few more real BCP 47 shapes the corrected pattern needs to get right, not just the spec's own
    // three examples: extlang, script+region, a registered variant, and a private-use suffix.
    [InlineData("zh-yue-HK")]
    [InlineData("sr-Latn-RS")]
    [InlineData("de-CH-1996")]
    [InlineData("en-a-bbb-x-a-ccc")]
    [InlineData("x-acme-dept1")]
    // RFC 5646 tags are explicitly case-insensitive (§2.1.1) - canonical casing is a presentation
    // recommendation, not a validity requirement.
    [InlineData("EN")]
    [InlineData("en-gb")]
    public void MatchingValue_IsValid(string value)
    {
        SingleValueModel model = new() { Language = value };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void NullValue_IsValid()
    {
        SingleValueModel model = new() { Language = null };

        Assert.Empty(Validate(model));
    }

    [Theory]
    [InlineData("e")]
    [InlineData("en_GB")]
    [InlineData("en--GB")]
    [InlineData("-en")]
    public void NonMatchingValue_IsInvalid(string value)
    {
        SingleValueModel model = new() { Language = value };

        List<ValidationResult> results = Validate(model);

        ValidationResult result = Assert.Single(results);
        Assert.Contains(value, result.ErrorMessage);
        Assert.Contains(nameof(SingleValueModel.Language), result.MemberNames);
    }

    [Fact]
    public void WellFormedButUnregisteredSubtag_IsCurrentlyAccepted()
    {
        // Not a gap specific to this pattern: RFC 5646 itself distinguishes a tag being
        // *well-formed* (matches the ABNF grammar - all a regex can check) from *valid* (every
        // subtag is an actually-registered IANA code - needs a registry lookup). "english" is 7
        // letters, which fits the language production's "5*8ALPHA - reserved for future/registered
        // use" branch, so it's syntactically well-formed even though it isn't a real ISO 639 code.
        SingleValueModel model = new() { Language = "english" };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void Array_AllMatchingValues_IsValid()
    {
        ArrayValueModel model = new() { Languages = ["en", "nl-NL"] };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void Array_WithOneNonMatchingValue_NamesOnlyTheBadOne()
    {
        ArrayValueModel model = new() { Languages = ["en", "not_a_tag"] };

        List<ValidationResult> results = Validate(model);

        ValidationResult result = Assert.Single(results);
        Assert.Contains("not_a_tag", result.ErrorMessage);
    }

    [Fact]
    public void Array_Null_IsValid()
    {
        ArrayValueModel model = new() { Languages = null };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void UnregisteredSchemaName_ThrowsOnValidation()
    {
        UnregisteredSchemaModel model = new() { Value = "anything" };

        Assert.Throws<InvalidOperationException>(() => Validate(model));
    }

    // Deliberately points at the real, already-registered "language" schema (rather than an ad-hoc
    // test-only pattern) - doubles as a check that it's actually registered in RegexPatterns.Values.
    // ReSharper disable UnusedAutoPropertyAccessor.Local - read only via DataAnnotations reflection.
    private class SingleValueModel
    {
        [RegexPattern("language")]
        public string? Language { get; set; }
    }

    private class ArrayValueModel
    {
        [RegexPattern("language")]
        public string[]? Languages { get; set; }
    }

    private class UnregisteredSchemaModel
    {
        [RegexPattern("thisSchemaNameDoesNotExist")]
        public string? Value { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Local
}
