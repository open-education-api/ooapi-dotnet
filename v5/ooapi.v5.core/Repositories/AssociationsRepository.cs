using Microsoft.EntityFrameworkCore;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Utility;
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

    public async Task<Pagination<Association>> GetAssociationsByPersonIdAsync(Guid personId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        var set = dbContext.Associations.Where(o => o.PersonId.Equals(personId));

        return await GetAllOrderedByAsync(dataRequestParameters, set, cancellationToken);
    }
}