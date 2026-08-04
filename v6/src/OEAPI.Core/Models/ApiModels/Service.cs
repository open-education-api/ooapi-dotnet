using System.Text.Json.Serialization;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     A metadata set providing details on the provider of this OEAPI implementation.
/// </summary>
public class Service
{
    /// <summary>
    ///     Contact e-mail address of the service owner.
    /// </summary>
    /// <example>admin@universiteitvanharderwijk.nl</example>
    [JsonPropertyName("contactEmail")]
    public string ContactEmail { get; set; } = string.Empty;

    /// <summary>
    ///     URL of the API specification (YAML or JSON, compliant with Open API Specification v3).
    /// </summary>
    /// <example>https://rawgit.com/open-education-api/specification/v3/docs.html#tag/course-offerings/paths/~1course-offerings/get</example>
    [JsonPropertyName("specification")]
    public string Specification { get; set; } = string.Empty;

    /// <summary>
    ///     URL of the API documentation, including general terms and privacy statement.
    /// </summary>
    /// <example>https://open-education-api.github.io/specification/v4/docs.html</example>
    [JsonPropertyName("documentation")]
    public string? Documentation { get; set; }

    /// <summary>
    ///     Object for communicating data to specific consumers (destinations).
    /// </summary>
    [JsonPropertyName("supportedConsumers")]
    public SupportedConsumer[]? SupportedConsumers { get; set; }

    /// <summary>
    ///     Object for communicating VERBS and endpoints that are supported by this implementation.
    /// </summary>
    [JsonPropertyName("supportedOperations")]
    public SupportedOperation[]? SupportedOperations { get; set; }

    /// <summary>
    ///     Object for communicating the expands and paths for which they are implemented.
    /// </summary>
    [JsonPropertyName("supportedExpands")]
    public SupportedExpand[]? SupportedExpands { get; set; }

    /// <summary>
    ///     Free-form extensions.
    /// </summary>
    [JsonPropertyName("ext")]
    public object? Ext { get; set; }
}

/// <summary>
///     Object for communicating data to a specific consumer (destination).
/// </summary>
public class SupportedConsumer
{
    /// <summary>
    ///     The key of the consumer (destination) for which this information is intended.
    /// </summary>
    /// <example>nl-test-admin</example>
    [JsonPropertyName("consumerKey")]
    public string ConsumerKey { get; set; } = string.Empty;

    /// <summary>
    ///     The version number of this consumer.
    /// </summary>
    /// <example>0.9.3</example>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;
}

/// <summary>
///     Object for communicating VERBS and endpoints that are supported by this implementation.
/// </summary>
public class SupportedOperation
{
    /// <summary>
    ///     The type of method or verb.
    /// </summary>
    [JsonPropertyName("verbs")]
    public string[] Verbs { get; set; } = [];

    /// <summary>
    ///     The path of the operation.
    /// </summary>
    /// <example>/courses</example>
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;
}

/// <summary>
///     Object for communicating the expands and paths for which they are implemented.
/// </summary>
public class SupportedExpand
{
    /// <summary>
    ///     The objects that are expandable for a specific path.
    /// </summary>
    [JsonPropertyName("expandableObjects")]
    public string[] ExpandableObjects { get; set; } = [];

    /// <summary>
    ///     The path of the operation.
    /// </summary>
    /// <example>/courses</example>
    [JsonPropertyName("path")]
    public string Path { get; set; } = string.Empty;
}
