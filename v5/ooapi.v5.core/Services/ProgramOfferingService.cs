using ooapi.v5.core.Repositories;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ProgramOfferingService : ServiceBase, IProgramOfferingService
{
    private readonly ProgramOfferingsRepository _repository;

    public ProgramOfferingService(CoreDBContext dbContext, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = new ProgramOfferingsRepository(dbContext);
    }

    public Pagination<ProgramOffering> GetAll(DataRequestParameters dataRequestParameters)
    {
        return _repository.GetAllOrderedBy(dataRequestParameters);
    }

    public ProgramOffering? Get(Guid programOfferingId)
    {
        return _repository.GetProgramOffering(programOfferingId);
    }

}
