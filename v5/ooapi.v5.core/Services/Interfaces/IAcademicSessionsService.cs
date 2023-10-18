using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAcademicSessionsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="academicSessionId"></param>
        /// <param name="dataRequestParameters"></param>
        /// <param name="errorResponse"></param>
        /// <returns></returns>
        AcademicSession? Get(Guid academicSessionId, DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataRequestParameters"></param>
        /// <param name="errorResponse"></param>
        /// <param name="academicSessionType"></param>
        /// <returns></returns>
        Pagination<AcademicSession>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse, string? academicSessionType);
    }
}