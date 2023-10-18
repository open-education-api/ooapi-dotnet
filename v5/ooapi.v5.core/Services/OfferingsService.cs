using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

/// <summary>
/// 
/// </summary>
public class OfferingsService : ServiceBase, IOfferingsService
{
    private readonly ProgramOfferingsRepository _programOfferingsRepository;
    private readonly CourseOfferingsRepository _courseOfferingsRepository;
    private readonly ComponentOfferingsRepository _componentOfferingsRepository;

    public OfferingsService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _programOfferingsRepository = new ProgramOfferingsRepository(dbContext);
        _courseOfferingsRepository = new CourseOfferingsRepository(dbContext);
        _componentOfferingsRepository = new ComponentOfferingsRepository(dbContext);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="offeringId"></param>
    /// <param name="errorResponse"></param>
    /// <returns></returns>
    public OneOfOfferingInstance? Get(Guid offeringId, out ErrorResponse? errorResponse)
    {
        try
        {
            OneOfOfferingInstance? result = null;
            var programOffering = _programOfferingsRepository.GetProgramOffering(offeringId);
            if (programOffering != null)
            {
                result = new OneOfOfferingInstance(programOffering.OfferingId, programOffering);
            }
            else
            {
                var componentOffering = _componentOfferingsRepository.GetComponentOffering(offeringId);
                if (componentOffering != null)
                {
                    result = new OneOfOfferingInstance(componentOffering.OfferingId, componentOffering);
                }
                else
                {
                    var courseOffering = _courseOfferingsRepository.GetCourseOffering(offeringId);
                    if (courseOffering != null)
                    {
                        result = new OneOfOfferingInstance(courseOffering.OfferingId, courseOffering);
                    }
                }
            }
            errorResponse = new ErrorResponse(404, $"No offering found for id {offeringId}");
            return result;
        }
        catch (Exception)
        {
            errorResponse = new ErrorResponse(500);
            return null;
        }
    }
}
