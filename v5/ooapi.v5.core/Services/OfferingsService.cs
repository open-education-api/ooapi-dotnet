using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;

namespace ooapi.v5.core.Services;

internal class OfferingsService : ServiceBase, IOfferingsService
{
    private readonly IProgramOfferingsRepository _programOfferingsRepository;
    private readonly ICourseOfferingsRepository _courseOfferingsRepository;
    private readonly IComponentOfferingsRepository _componentOfferingsRepository;

    public OfferingsService(
        ICoreDbContext dbContext,
        IUserRequestContext userRequestContext,
        IProgramOfferingsRepository programOfferingsRepository,
        ICourseOfferingsRepository courseOfferingsRepository,
        IComponentOfferingsRepository componentOfferingsRepository
        ) : base(dbContext, userRequestContext)
    {
        _programOfferingsRepository = programOfferingsRepository;
        _courseOfferingsRepository = courseOfferingsRepository;
        _componentOfferingsRepository = componentOfferingsRepository;
    }

    public OneOfOfferingInstance? Get(Guid offeringId)
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

        return result;
    }
}