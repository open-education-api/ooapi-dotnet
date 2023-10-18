using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services.Interfaces;

/// <summary>
/// 
/// </summary>
public interface IOfferingsService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="offeringId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    OneOfOfferingInstance? Get(Guid offeringId, out ErrorResponse? errorResponse);
}