using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories;

public class AssociationsRepository : BaseRepository<Association>, IAssociationsRepository
{
    public AssociationsRepository(ICoreDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Association?> GetAssociationAsync(Guid associationId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Associations.FirstOrDefaultAsync(x => x.AssociationId.Equals(associationId), cancellationToken);
    }

    public async Task<List<Association>> GetAssociationsByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Associations.Where(o => o.PersonId.Equals(personId)).ToListAsync(cancellationToken);
    }
}