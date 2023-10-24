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

    public Pagination<Person> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public Person? Get(Guid personId)
    {
        return _repository.GetPerson(personId);
    }

    public Pagination<Person> GetPersonsByGroupId(DataRequestParameters dataRequestParameters, Guid groupId)
    {
        var result = _repository.GetPersonsByGroupId(groupId);
        return new Pagination<Person>(result.AsQueryable(), dataRequestParameters);
    }
}