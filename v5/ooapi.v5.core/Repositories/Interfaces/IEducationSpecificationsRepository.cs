using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IEducationSpecificationsRepository
{
    Task<Pagination<EducationSpecification>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<EducationSpecification> GetEducationSpecificationAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<List<EducationSpecification>> GetEducationSpecificationsByEducationSpecificationIdAsync(Guid educationSpecificationId, CancellationToken cancellationToken = default);
    Task<List<EducationSpecification>> GetEducationSpecificationsByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default);
}