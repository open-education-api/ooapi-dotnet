using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IPersonsRepository
{
    Task<Person?> GetPersonAsync(Guid personId, CancellationToken cancellationToken = default);
    Task<Pagination<Person>> GetPersonsByGroupIdAsync(Guid groupId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Person>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, IQueryable<Person>? set = null, CancellationToken cancellationToken = default);
}