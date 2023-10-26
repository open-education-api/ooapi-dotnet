using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IProgramsRepository
{
    Task<Pagination<Program>> GetAllOrderedByAsync(DataRequestParameters dataRequestParameters,
        CancellationToken cancellationToken = default);
    Task<Program?> GetProgramAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Program>> GetProgramsByEducationSpecificationIdAsync(Guid educationSpecificationId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<List<Program>> GetProgramsByOrganizationIdAsync(Guid organizationId, CancellationToken cancellationToken = default);
    Task<List<Program>> GetProgramsByProgramIdAsync(Guid programId, CancellationToken cancellationToken = default);
}