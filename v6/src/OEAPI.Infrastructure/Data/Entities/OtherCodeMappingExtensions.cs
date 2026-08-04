using OEAPI.Core.Models.ApiModels;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     Converts between the owned <see cref="OtherCodeEntity" /> collection and the API's
///     <see cref="IdentifierEntry" /> array shape, so controllers don't each hand-roll the same
///     collection-to-array/array-to-collection mapping.
/// </summary>
public static class OtherCodeMappingExtensions
{
    /// <summary>
    ///     Maps an owned other-codes collection to the API's <c>otherCodes</c> array shape. Returns
    ///     <see langword="null" /> for an empty collection, matching how every controller already treats
    ///     an absent <c>otherCodes</c> field.
    /// </summary>
    public static IdentifierEntry[]? ToApiModel(this ICollection<OtherCodeEntity> otherCodes)
    {
        return otherCodes.Count > 0
            ? [.. otherCodes.Select(oc => new IdentifierEntry { CodeType = oc.CodeType, Code = oc.Code })]
            : null;
    }

    /// <summary>
    ///     Replaces the contents of an owned other-codes collection from the API's <c>otherCodes</c>
    ///     array. A <see langword="null" /> <paramref name="model" /> leaves the collection untouched
    ///     (matching the "only overwrite fields present in the request" semantics every write path in
    ///     this codebase already uses for JSON-backed fields).
    /// </summary>
    public static void SyncFrom(this ICollection<OtherCodeEntity> otherCodes, IdentifierEntry[]? model)
    {
        if (model == null) return;

        otherCodes.Clear();
        foreach (IdentifierEntry entry in model)
            otherCodes.Add(new OtherCodeEntity { CodeType = entry.CodeType, Code = entry.Code });
    }
}
