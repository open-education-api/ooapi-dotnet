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

    public async Task<Pagination<Group>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, null, cancellationToken);
    }

    public async Task<Group?> GetAsync(Guid groupId, CancellationToken cancellationToken = default)
    {
        return await _repository.GetGroupAsync(groupId, cancellationToken);
    }

    public async Task<Pagination<Group>> GetGroupsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetGroupsByOrganizationIdAsync(organizationId, cancellationToken);
        return new Pagination<Group>(result.AsQueryable(), dataRequestParameters);
    }

    public async Task<Pagination<Group>> GetGroupsByPersonIdAsync(DataRequestParameters dataRequestParameters, Guid personId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetGroupsByPersonIdAsync(personId, cancellationToken);
        return new Pagination<Group>(result.AsQueryable(), dataRequestParameters);
    }
}