using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IProgramsService
    {
        Program Get(Guid programId, DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<Program> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
        Pagination<Program> GetProgramsByEducationSpecificationId(DataRequestParameters dataRequestParameters, Guid educationSpecificationId, out ErrorResponse errorResponse);
        Pagination<Program> GetProgramsByOrganizationId(DataRequestParameters dataRequestParameters, Guid organizationId, out ErrorResponse errorResponse);
        Pagination<Program> GetProgramsByProgramId(DataRequestParameters dataRequestParameters, Guid programId, out ErrorResponse errorResponse);
    }
}