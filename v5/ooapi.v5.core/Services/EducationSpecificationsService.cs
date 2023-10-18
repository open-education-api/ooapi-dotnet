using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class EducationSpecificationsService : ServiceBase, IEducationSpecificationsService
{
    private readonly EducationSpecificationsRepository _repository;

    public EducationSpecificationsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new EducationSpecificationsRepository(dbContext);
    }

    public Pagination<EducationSpecification> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public EducationSpecification? Get(Guid educationSpecificationId, DataRequestParameters dataRequestParameters)
    {
        return _repository.GetEducationSpecification(educationSpecificationId, dataRequestParameters);
    }

    public Pagination<EducationSpecification> GetEducationSpecificationsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId)
    {
        var result = _repository.GetEducationSpecificationsByEducationSpecificationId(educationSpecificationId);
        return new Pagination<EducationSpecification>(result.AsQueryable(), dataRequestParameters);
    }

    public Pagination<EducationSpecification> GetEducationSpecificationsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId)
    {
        var result = _repository.GetEducationSpecificationsByOrganizationId(organizationId);
        return new Pagination<EducationSpecification>(result.AsQueryable(), dataRequestParameters);
    }
}
