using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class GroupsService : ServiceBase, IGroupsService
{
    private readonly IGroupsRepository _repository;

    public GroupsService(ICoreDbContext dbContext, IGroupsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<Group>> GetAll(DataRequestParameters dataRequestParameters)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters);
    }

    public Group? Get(Guid groupId)
    {
        return _repository.GetGroup(groupId);
    }

    public Pagination<Group> GetGroupsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId)
    {
        var result = _repository.GetGroupsByOrganizationId(organizationId);
        return new Pagination<Group>(result.AsQueryable(), dataRequestParameters);
    }

    public Pagination<Group> GetGroupsByPersonId(DataRequestParameters dataRequestParameters, Guid personId)
    {
        var result = _repository.GetGroupsByPersonId(personId);
        return new Pagination<Group>(result.AsQueryable(), dataRequestParameters);
    }
}