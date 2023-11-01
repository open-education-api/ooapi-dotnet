using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IPersonsService
{
    Task<Person?> GetAsync(Guid personId, CancellationToken cancellationToken = default);
    Task<Pagination<Person>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Person>> GetPersonsByGroupIdAsync(DataRequestParameters dataRequestParameters, Guid groupId, CancellationToken cancellationToken = default);
}