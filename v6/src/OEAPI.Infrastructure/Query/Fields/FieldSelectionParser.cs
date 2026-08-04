namespace OEAPI.Infrastructure.Query.Fields;

/// <summary>
///     Parses the spec's <c>fields</c> query parameter syntax: a comma-separated list of field names,
///     optionally wrapped in one pair of parens, where any field may itself carry a parenthesized
///     nested list (e.g. <c>(id,title,programme(code),campus(city))</c>). Malformed input is handled by
///     simply stopping the parse where it no longer makes sense, rather than throwing - <c>fields</c> is
///     a request hint per spec, not something that should ever 400 a request.
/// </summary>
public static class FieldSelectionParser
{
    /// <summary>
    ///     Parses a <c>fields</c> expression. Returns <see langword="null" /> if the expression is empty
    ///     or contains no recognizable field names (in which case callers should treat the request as
    ///     "no field selection requested").
    /// </summary>
    public static FieldSelection? Parse(string? expression)
    {
        if (string.IsNullOrWhiteSpace(expression)) return null;

        string text = expression.Trim();
        int pos = text.Length > 0 && text[0] == '(' ? 1 : 0;

        FieldSelection root = new();
        ParseFieldList(text, ref pos, root);
        return root.HasChildren ? root : null;
    }

    private static void ParseFieldList(string text, ref int pos, FieldSelection target)
    {
        while (pos < text.Length)
        {
            SkipWhitespace(text, ref pos);
            if (pos >= text.Length || text[pos] == ')')
            {
                if (pos < text.Length) pos++; // consume the closing paren
                return;
            }

            string name = ReadIdentifier(text, ref pos);
            if (name.Length == 0)
                // Unrecognized character where a field name was expected - stop parsing this group
                // rather than looping forever on malformed input.
                return;

            if (!target.Children.TryGetValue(name, out FieldSelection? child))
            {
                child = new FieldSelection();
                target.Children[name] = child;
            }

            SkipWhitespace(text, ref pos);
            if (pos < text.Length && text[pos] == '(')
            {
                pos++;
                ParseFieldList(text, ref pos, child);
            }

            SkipWhitespace(text, ref pos);
            if (pos < text.Length && text[pos] == ',') pos++;
        }
    }

    private static void SkipWhitespace(string text, ref int pos)
    {
        while (pos < text.Length && char.IsWhiteSpace(text[pos])) pos++;
    }

    private static string ReadIdentifier(string text, ref int pos)
    {
        int start = pos;
        while (pos < text.Length && text[pos] != ',' && text[pos] != '(' && text[pos] != ')' &&
               !char.IsWhiteSpace(text[pos])) pos++;

        return text[start..pos];
    }
}
