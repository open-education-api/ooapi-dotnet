using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     EF Core entity for Document.
/// </summary>
[Table("Documents")]
[Index(nameof(DocumentId), IsUnique = true)]
public class DocumentEntity : BaseEntity, IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the document unique identifier (UUID from API).
    /// </summary>
    [Required]
    [StringLength(36)]
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the primary code type.
    /// </summary>
    [Required]
    [StringLength(256)]
    public string PrimaryCodeType { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the primary code value.
    /// </summary>
    [Required]
    [StringLength(256)]
    public string PrimaryCode { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    [StringLength(256)]
    public string? Name { get; set; }

    /// <summary>
    ///     Gets or sets the document type.
    /// </summary>
    [StringLength(256)]
    public string? DocumentType { get; set; }

    /// <summary>
    ///     Gets or sets the description.
    /// </summary>
    [StringLength(2048)]
    public string? Description { get; set; }

    /// <summary>
    ///     Gets or sets the URL.
    /// </summary>
    [StringLength(2048)]
    public string? Url { get; set; }

    /// <summary>
    ///     Gets or sets the document's raw file content, served by the default
    ///     <see cref="OEAPI.Core.Interfaces.IDocumentStorageProvider" /> implementation for
    ///     <c>GET /documents/{documentId}</c>. Null for documents whose content lives elsewhere (e.g.
    ///     an institution-provided <see cref="OEAPI.Core.Interfaces.IDocumentStorageProvider" />
    ///     pointing at external object storage instead of this column).
    /// </summary>
    public byte[]? Content { get; set; }

    /// <summary>
    ///     Gets or sets the mime type.
    /// </summary>
    [StringLength(256)]
    public string? MimeType { get; set; }

    /// <summary>
    ///     Gets or sets the language.
    /// </summary>
    [StringLength(256)]
    public string? Language { get; set; }

    /// <summary>
    ///     Gets or sets the creation date.
    /// </summary>
    [StringLength(256)]
    public string? CreationDate { get; set; }

    /// <summary>
    ///     Gets or sets free-form extensions as JSON.
    /// </summary>
    public string? ExtJson { get; set; }

    /// <summary>
    ///     Gets or sets the consumer key for consumer-specific data.
    /// </summary>
    [StringLength(256)]
    public string? ConsumerKey { get; set; }

    /// <summary>
    ///     Gets or sets the consumer as JSON.
    /// </summary>
    public string? ConsumerJson { get; set; }

    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    public virtual ICollection<OtherCodeEntity> OtherCodes { get; set; } = [];

    // Foreign keys
    /// <summary>
    ///     Gets or sets the organisation foreign key. Relational-only: the spec's only operation for
    ///     this resource, <c>GET /documents/{documentId}</c>, is a pure binary-content download with no
    ///     JSON <c>Document</c> representation of its own (see <c>DocumentsController</c>'s doc comment
    ///     in <c>OEAPI.API</c>), so this link has no live JSON exposure of its own - same shape as
    ///     <see cref="LearningOutcomeEntity.OrganisationEntityId" />, kept for institutions/tools that
    ///     query the database directly. Where a <c>Document</c> API model instance genuinely is returned
    ///     as JSON (embedded, e.g. <c>TestComponentOfferingAssociationAttempt.documents</c>), it comes
    ///     from a separate, denormalized JSON blob on the owning entity, not from this table.
    /// </summary>
    public Guid? OrganisationEntityId { get; set; }

    // Navigation properties
    /// <summary>
    ///     Gets or sets the organisation that owns this document.
    /// </summary>
    [ForeignKey(nameof(OrganisationEntityId))]
    public virtual OrganisationEntity? Organisation { get; set; }
}
