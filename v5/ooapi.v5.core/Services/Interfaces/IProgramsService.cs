using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IProgramsService
{
    Task<Program?> GetAsync(Guid programId, DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Program>> GetAllAsync(DataRequestParameters dataRequestParameters, CancellationToken cancellationToken = default);
    Task<Pagination<Program>> GetProgramsByEducationSpecificationIdAsync(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, CancellationToken cancellationToken = default);
    Task<Pagination<Program>> GetProgramsByOrganizationIdAsync(DataRequestParameters dataRequestParameters, Guid organizationId, CancellationToken cancellationToken = default);
    Task<Pagination<Program>> GetProgramsByProgramIdAsync(DataRequestParameters dataRequestParameters, Guid programId, CancellationToken cancellationToken = default);
}