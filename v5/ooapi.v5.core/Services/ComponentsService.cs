using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ComponentsService : ServiceBase, IComponentsService
{
    private readonly ComponentsRepository _repository;

    public ComponentsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ComponentsRepository(dbContext);
    }

    public Component? Get(Guid componentId)
    {
        return _repository.GetComponent(componentId);
    }

    public Pagination<Component> GetComponentsByCourseId(DataRequestParameters dataRequestParameters, Guid courseId)
    {
        var result = _repository.GetComponentsByCourseId(courseId);
        return new Pagination<Component>(result.AsQueryable(), dataRequestParameters);
    }

    public Pagination<Component> GetComponentsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId)
    {
        var result = _repository.GetComponentsByOrganizationId(organizationId);
        return new Pagination<Component>(result.AsQueryable(), dataRequestParameters);
    }
}
