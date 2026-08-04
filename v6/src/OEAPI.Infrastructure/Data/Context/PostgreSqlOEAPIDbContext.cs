using Microsoft.EntityFrameworkCore;

namespace OEAPI.Infrastructure.Data.Context;

/// <summary>
///     Marker subclass of <see cref="OEAPIDbContext" />, used both by <c>dotnet ef migrations</c>
///     commands targeting PostgreSQL (<c>--context PostgreSqlOEAPIDbContext</c>) - so this provider
///     gets its own migration snapshot, independent of <see cref="SqlServerOEAPIDbContext" />'s - and at
///     runtime, registered in DI when <c>Database:Provider</c> is <c>PostgreSQL</c> (see
///     <c>Program.cs</c>'s <c>ConfigureDatabaseServices</c>). <see cref="OEAPIDbContext" /> is then
///     redirected to resolve to this same scoped instance, so application code injecting the base type
///     (as every controller does) transparently gets an instance whose actual runtime type EF's
///     migration lookup can match against - without this, <c>Database.Migrate()</c> silently finds zero
///     migrations for the base type alone.
/// </summary>
/// <inheritdoc />
public class PostgreSqlOEAPIDbContext(DbContextOptions<PostgreSqlOEAPIDbContext> options) : OEAPIDbContext(options)
{
}
