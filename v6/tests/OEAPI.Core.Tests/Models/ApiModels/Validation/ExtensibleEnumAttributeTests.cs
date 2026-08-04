using System.ComponentModel.DataAnnotations;
using OEAPI.Core.Models.ApiModels;
using OEAPI.Core.Models.ApiModels.Validation;
using Xunit;

namespace OEAPI.Core.Tests.Models.ApiModels.Validation;

public class ExtensibleEnumAttributeTests
{
    private static List<ValidationResult> Validate(object model)
    {
        List<ValidationResult> results = [];
        Validator.TryValidateObject(model, new ValidationContext(model), results, true);
        return results;
    }

    [Fact]
    public void KnownValue_IsValid()
    {
        SingleValueModel model = new() { Mode = "full_time" };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void KnownValue_IsCaseInsensitive()
    {
        SingleValueModel model = new() { Mode = "FULL_TIME" };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void CustomXPrefixedValue_IsValid()
    {
        SingleValueModel model = new() { Mode = "x-my-institution-mode" };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void NullValue_IsValid()
    {
        SingleValueModel model = new() { Mode = null };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void UnknownValue_IsInvalid()
    {
        SingleValueModel model = new() { Mode = "bogus" };

        List<ValidationResult> results = Validate(model);

        ValidationResult result = Assert.Single(results);
        Assert.Contains("bogus", result.ErrorMessage);
        Assert.Contains(nameof(SingleValueModel.Mode), result.MemberNames);
    }

    [Fact]
    public void Array_AllKnownValues_IsValid()
    {
        ArrayValueModel model = new() { Modes = ["blended", "online"] };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void Array_WithCustomXPrefixedValue_IsValid()
    {
        ArrayValueModel model = new() { Modes = ["blended", "x-custom"] };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void Array_WithOneUnknownValue_NamesOnlyTheBadOne()
    {
        ArrayValueModel model = new() { Modes = ["blended", "bogus"] };

        List<ValidationResult> results = Validate(model);

        ValidationResult result = Assert.Single(results);
        Assert.StartsWith("'bogus' is not a recognised value", result.ErrorMessage);
    }

    [Fact]
    public void Array_Null_IsValid()
    {
        ArrayValueModel model = new() { Modes = null };

        Assert.Empty(Validate(model));
    }

    [Fact]
    public void UnregisteredSchemaName_ThrowsOnValidation()
    {
        UnregisteredSchemaModel model = new() { Value = "anything" };

        Assert.Throws<InvalidOperationException>(() => Validate(model));
    }

    // The following cover the real SupplementaryInformation class directly (not a stub) - this
    // both proves the [ExtensibleEnum] annotations added to its Role/Type properties by the
    // fix-spec-conformance-tier1-additive-gaps change are actually wired up, and that the new
    // "supplementaryRole"/"supplementaryType" ExtensibleEnumValues dictionary entries have the right
    // values (announcement/badge/marketing/promo, image/text_http/text_md/text_plain/uri/video).
    [Fact]
    public void SupplementaryInformation_KnownRoleAndType_IsValid()
    {
        SupplementaryInformation item = new() { Role = "badge", Type = "image" };

        Assert.Empty(Validate(item));
    }

    [Fact]
    public void SupplementaryInformation_UnknownRole_IsInvalid()
    {
        SupplementaryInformation item = new() { Role = "bogus", Type = "image" };

        List<ValidationResult> results = Validate(item);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(SupplementaryInformation.Role)));
    }

    [Fact]
    public void SupplementaryInformation_UnknownType_IsInvalid()
    {
        SupplementaryInformation item = new() { Role = "badge", Type = "bogus" };

        List<ValidationResult> results = Validate(item);

        Assert.Contains(results, r => r.MemberNames.Contains(nameof(SupplementaryInformation.Type)));
    }

    [Fact]
    public void SupplementaryInformation_XPrefixedCustomRoleAndType_IsValid()
    {
        SupplementaryInformation item = new() { Role = "x-institution-role", Type = "x-institution-type" };

        Assert.Empty(Validate(item));
    }

    // Deliberately point at real, already-registered schema names (rather than an ad-hoc test-only
    // list) - ExtensibleEnumAttribute only accepts a schema name registered in
    // ExtensibleEnumValues.Values, so there's nothing else valid to point these at, and it doubles as
    // a check that "modeOfStudy"/"modeOfDelivery" are actually registered with the values below.
    // ReSharper disable UnusedAutoPropertyAccessor.Local - read only via DataAnnotations reflection.
    private class SingleValueModel
    {
        [ExtensibleEnum("modeOfStudy")]
        public string? Mode { get; set; }
    }

    private class ArrayValueModel
    {
        [ExtensibleEnum("modeOfDelivery")]
        public string[]? Modes { get; set; }
    }

    private class UnregisteredSchemaModel
    {
        [ExtensibleEnum("thisSchemaNameDoesNotExist")]
        public string? Value { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Local
}
