using System.Text.Json.Serialization;
using OEAPI.Core.Models.ApiModels.Validation;

namespace OEAPI.Core.Models.ApiModels;

/// <summary>
///     Document information.
/// </summary>
public class Document
{
    /// <summary>
    ///     The unique identifier of the document.
    /// </summary>
    /// <example>12345678-1234-1234-1234-123456789012</example>
    [JsonPropertyName("documentId")]
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>
    ///     The type of document.
    /// </summary>
    [JsonPropertyName("documentType")]
    [ExtensibleEnum("documentType")]
    public string DocumentType { get; set; } = string.Empty;

    /// <summary>
    ///     The name of the document.
    /// </summary>
    /// <example>paper_test_1234333.pdf</example>
    [JsonPropertyName("documentName")]
    public string DocumentName { get; set; } = string.Empty;
}
