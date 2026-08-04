namespace OEAPI.Core.Models.ApiModels.Validation;

/// <summary>
///     The OOAPI spec's own regex `pattern` for every schema currently validated by
///     <see cref="RegexPatternAttribute" />, keyed by the spec's own schema name (e.g.
///     <c>"language"</c>) so a usage like <c>[RegexPattern("language")]</c> is traceable straight back
///     to the spec. Centralised here - rather than repeating the same pattern literal inline at every
///     usage - because several of these schemas back the same-shaped property on multiple, otherwise-
///     unrelated model classes (e.g. <c>language</c> backs both <c>LanguageTypedString.Language</c>
///     and several standalone <c>string[]</c> fields like <c>teachingLanguages</c>); a single shared
///     source means the spec's pattern only ever needs updating in one place. Mirrors
///     <see cref="ExtensibleEnumValues" />'s structure exactly.
/// </summary>
public static class RegexPatterns
{
    /// <summary>
    ///     Every registered schema's pattern, by spec schema name.
    /// </summary>
    public static readonly IReadOnlyDictionary<string, string> Values = new Dictionary<string, string>
    {
        // Deliberately not the spec's own literal pattern - source/schemas/Language.yaml in the
        // specification repository declares a pattern that's internally inconsistent with its own
        // description (which gives "zh-Hant-TW"/"nl-sgn-NL"/"nl-s-NL" as valid examples the pattern
        // rejects) and cites the wrong RFC ("4647", which defines language-*range* matching, e.g.
        // HTTP Accept-Language, not tag syntax). Filed upstream as
        // open-education-api/specification#698. Built directly from RFC 5646 §2.1's ABNF grammar for
        // `Language-Tag` instead (wrong subtag order, wrong script case, and mismatched variant/
        // extension groups in the spec's own pattern). Validates that a tag is *well-formed* (matches
        // the grammar), not *valid* (every subtag IANA-registered, which needs a registry lookup a
        // regex can't do) - the same limitation the spec's own pattern already had. Grandfathered tags
        // (a small, fixed, mostly-deprecated list like "i-klingon") aren't covered - enumerable but
        // not expressible as a general pattern.
        ["language"] =
            "^(?:(?:[a-zA-Z]{2,3}(?:-[a-zA-Z]{3}){0,2}|[a-zA-Z]{4}|[a-zA-Z]{5,8})(?:-[a-zA-Z]{4})?" +
            "(?:-(?:[a-zA-Z]{2}|[0-9]{3}))?(?:-(?:[a-zA-Z0-9]{5,8}|[0-9][a-zA-Z0-9]{3}))*" +
            "(?:-[0-9A-WY-Za-wy-z](?:-[a-zA-Z0-9]{2,8})+)*(?:-x(?:-[a-zA-Z0-9]{1,8})+)?|" +
            "x(?:-[a-zA-Z0-9]{1,8})+)$",

        // source/schemas/Cost.yaml's `amount`/`vatAmount`/`amountWithoutVat` all share this exact
        // pattern literal - internally consistent, no defects to correct (unlike `language` above).
        ["amount"] = @"^\d+(?:\.\d+)?$"
    };
}
