namespace OEAPI.Core.Interfaces;

/// <summary>
///     Resolves the raw binary content of a document for <c>GET /documents/{documentId}</c>. This is
///     the one piece of that spec-defined file-download endpoint that is genuinely deployment
///     specific: institutions store the actual files however they already do (a database blob column,
///     object storage like S3/Azure Blob, a document management system, etc.), and the spec
///     deliberately says nothing about where document content lives - only that <c>GET</c> must return
///     it as <c>application/octet-stream</c>. The default implementation stores content directly in
///     this project's own database; institutions register their own implementation to point at their
///     real storage backend instead (see <c>Program.cs</c>).
/// </summary>
public interface IDocumentStorageProvider
{
    /// <summary>
    ///     Resolves the document with the given <c>documentId</c>, or <see langword="null" /> if no
    ///     document with that id has any content to serve.
    /// </summary>
    /// <param name="documentId">The spec's <c>documentId</c> path parameter.</param>
    /// <param name="consumer">
    ///     The spec's <c>consumer</c> query parameter, if the caller supplied one. Unlike every other
    ///     resource's <c>consumer</c> scoping (which selects consumer-specific data inside a JSON
    ///     <c>consumer</c> property), this endpoint's response is raw binary content with no JSON body
    ///     to scope a property within - so honouring it here means an implementation MAY serve
    ///     different bytes for the same <paramref name="documentId" /> depending on the requesting
    ///     consumer (e.g. a redacted or consumer-branded variant of the same file), if it has such
    ///     variants. Implementations with only one variant per document (like the default,
    ///     database-backed one) can safely ignore this parameter.
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task<DocumentContent?> GetContentAsync(string documentId, string? consumer = null,
        CancellationToken cancellationToken = default);
}

/// <summary>The raw content of a document, ready to be served as <c>application/octet-stream</c>.</summary>
public sealed class DocumentContent
{
    /// <summary>The document's raw bytes.</summary>
    public required byte[] Bytes { get; init; }

    /// <summary>The document's MIME type, if known - falls back to <c>application/octet-stream</c> if not set.</summary>
    public string? ContentType { get; init; }

    /// <summary>The document's file name, if known - used for the response's <c>Content-Disposition</c> header.</summary>
    public string? FileName { get; init; }
}
