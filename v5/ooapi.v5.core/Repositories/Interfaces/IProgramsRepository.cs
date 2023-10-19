using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IProgramsRepository
{
    Pagination<Program> GetAllOrderedBy(DataRequestParameters dataRequestParameters);
    Program? GetProgram(Guid programId, DataRequestParameters dataRequestParameters);
    Pagination<Program> GetProgramsByEducationSpecificationId(Guid educationSpecificationId, DataRequestParameters dataRequestParameters);
    List<Program> GetProgramsByOrganizationId(Guid organizationId);
    List<Program> GetProgramsByProgramId(Guid programId);
}