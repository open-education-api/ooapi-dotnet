using System.Text.Json;

namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Resolves an entity's stored <c>consumer</c> blob (<c>ConsumerJson</c>) into the value that
///     should actually be returned to the client, per the spec's rule that consumer-specific data is
///     only ever surfaced when the request explicitly asks for it via <c>?consumer=&lt;key&gt;</c>,
///     and only when the stored blob's own <c>consumerKey</c> matches that request. Otherwise the
///     <c>consumer</c> property is omitted (returned as <see langword="null" />) - it is never
///     returned unconditionally, unlike the previous behaviour across every controller in this
///     codebase.
/// </summary>
public static class ConsumerDataResolver
{
    /// <summary>
    ///     Resolves <paramref name="consumerJson" /> for <paramref name="requestedConsumerKey" />.
    /// </summary>
    /// <param name="consumerJson">The entity's stored <c>consumer</c> JSON blob, if any.</param>
    /// <param name="requestedConsumerKey">The <c>?consumer=</c> query value, if any.</param>
    /// <param name="typedRegistry">
    ///     An optional per-entity-shape registry mapping a known <c>consumerKey</c> (e.g. <c>rio</c>)
    ///     to a typed deserializer, so registered consumers with a known schema (see
    ///     <c>source/consumers/</c> in the spec repo) come back as a real typed object rather than a
    ///     generic <see cref="object" />. Consumers not present in the registry - including any
    ///     unregistered <c>x-</c>-prefixed consumer, per the spec - still round-trip correctly via a
    ///     generic passthrough deserialize, so nothing needs to be registered here to be usable.
    /// </param>
    /// <returns>
    ///     The resolved consumer data, or <see langword="null" /> if no consumer was requested, no
    ///     data is stored, the stored <c>consumerKey</c> doesn't match what was requested, or the
    ///     stored blob is malformed.
    /// </returns>
    public static object? Resolve(
        string? consumerJson,
        string? requestedConsumerKey,
        IReadOnlyDictionary<string, Func<JsonElement, object>>? typedRegistry = null)
    {
        if (string.IsNullOrEmpty(requestedConsumerKey) || string.IsNullOrEmpty(consumerJson)) return null;

        JsonElement root;
        try
        {
            using JsonDocument document = JsonDocument.Parse(consumerJson);
            root = document.RootElement.Clone();
        }
        catch (JsonException)
        {
            return null;
        }

        if (root.ValueKind != JsonValueKind.Object ||
            !root.TryGetProperty("consumerKey", out JsonElement keyElement) ||
            keyElement.ValueKind != JsonValueKind.String)
            return null;

        string? storedKey = keyElement.GetString();
        if (!string.Equals(storedKey, requestedConsumerKey, StringComparison.OrdinalIgnoreCase)) return null;

        if (typedRegistry != null &&
            typedRegistry.TryGetValue(requestedConsumerKey, out Func<JsonElement, object>? deserialize))
            try
            {
                return deserialize(root);
            }
            catch (JsonException)
            {
                // Stored data for a known consumer doesn't match its own schema - fall back to a
                // generic passthrough below rather than hiding the data or erroring the request.
            }

        return JsonSerializer.Deserialize<object>(root.GetRawText());
    }
}
