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

    public async Task<OneOfOfferingInstance?> GetAsync(Guid offeringId, CancellationToken cancellationToken = default)
    {
        OneOfOfferingInstance? result = null;
        var programOffering = await _programOfferingsRepository.GetProgramOfferingAsync(offeringId, cancellationToken);
        if (programOffering != null)
        {
            result = new OneOfOfferingInstance(programOffering.OfferingId, programOffering);
        }
        else
        {
            var componentOffering = await _componentOfferingsRepository.GetComponentOfferingAsync(offeringId, cancellationToken);
            if (componentOffering != null)
            {
                result = new OneOfOfferingInstance(componentOffering.OfferingId, componentOffering);
            }
            else
            {
                var courseOffering = await _courseOfferingsRepository.GetCourseOfferingAsync(offeringId, cancellationToken);
                if (courseOffering != null)
                {
                    result = new OneOfOfferingInstance(courseOffering.OfferingId, courseOffering);
                }

            }
        }

        return result;
    }
}