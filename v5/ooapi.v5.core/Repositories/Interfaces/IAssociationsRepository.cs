using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IAssociationsRepository
{
    Association? GetAssociation(Guid associationId);
    List<Association> GetAssociationsByPersonId(Guid personId);
    Task<Pagination<Association>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Association>? set = null, CancellationToken cancellationToken = default);
}