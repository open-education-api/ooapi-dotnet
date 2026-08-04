namespace OEAPI.Infrastructure.Data.Entities;

/// <summary>
///     Implemented by every entity that has a spec-defined <c>otherCodes</c> collection, so
///     <see cref="Context.OEAPIDbContext" /> can configure the owned-collection mapping for all of them
///     through one generic helper instead of 20+ near-identical Fluent API blocks.
/// </summary>
public interface IHasOtherCodes
{
    /// <summary>
    ///     Gets or sets the other codes for this entity.
    /// </summary>
    ICollection<OtherCodeEntity> OtherCodes { get; set; }
}
