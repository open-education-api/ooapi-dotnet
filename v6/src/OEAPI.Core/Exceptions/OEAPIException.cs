namespace OEAPI.Core.Exceptions;

/// <summary>
///     Base exception for OEAPI-related errors.
/// </summary>
public class OEAPIException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the OEAPIException class.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="statusCode">The HTTP status code. Default is 400.</param>
    /// <param name="errorCode">The error code.</param>
    public OEAPIException(string message, int statusCode = 400, string? errorCode = null)
        : base(message)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    /// <summary>
    ///     Initializes a new instance of the OEAPIException class with an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    /// <param name="statusCode">The HTTP status code. Default is 400.</param>
    /// <param name="errorCode">The error code.</param>
    public OEAPIException(string message, Exception innerException, int statusCode = 400, string? errorCode = null)
        : base(message, innerException)
    {
        StatusCode = statusCode;
        ErrorCode = errorCode;
    }

    /// <summary>
    ///     Gets the HTTP status code associated with this exception.
    /// </summary>
    public int StatusCode { get; }

    /// <summary>
    ///     Gets the error code associated with this exception.
    /// </summary>
    public string? ErrorCode { get; }
}
