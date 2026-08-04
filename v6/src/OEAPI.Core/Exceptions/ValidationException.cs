namespace OEAPI.Core.Exceptions;

/// <summary>
///     Exception thrown when validation fails.
/// </summary>
public class ValidationException : OEAPIException
{
    /// <summary>
    ///     Initializes a new instance of the ValidationException class.
    /// </summary>
    /// <param name="errors">
    ///     The dictionary of validation errors, where the key is the field name and the value is an array of
    ///     error messages.
    /// </param>
    public ValidationException(IDictionary<string, string[]> errors)
        : base("Validation failed", 400, "VALIDATION_ERROR")
    {
        Errors = errors;
    }

    /// <summary>
    ///     Initializes a new instance with a single error.
    /// </summary>
    /// <param name="fieldName">The name of the field with the error.</param>
    /// <param name="errorMessage">The error message.</param>
    public ValidationException(string fieldName, string errorMessage)
        : base("Validation failed", 400, "VALIDATION_ERROR")
    {
        Errors = new Dictionary<string, string[]>
        {
            [fieldName] = [errorMessage]
        };
    }

    /// <summary>
    ///     Initializes a new instance with multiple errors for the same field.
    /// </summary>
    /// <param name="fieldName">The name of the field with the errors.</param>
    /// <param name="errorMessages">The array of error messages.</param>
    public ValidationException(string fieldName, params string[] errorMessages)
        : base("Validation failed", 400, "VALIDATION_ERROR")
    {
        Errors = new Dictionary<string, string[]>
        {
            [fieldName] = errorMessages
        };
    }

    /// <summary>
    ///     Initializes a new instance with an inner exception.
    /// </summary>
    /// <param name="errors">The dictionary of validation errors.</param>
    /// <param name="innerException">The inner exception.</param>
    public ValidationException(IDictionary<string, string[]> errors, Exception innerException)
        : base("Validation failed", innerException, 400, "VALIDATION_ERROR")
    {
        Errors = errors;
    }

    /// <summary>
    ///     Gets the collection of validation errors.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    /// <summary>
    ///     Gets a formatted error message that includes all validation errors.
    /// </summary>
    public override string Message
    {
        get
        {
            if (Errors.Count == 0)
                return base.Message;

            IEnumerable<string> errorMessages = Errors.SelectMany(e => e.Value.Select(v => $"{e.Key}: {v}"));
            return $"{base.Message} - {string.Join("; ", errorMessages)}";
        }
    }
}
