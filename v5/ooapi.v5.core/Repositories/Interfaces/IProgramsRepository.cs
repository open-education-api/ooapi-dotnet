using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IProgramsRepository
{
    Task<Pagination<Program>> GetAllOrderedBy(DataRequestParameters dataRequestParameters);
    Program? GetProgram(Guid programId, DataRequestParameters dataRequestParameters);
    Task<Pagination<Program>> GetProgramsByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters);
    List<Program> GetProgramsByOrganizationId(Guid organizationId);
    List<Program> GetProgramsByProgramId(Guid programId);
}