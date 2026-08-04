using OEAPI.Core.Interfaces;

namespace OEAPI.API.Tests.Infrastructure;

/// <summary>
///     Test-only <see cref="IDocumentStorageProvider" /> that records the <c>consumer</c> argument it
///     was called with, so a test can assert <c>DocumentsController</c>'s
///     <c>[FromQuery] string? consumer</c> parameter actually reaches this interface call - a plain
///     <c>200</c>/content-equality assertion against the real, database-backed provider can't prove
///     that on its own, since that provider ignores <c>consumer</c> regardless of whether it was
///     threaded through correctly or dropped somewhere in between. Register via
///     <c>factory.WithWebHostBuilder(b => b.ConfigureTestServices(s =&gt;
///     s.AddScoped&lt;IDocumentStorageProvider&gt;(_ =&gt; new RecordingDocumentStorageProvider(...))))</c>,
///     mirroring <see cref="FakeCurrentPersonProvider" />'s own pattern.
/// </summary>
public sealed class RecordingDocumentStorageProvider(byte[] content) : IDocumentStorageProvider
{
    private readonly byte[] _content = content;

    /// <summary>The <c>consumer</c> value most recently passed to <see cref="GetContentAsync" />.</summary>
    public string? LastReceivedConsumer { get; private set; }

    public Task<DocumentContent?> GetContentAsync(string documentId, string? consumer = null,
        CancellationToken cancellationToken = default)
    {
        LastReceivedConsumer = consumer;
        return Task.FromResult<DocumentContent?>(new DocumentContent { Bytes = _content });
    }
}
