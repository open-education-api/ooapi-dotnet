using System.ComponentModel.DataAnnotations;

namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     One entry of an owning entity's <c>otherCodes</c> collection - an additional human-readable
///     code/identifier beyond the entity's primary code. Owned by exactly one parent entity type (see
///     <see cref="Context.OEAPIDbContext" />'s <c>ConfigureOtherCodes</c>) - every owner gets its own
///     dedicated table, so there is no shared/polymorphic <c>OtherCodes</c> table and no discriminator
///     column.
/// </summary>
public class OtherCodeEntity
{
    /// <summary>
    ///     Gets or sets the type of code or identifier.
    /// </summary>
    [Required]
    [StringLength(256)]
    public string CodeType { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the human readable code/identifier value.
    /// </summary>
    [Required]
    [StringLength(256)]
    public string Code { get; set; } = string.Empty;
}
