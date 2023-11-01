using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IAssociationsService
{
    Task<Association?> GetAsync(Guid associationId, CancellationToken cancellationToken = default);
    Task<Pagination<Association>> GetAssociationsByPersonIdAsync(DataRequestParameters dataRequestParameters, Guid personId, CancellationToken cancellationToken = default);
}