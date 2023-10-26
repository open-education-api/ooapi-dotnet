using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IAssociationsRepository
{
    Task<Association?> GetAssociationAsync(Guid associationId, CancellationToken cancellationToken = default);
    Task<List<Association>> GetAssociationsByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);
    Task<Pagination<Association>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Association>? set = null, CancellationToken cancellationToken = default);
}