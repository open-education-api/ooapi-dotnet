using ooapi.v5.core.Models;
using ooapi.v5.core.Models.OneOfModels;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class OfferingsService : ServiceBase
    {
        private readonly ProgramOfferingsRepository _programOfferingsRepository;
        private readonly CourseOfferingsRepository _courseOfferingsRepository;
        private readonly ComponentOfferingsRepository _componentOfferingsRepository;

        public OfferingsService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _programOfferingsRepository = new ProgramOfferingsRepository(dbContext);
            _courseOfferingsRepository = new CourseOfferingsRepository(dbContext);
            _componentOfferingsRepository = new ComponentOfferingsRepository(dbContext);
        }


        public OneOfOfferingInstance Get(Guid offeringId, out ErrorResponse errorResponse)
        {
            try
            {
                OneOfOfferingInstance result = null;
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
                errorResponse = new ErrorResponse(404,$"No offering found for id {offeringId}");
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }

        }

    }
}
