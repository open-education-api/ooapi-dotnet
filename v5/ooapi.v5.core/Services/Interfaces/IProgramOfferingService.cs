using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;


public interface IProgramOfferingService
{

    /// <param name="programOfferingId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    ProgramOffering? Get(Guid programOfferingId, out ErrorResponse? errorResponse);


    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<ProgramOffering>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);
}