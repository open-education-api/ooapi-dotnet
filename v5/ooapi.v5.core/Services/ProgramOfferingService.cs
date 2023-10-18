using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

/// <summary>
/// 
/// </summary>
public class ProgramOfferingService : ServiceBase, IProgramOfferingService
{
    private readonly ProgramOfferingsRepository _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="userRequestContext"></param>
    public ProgramOfferingService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ProgramOfferingsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataRequestParameters"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public Pagination<ProgramOffering>? GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse? errorResponse)
    {
        try
        {
            var result = _repository.GetAllOrderedBy(dataRequestParameters);

            errorResponse = null;
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="programOfferingId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public ProgramOffering? Get(Guid programOfferingId, out ErrorResponse? errorResponse)
    {
        try
        {
            var item = _repository.GetProgramOffering(programOfferingId);

            errorResponse = null;
            return item;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
