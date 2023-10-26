using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IEducationSpecificationsService
{
    Task<EducationSpecification?> GetAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<EducationSpecification>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<EducationSpecification>> GetEducationSpecificationsByEducationSpecificationIdAsync(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, CancellationToken cancellationToken = default);
    Task<Pagination<EducationSpecification>> GetEducationSpecificationsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default);
}