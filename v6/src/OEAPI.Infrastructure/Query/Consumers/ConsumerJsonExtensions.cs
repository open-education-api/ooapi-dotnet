namespace OEAPI.Infrastructure.Query.Consumers;

/// <summary>
///     Entry point mapping methods should call instead of unconditionally deserializing
///     <c>ConsumerJson</c> - see <see cref="ConsumerDataResolver" /> for the gating rule these all
///     share (only returned when explicitly requested and matching).
/// </summary>
public static class ConsumerJsonExtensions
{
    public static object? ToCourseConsumer(this string? consumerJson, string? requestedConsumerKey)
    {
        return ConsumerDataResolver.Resolve(consumerJson, requestedConsumerKey, CourseConsumerRegistry.Deserializers);
    }

    public static object? ToProgrammeConsumer(this string? consumerJson, string? requestedConsumerKey)
    {
        return ConsumerDataResolver.Resolve(consumerJson, requestedConsumerKey,
            ProgrammeConsumerRegistry.Deserializers);
    }

    public static object? ToOfferingConsumer(this string? consumerJson, string? requestedConsumerKey)
    {
        return ConsumerDataResolver.Resolve(consumerJson, requestedConsumerKey, OfferingConsumerRegistry.Deserializers);
    }

    public static object? ToPersonConsumer(this string? consumerJson, string? requestedConsumerKey)
    {
        return ConsumerDataResolver.Resolve(consumerJson, requestedConsumerKey, PersonConsumerRegistry.Deserializers);
    }

    /// <summary>
    ///     For every entity shape with no registered typed consumer schema - still gates on the
    ///     requested/stored <c>consumerKey</c> matching, just without a typed DTO for the result.
    /// </summary>
    public static object? ToGenericConsumer(this string? consumerJson, string? requestedConsumerKey)
    {
        return ConsumerDataResolver.Resolve(consumerJson, requestedConsumerKey);
    }
}
