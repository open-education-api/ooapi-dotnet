using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IProgramsService
{
    Program? Get(Guid programId, DataRequestParameters dataRequestParameters);
    Pagination<Program> GetAll(DataRequestParameters dataRequestParameters);
    Pagination<Program> GetProgramsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId);
    Pagination<Program> GetProgramsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId);
    Pagination<Program> GetProgramsByProgramId(DataRequestParameters dataRequestParameters, Guid programId);
}