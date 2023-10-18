using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

public class GroupsService : ServiceBase, IGroupsService
{
    private readonly GroupsRepository _repository;

    public GroupsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new GroupsRepository(dbContext);
    }

    public Pagination<Group> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
    {
        try
        {
            Pagination<Group> result = _repository.GetAllOrderedBy(dataRequestParameters);
            errorResponse = null;
            return result;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Group Get(Guid groupId, out ErrorResponse errorResponse)
    {
        try
        {
            var item = _repository.GetGroup(groupId);

            errorResponse = null;
            return item;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Group> GetGroupsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse)
    {
        try
        {
            var result = _repository.GetGroupsByOrganizationId(organizationId);
            var paginationResult = new Pagination<Group>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Group> GetGroupsByPersonId(DataRequestParameters dataRequestParameters, Guid personId, out ErrorResponse errorResponse)
    {
        try
        {
            var result = _repository.GetGroupsByPersonId(personId);
            var paginationResult = new Pagination<Group>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception ex)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

}
