using ooapi.v5.core.Repositories;
using ooapi.v5.core.Repositories.Interfaces;
using ooapi.v5.core.Security;
using ooapi.v5.core.Services.Interfaces;
using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Services;

internal class ProgramOfferingService : ServiceBase, IProgramOfferingService
{
    private readonly IProgramOfferingsRepository _repository;

    public ProgramOfferingService(ICoreDbContext dbContext, IProgramOfferingsRepository repository, IUserRequestContext userRequestContext) : base(dbContext, userRequestContext)
    {
        _repository = repository;
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