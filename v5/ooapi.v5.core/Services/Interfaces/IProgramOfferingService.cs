using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    public interface IProgramOfferingService
    {
        ProgramOffering Get(Guid programOfferingId, out ErrorResponse errorResponse);
        Pagination<ProgramOffering> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse);
    }
}