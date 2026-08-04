using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     Shared database/audit bookkeeping columns repeated identically across every top-level entity in
///     this codebase - not mapped to its own table (no <c>[Table]</c>), so EF Core treats this purely as
///     a property source via normal C# inheritance, the same way <see cref="IHasOtherCodes" /> makes the
///     <c>OtherCodes</c> collection visually obvious at each entity's declaration.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    ///     Gets or sets the database primary key - a surrogate key, purely internal: every FK column
    ///     elsewhere in the schema (<c>OrganisationEntityId</c> and friends) points at this, and it's
    ///     never serialized into any API model. Distinct from each entity's own spec-facing identifier
    ///     (e.g. <c>OrganisationEntity.OrganisationId</c>, a <see langword="string" />) - that's a
    ///     business key: caller-chosen (the spec's <c>PUT /organisations/{organisationId}</c> write
    ///     semantics put it in the URL, not server-generated), and it's what actually appears in JSON
    ///     responses and cross-resource <c>Identifier</c> references. The two happen to hold the same
    ///     underlying value in <c>DemoDataSeeder</c> (one <see cref="Guid" /> variable reused for both,
    ///     for convenience), which can look redundant when inspecting seeded data directly - they are
    ///     not the same value in general; every real <c>PUT</c>-created row gets two independently
    ///     generated values. Keeping them separate means FK joins use the cheaper, natively-indexed
    ///     <see cref="Guid" /> rather than string comparison, and a client's choice of external ID never
    ///     becomes this table's actual join key.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    ///     Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     Gets or sets the last modification timestamp.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the entity is active.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
