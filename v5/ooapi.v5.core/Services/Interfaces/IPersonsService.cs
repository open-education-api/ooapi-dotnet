using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IPersonsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="personId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Person? Get(Guid personId, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Person>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="groupId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    Pagination<Person>? GetPersonsByGroupId(DataRequestParameters dataRequestParameters, Guid groupId, out ErrorResponse? errorResponse);
}