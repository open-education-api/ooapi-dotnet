namespace OEAPI.Infrastructure.Query.Fields;

/// <summary>
///     A parsed node of the spec's <c>fields</c> nested-parens selection syntax (e.g.
///     <c>(id,title,programme(code),campus(city))</c>). Each key is a requested field name; a field
///     with its own nested selection (<see cref="HasChildren" />) means only those sub-fields of that
///     nested object should be kept, not the whole thing.
/// </summary>
public sealed class FieldSelection
{
    /// <summary>Gets the requested child fields, keyed by field name (case-insensitive).</summary>
    public Dictionary<string, FieldSelection> Children { get; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>Gets a value indicating whether this node has any requested child fields.</summary>
    public bool HasChildren => Children.Count > 0;
}
