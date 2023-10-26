using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class PersonsService : ServiceBase, IPersonsService
{
    private readonly IPersonsRepository _repository;

    public PersonsService(ICoreDbContext dbContext, IPersonsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<Person>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, null, cancellationToken);
    }

    public async Task<Person?> GetAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetPersonAsync(personId, cancellationToken);
    }

    public async Task<Pagination<Person>> GetPersonsByGroupIdAsync(DataRequestParameters dataRequestParameters, Guid groupId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetPersonsByGroupIdAsync(groupId, cancellationToken);

        var pagination = new Pagination<Person>();
        await pagination.LoadData(result.AsQueryable(), dataRequestParameters);
        return pagination;
    }
}