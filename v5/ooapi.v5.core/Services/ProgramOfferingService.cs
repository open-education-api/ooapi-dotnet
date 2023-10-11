using ooapi.v5.core.Models;
using ooapi.v5.core.Repositories;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services
{
    public class ProgramOfferingService : ServiceBase, IProgramOfferingService
    {
        private readonly ProgramOfferingsRepository _repository;

        public ProgramOfferingService(CoreDBContext dbContext, UserRequestContext userRequestContext) : base(dbContext, userRequestContext)
        {
            _repository = new ProgramOfferingsRepository(dbContext);
        }

        public Pagination<ProgramOffering> GetAll(DataRequestParameters dataRequestParameters, out ErrorResponse errorResponse)
        {
            try
            {
                Pagination<ProgramOffering> result = _repository.GetAllOrderedBy(dataRequestParameters);

                errorResponse = null;
                return result;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }
        }

        public ProgramOffering Get(Guid programOfferingId, out ErrorResponse errorResponse)
        {
            try
            {
                var item = _repository.GetProgramOffering(programOfferingId);

                errorResponse = null;
                return item;
            }
            catch (Exception ex)
            {
                errorResponse = new ErrorResponse(500);
                return null;
            }

        }

    }
}
