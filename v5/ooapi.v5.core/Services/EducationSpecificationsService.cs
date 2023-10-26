using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class EducationSpecificationsService : ServiceBase, IEducationSpecificationsService
{
    private readonly IEducationSpecificationsRepository _repository;

    public EducationSpecificationsService(ICoreDbContext dbContext, IEducationSpecificationsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
    }

    public async Task<Pagination<EducationSpecification>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllOrderedByAsync(dataRequestParameters, cancellationToken);
    }

    public async Task<EducationSpecification?> GetAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default)
    {
        return await _repository.GetEducationSpecificationAsync(educationSpecificationId, dataRequestParameters, cancellationToken);
    }

    public async Task<Pagination<EducationSpecification>> GetEducationSpecificationsByEducationSpecificationIdAsync(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetEducationSpecificationsByEducationSpecificationIdAsync(educationSpecificationId, cancellationToken);
        return new Pagination<EducationSpecification>(result.AsQueryable(), dataRequestParameters);
    }

    public async Task<Pagination<EducationSpecification>> GetEducationSpecificationsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetEducationSpecificationsByOrganizationIdAsync(organizationId, cancellationToken);
        return new Pagination<EducationSpecification>(result.AsQueryable(), dataRequestParameters);
    }
}