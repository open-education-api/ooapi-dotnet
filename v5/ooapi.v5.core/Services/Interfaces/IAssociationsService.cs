using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

public interface IAssociationsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="associationId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Association? Get(Guid associationId, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="personId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Association>? GetAssociationsByPersonId(DataRequestParameters dataRequestParameters, Guid personId, out ErrorResponse? errorResponse);
}
