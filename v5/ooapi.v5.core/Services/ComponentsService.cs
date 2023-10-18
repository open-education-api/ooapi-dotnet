using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

/// <summary>
/// 
/// </summary>
public class ComponentsService : ServiceBase, IComponentsService
{
    private readonly ComponentsRepository _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="userRequestContext"></param>
    public ComponentsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ComponentsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="componentId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Component? Get(Guid componentId, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetComponent(componentId);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="courseId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<Component>? GetComponentsByCourseId(DataRequestParameters dataRequestParameters, Guid courseId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetComponentsByCourseId(courseId);
            var paginationResult = new Pagination<Component>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    public Pagination<Component>? GetComponentsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetComponentsByOrganizationId(organizationId);
            var paginationResult = new Pagination<Component>(result.AsQueryable(), dataRequestParameters);
            errorResponse = null;
            return paginationResult;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
