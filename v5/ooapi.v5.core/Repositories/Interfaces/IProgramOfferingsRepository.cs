using ooapi.v5.core.Utility;
using ooapi.v5.Models;

namespace ooapi.v5.core.Repositories.Interfaces;

public interface IProgramOfferingsRepository
{
    ProgramOffering? GetProgramOffering(Guid programOfferingId);
    Pagination<ProgramOffering> GetProgramOfferingByProgramId(Guid programId, DataRequestParameters dataRequestParameters);
    Pagination<ProgramOffering> GetAllOrderedBy(DataRequestParameters dataRequestParameters, IQueryable<ProgramOffering>? set = null);
}