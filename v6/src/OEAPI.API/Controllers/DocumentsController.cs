using Microsoft.AspNetCore.Mvc;
using OEAPI.Core.Interfaces;

namespace OEAPI.API.Controllers;

/// <summary>
///     Controller for the spec's <c>documents</c> resource. Doesn't extend
///     <see cref="GenericEntityController{TEntity,TApiModel}" />: the spec defines exactly one
///     documents-related operation, <c>GET /documents/{documentId}</c>, and - unlike every other
///     resource in this codebase - it is <em>not</em> a JSON resource at all. Its response is the
///     document's raw file bytes as <c>application/octet-stream</c> (a file download); there is no
///     bare list endpoint and no JSON <c>Document</c> representation returned here. (The <c>Document</c>
///     JSON schema does exist in the spec, but only as an object <em>embedded</em> elsewhere, e.g.
///     <see cref="Core.Models.ApiModels.TestComponentOfferingAssociationAttempt.Documents" /> - never as
///     this endpoint's own response shape.) Inheriting the generic JSON-CRUD base class here would have
///     been actively wrong, not just incomplete.
/// </summary>
[ApiController]
[Route("[controller]")]
[Tags("Documents")]
public class DocumentsController(IDocumentStorageProvider documentStorageProvider, ILogger<DocumentsController> logger) : ControllerBase
{
    private readonly IDocumentStorageProvider _documentStorageProvider = documentStorageProvider;
    private readonly ILogger<DocumentsController> _logger = logger;

    /// <summary>
    ///     Downloads the raw binary content of a document. GET /documents/{documentId}.
    /// </summary>
    /// <param name="documentId">The document ID.</param>
    /// <param name="consumer">Consumer key - see <see cref="IDocumentStorageProvider.GetContentAsync" />.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    [HttpGet("{documentId}")]
    [ProducesResponseType(typeof(Stream), StatusCodes.Status200OK, "application/octet-stream")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, "application/problem+json")]
    public async Task<IActionResult> GetDocumentContent(string documentId, [FromQuery] string? consumer,
        CancellationToken cancellationToken = default)
    {
        try
        {
            DocumentContent? content = await _documentStorageProvider
                .GetContentAsync(documentId, consumer, cancellationToken)
                .ConfigureAwait(false);
            if (content == null)
                return Problem(
                    detail: $"Document with ID '{documentId}' not found.",
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found");

            return File(content.Bytes, content.ContentType ?? "application/octet-stream", content.FileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving document content for {DocumentId}", documentId);
            return Problem(
                detail: "An unexpected error occurred",
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error");
        }
    }
}
