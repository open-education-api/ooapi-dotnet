using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace OEAPI.Infrastructure.Data.Context;

/// <summary>
///     Generates <see cref="OEAPI.Infrastructure.Data.Entities.BaseEntity.Id" /> values as RFC 9562
///     UUID v7 (timestamp-prefixed, mostly monotonically increasing) instead of EF Core's default
///     fully-random v4 <see cref="Guid.NewGuid" />. Random insertion order into a B-tree index causes
///     page splits under sustained write load - worse on SQL Server specifically, since <c>Id</c> is
///     this codebase's clustered index by EF Core convention, so every non-clustered index inherits the
///     fragmentation too. UUID v7's mostly-increasing values turn inserts back into cheap page-appends.
///     No schema change: same column type, same uniqueness guarantees, just a different generation
///     strategy - applied in <see cref="OEAPIDbContext.OnModelCreating" />.
/// </summary>
public sealed class SequentialGuidValueGenerator : ValueGenerator<Guid>
{
    public override Guid Next(EntityEntry entry)
    {
        return Guid.CreateVersion7();
    }

    public override bool GeneratesTemporaryValues => false;
}
