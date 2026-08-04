namespace OEAPI.Core.Exceptions;

/// <summary>
///     Exception thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : OEAPIException
{
    /// <summary>
    ///     Initializes a new instance of the NotFoundException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorCode">The error code.</param>
    public NotFoundException(string message, string? errorCode = null)
        : base(message, 404, errorCode)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the NotFoundException class with an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="errorCode">The error code.</param>
    public NotFoundException(string message, Exception innerException, string? errorCode = null)
        : base(message, innerException, 404, errorCode)
    {
    }

    /// <summary>
    ///     Initializes a new instance for a specific entity type and identifier.
    /// </summary>
    /// <param name="entityType">The type of the entity.</param>
    /// <param name="entityId">The identifier of the entity.</param>
    /// <param name="errorCode">The error code. Default is "NOT_FOUND".</param>
    public NotFoundException(string entityType, string entityId, string errorCode = "NOT_FOUND")
        : base($"{entityType} with ID '{entityId}' not found.", 404, errorCode)
    {
        EntityType = entityType;
        EntityId = entityId;
    }

    /// <summary>
    ///     Gets the type of the entity that was not found.
    /// </summary>
    public string? EntityType { get; }

    /// <summary>
    ///     Gets the identifier of the entity that was not found.
    /// </summary>
    public string? EntityId { get; }
}
