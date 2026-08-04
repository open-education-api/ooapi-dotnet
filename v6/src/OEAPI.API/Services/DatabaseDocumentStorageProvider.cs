using Microsoft.EntityFrameworkCore;
using OEAPI.Core.Interfaces;
using OEAPI.Infrastructure.Data.Context;
using OEAPI.Infrastructure.Data.Entities;

namespace OEAPI.API.Services;

/// <summary>
///     Default <see cref="IDocumentStorageProvider" />: reads document content directly from this
///     project's own database (<c>DocumentEntity.Content</c>). Requires no external configuration, so
///     it is registered by default. Institutions with an existing document management system or object
///     storage (S3, Azure Blob, etc.) should register their own <see cref="IDocumentStorageProvider" />
///     implementation instead (see <c>ConfigureAuthenticationServices</c> in <c>Program.cs</c>, where
///     this is registered alongside the other pluggable, deployment-specific interfaces).
/// </summary>
public class DatabaseDocumentStorageProvider(OEAPIDbContext dbContext) : IDocumentStorageProvider
{
    private readonly OEAPIDbContext _dbContext = dbContext;

    /// <summary>
    ///     <paramref name="consumer" /> is accepted but ignored: <see cref="DocumentEntity" /> stores
    ///     exactly one <see cref="DocumentEntity.Content" /> blob per document, no per-consumer variants
    ///     - see the interface's own doc comment for why that's a valid, spec-conformant response for an
    ///     implementation with nothing consumer-specific to serve.
    /// </summary>
    /// <inheritdoc />
    public async Task<DocumentContent?> GetContentAsync(string documentId, string? consumer = null,
        CancellationToken cancellationToken = default)
    {
        DocumentEntity? entity = await _dbContext.Documents
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DocumentId == documentId, cancellationToken)
            .ConfigureAwait(false);

        if (entity?.Content == null) return null;

        return new DocumentContent
        {
            Bytes = entity.Content,
            ContentType = entity.MimeType,
            FileName = entity.Name
        };
    }
}
